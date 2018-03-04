﻿using System;
using System.Collections.Generic;
using IntervalSet.Interval;
using IntervalSet.Interval.Boundaries.Kind;

namespace IntervalSet
{
    /// <summary>
    /// Base class for implementations of <see cref="IIntervalSet{T}"/> that do not represent the entire <typeparamref name="T"/> space
    /// </summary>
    public abstract class EmptyIntervalSet<TSet, TBuilder, TStartingInterval, TInterval, T> : IntervalSet<TSet, TBuilder, TStartingInterval, TInterval, T>
        where TSet : IIntervalSet<T>
        where TBuilder : IBuilder<TSet, TInterval, TStartingInterval, T>, new()
        where TStartingInterval : class, TInterval, IStartingInterval<TInterval, T>
        where T : IComparable<T>, IEquatable<T>
    {
        /// <inheritdoc />
        public override bool ContainsNegativeInfinity()
        {
            return false;
        }

        /// <inheritdoc />
        public override bool Contains(T item)
        {
            return false;
        }

        /// <inheritdoc />
        public override bool ContainsInterval(T from, T to)
        {
            return false;
        }

        /// <inheritdoc />
        public override bool IsNonEmpty(out TInterval nonEmpty)
        {
            nonEmpty = default(TInterval);
            return false;
        }

        /// <inheritdoc />
        public override bool IsEmpty => true;

        /// <inheritdoc />
        public override int IntervalCount => 0;

        /// <inheritdoc />
        public override BoundaryKind Cross(T location)
        {
            return null;
        }

        /// <inheritdoc />
        public override IEnumerable<TT> Select<TT>(Func<TInterval, TT> selector)
        {
            yield break;
        }

        /// <inheritdoc />
        public override void ForEach(Action<TInterval> what)
        {
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return 0;
        }

        /// <inheritdoc />
        public override string ToString(string format, IFormatProvider provider)
        {
            return "(empty)";
        }
    }
}
