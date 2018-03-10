using System;
using System.Collections.Generic;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Boundaries.Kind;

namespace IntervalSet.Interval
{
    /// <summary>
    /// A base class for implementations of an <see cref="IIntervalSet{T}"/> representing an interval with a start and an end
    /// </summary>
    public abstract class DoubleBoundaryInterval<TSet, TBuilder, TInterval, T> : SingleBoundaryInterval<TSet, TBuilder, TInterval, T>
        where TSet : IIntervalSet<T>
        where TInterval : IIntervalSet<T>
        where TBuilder : IBuilder<TSet, TInterval, T>, new()
        where T : IComparable<T>, IEquatable<T>
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

        private readonly Boundary<T> _min;

        private readonly Boundary<T> _max;

        /// <inheritdoc />
        public override Boundary<T> StartingBoundary => _min;

        /// <inheritdoc />
        public override Boundary<T> EndingBoundary => _max;

        /// <summary>
        /// Initializes a new <see cref="DoubleBoundaryInterval{TSet,TBuilder,TInterval,T}"/> with two <see cref="Boundary{T}"/>s
        /// </summary>
        /// <param name="one"></param>
        /// <param name="other"></param>
        protected DoubleBoundaryInterval(Boundary<T> one, Boundary<T> other) : base(one)
        {
            OtherBoundary = other;
            if (Boundary.Location.CompareTo(OtherBoundary.Location) < 0)
            {
                _min = Boundary;
                _max = OtherBoundary;
            }
            else
            {
                _min = OtherBoundary;
                _max = Boundary;
            }
        }

        /// <inheritdoc />
        public override bool Contains(T item)
        {
            if (item.Equals(_min.Location))
            {
                return _min.Inclusive;
            }
            if (item.Equals(_max.Location))
            {
                return _max.Inclusive;
            }
            if (item.CompareTo(_min.Location) > 0 && item.CompareTo(_max.Location) < 0)
            {
                return true;
            }
            return false;
        }

        /// <inheritdoc />
        public override bool ContainsInterval(T from, T to)
        {
            return from.CompareTo(_min.Location) >= 0 && to.CompareTo(_max.Location) <= 0;
        }

        /// <inheritdoc />
        public override IEnumerable<Boundary<T>> Boundaries
        {
            get
            {
                yield return _min;
                yield return _max;
            }
        }

        /// <inheritdoc />
        public override BoundaryKind Cross(T location)
        {
            if (location.Equals(_min.Location))
            {
                return _min.Kind;
            }
            if (location.Equals(_max.Location))
            {
                return _max.Kind;
            }
            if (location.CompareTo(_min.Location) < 0 || location.CompareTo(_max.Location) > 0)
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
                return (_min.GetHashCode() * 397) ^ _max.GetHashCode();
            }
        }
    }
}
