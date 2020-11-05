using System;
using System.Text;

using TradingBlockApiTestHarness.DTO.Enums;

namespace TradingBlockApiTestHarness.DTO.History
{
    /// <summary>
    /// Corresponds to a closed position
    /// </summary>
    public class PnLItem
    {
        /// <summary>
        /// Original order id for the opening transaction of this record
        /// </summary>
        public int OpenOrderId { get; set; }
        /// <summary>
        /// Order id for the (first, if there are multiple) closing
        /// transaction(s) of this record
        /// </summary>
        public int CloseOrderId { get; set; }
        /// <summary>
        /// Symbol of this given record
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Description or name of the security
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Timestamp in UTC format of when this position was originally
        /// opened
        /// </summary>
        public DateTime DateOpened { get; set; }
        /// <summary>
        /// Timestamp in UTC format of when this position was originally
        /// closed
        /// </summary>
        public DateTime DateClosed { get; set; }
        /// <summary>
        /// Number of shares/contracts closed
        /// </summary>
        public double Quantity { get; set; }
        /// <summary>
        /// The original value of the position when opened, including
        /// any commissions
        /// </summary>
        public double CostBasis { get; set; }
        /// <summary>
        /// The total amount of the closing transaction, including
        /// any commissions
        /// </summary>
        public double CloseAmount { get; set; }
        /// <summary>
        /// The asset type of the security
        /// </summary>
        public enumAssetType AssetType { get; set; }
        /// <summary>
        /// The underlying deliverable's ticker symbol
        /// </summary>
        public string UnderlyingSymbol { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            sb.Append(" - OpenOrderId:").Append(OpenOrderId);
            sb.Append("; CloseOrderId:").Append(CloseOrderId);
            sb.Append("; Symbol:").Append(Symbol);
            sb.Append("; Description:").Append(Description);
            sb.Append("; DateOpened:").Append(DateOpened);
            sb.Append("; DateClosed:").Append(DateClosed);
            sb.Append("; Quantity:").Append(Quantity);
            sb.Append("; CostBasis:").Append(CostBasis);
            sb.Append("; CloseAmount:").Append(CloseAmount);
            sb.Append("; AssetType:").Append(AssetType);
            sb.Append("; UnderlyingSymbol:").Append(UnderlyingSymbol);
            return sb.ToString();
        }
    }
}
