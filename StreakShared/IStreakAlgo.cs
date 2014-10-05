using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarzenda.Streak
{
    public interface IStreakAlgo
    {
        StreakResults Calculate(int n, int k);
    }
}
