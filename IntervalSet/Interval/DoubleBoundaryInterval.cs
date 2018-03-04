using System;
using System.Collections.Generic;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Boundaries.Kind;

namespace IntervalSet.Interval
{
    /// <summary>
    /// A base class for implementations of an <see cref="IIntervalSet{T}"/> representing an interval with a start and an end
    /// </summary>
    public abstract class DoubleBoundaryInterval<TSet, TBuilder, TStartingInterval, TInterval, T> : SingleBoundaryInterval<TSet, TBuilder, TStartingInterval, TInterval, T>
        where TSet : IIntervalSet<T>
        where TBuilder : IBuilder<TSet, TInterval, TStartingInterval, T>, new()
        where TStartingInterval : class, TInterval, IStartingInterval<TInterval, T>
        where T : IComparable<T>, IEquatable<T>, IFormattable
    {
        /// <inheritdoc />
        public override bool ContainsNegativeInfinity()
        {
            return false;
        }

        /// <summary>
        /// The second <see cref="Boundary{T}"/> of this interval
        /// </summary>
        protected Boundary<T> OtherBoundary;

        /// <summary>
        /// The smallest <see cref="Boundary{T}"/> of this interval
        /// </summary>
        protected Boundary<T> Min { get; }

        /// <summary>
        /// The largest <see cref="Boundary{T}"/> of this interval
        /// </summary>
        protected Boundary<T> Max { get; }

        /// <summary>
        /// Initializes a new <see cref="DoubleBoundaryInterval{TSet,TBuilder,TStartingInterval,TInterval,T}"/> with two <see cref="Boundary{T}"/>s
        /// </summary>
        /// <param name="one"></param>
        /// <param name="other"></param>
        protected DoubleBoundaryInterval(Boundary<T> one, Boundary<T> other) : base(one)
        {
            OtherBoundary = other;
            if (Boundary.Location.CompareTo(OtherBoundary.Location) < 0)
            {
                Min = Boundary;
                Max = OtherBoundary;
            }
            else
            {
                Min = OtherBoundary;
                Max = Boundary;
            }
        }

        /// <inheritdoc />
        public override bool Contains(T item)
        {
            if (item.Equals(Min.Location))
            {
                return Min.Inclusive;
            }
            if (item.Equals(Max.Location))
            {
                return Max.Inclusive;
            }
            if (item.CompareTo(Min.Location) > 0 && item.CompareTo(Max.Location) < 0)
            {
                return true;
            }
            return false;
        }

        /// <inheritdoc />
        public override bool ContainsInterval(T from, T to)
        {
            return from.CompareTo(Min.Location) >= 0 && to.CompareTo(Max.Location) <= 0;
        }

        /// <inheritdoc />
        public override IEnumerable<Boundary<T>> Boundaries
        {
            get
            {
                yield return Min;
                yield return Max;
            }
        }

        /// <inheritdoc />
        public override BoundaryKind Cross(T location)
        {
            if (location.Equals(Min.Location))
            {
                return Min.Kind;
            }
            if (location.Equals(Max.Location))
            {
                return Max.Kind;
            }
            if (location.CompareTo(Min.Location) < 0 || location.CompareTo(Max.Location) > 0)
            {
                return null;
            }
            return new ContinuationKind();
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (Min.GetHashCode() * 397) ^ Max.GetHashCode();
            }
        }

        /// <inheritdoc />
        public override string ToString(string format, IFormatProvider formatProvider)
        {
            return Min.ToString(format, formatProvider) + ", " + Max.ToString(format, formatProvider);
        }
    }
}
