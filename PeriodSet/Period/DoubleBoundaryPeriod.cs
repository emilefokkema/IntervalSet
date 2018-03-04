using System;
using System.Collections.Generic;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Boundaries.Kind;

namespace PeriodSet.Period
{
    /// <summary>
    /// A base class for implementations of an <see cref="IPeriodSet"/> representing a single period of time with a start date and an end date
    /// </summary>
    public abstract class DoubleBoundaryPeriod<TSet, TBuilder, TStartingPeriod, TPeriod> : SingleBoundaryPeriod<TSet, TBuilder, TStartingPeriod, TPeriod>
        where TSet : IPeriodSet
        where TBuilder : IBuilder<TSet, TPeriod, TStartingPeriod>, new()
        where TStartingPeriod : class, TPeriod, IStartingPeriod<TPeriod>
    {
        /// <inheritdoc />
        public override bool ContainsNegativeInfinity()
        {
            return false;
        }

        /// <summary>
        /// The second <see cref="Boundary{T}"/> of this period
        /// </summary>
        protected Boundary<DateTime> OtherBoundary;

        /// <summary>
        /// The smallest <see cref="Boundary"/> of this period
        /// </summary>
        protected Boundary<DateTime> Min { get; }

        /// <summary>
        /// The largest <see cref="Boundary"/> of this period
        /// </summary>
        protected Boundary<DateTime> Max { get; }

        /// <summary>
        /// Initializes a new <see cref="DoubleBoundaryPeriod{TSet,TBuilder,TStartingPeriod,TPeriod}"/> with two <see cref="Boundary"/>s
        /// </summary>
        /// <param name="one"></param>
        /// <param name="other"></param>
        protected DoubleBoundaryPeriod(Boundary<DateTime> one, Boundary<DateTime> other):base(one)
        {
            OtherBoundary = other;
            if (Boundary.Location < OtherBoundary.Location)
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
        public override bool ContainsDate(DateTime date)
        {
            if (date == Min.Location)
            {
                return Min.Inclusive;
            }
            if (date == Max.Location)
            {
                return Max.Inclusive;
            }
            if (date > Min.Location && date < Max.Location)
            {
                return true;
            }
            return false;
        }

        /// <inheritdoc />
        public override bool ContainsPeriod(DateTime from, DateTime to)
        {
            return from >= Min.Location && to <= Max.Location;

        }

        /// <inheritdoc />
        public override IEnumerable<Boundary<DateTime>> Boundaries
        {
            get
            {
                yield return Min;
                yield return Max;
            }
        }

        /// <inheritdoc />
        public override BoundaryKind Cross(DateTime date)
        {
            if (date == Min.Location)
            {
                return Min.Kind;
            }
            if (date == Max.Location)
            {
                return Max.Kind;
            }
            if (date < Min.Location || date > Max.Location)
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
