using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JoshCodes.Core;

namespace JoshCodes.Collections.Generic
{
    public static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }

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

        public static T SelectRandom<T>(this IEnumerable<T> items, int total, Random rand = null)
        {
            if (rand == null)
            {
                rand = new Random();
            }
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

        public static T SelectRandom<T>(this IEnumerable<T> items, Random rand = null)
        {
            return items.SelectRandom(items.Count(), rand);
        }

        public static TItem MinOn<TItem, TValue>(this IEnumerable<TItem> items, Func<TItem, TValue> selector)
            where TValue : IComparable
        {
            bool firstValueSet = false;
            var minValue = default(TValue);
            var minItem = default(TItem);
            foreach (var item in items)
            {
                var candidateValue = selector.Invoke(item);
                if (!firstValueSet)
                {
                    minItem = item;
                    minValue = candidateValue;
                    firstValueSet = true;
                    continue;
                }
                if (candidateValue.CompareTo(minValue) < 0)
                {
                    minItem = item;
                    minValue = candidateValue;
                }
            }
            return minItem;
        }

        public static IEnumerable<T> SubsetRandom<T>(this IEnumerable<T> items, int min, int max)
        {
            var rand = new Random();
            var remainingItems = new List<T>(items);

            var total = new Random().Between(min, max);
            int count = 0;
            while (count < total)
            {
                if(remainingItems.Count == 0)
                {
                    if(count < min)
                    {
                        throw new ArgumentException("min", "Minimum is greater than total number of items from which to subselect");
                    }
                    yield break;
                }
                var selectedItem = remainingItems.SelectRandom();
                remainingItems.Remove(selectedItem);
                yield return selectedItem;
                count++;
            }
        }
    }
}
