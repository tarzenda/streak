using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tarzenda.Streak
{
    public interface IStreakAlgo
    {
        StreakResults Calculate(StreakVariant variant, int n, int k);
    }

    public static class IStreakAlgoExtensions
    {
        public static List<Type> ListAlgos()
        {
            Type iface = typeof(IStreakAlgo);
            List<Type> types = new List<Type>();
            foreach (Type t in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (t.IsClass && !t.IsAbstract && iface.IsAssignableFrom(t))
                {
                    types.Add(t);
                }
            }
            return types;
        }

        public static void AssertValidArgs(int n, int k)
        {
            if (n < k)
                throw new ArgumentOutOfRangeException("n must be >= k");
            if (n <= 0)
                throw new ArgumentOutOfRangeException("n must be > 0");
            if (k <= 0)
                throw new ArgumentOutOfRangeException("k must be > 0");
        }
    }
}
