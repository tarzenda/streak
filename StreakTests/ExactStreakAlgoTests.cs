using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tarzenda.Streak.Tests
{
    [TestClass]
    public class ExactStreakAlgoTests
    {
        private ExactStreakAlgo _algo = new ExactStreakAlgo();

        [TestMethod]
        public void TestHeadsOnly()
        {
            Assert.AreEqual( 1ul, _algo.Calculate(StreakVariant.HeadsOnly, 5, 5).Matches);
            Assert.AreEqual(31ul, _algo.Calculate(StreakVariant.HeadsOnly, 5, 1).Matches);
            Assert.AreEqual(20ul, _algo.Calculate(StreakVariant.HeadsOnly, 8, 5).Matches);
         }

        [TestMethod]
        public void TestHeadsAndTailsOnly()
        {
            Assert.AreEqual( 2ul, _algo.Calculate(StreakVariant.HeadsAndTails, 5, 5).Matches);
            Assert.AreEqual(32ul, _algo.Calculate(StreakVariant.HeadsAndTails, 5, 1).Matches);
            Assert.AreEqual(40ul, _algo.Calculate(StreakVariant.HeadsAndTails, 8, 5).Matches);
        }
    }
}
