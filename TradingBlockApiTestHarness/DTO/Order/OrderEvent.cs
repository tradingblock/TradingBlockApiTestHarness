using System;
using System.Text;

using TradingBlockApiTestHarness.DTO.Enums;

namespace TradingBlockApiTestHarness.DTO.Order
{
    /// <summary>
    /// Details about an event for an order
    /// </summary>
    public class OrderEvent
    {
        /// <summary>
        /// Time of this event
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Status of this event
        /// </summary>
        public enumOrderStatus OrderStatus { get; set; }

        /// <summary>
        /// Limit price of the order at this time, if applicable
        /// </summary>
        public double Limit { get; set; }

        /// <summary>
        /// Stop price of the order at this time, if applicable
        /// </summary>
        public double Stop { get; set; }

        /// <summary>
        /// Quantity filled at this time (not to be mistaken for total fill quantity of the order)
        /// </summary>
        public double FillQuantity { get; set; }

        /// <summary>
        /// Corresponding symbol of this record
        /// </summary>
        public string Symbol { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            sb.Append(" - TimeStamp:").Append(TimeStamp);
            sb.Append("; OrderStatus:").Append(OrderStatus);
            sb.Append("; Limit:").Append(Limit);
            sb.Append("; Stop:").Append(Stop);
            sb.Append("; FillQuantity:").Append(FillQuantity);
            sb.Append("; Symbol:").Append(Symbol);
            return sb.ToString();
        }
    }
}
