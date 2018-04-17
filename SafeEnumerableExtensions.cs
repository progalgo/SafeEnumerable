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
                if (e is InvalidOperationException)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public static IEnumerable<T> Safe<T>(this IEnumerable<T> enumerable)
        {
            var enumerator = enumerable.GetEnumerator();

            bool end = enumerator.MoveNextSafely();

            while (!end)
            {
                yield return enumerator.Current;
                end = enumerator.MoveNextSafely();
            }
        }
    }
}
