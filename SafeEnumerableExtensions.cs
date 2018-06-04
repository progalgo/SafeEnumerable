using System;
using System.Collections.Generic;

namespace SafeEnumerable
{
    public static class SafeEnumerableExtensions
    {
        public static bool MoveNextSafely<T>(this IEnumerator<T> enumerator)
        {
            try
            {
                return enumerator.MoveNext();
            }
            catch (Exception e)
            {
                return !(e is InvalidOperationException)
            }
        }

        public static IEnumerable<T> Safe<T>(this IEnumerable<T> enumerable)
        {
            var enumerator = enumerable.GetEnumerator();

            while (enumerator.MoveNextSafely())
            {
                yield return enumerator.Current;
            }
        }
    }
}
