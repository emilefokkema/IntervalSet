﻿using System;
using System.Collections.Generic;
using IntervalSet.Interval;
using IntervalSet.Interval.Boundaries;

namespace PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time with a start date and an end date
    /// </summary>
    public class StartEndingBoundedPeriod : DoubleBoundaryInterval<BoundedPeriodSet, BoundedPeriodListBuilder,IBoundedPeriod, DateTime>, IBoundedPeriod
    {
        /// <inheritdoc />
        public StartEndingBoundedPeriod(Start<DateTime> from, End<DateTime> to):base(from, to)
        {
        }

        /// <inheritdoc />
        protected override IBoundedPeriod GetInterval()
        {
            return this;
        }

        protected override BoundedPeriodSet MakeSet(IList<IBoundedPeriod> intervals)
        {
            return new BoundedPeriodSet(intervals);
        }

        /// <inheritdoc />
        public DateTime To => Max.Location;

        /// <inheritdoc />
        public DateTime Earliest => Min.Location;
    }
}
