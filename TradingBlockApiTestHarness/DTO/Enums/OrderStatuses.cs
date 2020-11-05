namespace TradingBlockApiTestHarness.DTO.Enums
{
    /// <summary>
    /// All possible statuses a given order may be in
    /// </summary>
    public enum enumOrderStatus
    {
        /// <summary>
        /// Order is live. Cannot be set by user.
        /// </summary>
        New = 1,
        /// <summary>
        /// Order is still live and is partially filled. Cannot be set by user.
        /// </summary>
        PartiallyFilled = 2,
        /// <summary>
        /// Order is done and completely filled. Cannot be set by user.
        /// </summary>
        Filled = 3,
        /// <summary>
        /// Order is done and cancelled. Cannot be set by user.
        /// </summary>
        Cancelled = 5,
        /// <summary>
        /// Order is live and has been replaced. Cannot be set by user.
        /// </summary>
        Replaced = 6,
        /// <summary>
        /// Cancel request has been received by the exchange and is pending to receive a response status. Cannot be set by user.
        /// </summary>
        PendingCancel = 7,
        /// <summary>
        /// Last request received for the given order by the exchange was rejected. Cannot be set by user.
        /// </summary>
        Rejected = 9,
        /// <summary>
        /// New order request has been received by the exchange and is pending to receive a response status. Cannot be set by user.
        /// </summary>
        PendingNew = 11,
        /// <summary>
        /// Not used
        /// </summary>
        Expired = 13,
        /// <summary>
        /// Replace order request has been received by the exchange and is pending to receive a response status. Cannot be set by user.
        /// </summary>
        PendingReplace = 14,
        /// <summary>
        /// Order details are saved internally to be used at a future time. Not sent to the exchange.
        /// </summary>
        Saved = 15,
        /// <summary>
        /// New order request that has not yet been sent to the exhange.
        /// </summary>
        Initiated = 20,
        /// <summary>
        /// Replace order request that has not yet been sent to the exchange.
        /// </summary>
        ReplaceInitiated = 21,
        /// <summary>
        /// Cancel order request that has not yet been sent to the exchange.
        /// </summary>
        CancelInitiated = 22,
        /// <summary>
        /// Cancel order request has been rejected by the exchange. Cannot be set by user.
        /// </summary>
        CancelRejected = 23,
        /// <summary>
        /// Replace order request has been rejected by the exchange. Cannot be set by user.
        /// </summary>
        ReplaceRejected = 24
    }
}
