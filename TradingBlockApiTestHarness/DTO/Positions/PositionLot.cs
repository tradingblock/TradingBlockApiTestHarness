using System;
using System.Text;

using TradingBlockApiTestHarness.DTO.Enums;

namespace TradingBlockApiTestHarness.DTO.Positions
{
    class PositionLot
    {
        /// <summary>
        /// Symbol for the given lot
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// The underlying deliverable's ticker symbol
        /// </summary>
        public string UnderlyingSymbol { get; set; }
        /// <summary>
        /// Description or name of the security
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Current quantity in this position
        /// </summary>
        public double OpenQuantity { get; set; }
        /// <summary>
        /// Average fill price when this position was opened
        /// </summary>
        public double OpenPrice { get; set; }
        /// <summary>
        /// Commission charged when opening this position
        /// </summary>
        public double Commission { get; set; }
        /// <summary>
        /// The original value of the position when opened, including any commissions
        /// </summary>
        public decimal CostBasis { get; set; }
        /// <summary>
        /// Timestamp of when this position was originally opened
        /// </summary>
        public DateTime DateOpened { get; set; }
        /// <summary>
        /// The asset type of the security
        /// </summary>
        public enumAssetType AssetType { get; set; }
        /// <summary>
        /// The multiplier (number of deliverable shares per option contract), or 1 for non-option symbols
        /// </summary>
        public int OptionMultiplier { get; set; }
        /// <summary>
        /// Whether or not an order exists to open more shares of the security
        /// </summary>
        public bool HasWorkingOpenOrder { get; set; }
        /// <summary>
        /// Whether or not an order exists to close some or all shares of the security
        /// </summary>
        public bool HasWorkingCloseOrder { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            sb.Append(" - Symbol:").Append(Symbol);
            sb.Append("; UnderlyingSymbol:").Append(UnderlyingSymbol);
            sb.Append("; Description:").Append(Description);
            sb.Append("; OpenQuantity:").Append(OpenQuantity);
            sb.Append("; OpenPrice:").Append(OpenPrice);
            sb.Append("; Commission:").Append(Commission);
            sb.Append("; CostBasis:").Append(CostBasis);
            sb.Append("; DateOpened:").Append(DateOpened);
            sb.Append("; AssetType:").Append(AssetType);
            sb.Append("; OptionMultiplier:").Append(OptionMultiplier);
            sb.Append("; HasWorkingOpenOrder:").Append(HasWorkingOpenOrder);
            sb.Append("; HasWorkingCloseOrder:").Append(HasWorkingCloseOrder);
            return sb.ToString();
        }
    }
}
