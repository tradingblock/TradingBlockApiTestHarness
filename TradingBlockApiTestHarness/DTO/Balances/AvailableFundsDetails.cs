namespace TradingBlockApiTestHarness.DTO.Balances
{
    public class AvailableFundsDetails
    {
        /// <summary>
        /// Funds available for trading. May differ than funds available for withdrawal
        /// due to pending dividends, unsettled funds, etc.
        /// </summary>
        public double AvailableFunds { get; set; }
        /// <summary>
        /// Pending transaction(s) dollar amount, if applicable
        /// </summary>
        public double PendingTransactions { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - AvailableFunds:{1} ; PendingTransactions:{2}", base.ToString(), AvailableFunds, PendingTransactions);
        }
    }
}
