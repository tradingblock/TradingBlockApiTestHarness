using System;
using System.Text;

namespace TradingBlockApiTestHarness.DTO.Chains
{
    public sealed class OptionPair
    {
        public double Strike { get; set; }
        public double Multiplier { get; set; }
        public DateTime Expiry { get; set; }
        public Option Call { get; set; }
        public Option Put { get; set; }
        public string ExpirationFreqCode { get; set; }
        public string SettlementStyle { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            sb.Append(" - Strike:").Append(Strike);
            sb.Append("; Multiplier:").Append(Multiplier);
            sb.Append("; Expiry:").Append(Expiry.ToString("yyyy-MM-dd"));
            sb.Append("; Call:{").Append(Call).Append('}');
            sb.Append("; Put:{").Append(Put).Append('}');
            sb.Append("; ExpirationFreqCode:").Append(ExpirationFreqCode);
            sb.Append("; SettlementStyle:").Append(SettlementStyle);
            return sb.ToString();
        }
    }
}
