﻿using System;
using System.Collections.Generic;
using PeriodSet.Period.Boundaries;
using PeriodSet.Period.Boundaries.Kind;

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
        /// The second <see cref="Boundary"/> of this period
        /// </summary>
        protected Boundary OtherBoundary;

        /// <summary>
        /// The smallest <see cref="Boundary"/> of this period
        /// </summary>
        protected Boundary Min { get; }

        /// <summary>
        /// The largest <see cref="Boundary"/> of this period
        /// </summary>
        protected Boundary Max { get; }

        /// <summary>
        /// Initializes a new <see cref="DoubleBoundaryPeriod{TSet,TBuilder,TStartingPeriod,TPeriod}"/> with two <see cref="Boundary"/>s
        /// </summary>
        /// <param name="one"></param>
        /// <param name="other"></param>
        protected DoubleBoundaryPeriod(Boundary one, Boundary other):base(one)
        {
            OtherBoundary = other;
            if (Boundary.Date < OtherBoundary.Date)
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
            if (date == Min.Date)
            {
                return Min.Inclusive;
            }
            if (date == Max.Date)
            {
                return Max.Inclusive;
            }
            if (date > Min.Date && date < Max.Date)
            {
                return true;
            }
            return false;
        }

        /// <inheritdoc />
        public override bool ContainsPeriod(DateTime from, DateTime to)
        {
            return from >= Min.Date && to <= Max.Date;

        }

        /// <inheritdoc />
        public override IEnumerable<Boundary> Boundaries
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
            if (date == Min.Date)
            {
                return Min.Kind;
            }
            if (date == Max.Date)
            {
                return Max.Kind;
            }
            if (date < Min.Date || date > Max.Date)
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