namespace TradingBlockApiTestHarness.DTO.Enums
{
    public enum enumOrderClass
    {
        Single = 1,
        Multileg = 2
    }

    /// <summary>
    /// A list of the different trade strategies
    /// </summary>
    public enum enumStrategyType
    {
        Undefined = 0,
        Single = 1,//single option
        Vertical = 2,
        Calendar = 3,
        Straddle = 5,
        Strangle = 6,
        Butterfly = 7,
        Condor = 8,
        IronButterfly = 9,
        IronCondor = 10, 
        Box = 11,
        CoveredCall = 13,
        MarriedPut = 14,
        Conversion = 15,
        Collar = 16,
        SyntheticCombo = 17,
        CreateMyOwn = 18,
        HedgeCollar = 19,
        Contingent = 20,
        Trailing = 21,
        OneCancelsOther = 22,
        Stock = 23,
        Bond = 24,
        MutualFund = 25,
        CoveredPut = 26,
        MarriedCall = 27
    }

    /// <summary>
    /// How an order is placed, in regards to client-rep/advisor recommendation and/or consent
    /// </summary>
    public enum enumPlacedAs : byte
    {
        /// <summary>
        /// Client has initiated trade without any prior recommendation from representative/advisor
        /// </summary>
        Unsolicited = 0,
        /// <summary>
        /// Trade initiated from a representative/advisor's recommendation
        /// </summary>
        Solicited = 1,
        /// <summary>
        /// Representative/advisor initiated trade without specific client consent
        /// </summary>
        Discretionary = 2,
        Undefined = 255
    }

    public enum enumCommissionType : byte
    {
        /// <summary>
        /// Normal commission type; typical commission type when a rep or client places a trade in an account.
        /// </summary>
        Online = 0,
        /// <summary>
        /// Trade is assisted; typically greater than Online charge, used when someone like the trade desk places a trade for a client over the phone
        /// </summary>
        Assisted = 1,
        /// <summary>
        /// Typically represents a manual fixed commission was charged
        /// </summary>
        Override = 2,
        Undefined = 255
    }

    /// <summary>
    /// Ways a trade can be executed, ex as the accountholder or on their behalf.
    /// http://work.chron.com/definition-principal-vs-agent-14381.html
    /// </summary>
    public enum enumExecutedAs : byte
    {
        /// <summary>
        /// Acting on behalf of accountholder (user is not accountholder, but trades on their behalf)
        /// </summary>
        Agent = 0,
        /// <summary>
        /// Acting as accountholder (user is the accountholder)
        /// </summary>
        Principal = 1,
        Undefined = 255
    }
}
