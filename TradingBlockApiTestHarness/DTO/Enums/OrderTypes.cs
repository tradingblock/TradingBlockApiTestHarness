namespace TradingBlockApiTestHarness.DTO.Enums
{
    /// <summary>
    /// Possible trading types of an order
    /// </summary>
    public enum enumOrderType
    {
        /// <summary>
        /// Buy or sell at the current price, whatever that price may be
        /// </summary>
        Market = 1,
        /// <summary>
        /// Buy or sell at a specific price or better
        /// </summary>
        Limit = 2,
        /// <summary>
        /// Converts the order to a market order if the price of the security reaches a certain threshold
        /// </summary>
        Stop_Market = 3,
        /// <summary>
        /// Converts the order to a limit order if the price of the security reaches a certain threshold
        /// </summary>
        Stop_Limit = 4,
        /// <summary>
        /// Marks the order to be executed immediately before the market close as a market order
        /// </summary>
        Market_On_Close = 5
    }
}
