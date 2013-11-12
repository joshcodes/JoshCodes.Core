using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshCodes.Collections.Generic
{
    public static class IEnumerableExtensions
    {
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue, TItem>(
            this IEnumerable<TItem> items, Func<TItem, TKey> keySelector, Func<TItem, TValue> valueSelector)
        {
            var dictionary = new Dictionary<TKey, TValue>();
            foreach (var item in items)
            {
                dictionary.Add(keySelector.Invoke(item), valueSelector.Invoke(item));
            }
            return dictionary;
        }

        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            var dictionary = new Dictionary<TKey, TValue>();
            foreach (var item in items)
            {
                dictionary.Add(item.Key, item.Value);
            }
            return dictionary;
        }

        public static IEnumerable<T> ToEnumerable<T>(this T item)
        {
            yield return item;
        }

        public static T SelectRandom<T>(this IEnumerable<T> items, int total)
        {
            var rand = new Random();
            var totalD = (double)total;
            var arrayItems = new T[total];
            var arrayItemsIndex = 0;
            foreach (var item in items)
            {
                if (rand.NextDouble() < (1.0 / totalD))
                {
                    return item;
                }
                totalD -= 1.0;
                arrayItems[arrayItemsIndex] = item;
                arrayItemsIndex++;
            }
            if (arrayItemsIndex == 0)
            {
                return default(T);
            }
            var selectedIndex = (int)(arrayItemsIndex * rand.NextDouble());
            return arrayItems[selectedIndex];
        }

        public static T SelectRandom<T>(this IEnumerable<T> items)
        {
            return items.SelectRandom(items.Count());
        }
    }
}
