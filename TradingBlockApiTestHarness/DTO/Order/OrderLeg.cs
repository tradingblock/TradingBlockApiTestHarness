using System.Text;

using TradingBlockApiTestHarness.DTO.Enums;

namespace TradingBlockApiTestHarness.DTO.Orders
{
    /// <summary>
    /// Details of an individual leg of an order
    /// </summary>
    public class OrderLeg
    {
        /// <summary>
        /// The asset type of the given leg, such as Equity or Option.
        /// </summary>
        public enumAssetType AssetType { get; set; }

        /// <summary>
        /// Specific case-sensitive symbol of the given order leg. Option
        /// symbols will be in the format "AAPL 18062C225000", which
        /// corresponds to a June 22, 2018 AAPL $225 call option.
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Order action of this leg, such as Buy or Sell.
        /// </summary>
        public enumOrderAction Action { get; set; }

        /// <summary>
        /// Only applicable (and required) for spread orders, this is
        /// a positive integer that specifies the ratio of this leg.
        /// For example, a vertical would be 1:1, a butterfly would be
        /// 1:2:1, and a covered call would be 1:100.
        /// </summary>
        public int SpreadRatio { get; set; }
        /// <summary>
        /// Position effect, or "side" of this order leg, such as
        /// "Open" for an opening transaction or "Close" for a closing
        /// transaction.
        /// </summary>
        public enumPositionEffect PositionEffect { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            sb.Append(" - AssetType:").Append(AssetType);
            sb.Append("; Symbol:").Append(Symbol);
            sb.Append("; Action:").Append(Action);
            sb.Append("; SpreadRatio:").Append(SpreadRatio);
            sb.Append("; PositionEffect:").Append(PositionEffect);
            return sb.ToString();
        }
    }
}
