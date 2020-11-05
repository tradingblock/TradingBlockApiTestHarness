using System;
using System.Text;

namespace TradingBlockApiTestHarness.DTO.History
{
    public class HistoryItem
    {
        public DateTime RecordDate { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
        public double FillQuantity { get; set; }
        public double FillPrice { get; set; }
        public double Commission { get; set; }
        public double Amount { get; set; }
        public string OrderAction { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            sb.Append(" - TradeDate:").Append(RecordDate);
            sb.Append("; Symbol:").Append(Symbol);
            sb.Append("; Description:").Append(Description);
            sb.Append("; FillQuantity:").Append(FillQuantity);
            sb.Append("; FillPrice:").Append(FillPrice);
            sb.Append("; Commission:").Append(Commission);
            sb.Append("; Amount:").Append(Amount);
            sb.Append("; OrderAction:").Append(OrderAction);
            return sb.ToString();
        }
    }
}
