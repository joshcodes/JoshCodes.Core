﻿using System;
using System.Collections;

namespace JoshCodes.Collections
{
    public static class CollectionExtensions
    {
        public static System.Collections.Generic.IEnumerable<T> Select<T>(this ICollection collection, Func<object, T> expr)
        {
            foreach (var item in collection)
            {
                yield return expr.Invoke(item);
            }
        }
    }
}
