using System.Text;

namespace TradingBlockApiTestHarness.DTO.Balances
{
    public sealed class BalanceDetails
    {
        public AccountBalances Balances { get; set; }
        public SecuritiesValues Securities { get; set; }
        public AvailableFundsDetails BuyingPower { get; set; }
        public MarginRequirements MarginRequirements { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            sb.Append(" - Balances:{ ").Append(Balances);
            sb.Append(" }; Securities:{ ").Append(Securities);
            sb.Append(" }; BuyingPower:{ ").Append(BuyingPower);
            sb.Append(" }; MarginRequirements:{ ").Append(MarginRequirements);
            sb.Append(" }");
            return sb.ToString();
        }
    }
}
