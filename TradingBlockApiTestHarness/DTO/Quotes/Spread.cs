using System;
using TradingBlockApiTestHarness.DTO.Enums;

namespace TradingBlockApiTestHarness.DTO.Quotes
{
    public sealed class Spread
    {
        public Leg[] Legs { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
        public double Last { get; set; }
        public double Change { get; set; }
        public SpreadType Type { get; set; }
        public DateTime SpreadTime { get; set; }
    }
}
