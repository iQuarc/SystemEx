using System;
using System.Collections.Generic;
using System.Linq;

namespace iQuarc.SystemEx
{
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Replaces all occurrences of source element in given IEnumerable with replacement element.
        ///     If more occurrences are found all are replaced, with ONE replacement. Therefore, the resulted IEnumerable has less elements.
        ///     Default EqualityComparer is used to determine equality.
        /// </summary>
        /// <remarks>Null element is handled as any other element</remarks>
        public static IEnumerable<T> ReplaceAll<T>(this IEnumerable<T> collection, T source, T replacement)
        {
            return ReplaceAll(collection, source, replacement, EqualityComparer<T>.Default);
        }

        /// <summary>
        ///     Replaces all occurrences of source element in given IEnumerable with replacement element.
        ///     If more occurrences are found all are replaced, with ONE replacement. Therefore, the resulted IEnumerable has less elements.
        /// </summary>
        /// <remarks>Null element is handled as any other element</remarks>
        public static IEnumerable<T> ReplaceAll<T>(this IEnumerable<T> collection, T source, T replacement, IEqualityComparer<T> comparer)
        {
            bool found = false;
            foreach (var e in collection)
            {
                if (comparer.Equals(e, source))
                {
                    if (!found)
                    {
                        found = true;
                        yield return replacement;
                    }
                }
                else
                {
                    yield return e;
                }
            }
        }

        /// <summary>
        ///     Replaces each occurrences of source element in given IEnumerable with replacement element.
        ///     If more occurrences are found each one is replaced, resulting same number of elements in output IEnumerable
        ///     Default EqualityComparer is used to determine equality.
        /// </summary>
        /// <remarks>Null element is handled as any other element</remarks>
        public static IEnumerable<T> Replace<T>(this IEnumerable<T> collection, T source, T replacement)
        {
            return Replace(collection, source, replacement, EqualityComparer<T>.Default);
        }

        /// <summary>
        ///     Replaces each occurrences of source element in given IEnumerable with replacement element.
        ///     If more occurrences are found each one is replaced, resulting same number of elements in output IEnumerable
        /// </summary>
        /// <remarks>Null element is handled as any other element</remarks>
        public static IEnumerable<T> Replace<T>(this IEnumerable<T> collection, T source, T replacement, IEqualityComparer<T> comparer)
        {
            foreach (var e in collection)
            {
                if (comparer.Equals(e, source))
                    yield return replacement;
                else
                    yield return e;
            }
        }

        /// <summary>
        ///     Executes the action on each element from the enumerable.
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (action == null) throw new ArgumentNullException("action");
            if (collection == null) return;

            foreach (T value in collection)
                action(value);
        }

        /// <summary>
        ///     Verifies if every element of the subset is also an element of the superset.
        /// </summary>
        public static bool Includes<T>(this IEnumerable<T> superset, IEnumerable<T> subset)
        {
            return subset.All(superset.Contains);
        }

        /// <summary>
        ///     Verifies if a collection is null or does not have any elements.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            if (collection == null || !collection.Any())
                return true;

            return false;
        }

        ///     Selects elements from the enumerable distinct by a Func selector.
        /// </summary>
        public static IEnumerable<T> DistinctBy<T>(this IEnumerable<T> collection, Func<T, object> selector) {
            IEnumerable<T> result = collection.Distinct(new FuncEqualityComparer<T>(selector));

            return result;
        }

        private class FuncEqualityComparer<T> : IEqualityComparer<T> {
            private readonly Func<T, object> comparer;

            public FuncEqualityComparer(Func<T, object> comparer) {
                this.comparer = comparer;
            }

            public bool Equals(T x, T y) {
                return comparer(x).Equals(comparer(y));
            }

            public int GetHashCode(T obj) {
                return comparer(obj).GetHashCode();
            }
        }
    }
}