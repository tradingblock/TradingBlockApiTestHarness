using System;

namespace TradingBlockApiTestHarness.DTO.Chains
{
    public sealed class ExpirationDate
    {
        public DateTime Date { get; set; }
        public OptionExpirationTime Time { get; set; }
        public string RootSymbol { get; set; }

        public ExpirationDate(DateTime date, OptionExpirationTime time, string root)
        {
            Date = date;
            Time = time;
            RootSymbol = root;
        }
    }
}
