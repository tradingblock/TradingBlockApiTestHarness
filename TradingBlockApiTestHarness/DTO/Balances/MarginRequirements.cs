using System.Text;

namespace TradingBlockApiTestHarness.DTO.Balances
{
    public sealed class MarginRequirements
    {
        public double StockMarginRequirement { get; set; }
        public double OptionMarginRequirement { get; set; }
        public double OtherMarginRequirement { get; set; }
        public double MarginableEquityPercent { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            sb.Append(" - StockMarginrequirement:").Append(StockMarginRequirement);
            sb.Append("; OptionMarginRequirement:").Append(OptionMarginRequirement);
            sb.Append("; OtherMarginRequirement:").Append(OtherMarginRequirement);
            sb.Append("; MarginableEquityPercent:").Append(MarginableEquityPercent);
            return sb.ToString();
        }
    }
}
