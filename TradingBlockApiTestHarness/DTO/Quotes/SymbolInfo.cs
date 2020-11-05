namespace TradingBlockApiTestHarness.DTO.Quotes
{
    public class SymbolInfo
    {
        /// <summary>
        /// Ticker of the security
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Description or name of the security
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Primary exchange of the security
        /// </summary>
        public string PrimaryExchange { get; set; }
    }
}