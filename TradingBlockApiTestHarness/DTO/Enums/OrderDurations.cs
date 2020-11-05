namespace TradingBlockApiTestHarness.DTO.Enums
{
    /// <summary>
    /// Possible types of order durations
    /// </summary>
    public enum enumOrderDuration
    {
        /// <summary>
        /// Day order
        /// </summary>
        Day = 1,
        /// <summary>
        /// Good-till-cancelled
        /// </summary>
        GTC = 2,
        /// <summary>
        /// Manual trade correction; not sent to the exchange
        /// </summary>
        Manual = 4
    }
}
