﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoshCodes.Core
{
    // Much code borrowed from:  http://stackoverflow.com/questions/716552/can-you-create-a-simple-equalitycomparert-using-a-lamba-expression

    // Helper class for construction
    public static class ProjectionEqualityComparer
    {
        public static ProjectionEqualityComparer<TSource, TKey>
            Create<TSource, TKey>(Func<TSource, TKey> projection)
        {
            return new ProjectionEqualityComparer<TSource, TKey>(projection);
        }

        public static ProjectionEqualityComparer<TSource, TKey>
            Create<TSource, TKey>(TSource ignored,
                                   Func<TSource, TKey> projection)
        {
            return new ProjectionEqualityComparer<TSource, TKey>(projection);
        }
    }

    public static class ProjectionEqualityComparer<TSource>
    {
        public static ProjectionEqualityComparer<TSource, TKey>
            Create<TKey>(Func<TSource, TKey> projection)
        {
            return new ProjectionEqualityComparer<TSource, TKey>(projection);
        }
    }

    public class ProjectionEqualityComparer<TSource, TKey>
        : IEqualityComparer<TSource>
    {
        readonly Func<TSource, TKey> projection;
        readonly IEqualityComparer<TKey> comparer;

        public ProjectionEqualityComparer(Func<TSource, TKey> projection)
            : this(projection, null)
        {
        }

        public ProjectionEqualityComparer(
            Func<TSource, TKey> projection,
            IEqualityComparer<TKey> comparer)
        {
            if(projection == null)
                throw new ArgumentNullException("projection");

            this.comparer = comparer ?? EqualityComparer<TKey>.Default;
            this.projection = projection;
        }

        public bool Equals(TSource x, TSource y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            if (x == null || y == null)
            {
                return false;
            }
            return comparer.Equals(projection(x), projection(y));
        }

        public int GetHashCode(TSource obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            return comparer.GetHashCode(projection(obj));
        }
    }
}
