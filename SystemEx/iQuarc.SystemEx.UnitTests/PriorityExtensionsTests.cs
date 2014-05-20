using System.Linq;
using iQuarc.SystemEx.Priority;
using Xunit;

namespace iQuarc.SystemEx.UnitTests
{
    public class PriorityExtensionsTests
    {
        [Fact]
        public void OrderByPriority_NoPriorities_SameOrder()
        {
            TestOrderByPriority(new object[] {"first", "second"}, new object[] {"first", "second"});
        }

        [Fact]
        public void OrderByPriority_FirstWithLowerPriority_Reordered()
        {
            var p1 = new Prio1();
            var p2 = new Prio2();

            TestOrderByPriority(new object[] {p2, p1}, new object[] {p1, p2});
        }

        [Fact]
        public void OrderByPriority_FirstWithNoPriorityAndSecondHighPriority_Reordered()
        {
            var noPrio = "NoPriority";
            var highPrio = new PrioHigh();

            TestOrderByPriority(new object[] {noPrio, highPrio}, new object[] {highPrio, noPrio});
        }

        [Fact]
        public void OrderByPriority_FirstWithNoPriorityAndSecondWithLowPriority_SameOrder()
        {
            var noPrio = "NoPriority";
            var lowPrio = new PrioLow();

            TestOrderByPriority(new object[] {noPrio, lowPrio}, new object[] {noPrio, lowPrio});
        }

        [Fact]
        public void OrderByPriority_FirstNoPrioAndSecondMediumPrio_SameOrder()
        {
            var noPrio = "NoPriority";
            var mediumPrio = new PrioMedium();

            TestOrderByPriority(new object[] { noPrio, mediumPrio }, new object[] { noPrio, mediumPrio });
        }

        [Fact]
        public void OrderByPriority_FirstMediumPrioAndSecondNoPrio_SameOrder()
        {
            var noPrio = "NoPriority";
            var mediumPrio = new PrioMedium();

            TestOrderByPriority(new object[] { mediumPrio, noPrio}, new object[] { mediumPrio, noPrio});
        }

        private static void TestOrderByPriority(object[] input, object[] expected)
        {
            var actual = input.OrderByPriority().ToArray();
            Assert.Equal(expected, actual);
        }

        [Priority(1)]
        private class Prio1
        {
        }

        [Priority(2)]
        private class Prio2
        {
        }

        [Priority(Priorities.High)]
        private class PrioHigh
        {
        }

        [Priority(Priorities.Low)]
        private class PrioLow
        {
        }

        [Priority(Priorities.Medium)]
        private class PrioMedium
        {
        }
    }
}