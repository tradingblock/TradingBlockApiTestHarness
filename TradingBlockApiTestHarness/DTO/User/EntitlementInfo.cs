using System.Collections.Generic;

namespace TradingBlockApiTestHarness.DTO.User
{
    public class EntitlementInfo
    {
        public bool IsMarketDataEntitlementPolicyUpdateNeeded { get; set; }

        public int MarketDataEntitlementTypeId { get; set; }

        public List<ExchangeEntitlement> ExchangeEntitlements { get; set; }
    }

    public class ExchangeEntitlement
    {
        /// <summary>
        /// The given exchange's code
        /// </summary>
        public string ExchangeCode { get; set; }
        /// <summary>
        /// Name/description of this given exchange
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The market data entitlement type id for this given exchange
        /// </summary>
        public int MarketDataEntitlementTypeId { get; set; }
    }
}
