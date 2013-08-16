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
    }
}
