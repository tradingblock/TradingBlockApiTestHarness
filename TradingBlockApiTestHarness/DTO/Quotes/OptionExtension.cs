using System;

namespace TradingBlockApiTestHarness.DTO.Quotes
{
    public class OptionExtension
    {
        public double StrikePrice { get; set; }
        public DateTime Expiration { get; set; }
        public long SharePerContract { get; set; }
        public long SharePerContract1 { get; set; }
        public long SharePerContract2 { get; set; }
        public long SharePerContract3 { get; set; }
        public string ExpirationFrequencyCode { get; set; }
        public string ProductType { get; set; }
        public long PreviousVolume { get; set; }
        public long OpenInterest { get; set; }
        public string LocalCode { get; set; }
        /// <summary>
        /// C = Call, P = Put
        /// </summary>
        public string OptionType { get; set; }
        /// <summary>
        /// AM/PM settlement
        /// </summary>
        public string SettlementStyle { get; set; }
    }
}