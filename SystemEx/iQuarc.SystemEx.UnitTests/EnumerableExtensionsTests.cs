using System;
using System.Collections.Generic;
using System.Linq;
using iQuarc.xUnitEx;
using Xunit;

namespace iQuarc.SystemEx.UnitTests
{
    public class EnumerableExtensionsTests
    {
        [Fact]
        public void ReplaceAll_OneOccurrenceForReplacedElement_ElementReplaced()
        {
            char[] chars = {'a', 'b', 'c'};

            IEnumerable<char> actual = chars.ReplaceAll('b', 'x');

            AssertEx.AreEquivalent(actual, 'a', 'x', 'c');
        }

        [Fact]
        public void ReplaceAll_MoreOccurrenceForReplacedElement_AllReplacedWithOneElement()
        {
            char[] chars = new[] {'a', 'b', 'b'};

            var actual = chars.ReplaceAll('b', 'x');

            AssertEx.AreEquivalent(actual, 'a', 'x');
        }

        [Fact]
        public void ReplaceAll_NoOccurrence_SameElementsReturned()
        {
            char[] chars = new[] {'a', 'b', 'c'};

            var actual = chars.ReplaceAll('x', 'y');

            AssertEx.AreEquivalent(actual, 'a', 'b', 'c');
        }

        [Fact]
        public void ReplaceAll_ReplacementIsNull_AllAreReplacedWithNull()
        {
            string[] chars = new[] {"a", "b", "c"};

            var actual = chars.ReplaceAll("b", null);

            AssertEx.AreEquivalent(actual, "a", null, "c");
        }

        [Fact]
        public void ReplaceAll_SourceIsNull_AllNullsAreReplaced()
        {
            string[] chars = new[] {"a", null, "c"};

            var actual = chars.ReplaceAll(null, "b");

            AssertEx.AreEquivalent(actual, "a", "b", "c");
        }

        [Fact]
        public void Replace_OneOccurrenceForReplacedElement_ElementReplaced()
        {
            char[] chars = new[] {'a', 'b', 'c'};

            var actual = chars.Replace('b', 'x');

            AssertEx.AreEquivalent(actual, 'a', 'x', 'c');
        }

        [Fact]
        public void Replace_MoreOccurrenceForReplacedElement_AllReplacedWithOneElement()
        {
            char[] chars = new[] {'a', 'b', 'b'};

            var actual = chars.Replace('b', 'x').ToArray();

            AssertEx.AreEquivalent(actual, 'a', 'x', 'x');
        }

        [Fact]
        public void Replace_NoOccurrence_SameElementsReturned()
        {
            char[] chars = new[] {'a', 'b', 'c'};

            var actual = chars.Replace('x', 'y');

            AssertEx.AreEquivalent(actual, 'a', 'b', 'c');
        }

        [Fact]
        public void Replace_ReplacementIsNull_ReplacedWithNull()
        {
            string[] chars = new[] {"a", "b", "c"};

            var actual = chars.Replace("b", null);

            AssertEx.AreEquivalent(actual, "a", null, "c");
        }

        [Fact]
        public void Replace_SourceIsNull_NullIsReplaced()
        {
            string[] chars = new[] {"a", null, "c"};

            var actual = chars.Replace(null, "b");

            AssertEx.AreEquivalent(actual, "a", "b", "c");
        }

        [Fact]
        public void ForEach_IEnumerableIsNull_NoExceptionIsThrown()
        {
            Action<int> a = i => i++;

            EnumerableExtensions.ForEach(null, a);
        }

        [Fact]
        public void ForEach_ActionIsNull_ArgumentException()
        {
            int[] e = new[] {2, 2};

            Action act = () => e.ForEach(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ForEach_MoreElements_ActionExecutedForEachElement()
        {
            List<int> actual = new List<int>();
            int[] e = new[] {1, 2};

            e.ForEach(actual.Add);

            List<int> expected = new List<int> {1, 2};
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Includes_AllSubsetElementsExistInSuperset_ReturnsTrue()
        {
            char[] superset = new[] {'a', 'b', 'c'};
            char[] subset = new[] {'b', 'c'};

            bool actual = superset.Includes(subset);

            Assert.True(actual);
        }

        [Fact]
        public void Includes_NotAllItemsFromSubsetAreInTheSuperset_ReturnsFalse()
        {
            char[] superset = new[] {'a', 'b', 'c'};
            char[] subset = new[] {'b', 'c', 'd'};

            bool actual = superset.Includes(subset);

            Assert.False(actual);
        }

        [Fact]
        public void Includes_AllSubsetElementsExistInSupersetAndSubsetHasDuplicates_ReturnsTrue()
        {
            char[] superset = new[] {'a', 'b', 'c'};
            char[] subset = new[] {'b', 'c', 'c', 'c', 'c'};

            bool actual = superset.Includes(subset);

            Assert.True(actual);
        }
    }
}