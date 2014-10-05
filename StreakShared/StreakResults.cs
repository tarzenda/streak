using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarzenda.Streak
{
    public class StreakResults
    {
        public ulong Matches { get; set; }
        public ulong Samples { get; set; }
        public decimal PercentMatch
        {
            get
            {
                return this.Samples == 0
                    ? 0
                    : 100*Convert.ToDecimal(this.Matches) / Convert.ToDecimal(this.Samples);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Matches: {0}", this.Matches).AppendLine()
                .AppendFormat("Samples: {0}", this.Samples).AppendLine()
                .AppendFormat("% Match: {0:N3}", this.PercentMatch).AppendLine();
            return sb.ToString();
        }
    }
}
