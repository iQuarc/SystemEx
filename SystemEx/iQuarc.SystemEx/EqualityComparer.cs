using System;
using System.Collections.Generic;

namespace iQuarc.SystemEx
{
    public static class EqualityComparer
    {
        /// <summary>
        /// Creates an IEqualityComparer of T, based on equality and hash functions which are given as parameters
        /// </summary>
        public static IEqualityComparer<T> From<T>(Func<T, T, bool> equality = null, Func<T, int> hash = null)
        {
            EqualityComparer<T> @default = EqualityComparer<T>.Default;

            if (equality == null && hash == null)
                return @default;

            if (hash == null)
                return new CustomEqualityComparer<T>(equality, @default.GetHashCode);

            if (equality==null)
                return new CustomEqualityComparer<T>(@default.Equals, hash);

            return new CustomEqualityComparer<T>(equality, hash);
        }

        private class CustomEqualityComparer<T> : EqualityComparer<T>
        {
            private readonly Func<T, T, bool> equality;
            private readonly Func<T, int> hash;

            public CustomEqualityComparer(Func<T, T, bool> equality, Func<T, int> hash)
            {
                this.equality = equality;
                this.hash = hash;
            }

            public override bool Equals(T x, T y)
            {
                return equality(x, y);
            }

            public override int GetHashCode(T obj)
            {
                return hash(obj);
            }
        }
    }
}