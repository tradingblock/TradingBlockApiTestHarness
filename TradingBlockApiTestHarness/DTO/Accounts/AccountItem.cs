using System.Text;

using TradingBlockApiTestHarness.DTO.Enums;

namespace TradingBlockApiTestHarness.DTO.Accounts
{
    public class AccountItem
    {
        /// <summary>
        /// Unique primary key of the account
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// Account number given to the account by the clearing firm, or null if account is not yet opened
        /// </summary>
        public string AccountNumber { get; set; }
        /// <summary>
        /// Title on the account
        /// </summary>
        public string AccountTitle { get; set; }
        /// <summary>
        /// First name of the primary account holder
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of the primary account holder
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Email address of the primary account holder
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Type of the given account, or null if the account is an "instant" account and account application has not yet been started
        /// </summary>
        public enumAccountType? AccountType { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            sb.Append(" - AccountId:").Append(AccountId);
            sb.Append("; AccountNumber:").Append(AccountNumber);
            sb.Append("; AccountTitle:").Append(AccountTitle);
            sb.Append("; FirstName:").Append(FirstName);
            sb.Append("; LastName:").Append(LastName);
            sb.Append("; Email:").Append(Email);
            sb.Append("; AccountType:").Append(AccountType);
            return sb.ToString();
        }
    }
}
