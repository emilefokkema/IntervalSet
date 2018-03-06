using System;
using System.Collections.Generic;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Boundaries.Kind;

namespace IntervalSet.Interval
{
    /// <summary>
    /// A base class for implementations of an <see cref="IIntervalSet{T}"/> representing an interval with a single boundary
    /// </summary>
    public abstract class SingleBoundaryInterval<TSet, TBuilder, TInterval, T> : IntervalSet<TSet, TBuilder, TInterval, T>
        where TSet : IIntervalSet<T>
        where TBuilder : IBuilder<TSet, TInterval, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// returns a typed version of this instance
        /// </summary>
        /// <returns></returns>
        protected abstract TInterval GetInterval();

        /// <inheritdoc />
        public override bool ContainsNegativeInfinity()
        {
            return Boundary.IsEnd;
        }

        /// <summary>
        /// The boundary of this period
        /// </summary>
        public Boundary<T> Boundary { get; }

        /// <summary>
        /// Initializes a new <see cref="SingleBoundaryInterval{TSet,TBuilder,TInterval,T}"/> with a given boundary
        /// </summary>
        /// <param name="boundary"></param>
        protected SingleBoundaryInterval(Boundary<T> boundary)
        {
            Boundary = boundary;
        }

        /// <inheritdoc />
        public override bool Contains(T item)
        {
            if (item.Equals(Boundary.Location))
            {
                return Boundary.Inclusive;
            }
            return item.CompareTo(Boundary.Location) > 0 && Boundary.IsStart || item.CompareTo(Boundary.Location) < 0 && Boundary.IsEnd;
        }

        /// <inheritdoc />
        public override bool ContainsInterval(T from, T to)
        {
            if (from.CompareTo(Boundary.Location) >= 0)
            {
                return Boundary.IsStart;
            }
            if (to.CompareTo(Boundary.Location) <= 0)
            {
                return Boundary.IsEnd;
            }
            return Boundary.IsContinuation;
        }

        /// <inheritdoc />
        public override IEnumerable<Boundary<T>> Boundaries
        {
            get { yield return Boundary; }
        }

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
        public override BoundaryKind Cross(T location)
        {
            if (location.Equals(Boundary.Location))
            {
                return Boundary.Kind;
            }
            if (location.CompareTo(Boundary.Location) > 0 && Boundary.IsStart || location.CompareTo(Boundary.Location) < 0 && Boundary.IsEnd)
            {
                return new ContinuationKind();
            }
            return null;
        }

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
        public override int GetHashCode()
        {
            return Boundary.GetHashCode();
        }

        /// <inheritdoc />
        public override string ToString(string format, IFormatProvider formatProvider)
        {
            string boundaryString = Boundary.ToString(format, formatProvider);
            if (Boundary.IsContinuation)
            {
                return "(-Infinity, Infinity)";
            }
            if (Boundary.IsEnd)
            {
                if (Boundary.IsStart)
                {
                    return "(-Infinity, " + boundaryString + ", Infinity)";
                }
                return "(-Infinity, " + boundaryString;
            }
            if (Boundary.IsStart)
            {
                return boundaryString + ", Infinity)";
            }
            return boundaryString;
        }
    }
}
