using System.Text;

namespace TradingBlockApiTestHarness.DTO.Accounts
{
    public class SubAccountItem
    {
        /// <summary>
        /// Unique primary key of the sub-account
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Sub-account's parent AccountId
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// Short-name suffix of the sub-account
        /// </summary>
        public string AcctSuffix { get; set; }
        /// <summary>
        /// Number of the sub-account
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// Nickname (if set) of this sub-account
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        /// Description of this sub-account
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Distribution ratio (if applicable) of this sub-account. If set, will be in the
        /// range of 0-100. This is used for when funds are
        /// added to the account, this percentage of the funds will be moved
        /// to this particular sub-account.
        /// </summary>
        public decimal? DistributionRatio { get; set; }
        /// <summary>
        /// Whether or not this sub-account is the Master, or default/remainder, sub-account
        /// that gets any remainder of positions and balances that is not allocated to all
        /// regular sub-accounts.
        /// </summary>
        public bool IsMaster { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            sb.Append(" - Id:").Append(Id);
            sb.Append("; AccountId:").Append(AccountId);
            sb.Append("; AcctSuffix:").Append(AcctSuffix);
            sb.Append("; Number:").Append(Number);
            sb.Append("; Nickname:").Append(Nickname);
            sb.Append("; Description:").Append(Description);
            sb.Append("; DistributionRatio:").Append(DistributionRatio);
            sb.Append("; IsMaster:").Append(IsMaster);
            return sb.ToString();
        }
    }
}
