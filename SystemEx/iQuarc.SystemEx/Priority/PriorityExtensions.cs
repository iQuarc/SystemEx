using System.Collections.Generic;
using System.Linq;

namespace iQuarc.SystemEx.Priority
{
    public static class PriorityExtensions
    {
        /// <summary>
        ///     Orders the elements by priority attribute on their type.
        /// </summary>
        public static IEnumerable<T> OrderByPriority<T>(this IEnumerable<T> items)
        {
            if (items == null)
                return null;

            return items.OrderBy(i => i, new PriorityComparer<T>());
        }

        private class PriorityComparer<T> : IComparer<T>
        {
            public int Compare(T x, T y)
            {
                PriorityAttribute xAttribute = ReflectionExtensions.GetAttribute<PriorityAttribute>(x);
                PriorityAttribute yAttribute = ReflectionExtensions.GetAttribute<PriorityAttribute>(y);

                int priorityX = xAttribute == null ? int.MaxValue/2 : xAttribute.Value;
                int priorityY = yAttribute == null ? int.MaxValue/2 : yAttribute.Value;

                int rezult = priorityX.CompareTo(priorityY);
                return rezult;
            }
        }
    }
}