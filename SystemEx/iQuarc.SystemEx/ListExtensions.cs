using System;
using System.Collections.Generic;

namespace iQuarc.SystemEx
{
    public static class ListExtensions
    {
        /// <summary>
        /// Performs a binary search by a specific Func selector.
        /// </summary>
        public static int BinarySearchBy<T>(this List<T> list, T item, Func<T, IComparable> selector)
        {
            return list.BinarySearch(item, new FuncComparer<T>(selector));
        }

        private class FuncComparer<T> : IComparer<T> {
            private readonly Func<T, IComparable> comparer;

            public FuncComparer(Func<T, IComparable> comparer) {
                this.comparer = comparer;
            }

            public int Compare(T x, T y)
            {
                return comparer(x).CompareTo(comparer(y));
            }
        }
    }
}