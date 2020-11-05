using TradingBlockApiTestHarness.DTO.Enums;

namespace TradingBlockApiTestHarness.DTO.Quotes
{
    public sealed class Leg
    {
        public string Symbol { get; set; }

        public enumAssetType AssetType { get; set; }

        /// <summary>
        /// Bid of the instrument.
        /// </summary>
        public double LegBid { get; set; }

        /// <summary>
        /// Ask of the instrument.
        /// </summary>
        public double LegAsk { get; set; }
    }
}
