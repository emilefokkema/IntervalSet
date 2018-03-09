using System;
using System.Collections.Generic;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Default;

namespace IntervalSet.Interval
{
    public abstract class Interval<TSet, TIntervalBuilder, TInterval, T> : IntervalSet<TSet, TIntervalBuilder, TInterval, T>, IDefaultInterval<T>
        where TSet : IIntervalSet<T>
        where TIntervalBuilder : IIntervalBuilder<TInterval, T>, ISetBuilder<TSet, TInterval, T>, new()
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

        public virtual Boundary<T> StartingBoundary => IntervalBuilder.NegativeInfinityBoundary;

        public virtual Boundary<T> EndingBoundary => IntervalBuilder.PositiveInfinityBoundary;

        public T Start => StartingBoundary;

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
