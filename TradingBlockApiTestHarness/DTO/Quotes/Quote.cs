using System;

using TradingBlockApiTestHarness.DTO.Enums;

namespace TradingBlockApiTestHarness.DTO.Quotes
{
    public sealed class Quote
    {
        /// <summary>
        /// Current price a seller is willing to pay for the security
        /// </summary>
        public double AskPrice { get; set; }
        /// <summary>
        /// Number of shares a market maker is offering to sell at
        /// </summary>
        public long AskSize { get; set; }
        /// <summary>
        /// The asset type of the security
        /// </summary>
        public enumAssetType AssetClass { get; set; }
        /// <summary>
        /// The midpoint of BidPrice and AskPrice
        /// </summary>
        public double BidAskMidPrice { get; set; }
        /// <summary>
        /// Current price a buyer is willing to pay for the security
        /// </summary>
        public double BidPrice { get; set; }
        /// <summary>
        /// Number of shares being offered to purchase at the bid price
        /// </summary>
        public long BidSize { get; set; }
        /// <summary>
        /// The most recent date the security last closed at
        /// </summary>
        public DateTime? CloseDate { get; set; }
        /// <summary>
        /// The final price at which the security traded on the the given
        /// CloseDate
        /// </summary>
        public double ClosePrice { get; set; }
        /// <summary>
        /// Description or name of the security
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The primary exchange (marketplace) in which the security is traded
        /// </summary>
        public string Exchange { get; set; }
        /// <summary>
        /// Extended quote details about the given security, if applicable
        /// </summary>
        public QuoteExtension ExtendedQuote { get; set; }
        /// <summary>
        /// The highest price the security has reached on the most recent
        /// trading day
        /// </summary>
        public double High { get; set; }
        /// <summary>
        /// The most recent price the security was traded at
        /// </summary>
        public double LastTradePrice { get; set; }
        /// <summary>
        /// The lowest price the security reached on the most recent trading day
        /// </summary>
        public double Low { get; set; }
        /// <summary>
        /// For options, the number of shares of the deliverable per contract
        /// </summary>
        public double Multiplier { get; set; }
        /// <summary>
        /// The difference between the prior session's closing price and the
        /// current session's closing price
        /// </summary>
        public double NetChange { get; set; }
        /// <summary>
        /// The price at which the security first traded at upon the opening of
        /// the most recent trading day
        /// </summary>
        public double Open { get; set; }
        /// <summary>
        /// Extended option details about the security, if the security is an option
        /// </summary>
        public OptionExtension OptionExtend { get; set; }
        /// <summary>
        /// The final price at which the security traded on the most recent
        /// trading day prior to CloseDate
        /// </summary>
        public double PreviousClose { get; set; }
        /// <summary>
        /// The time at which the most recent price reflects
        /// </summary>
        public DateTime QuoteTime { get; set; }
        /// <summary>
        /// The time at which we received the most recent price
        /// </summary>
        public DateTime ReceivedTime { get; set; }
        /// <summary>
        /// Ticker symbol for the given security
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Price of the underlying deliverable, if applicable
        /// </summary>
        public double UnderlyingPrice { get; set; }
        /// <summary>
        /// Ticker symbol for the underlying deliverable, if applicable
        /// </summary>
        public string UnderlyingSymbol { get; set; }
        /// <summary>
        /// The total number of shares or contracts traded in the security
        /// </summary>
        public long Volume { get; set; }
    }
}
