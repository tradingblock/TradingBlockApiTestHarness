using System.Collections.Generic;

namespace TradingBlockApiTestHarness.DTO
{
    /// <summary>
    /// This is a wrapper around a collection of items that are returned from
    /// API that may not include all results. For example, when calling GetOrders
    /// method, you can specify only to return the first 10 orders when you
    /// really have 256 total orders. In this case, Items will contain 10 orders,
    /// but TotalNumOfAvailableItems will be 256.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginatedPayload<T>
    {
        public List<T> Items { get; set; }

        /// <summary>
        /// This is not the same as Items.Count. This is the total
        /// number of items that your search filter includes
        /// without pagination, whereas Items only contains items
        /// in your pagination settings.
        /// </summary>
        public int TotalNumOfAvailableItems { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - TotalNumOfAvailableItems:{1} ; Items:{2}", base.ToString(), TotalNumOfAvailableItems, Items != null ? Items.ToStringExtended() : null);
        }
    }
}
