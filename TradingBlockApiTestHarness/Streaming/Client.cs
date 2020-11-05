﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;

using TradingBlockApiTestHarness.DTO.Enums;
using TradingBlockApiTestHarness.DTO.Quotes;

namespace TradingBlockApiTestHarness.Streaming
{
    /// <summary>
    /// You can subscribe to multiple types simply by appending requests
    /// </summary>
    internal sealed class Client : IDisposable
    {
        private readonly WSClient client;
        private readonly string token;
        private readonly int accountId;

        internal Client(string token, int accountId, string url)
        {
            this.token = token;
            this.accountId = accountId;
            client = new WSClient(url);
            client.OnMessage(OnMessageReceived);
            client.OnConnect(OnConnect);
            client.OnDisconnect(OnDisconnect);
        }

        public void Dispose()
        {
            try
            {
                Disconnect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private readonly object connectDisconnectLocker = new object();
        private volatile bool isConnected;

        internal async void RunAsync()
        {
            await Task.Run(() =>
            {
                if (!isConnected)
                {
                    lock (connectDisconnectLocker)
                    {
                        if (!isConnected)
                        {
                            client.Connect();
                            isConnected = true;
                        }
                    }
                }
            });
        }

        internal void Disconnect()
        {
            if (isConnected)
            {
                lock (connectDisconnectLocker)
                {
                    if (isConnected)
                    {
                        client.Disconnect();
                        isConnected = false;
                    }
                }
            }
        }

        private void OnDisconnect(WSClient obj)
        {
            Console.WriteLine("Disconnected");
        }

        private void OnConnect(WSClient obj)
        {
            Console.WriteLine("Connected");
            string msg = SubscribeToSingles("S");
            //msg = SubscribeToSpreads();
            obj.SendMessage(msg);
        }

        private void OnMessageReceived(string arg1, WSClient arg2)
        {
            // 0 = (int)TradingBlockApiTestHarness.DTO.Enums.ResponseCode
            // 1 = MessageType (0=HeartBeat, 1=Quote, 2=Spread, 3=OrderUpdate)

            // If 0 != (int)DTO.Enums.ResponseCode.Success (1), no other fields will be supplied

            //0 = 0 (Success)
            //| 1 = 100 (heartbeat every 10 seconds)

            //0 = 0 (Success)
            //| 1 = 1 (i.e. single leg quote. any instrument)
            //| 2 = QuoteTime
            //| 3 = Symbol
            //| 4 = LastTradePrice
            //| 5 = BidPrice
            //| 6 = AskPrice
            //| 7 = NetChange
            //| 8 = High
            //| 9 = Low
            //| 10 = Volume
            //| 11 = Change Percentage
            //| 12 = Bid Size
            //| 13 = Ask Size

            //0 = 0 (Success)
            //| 1 = 2 (i.e. Spread)
            //| 2 = SpreadTime
            //| 3 = Name
            //| 4 = Last
            //| 5 = Bid
            //| 6 = Ask
            //| 7 = NetChange

            //0 = 0 (Success)
            //| 1 = 3 (i.e. Order update)
            //| 2 = AccountId
            //| 3 = OrderID
            //| 4 = OrderTime.ToUniversalTime().ToString("MM/dd/yyyy HH:mm:ss:fffZ"))
            //| 5 = Symbol
            //| 6 = OrderQuantity
            //| 7 = FilledQuantity
            //| 8 = OrderPrice
            //| 9 = OrderAction
                    //NONE = 0,
                    //BUY = 1,
                    //SELL = 2,
                    //SHORT = 5,
                    //SHORT_EXEMPT = 6
            //| 10 = OrderStatus
                    //Undefined = 0
                    //New = 1
                    //PartiallyFilled = 2
                    //Filled = 3
                    //DoneForDay = 4
                    //Cancelled = 5
                    //Replaced = 6
                    //PendingCancel = 7
                    //Stopped = 8
                    //Rejected = 9
                    //Suspended = 10
                    //PendingNew = 11
                    //Calculated = 12
                    //Expired = 13
                    //PendingReplace = 14
                    //Saved = 15
                    //LiveUntriggered = 16
                    //Scheduled = 17
                    //OCO_Untriggered = 18(OCO not supported at this time)
                    //CancelledUntriggered = 19
                    //Initiated = 20
                    //ReplaceInitiated = 21
                    //CancelInitiated = 22
                    //CancelRejected = 23
                    //ReplaceRejected = 24
                    //Busted = 25
                    //PreAllocated = 26
            //| 11 = Option Expiration (if update is for option)
            //| 12 = Option Call/Put (if update is for option)
            //| 13 = Option Strike (if update is for option)

            //0 = 0
            //| 1 = 4 (Streaming bar update)
            //| 2 = Bar start time
            //| 3 = Symbol
            //| 4 = Open
            //| 5 = High
            //| 6 = Low
            //| 7 = Close
            //| 8 = Volume

            //0 = 2 Authorization token
            //| 1 = 90 expired token
            //| 2 = code (10: duplicate login)
            //| 3 = expiration reason

            Console.WriteLine("Received: " + arg1);
        }

        /// <summary>
        /// Single leg => 10=
        /// </summary>
        /// <param name="eqList"></param>
        /// <returns></returns>
        private string SubscribeToSingles(string eqList)
        {
            return $"0={token}|1={accountId}|10={eqList}";
        }

        /// <summary>
        /// Multi leg => 11=
        /// </summary>
        /// <returns></returns>
        private string SubscribeToSpreads()
        {
            List<Spread> req = new List<Spread>();

            Spread spread = new Spread();
            spread.Type = SpreadType.Vertical;
            spread.Legs = new Leg[2];

            Leg leg = new Leg();
            leg.Symbol = ".IWM 190215C134000";
            leg.AssetType = enumAssetType.Option;

            spread.Legs[0] = leg;

            leg = new Leg();
            leg.Symbol = ".IWM 190215C139000";
            leg.AssetType = enumAssetType.Option;

            spread.Legs[1] = leg;

            req.Add(spread);

            string request = JsonConvert.SerializeObject(req);

            return $"0={token}|1={accountId}|11={request}";
        }

        /// <summary>
        /// Order updates => 13=
        /// </summary>
        /// <returns></returns>
        private string SubscribeToOrderUpdates()
        {
            //'13=1' is an order subscription request
            return $"0={token}|1={accountId}|13=1";
        }

        /// <summary>
        /// Bar updates => 14=
        /// </summary>
        /// <param name="barSymbol"></param>
        /// <returns></returns>
        private string SubscribeToBarUpdates(string barSymbol)
        {
            return $"0={token}|1={accountId}|14={barSymbol}";
        }

        /// <summary>
        /// Unsubscribe from updates
        /// </summary>
        /// <param name="type">Quote = 10, Spread = 11, Order updates = 13, Bar = 14</param>
        /// <param name="topic">Symbol/BarSymbol/Spread. OrdUpdate = 0</param>
        /// <returns></returns>
        private string UnsubscribeFromUpdates(int type, string topic)
        {
            //9=0 indicates intent to unsubscribe from type and topic
            return $"0={token}|1={accountId}|9=0|{type}={topic}";
        }
    }
}