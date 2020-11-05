using System;

namespace TradingBlockApiTestHarness.DTO.Quotes
{
    public sealed class Bar
    {
        /// <summary>
        /// Symbol
        /// </summary>
        public string S { get; set; }
        /// <summary>
        /// Date
        /// </summary>
        public DateTime D { get; set; }
        /// <summary>
        /// Open
        /// </summary>
        public double O { get; set; }
        /// <summary>
        /// High
        /// </summary>
        public double H { get; set; }
        /// <summary>
        /// Low
        /// </summary>
        public double L { get; set; }
        /// <summary>
        /// Close
        /// </summary>
        public double C { get; set; }
        /// <summary>
        /// Volume
        /// </summary>
        public long V { get; set; }
    }
}
