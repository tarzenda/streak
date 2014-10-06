using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarzenda.Streak.CLI
{
    class Program
    {
        private const int NDimensionIndex = 0;
        private const int KDimensionIndex = 1;

        static void Main(string[] args)
        {
            ulong[,] results = new ulong[15, 15];

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ExactStreakAlgo algo = new ExactStreakAlgo();
            Parallel.For(0, results.GetLength(NDimensionIndex), (n) =>
            {
                Parallel.For(0, results.GetLength(KDimensionIndex), (k) =>
                {
                    results[n, k] = n < k ? 0 : algo.Calculate(StreakVariant.HeadsOnly, n+1, k+1).Matches;
                });
            });
            stopwatch.Stop();

            File.WriteAllText("Streak.csv", PrintAsCSV(results));
            File.AppendAllText("Streak.csv", string.Format("Elapse: {0}\r\n", stopwatch.Elapsed));
            Process.Start("Streak.csv");
        }

        private static string PrintAsCSV(ulong[,] input)
        {
            StringBuilder output = new StringBuilder();

            // Header
            output.Append(@"N\K,");
            for (int k = 0; k < input.GetLength(KDimensionIndex); k++)
            {
                output.Append(k+1).Append(',');
            }
            output.Remove(output.Length-1,1);
            output.Append("\r\n");

            // Body
            for (int n = 0; n < input.GetLength(NDimensionIndex); n++)
            {
                output.Append(n+1).Append(',');
                for (int k = 0; k < input.GetLength(KDimensionIndex); k++)
                {
                    string x = input[n, k].ToString();
                    x = x == "0" ? "" : x;
                    output.Append(x).Append(',');
                }
                output.Remove(output.Length-1,1);
                output.Append("\r\n");
            }

            return output.ToString();
        }
    }
}
