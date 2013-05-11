using System;
using System.Collections.Generic;

namespace OpenCat
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> NonConsecutive<T>(this IEnumerable<T> input)
        {
            if (input == null) throw new ArgumentNullException("input");
            return NonConsecutiveImpl(input);
        }

        static IEnumerable<T> NonConsecutiveImpl<T>(this IEnumerable<T> input)
        {
            bool isFirst = true;
            T last = default(T);
            foreach (var item in input)
            {
                if (isFirst || !object.Equals(item, last))
                {
                    yield return item;
                    last = item;
                    isFirst = false;
                }
            }
        }
    }
}