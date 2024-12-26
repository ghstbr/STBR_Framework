using System.Collections.Generic;
using System;

namespace STBR_Framework
{
    public static class CollectionExtensions
    {
        public static void Update<T>(this List<T> list, Func<T, bool> predicate, T newValue)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                {
                    list[i] = newValue;
                    return;
                }
            }
            list.Add(newValue);
        }

        public static IEnumerable<T> Update<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate, T newValue)
        {
            bool updated = false;
            foreach (var item in enumerable)
            {
                if (predicate(item))
                {
                    yield return newValue;
                    updated = true;
                }
                else
                {
                    yield return item;
                }
            }
            if (!updated)
            {
                yield return newValue;
            }
        }


    }
}
