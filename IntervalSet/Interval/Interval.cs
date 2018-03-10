using System;
using System.Collections.Generic;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Default;

namespace IntervalSet.Interval
{
    /// <summary>
    /// Implementation of an <see cref="IIntervalSet{T}"/> that represents a single interval
    /// </summary>
    /// <typeparam name="TSet"></typeparam>
    /// <typeparam name="TBuilder"></typeparam>
    /// <typeparam name="TInterval"></typeparam>
    /// <typeparam name="T"></typeparam>
    public abstract class Interval<TSet, TBuilder, TInterval, T> : IntervalSet<TSet, TBuilder, TInterval, T>, IDefaultInterval<T>
        where TSet : IIntervalSet<T>
        where TInterval : IIntervalSet<T>
        where TBuilder : IBuilder<TSet, TInterval, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// returns a typed version of this instance
        /// </summary>
        /// <returns></returns>
        protected abstract TInterval GetInterval();

        /// <inheritdoc />
        public override bool IsNonEmpty(out TInterval nonEmpty)
        {
            nonEmpty = GetInterval();
            return true;
        }

        /// <summary>
        /// An interval is never empty
        /// </summary>
        public override bool IsEmpty => false;

        /// <summary>
        /// Namely, this one
        /// </summary>
        public override int IntervalCount => 1;

        /// <inheritdoc />
        public override IEnumerable<TT> Select<TT>(Func<TInterval, TT> selector)
        {
            yield return selector(GetInterval());
        }

        /// <inheritdoc />
        public override void ForEach(Action<TInterval> what)
        {
            what(GetInterval());
        }

        /// <inheritdoc />
        public virtual Boundary<T> StartingBoundary => IntervalBuilder.NegativeInfinityBoundary;

        /// <inheritdoc />
        public virtual Boundary<T> EndingBoundary => IntervalBuilder.PositiveInfinityBoundary;

        /// <inheritdoc />
        public T Start => StartingBoundary;

        /// <inheritdoc />
        public T End => EndingBoundary;

        /// <inheritdoc />
        public override string ToString(string format, IFormatProvider formatProvider)
        {
            if (StartingBoundary.Equals(EndingBoundary))
            {
                return StartingBoundary.ToString(format, formatProvider);
            }
            return StartingBoundary.ToString(format, formatProvider) + ", " + EndingBoundary.ToString(format, formatProvider);
        }
    }
}
