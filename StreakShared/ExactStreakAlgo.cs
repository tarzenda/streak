using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarzenda.Streak
{
    public class ExactStreakAlgo : IStreakAlgo
    {
        public StreakResults Calculate(StreakVariant variant, int n, int k)
        {
            if (n < k)
                throw new ArgumentOutOfRangeException("n must be >= k");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            StreakResults results = new StreakResults();
            string falseStreak = new String('0', k);
            string trueStreak = new String('1', k);
            string zeroSample = new String('0', n);

            //Console.WriteLine("{0} streak {1} ({2})", n, k, variant);

            int matches = 0;
            string sample = zeroSample;
            do
            {
                if (sample.Contains(trueStreak) || 
                    sample.Contains(falseStreak) && variant == StreakVariant.HeadsAndTails)
                {
                    //Console.WriteLine(sample);
                    matches++;
                }
                sample = IncrementString(sample);
            } while (sample != zeroSample);

            stopwatch.Stop();

            return new StreakResults()
            {
                Matches = (ulong)matches,
                Samples = (ulong)Math.Pow(2,n),
                Elapse = stopwatch.Elapsed,
            };
        }

        private static string IncrementString(string n)
        {
            StringBuilder sb = new StringBuilder(n);
            for (int i = n.Length - 1; i >= 0; i--)
            {
                if (sb[i] == '0')
                {
                    sb[i] = '1';
                    break;
                }
                else
                {
                    sb[i] = '0';
                }
            }
            return sb.ToString();
        }
    }
}
