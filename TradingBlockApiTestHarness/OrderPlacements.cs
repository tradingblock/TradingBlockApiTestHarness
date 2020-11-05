using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

using Newtonsoft.Json;

using TradingBlockApiTestHarness.DTO;
using TradingBlockApiTestHarness.DTO.Enums;
using TradingBlockApiTestHarness.DTO.Orders;
using TradingBlockApiTestHarness.DTO.Positions;

namespace TradingBlockApiTestHarness
{
    internal class OrderPlacements
    {
        private readonly int AccountId;

        private readonly int? SubaccountId;

        internal OrderPlacements(int accountId, int? subaccountid)
        {
            AccountId = accountId;
            SubaccountId = subaccountid;
        }

        internal void Run(string token)
        {
            var orders = GetOrders(token);
            if (orders != null && orders.Items.Any())
            {
                foreach (var order in orders.Items)
                {
                    GetOrders(token, null, order.OrderId.Value, false);
                    UseCancelOrder(token, order.OrderId.Value, 123456);
                }
            }

            GetOrders(token, null, 0, true);

            //var confirm = PlaceSpreadOrder(token, enumOrderType.Market, null, null, 12345);

            //Thread.Sleep(2000);

            //if (confirm.OrderId > 0)
            //    UseCancelOrder(token, confirm.OrderId, 123456);
            Stopwatch sw = new Stopwatch();

            sw.Start();
            //place option order
            OrderConfirmation confirmation = PlaceSimpleOrder(token, "GS", "GS", enumAssetType.Equity, enumPositionEffect.Open, enumOrderAction.Buy, enumOrderType.Limit, (decimal)10.01, null, 123);

            sw.Stop();

            Console.WriteLine("Placed order in " + sw.ElapsedMilliseconds);

            // Wait for the market to fill/acknowledge the order
            //Thread.Sleep(2000);

            if (confirmation.OrderId > 0)
            {
                //sw.Restart();
                //confirmation = ModifyOrder(token, confirmation.OrderId, enumOrderType.Limit, (decimal)5.05, null, 123);

                //sw.Stop();
                //Console.WriteLine("Modify order in " + sw.ElapsedMilliseconds);
                sw.Restart();

                confirmation = UseCancelOrder(token, confirmation.OrderId, 123);

                sw.Stop();
                Console.WriteLine("Cancel order in " + sw.ElapsedMilliseconds);
                sw.Reset();
            }
/*
            // Wait for the market to fill/acknowledge the order
            Thread.Sleep(2000);

            if (confirmation.OrderId > 0)
            {
                PaginatedPayload<Order> orderResponse = GetOrders(token, "GS", confirmation.OrderId, true);

                Order placedOrder = orderResponse.Items.FirstOrDefault();
                if (placedOrder != null && placedOrder.OrderStatus == enumOrderStatus.Filled)
                {
                    confirmation = PlaceSimpleOrder(token, "F", "F", enumAssetType.Option, enumPositionEffect.Close, enumOrderAction.Sell, enumOrderType.Market, null, null, 123);
                }
                else
                {
                    confirmation = UseCancelOrder(token, confirmation.OrderId, 123);
                }
            }
            */
            //stock
            confirmation = PlaceSimpleOrder(token, "S", "", enumAssetType.Equity, enumPositionEffect.Open, enumOrderAction.Buy, enumOrderType.Market, null, null, 124);

            // Wait for the market to fill/acknowledge the order
            Thread.Sleep(1000);

            if (confirmation.OrderId > 0)
                confirmation = ModifyOrder(token, confirmation.OrderId, enumOrderType.Limit, (decimal)4.01, null, 125);
        }

        private Order InitOrder()
        {
            Order order = new Order();
            if (AccountId > 0)
                order.AccountId = AccountId; // THIS FIELD IS OPTIONAL. ONLY REQUIRED IF YOU HAVE MORE THAN ONE ACCOUNT
            if (SubaccountId.HasValue && SubaccountId.Value > 0)
                order.SubaccountId = SubaccountId.Value; // THIS FIELD IS OPTIONAL. ONLY REQUIRED IF YOU HAVE MORE THAN ONE SUBACCOUNT
            return order;
        }

        private OrderConfirmation PlaceNewOrder(string token, Order order)
        {
            string responseString;
            using (var client = ApiClient.CreateClient(token))
            {
                responseString = client.UploadString("Orders/PlaceOrder", "POST", JsonConvert.SerializeObject(order));
            }

            ResponseContainer<OrderConfirmation> response = JsonConvert.DeserializeObject<ResponseContainer<OrderConfirmation>>(responseString);

            if (response.ResponseCode == (int)enumResponseCode.Success)
                Console.WriteLine("Successfully placed order. Response: " + response.Payload);
            else
                Console.WriteLine("Failed to place order. Code: " + response.ResponseCode);

            return response.Payload;
        }

        private OrderConfirmation PlaceChangeOrder(string token, Order order)
        {
            string responseString;
            using (var client = ApiClient.CreateClient(token))
            {
                responseString = client.UploadString("Orders/ChangeOrder", "POST", JsonConvert.SerializeObject(order));
            }

            ResponseContainer<OrderConfirmation> response = JsonConvert.DeserializeObject<ResponseContainer<OrderConfirmation>>(responseString);

            if (response.ResponseCode == (int)enumResponseCode.Success)
                Console.WriteLine("Successfully placed order. Response: " + response.Payload);
            else
                Console.WriteLine("Failed to place order. Code: " + response.ResponseCode);

            return response.Payload;
        }

        internal OrderConfirmation PlaceSimpleOrder(string token, string symbol, string underlying,
            enumAssetType assetType, enumPositionEffect positionEffect, enumOrderAction action, enumOrderType orderType,
            decimal? price, decimal? stop, int referenceId)
        {
            Order order = InitOrder();
            order.UnderlyingSymbol = underlying;
            if (AccountId > 0)
                order.AccountId = AccountId; // THIS FIELD IS OPTIONAL. ONLY REQUIRED IF YOU HAVE MORE THAN ONE ACCOUNT
            if (SubaccountId.HasValue && SubaccountId.Value > 0)
                order.SubaccountId = SubaccountId.Value; // THIS FIELD IS OPTIONAL. ONLY REQUIRED IF YOU HAVE MORE THAN ONE SUBACCOUNT

            OrderLeg leg = new OrderLeg()
            {
                AssetType = assetType,
                Symbol = symbol,
                Action = action,
                PositionEffect = positionEffect
            };

            order.Legs.Add(leg);

            order.ClientRefId = referenceId;

            order.OrderClass = enumOrderClass.Single;
            order.Quantity = 10;

            order.OrderType = orderType;
            order.Price = price;
            order.Stop = stop;
            order.AllOrNone = false;
            order.Duration = enumOrderDuration.Day;

            return PlaceNewOrder(token, order);
        }

        private OrderConfirmation ModifyOrder(string token, int orderId, enumOrderType orderType, decimal? price, decimal? stop, int referenceId)
        {
            Order order = InitOrder();

            if (AccountId > 0)
                order.AccountId = AccountId; // THIS FIELD IS OPTIONAL. ONLY REQUIRED IF YOU HAVE MORE THAN ONE ACCOUNT
            if (SubaccountId.HasValue && SubaccountId.Value > 0)
                order.SubaccountId = SubaccountId.Value; // THIS FIELD IS OPTIONAL. ONLY REQUIRED IF YOU HAVE MORE THAN ONE SUBACCOUNT

            order.OrderId = orderId;
            order.ClientRefId = referenceId;

            order.OrderType = orderType;
            order.Price = price;
            order.Stop = stop;

            return PlaceChangeOrder(token, order);
        }

        private OrderConfirmation PlaceSpreadOrder(string token, enumOrderType orderType, decimal? price, decimal? stop, int refid)
        {
            Order order = InitOrder();
            order.UnderlyingSymbol = "AA";

            OrderLeg leg = new OrderLeg()
            {
                AssetType = enumAssetType.Equity,
                Symbol = "AA",
                Action = enumOrderAction.Sell,
                SpreadRatio = 100,
                PositionEffect = enumPositionEffect.Close
            };

            order.Legs.Add(leg);

            leg = new OrderLeg()
            {
                AssetType = enumAssetType.Option,
                Symbol = "AA 180817C50000",
                Action = enumOrderAction.Buy,
                SpreadRatio = 1,
                PositionEffect = enumPositionEffect.Close
            };

            order.Legs.Add(leg);

            leg = new OrderLeg()
            {
                AssetType = enumAssetType.Option,
                Symbol = "AA 180817P48000",
                Action = enumOrderAction.Sell,
                SpreadRatio = 1,
                PositionEffect = enumPositionEffect.Close
            };

            order.Legs.Add(leg);

            order.ClientRefId = refid;

            order.OrderClass = enumOrderClass.Multileg;
            order.Quantity = 1; //this is spread quantity
            order.DebitCredit = enumDebitCredit.Debit;

            order.OrderType = orderType;
            order.Price = price;
            order.Stop = stop;
            order.AllOrNone = false;
            order.Duration = enumOrderDuration.Day;

            var confirm = PlaceNewOrder(token, order);

            return confirm;
        }

        private OrderConfirmation UseCancelOrder(string token, int orderId, int? clientRefId)
        {
            Order order = InitOrder();

            order.OrderId = orderId;
            order.ClientRefId = clientRefId;

            string responseString;
            using (var client = ApiClient.CreateClient(token))
            {
                responseString = client.UploadString("Orders/CancelOrder", "POST", JsonConvert.SerializeObject(order));
            }

            ResponseContainer<OrderConfirmation> response = JsonConvert.DeserializeObject<ResponseContainer<OrderConfirmation>>(responseString);

            if (response.ResponseCode == (int)enumResponseCode.Success)
                Console.WriteLine("Successfully cancelled order. Response: " + response.Payload);
            else
                Console.WriteLine("Failed to cancel order. Code: " + response.ResponseCode);

            return response.Payload;
        }

        private PaginatedPayload<Order> GetOrders(string token)
        {
            return GetOrders(token, null, 0);
        }

        private PaginatedPayload<Order> GetOrders(string token, string symbol, int orderId, bool workingOrdersOnly = true)
        {
            //string address = string.Format("Orders/GetOrders?accountId={0}&subaccountId={1}&dateFrom={2}", AccountId, SubaccountId, new DateTime(2018, 3, 1).ToString("yyyy-MM-dd"));
            string address = string.Format("Orders/GetOrders?accountId={0}&subaccountId={1}", AccountId, SubaccountId);
            if (!string.IsNullOrEmpty(symbol))
                address += "&symbol=" + symbol;

            if (orderId > 0)
                address += "&orderId=" + orderId + "&IncludeOrderEvents=true";

            if (workingOrdersOnly)
                address += "&WorkingOrdersOnly=true";

            string responseString;
            using (var client = ApiClient.CreateClient(token))
            {
                responseString = client.DownloadString(address);
            }

            ResponseContainer<PaginatedPayload<Order>> response = JsonConvert.DeserializeObject<ResponseContainer<PaginatedPayload<Order>>>(responseString);

            if (response.ResponseCode == (int)enumResponseCode.Success)
                Console.WriteLine(string.Format("Received {0} out of {1} orders", response.Payload.Items.Count, response.Payload.TotalNumOfAvailableItems));
            else
                Console.WriteLine("Failed to get orders. Code: " + response.ResponseCode);

            return response.Payload;
        }

        internal void ClosePosition(string token, PositionLot position)
        {
            PlaceSimpleOrder(token, position.Symbol, position.UnderlyingSymbol, position.AssetType, enumPositionEffect.Close, (position.OpenQuantity > 0 ? enumOrderAction.Sell : enumOrderAction.Buy), enumOrderType.Market, null, null, 1);
        }

        internal void CancelAllOrders(string token)
        {
            var orders = GetOrders(token);
            if (orders != null && orders.Items.Any())
            {
                foreach (var order in orders.Items)
                {
                    GetOrders(token, null, order.OrderId.Value, false);
                    UseCancelOrder(token, order.OrderId.Value, 123456);
                }
            }
        }
    }
}
