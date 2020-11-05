using System.Text;

namespace TradingBlockApiTestHarness.DTO.Balances
{
    public sealed class AccountBalances
    {
        public double CashBalance { get; set; }
        public double CashBalanceTodaysChange { get; set; }
        public double MarginBalance { get; set; }
        public double MarginBalanceTodaysChange { get; set; }
        public double ShortBalance { get; set; }
        public double ShortBalanceTodaysChange { get; set; }
        /// <summary>
        /// Funds available for withdrawal
        /// </summary>
        public double WithdrawalAmount { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            sb.Append(" - CashBalance:").Append(CashBalance);
            sb.Append("; CashBalanceTodaysChange:").Append(CashBalanceTodaysChange);
            sb.Append("; MarginBalance:").Append(MarginBalance);
            sb.Append("; MarginBalanceTodaysChange:").Append(MarginBalanceTodaysChange);
            sb.Append("; ShortBalance:").Append(ShortBalance);
            sb.Append("; ShortBalanceTodaysChange:").Append(ShortBalanceTodaysChange);
            sb.Append("; WithdrawalAmount:").Append(WithdrawalAmount);
            return sb.ToString();
        }
    }
}
