using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;

using Newtonsoft.Json;

using TradingBlockApiTestHarness.DTO;
using TradingBlockApiTestHarness.DTO.Auth;
using TradingBlockApiTestHarness.DTO.Balances;
using TradingBlockApiTestHarness.DTO.Enums;
using TradingBlockApiTestHarness.DTO.Positions;
using TradingBlockApiTestHarness.DTO.Accounts;
using TradingBlockApiTestHarness.DTO.Chains;
using TradingBlockApiTestHarness.DTO.History;
using TradingBlockApiTestHarness.DTO.Quotes;
using TradingBlockApiTestHarness.Streaming;
using TradingBlockApiTestHarness.DTO.User;

namespace TradingBlockApiTestHarness
{
    internal class ApiClient
    {
        private static readonly string RequestResponseUrl = ConfigurationManager.AppSettings["RequestResponseUrl"];
        private static readonly string StreamingUrl = ConfigurationManager.AppSettings["StreamingUrl"];

        private const string HEADER__Authorization = "Authorization";

        private static readonly int AccountId;

        private static readonly int? SubaccountId;

        static ApiClient()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            #region THIS IS FOR TESTING ONLY. NOT TO BE USED IN PRODUCTION
            //this basically acceps whatever certificate
            ServicePointManager.ServerCertificateValidationCallback +=
                (se, cert, chain, sslerror) =>
                {
                    return true;
                };
            #endregion

            int.TryParse(ConfigurationManager.AppSettings["AccountId"], out AccountId);
            int temp;
            if (!int.TryParse(ConfigurationManager.AppSettings["SubaccountId"], out temp))
                SubaccountId = null;
            else
                SubaccountId = temp;
        }

        public void RunTest()
        {
            string token = GetToken();
            if (string.IsNullOrEmpty(token))
                return;

            try
            {
                GetUserEntitlementInfo(token);
                //using (Client wSClient = CreateStreamingClient(token))
                //{
                //    wSClient.RunAsync();

                //Console.Read();
                //}


                GetQuotes(token, new[] { "FB", "TSLA", "AMZN", "NVDA" });
                GetBars(token, "IBM");
                GetSpreadQuotes(token);
                //GetChain(token, "AAPL", null, new DateTime(2019, 11, 22), 235, 260, 8);
                GetChains(token, "$SPX");
                GetAccountDetails(token);
                GetSubAccountDetails(token);

                GetAvailableFunds(token);

                GetPnL(token, DateTime.Today.AddDays(-20), null);

                GetSymbols(token, "GOOG");

                GetWarningSymbols(token);

                OrderPlacements orders = new OrderPlacements(AccountId, SubaccountId);
                orders.CancelAllOrders(token);
                orders.Run(token);

                var positions = GetPositions(token, null, null);
                foreach (var position in positions)
                {
                    orders.ClosePosition(token, position);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                LogoutToken(token);
            }
        }

        private static Client CreateStreamingClient(string token)
        {
            return new Client(token, AccountId, StreamingUrl);
        }

        private static WebClient CreateClient()
        {
            return CreateClient(null);
        }

        internal static WebClient CreateClient(string token)
        {
            WebClient client = new WebClient();
            client.BaseAddress = RequestResponseUrl;
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = Encoding.UTF8;
            if (!string.IsNullOrEmpty(token))
                client.Headers[HEADER__Authorization] = token;
            return client;
        }

        #region Auth
        private string GetToken()
        {
            TokenRequest request = new TokenManager().CreateTokenRequest();

            string responseString;

            using (var client = CreateClient())
            {
                try
                {
                    responseString = client.UploadString("Auth/token", "POST", JsonConvert.SerializeObject(request));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }

            ResponseContainer<string> response = JsonConvert.DeserializeObject<ResponseContainer<string>>(responseString);

            if (response.ResponseCode == (int)enumResponseCode.Success)
            {
                Console.WriteLine("Received Token: " + response.Payload);
                return response.Payload;
            }
            else
            {
                Console.WriteLine("Failed to get Token. Response Code: " + response.ResponseCode);
                return null;
            }
        }

        private string LogoutToken(string token)
        {
            using (var client = CreateClient(token))
            {
                string response = client.UploadString("Auth/LogoutToken", "POST");

                Console.WriteLine("LogoutToken response: " + response);

                return response;
            }
        }
        #endregion Auth

        #region User
        /// <summary>
        /// MarketDataEntitlementTypeId:
        ///             1	ProfessionalUser
        ///             2	NonProfessionalUser
        ///             3	DelayedQuotesOnlyUser
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private EntitlementInfo GetUserEntitlementInfo(string token)
        {
            string address = "User/GetEntitlements";

            string resultString;
            using (var client = CreateClient(token))
            {
                resultString = client.DownloadString(address);
            }

            ResponseContainer<EntitlementInfo> response = JsonConvert.DeserializeObject<ResponseContainer<EntitlementInfo>>(resultString);

            if (response.ResponseCode == (int)enumResponseCode.Success)
                Console.WriteLine("Received account details: " + response.Payload);
            else
                Console.WriteLine("Failed to receive account details. Code: " + response.ResponseCode);

            return response.Payload;
        }
        #endregion

        #region Account
        private List<AccountItem> GetAccountDetails(string token)
        {
            string address = string.Format("Accounts/GetAccounts?accountId={0}&subaccountId={1}", AccountId, SubaccountId);

            string resultString;
            using (var client = CreateClient(token))
            {
                resultString = client.DownloadString(address);
            }

            ResponseContainer<List<AccountItem>> response = JsonConvert.DeserializeObject<ResponseContainer<List<AccountItem>>>(resultString);

            if (response.ResponseCode == (int)enumResponseCode.Success)
                Console.WriteLine("Received account details: " + response.Payload);
            else
                Console.WriteLine("Failed to receive account details. Code: " + response.ResponseCode);

            return response.Payload;
        }

        private List<SubAccountItem> GetSubAccountDetails(string token)
        {
            string address = string.Format("Accounts/GetSubaccounts?accountId={0}&subaccountId={1}", AccountId, SubaccountId);

            string resultString;
            using (var client = CreateClient(token))
            {
                resultString = client.DownloadString(address);
            }

            ResponseContainer<List<SubAccountItem>> response = JsonConvert.DeserializeObject<ResponseContainer<List<SubAccountItem>>>(resultString);

            if (response.ResponseCode == (int)enumResponseCode.Success)
                Console.WriteLine("Received sub-account details: " + response.Payload);
            else
                Console.WriteLine("Failed to receive sub-account details. Code: " + response.ResponseCode);

            return response.Payload;
        }
        #endregion

        #region Balances
        private void GetAvailableFunds(string token)
        {
            //simplified:
            string address = string.Format("Balances/GetAvailableFunds?accountId={0}&subaccountId={1}", AccountId, SubaccountId);

            string resultString;
            using (var client = CreateClient(token))
            {
                resultString = client.DownloadString(address);
            }

            ResponseContainer<AvailableFundsDetails> response = JsonConvert.DeserializeObject<ResponseContainer<AvailableFundsDetails>>(resultString);

            if (response.ResponseCode == (int)enumResponseCode.Success)
                Console.WriteLine("Received available funds response: " + response.Payload);
            else
                Console.WriteLine("Failed to receive available funds. Code: " + response.ResponseCode);

            //all balances:
            address = string.Format("Balances/GetBalances?accountId={0}&subaccountId={1}", AccountId, SubaccountId);
            using (var client = CreateClient(token))
            {
                resultString = client.DownloadString(address);
            }

            ResponseContainer<BalanceDetails> balance = JsonConvert.DeserializeObject<ResponseContainer<BalanceDetails>>(resultString);

            if (balance.ResponseCode == (int)enumResponseCode.Success)
                Console.WriteLine("Received balances response: " + balance.Payload);
            else
                Console.WriteLine("Failed to receive balances. Code: " + balance.ResponseCode);
        }
        #endregion Balances

        #region Option Chains
        private void GetChains(string token, string underlyingSymbol)
        {
            ExpirationDate[] expirations = GetExpirations(token, underlyingSymbol);

            if (expirations != null && expirations.Length > 0)
            {
                var expiry = expirations.First();

                GetStrikes(token, underlyingSymbol, expiry.Date, expiry.Time);

                //GetChain(token, underlyingSymbol, null, expiry, null, null);

                GetChain(token, underlyingSymbol, null, null, 0, 10000, null);
            }
        }

        private ExpirationDate[] GetExpirations(string token, string underlyingSymbol)
        {
            string address = string.Format("Chains/GetExpirations?underlyingSymbol={0}&IsExpanded=true", underlyingSymbol);

            string resultString;
            using (var client = CreateClient(token))
            {
                resultString = client.DownloadString(address);
            }

            ResponseContainer<ExpirationDate[]> response = JsonConvert.DeserializeObject<ResponseContainer<ExpirationDate[]>>(resultString);

            if (response.ResponseCode == (int)enumResponseCode.Success)
                Console.WriteLine(string.Format("Received {0} expirations for underlyingSymbol {1}", response.Payload.Length, underlyingSymbol));
            else
                Console.WriteLine(string.Format("Failed to receive expirations for underlyingSymbol {0}. Code: {1}", response.ResponseCode, response.ResponseCode));

            return response.Payload;
        }

        private double[] GetStrikes(string token, string underlyingSymbol, DateTime expiration, OptionExpirationTime time)
        {
            string expiryString = expiration.ToString("yyyy-MM-dd");

            //string address = string.Format("Chains/GetStrikes?underlyingSymbol={0}&expiration={1}&time={2}", underlyingSymbol, expiryString, time);
            string address = string.Format("Chains/GetStrikes?underlyingSymbol={0}&expiration={1}", underlyingSymbol, expiryString);

            string resultString;
            using (var client = CreateClient(token))
            {
                resultString = client.DownloadString(address);
            }

            ResponseContainer<double[]> response = JsonConvert.DeserializeObject<ResponseContainer<double[]>>(resultString);

            if (response.ResponseCode == (int)enumResponseCode.Success)
                Console.WriteLine(string.Format("Received {0} strikes for underlyingSymbol {1} with expiration {2}", response.Payload.Length, underlyingSymbol, expiryString));
            else
                Console.WriteLine(string.Format("Failed to receive strikes for underlyingSymbol {0} with expiration {1}. Code: {2}", response.ResponseCode, expiryString, response.ResponseCode));

            return response.Payload;
        }

        /// <summary>
        /// Get regular chain
        /// </summary>
        /// <param name="token">Auth token, required</param>
        /// <param name="underlyingSymbol">Underlying symbol for which to find option chains, required</param>
        /// <param name="rootSymbol">Option root symbol, optional (ex $SPX underlying sometimes has SPX and SPXW root symbols)</param>
        /// <param name="expiry">Option expiration date to filter, optional</param>
        /// <param name="strikeMin">Minimum (inclusive) strike price to filter, optional</param>
        /// <param name="strikeMax">Maximum (inclusive) strike price to filter, optional</param>
        /// <param name="strikesAroundMarket">Number of strikes above/below underlyin's current market price. If set, min/max strikes are ignored. Optional</param>
        /// <returns>RootSymbol, ExpirationDate, StrikePrice, OptionPair</returns>
        private Dictionary<string, Dictionary<DateTime, Dictionary<double, OptionPair>>> GetChain(string token, string underlyingSymbol, string rootSymbol, DateTime? expiry, 
            double? strikeMin, double? strikeMax, int? strikesAroundMarket, bool? fetchGreeks = null)
        {
            string address = string.Format("Chains/GetChain?underlyingSymbol={0}", underlyingSymbol);

            if (!string.IsNullOrEmpty(rootSymbol))
                address += "&rootSymbol=" + rootSymbol;

            if (expiry.HasValue)
                address += "&expiration=" + expiry.Value.ToString("yyyy-MM-dd");

            if (strikeMin.HasValue)
                address += "&strikeMin=" + strikeMin.Value;

            if (strikeMax.HasValue)
                address += "&strikeMax=" + strikeMax.Value;

            if (strikesAroundMarket.HasValue)
                address += "&strikesAroundMarket=" + strikesAroundMarket.Value;

            if (fetchGreeks.HasValue && fetchGreeks.Value)
                address += "&fetchGreeks=true";

            string resultString;
            using (var client = CreateClient(token))
            {
                resultString = client.DownloadString(address);
            }

            ResponseContainer<Dictionary<string, Dictionary<DateTime, Dictionary<double, OptionPair>>>> response 
                = JsonConvert.DeserializeObject<ResponseContainer<Dictionary<string, Dictionary<DateTime, Dictionary<double, OptionPair>>>>>(resultString);

            // Keys of the resulting dictionary are the following, starting with the outer-most:
            // Option RootSymbol
            // ExpirationDate
            // StrikePrice

            if (response.ResponseCode == (int)enumResponseCode.Success)
                Console.WriteLine(string.Format("Received {0} option pairs for: {1}", response.Payload.Count, address));
            else
                Console.WriteLine(string.Format("Failed to receive option pairs. Code: {0}, for: {1}", response.ResponseCode, address));

            return response.Payload;
        }
        #endregion Option Chains

        #region Positions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="reqType">Undefined = 0, BySymbol = 1, BySymbolList = 2, ByAssetType = 3, OpenPositions = 4</param>
        /// <param name="symbols">List of Symbols separated by comma. Positions request is by Underlying</param>
        /// <returns></returns>
        private List<PositionLot> GetPositions(string token, int? reqType, string symbols)
        {
            string address = string.Format("Positions/GetPositions?accountId={0}&subaccountId={1}", AccountId, SubaccountId);

            if (reqType.HasValue)
                address += "&requestType=" + reqType;

            if (!string.IsNullOrEmpty(symbols))
                address += "&symbols=" + symbols;

            string responseString;
            using (var client = CreateClient(token))
            {
                responseString = client.DownloadString(address);
            }

            var response = JsonConvert.DeserializeObject<ResponseContainer<List<PositionLot>>>(responseString);

            if (response.ResponseCode == (int)enumResponseCode.Success)
                Console.WriteLine(string.Format("Received {0} positions", response.Payload.Count));
            else
                Console.WriteLine("Failed to receive current positions. Code: " + response.ResponseCode);

            return response.Payload;
        }
        #endregion Positions

        #region Quotes

        private List<Bar> GetBars(string token, string symbol)
        {
             //request fields:
            //symbol, 
            //starttime, 
            //endtime, 
            //bars (how many bars back you would like. if this field is SET, it will disregard starttime), 
            //period: (possible values are: M1, M5, M15, M30, H1, Day. if not SET, will default to Day)
            string address = string.Format("Quotes/GetBars?symbol={0}&starttime={1}&endtime={2}&period={3}", symbol, "01-01-2010", "03-29-2019", "Day");
            //string address = string.Format("Quotes/GetBars?symbol={0}&bars={1}&period={2}", symbol, 100, "M5");

            string responseString;
            using (var client = CreateClient(token))
            {
                responseString = client.DownloadString(address);
            }

            var response = JsonConvert.DeserializeObject<ResponseContainer<List<Bar>>>(responseString);

            if (response.ResponseCode == (int)enumResponseCode.Success)
                Console.WriteLine(string.Format("Received {0} quotes", response.Payload.Count));
            else
                Console.WriteLine("Failed to receive quotes. Code: " + response.ResponseCode);

            return response.Payload;
        }

        private List<Quote> GetQuote(string token, string symbol)
        {
            return GetQuotes(token, new[] { symbol });
        }

        private List<Quote> GetQuotes(string token, IEnumerable<string> symbols)
        {
            string address = string.Format("Quotes/GetQuotes?symbols={0}", string.Join(",", symbols));

            string responseString;
            using (var client = CreateClient(token))
            {
                responseString = client.DownloadString(address);
            }


            var response = JsonConvert.DeserializeObject<ResponseContainer<List<Quote>>>(responseString);

            if (response.ResponseCode == (int)enumResponseCode.Success)
                Console.WriteLine(string.Format("Received {0} quotes", response.Payload.Count));
            else
                Console.WriteLine("Failed to receive quotes. Code: " + response.ResponseCode);

            return response.Payload;
        }

        private List<Spread> GetSpreadQuotes(string token)
        {
            //bare minimum request must contain spread.Type and individual legs. Each leg must (at the minimum) contain Symbol and AssetType.
            List<Spread> spreads = new List<Spread>();
            Spread spread = new Spread();
            spread.Type = SpreadType.Vertical;
            Leg leg = new Leg() { Symbol = ".PANW 181019C205000", AssetType = enumAssetType.Option };
            spread.Legs = new Leg[2];
            spread.Legs[0] = leg;
            leg = new Leg() { Symbol = ".PANW 181019C207500", AssetType = enumAssetType.Option };
            spread.Legs[1] = leg;
            spreads.Add(spread);

            string address = string.Format("Quotes/GetSpreadQuotes?spreads={0}", JsonConvert.SerializeObject(spreads));

            string responseString;
            using (var client = CreateClient(token))
            {
                responseString = client.DownloadString(address);
            }

            var spreadsresponse = JsonConvert.DeserializeObject<ResponseContainer<List<Spread>>>(responseString);

            return spreadsresponse.Payload;
        }


        #endregion Quotes

        #region Symbol
        private List<SymbolInfo> GetSymbols(string token, string query)
        {
            string address = string.Format("Symbol/GetSymbols?query={0}&limit=25", query);

            string responseString;
            using (var client = CreateClient(token))
            {
                responseString = client.DownloadString(address);
            }

            var response = JsonConvert.DeserializeObject<ResponseContainer<List<SymbolInfo>>>(responseString);

            if (response.ResponseCode == (int)enumResponseCode.Success)
                Console.WriteLine(string.Format("Received {0} symbols", response.Payload.Count));
            else
                Console.WriteLine("Failed to receive symbols. Code: " + response.ResponseCode);

            return response.Payload;
        }

        private List<string> GetWarningSymbols(string token)
        {
            string address = string.Format("Symbol/GetWarningSymbols");

            string responseString;
            using (var client = CreateClient(token))
            {
                responseString = client.DownloadString(address);
            }

            var response = JsonConvert.DeserializeObject<ResponseContainer<List<string>>>(responseString);

            if (response.ResponseCode == (int)enumResponseCode.Success)
                Console.WriteLine(string.Format("Received {0} symbols", response.Payload.Count));
            else
                Console.WriteLine("Failed to receive symbols. Code: " + response.ResponseCode);

            return response.Payload;
        }
        #endregion

        #region History
        private List<PnLItem> GetPnL(string token, DateTime? from = null, DateTime? to = null, int? pageSize = null, int? pageIndex = null)
        {
            string address = string.Format("History/GetPnL?accountId={0}&subaccountId={1}", AccountId, SubaccountId);

            if (from.HasValue)
                address += "&dateFrom=" + from.Value.Date;

            if (to.HasValue)
                address += "&dateTo=" + to.Value.Date;

            if (pageSize.HasValue)
                address += "&pageSize=" + pageSize.Value;

            if (pageIndex.HasValue)
                address += "&pageIndex=" + pageIndex.Value;

            string responseString;
            using (var client = CreateClient(token))
            {
                responseString = client.DownloadString(address);
            }

            var response = JsonConvert.DeserializeObject<ResponseContainer<PaginatedPayload<PnLItem>>>(responseString);

            if (response.ResponseCode == (int)enumResponseCode.Success)
            {
                Console.WriteLine(string.Format("Received {0} PnL items", response.Payload.Items.Count));
                return response.Payload.Items;
            }
            else
                Console.WriteLine("Failed to receive pnl items. Code: " + response.ResponseCode);

            return null;            
        }
        #endregion
    }
}
