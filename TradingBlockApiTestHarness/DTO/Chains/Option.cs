using System;
using System.Text;

namespace TradingBlockApiTestHarness.DTO.Chains
{
    public enum OptionExpirationTime
    {
        Unknown = 0,
        AM = 1,
        PM = 2
    }

    public sealed class Option
    {
        public string Symbol { get; set; }
        public string RootSymbol { get; set; }
        public double Strike { get; set; }
        public DateTime Expiry { get; set; }
        public string OptionType { get; set; }
        public OptionExpirationTime ExpiryTime { get; set; }

        public double Bid { get; set; }
        public double Ask { get; set; }
        public double Last { get; set; }
        public double Close { get; set; }
        public double Change { get; set; }
        public long Volume { get; set; }

        public double Multiplier { get; set; }
        public string Description { get; set; }
        public string ExpirationFrequencyCode { get; set; }
        public string SettlementStyle { get; set; }
        public long OpenInterest { get; set; }

        public double ImpliedVolatility { get; set; }
        public double Delta { get; set; }
        public double Gamma { get; set; }
        public double Theta { get; set; }
        public double Vega { get; set; }
        public double TimeValue { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            sb.Append(" - Symbol:").Append(Symbol);
            sb.Append("; RootSymbol:").Append(RootSymbol);
            sb.Append("; Strike:").Append(Strike);
            sb.Append("; Expiry:").Append(Expiry.ToString("yyyy-MM-dd"));
            sb.Append("; OptionType:").Append(OptionType);
            sb.Append("; Bid:").Append(Bid);
            sb.Append("; Ask:").Append(Ask);
            sb.Append("; Last:").Append(Last);
            sb.Append("; Close:").Append(Close);
            sb.Append("; Change:").Append(Change);
            sb.Append("; Volume:").Append(Volume);
            sb.Append("; Multiplier:").Append(Multiplier);
            sb.Append("; Description:").Append(Description);
            sb.Append("; ExpirationFrequencyCode:").Append(ExpirationFrequencyCode);
            sb.Append("; SettlementStyle:").Append(SettlementStyle);
            sb.Append("; OpenInterest:").Append(OpenInterest);
            sb.Append("; ImpliedVolatility:").Append(ImpliedVolatility);
            sb.Append("; Delta:").Append(Delta);
            sb.Append("; Gamma:").Append(Gamma);
            sb.Append("; Theta:").Append(Theta);
            sb.Append("; Vega:").Append(Vega);
            sb.Append("; TimeValue:").Append(TimeValue);
            return sb.ToString();
        }
    }
}
