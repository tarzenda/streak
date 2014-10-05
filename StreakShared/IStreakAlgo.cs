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
    }
}
