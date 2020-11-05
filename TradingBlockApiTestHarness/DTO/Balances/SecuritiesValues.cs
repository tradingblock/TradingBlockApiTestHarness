using System.Text;

namespace TradingBlockApiTestHarness.DTO.Balances
{
    public sealed class SecuritiesValues
    {
        public double LongStockValue { get; set; }
        public double LongStockValueTodaysChange { get; set; }
        public double ShortStockValue { get; set; }
        public double ShortStockValueTodaysChange { get; set; }
        public double ShortOptionValue { get; set; }
        public double ShortOptionValueTodaysChange { get; set; }
        public double LongOptionValue { get; set; }
        public double LongOptionValueTodaysChange { get; set; }
        public double MutualFundsValue { get; set; }
        public double MutualFundsValueTodaysChange { get; set; }
        public double BondsAndCDValue { get; set; }
        public double BondsAndCDValueTodaysChange { get; set; }
        public double MoneyMarketFundValue { get; set; }
        public double MoneyMarketFundValueTodaysChange { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            sb.Append(" - LongStockValue:").Append(LongStockValue);
            sb.Append("; LongStockValueTodaysChange:").Append(LongStockValueTodaysChange);
            sb.Append("; ShortStockValue:").Append(ShortStockValue);
            sb.Append("; ShortStockValueTodaysChange:").Append(ShortStockValueTodaysChange);
            sb.Append("; ShortOptionValue:").Append(ShortOptionValue);
            sb.Append("; ShortOptionValueTodaysChange:").Append(ShortOptionValueTodaysChange);
            sb.Append("; LongOptionValue:").Append(LongOptionValue);
            sb.Append("; LongOptionValueTodaysChange:").Append(LongOptionValueTodaysChange);
            sb.Append("; MutualFundsValue:").Append(MutualFundsValue);
            sb.Append("; MutualFundsValueTodaysChange:").Append(MutualFundsValueTodaysChange);
            sb.Append("; BondsAndCDValue:").Append(BondsAndCDValue);
            sb.Append("; BondsAndCDValueTodaysChange:").Append(BondsAndCDValueTodaysChange);
            sb.Append("; MoneyMarketFundValue:").Append(MoneyMarketFundValue);
            sb.Append("; MoneyMarketFundValueTodaysChange:").Append(MoneyMarketFundValueTodaysChange);
            return sb.ToString();
        }
    }
}
