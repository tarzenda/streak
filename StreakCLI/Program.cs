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
            // Parse commandline args...
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: StreakCLI.exe N K OutputFile.csv");
            }
            ulong nMax = ulong.Parse(args[0]);
            ulong kMax = ulong.Parse(args[1]);
            string file = args[2];


            // Perform calculations...
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ulong[,] results = new ulong[nMax, kMax];
            ExactStreakAlgo algo = new ExactStreakAlgo();
            Parallel.For(0, results.GetLength(NDimensionIndex), (n) =>
            {
                Parallel.For(0, results.GetLength(KDimensionIndex), (k) =>
                {
                    results[n, k] = n < k ? 0 : algo.Calculate(StreakVariant.HeadsOnly, n+1, k+1).Matches;
                });
                Console.WriteLine("Finish n={0}", n + 1);
            });
            stopwatch.Stop();

            // Dump to file...
            File.WriteAllText(file, PrintAsCSV(results));
            File.AppendAllText(file, string.Format("Elapse: {0}\r\n", stopwatch.Elapsed));
            //Process.Start(file);
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
