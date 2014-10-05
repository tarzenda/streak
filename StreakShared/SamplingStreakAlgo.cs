﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarzenda.Streak
{
    public class SamplingStreakAlgo : IStreakAlgo
    {
        private Random _rand = new Random();

        public StreakResults Calculate(int n, int k)
        {
            if (n < k)
                throw new ArgumentOutOfRangeException("n must be >= k");

            StreakResults results = new StreakResults();
            string falseStreak = new String('0', k);
            string trueStreak = new String('1', k);

            int attempts = 0;
            int matches = 0;
            while (++attempts <= 100000)
            {
                string sample = this.GenerateBinaryString(n);
                if (sample.Contains(falseStreak) || sample.Contains(trueStreak))
                    matches++;
            }
            attempts--;

            return new StreakResults()
            {
                Matches = (ulong) matches,
                Samples = (ulong) attempts,
            };
        }

        private string GenerateBinaryString(int n)
        {
            StringBuilder result = new StringBuilder(n);
            for (int i = 0; i < n; i++)
            {
                result.Append(_rand.Next(0, 2).ToString());
            }
            return result.ToString();
        }
    }

}