using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streak
{
    public class Program
    {
        private static readonly TimeSpan RunTime = TimeSpan.Parse("00:01:00");
        private const UInt64 HighMatch = 31; // e.g., 2^5-1

        public static void Main()
        {

            bool keepRunning = true;
            var rand = new Random();
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            UInt64 attempts = 0;
            UInt64 matches = 0;
            byte[] bytes = new byte[2 * sizeof(UInt64)];

            while (keepRunning)
            {
                rand.NextBytes(bytes);
                UInt64 high = BitConverter.ToUInt64(bytes, 0) >> (64 * 2 - 100);
                UInt64 low = BitConverter.ToUInt64(bytes, sizeof(UInt64));

                high = high << 5 | low >> (64 - 5);
                bool match = IsMatch(high, 64 * 2 - 100) || IsMatch(low, 64);
                attempts++;
                if (match)
                    matches++;

                if (attempts % 1000 == 0 && stopwatch.Elapsed >= RunTime)
                {
                    keepRunning = false;
                }
            }


            stopwatch.Stop();

            Console.WriteLine("Matches:  {0:#,#}", matches);
            Console.WriteLine("Attempts: {0:#,#}", attempts);
            Console.WriteLine("          {0:00.000}%", 100 * Convert.ToDecimal(matches) / Convert.ToDecimal(attempts));
            Console.WriteLine("Elapse:   {0}", stopwatch.Elapsed);
            Console.ReadLine();
        }

        private static bool IsMatch(UInt64 i, int bitsToCheck)
        {
            UInt64 j = i;
            bitsToCheck -= 5;
            while (bitsToCheck > 0)
            {
                UInt64 k = j & HighMatch;
                if (k == HighMatch || k == 0)
                    return true;
                bitsToCheck--;
                j >>= 1;
            }
            return false;
        }

        private static string ToBinary(UInt64 i)
        {
            StringBuilder sb = new StringBuilder();
            while (i > 0)
            {
                sb.Insert(0, i % 2);
                i >>= 1;
            }
            if (sb.Length == 0)
                sb.Append("0");
            sb.Append(":").Append(sb.Length - 1);
            return sb.ToString();
        }
    }
}
