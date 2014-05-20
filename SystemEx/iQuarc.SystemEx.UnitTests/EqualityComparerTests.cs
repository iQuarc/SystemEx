using System;
using System.Collections.Generic;
using Xunit;

namespace iQuarc.SystemEx.UnitTests
{
    public class EqualityComparerTests
    {
        [Fact]
        public void From_NoParams_DefaultComparer()
        {
            IEqualityComparer<int> comparer = EqualityComparer.From<int>();

            Assert.Same(EqualityComparer<int>.Default, comparer);
        }

        [Fact]
        public void From_EqualityAndNoHash_EqualityUsed()
        {
            var comparer = EqualityComparer.From<string>((s1, s2) => s1[0] == s2[0]);

            bool result = comparer.Equals("abc", "aa");

            Assert.True(result);
        }

        [Fact]
        public void From_EqualityAndNoHash_DefaultHashImpl()
        {
            var comparer = EqualityComparer.From<bool>((b, b1) => true);

            int result = comparer.GetHashCode(true);

            Assert.Equal(1, result);
        }

        [Fact]
        public void From_NoEqualityAndCustomHash_HashUsed()
        {
            var comparer = EqualityComparer.From<bool>(null, b => 3);

            int result = comparer.GetHashCode(true);

            Assert.Equal(3, result);
        }

        [Fact]
        public void From_NoEqualityAndCustomHash_DefaultEqualityUsed()
        {
            var comparer = EqualityComparer.From<bool>(null, b => 3);

            bool result = comparer.Equals(true, true);

            Assert.True(result);
        }

        [Fact]
        public void From_BothEqualityAndHash_CustomEqualityUsed()
        {
            Func<bool, bool, bool> equality = (b1, b2) => b1 != b2;
            Func<bool, int> hash = b => 3;
            var comparer = EqualityComparer.From(equality, hash);

            bool equalityResult = comparer.Equals(true, false);
         
            Assert.True(equalityResult);
        }

        [Fact]
        public void From_BothEqualityAndHash_CustomHashUsed()
        {
            Func<bool, bool, bool> equality = (b1, b2) => true;
            Func<bool, int> hash = b => 3;
            var comparer = EqualityComparer.From(equality, hash);

            int hashResult = comparer.GetHashCode(true);
            Assert.Equal(3, hashResult);
        }
    }
}