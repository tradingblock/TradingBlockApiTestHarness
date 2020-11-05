using System;

using TradingBlockApiTestHarness.DTO.Enums;

namespace TradingBlockApiTestHarness.DTO.Quotes
{
    public sealed class QuoteExtension
    {
        public double PreviousAsk { get; set; }
        public double PreviousBid { get; set; }
        public double PreviousClose { get; set; }
        public double NetAssetValue { get; set; }
        public double YTDReturn { get; set; }
        public double OfferPrice { get; set; }
        public string SymbolName { get; set; }
        public double Low52Week { get; set; }
        public double High52Week { get; set; }

        /// <summary>
        /// Average 30 day volume
        /// </summary>
        public long AvgVolume { get; set; }
        public long Shares { get; set; }
        public DividendType DividendType { get; set; }
        public DateTime ExDivDate { get; set; }
        public DateTime DivDate { get; set; }
        public uint RegDivFrequency { get; set; }
        public double RegDivAmount { get; set; }
        public double SpecialDivAmount { get; set; }
        public double SpecialDivFrequency { get; set; }
        public DateTime SpecialDivDate { get; set; }
        public DateTime SpecialDivExDate { get; set; }
        /// <summary>
        /// Earnings Per Share
        /// </summary>
        public double EPS { get; set; }
        public string Issuer { get; set; }
        public double MarketCap { get; set; }
        public double Yield { get; set; }
        public double PERatio { get; set; }
        public DateTime High52WeekDate { get; set; }
        public DateTime Low52WeekDate { get; set; }
        public long AvgVolume10Day { get; set; }
        public double Beta { get; set; }
        public long ShortInterest { get; set; }
        public int SicCode { get; set; }
        public string PrimaryExchange { get; set; }
        public int IssueType { get; set; }
    }
}
