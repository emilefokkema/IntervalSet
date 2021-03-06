﻿using System;
using System.Collections.Generic;
using IntervalSet.Interval;
using IntervalSet.Interval.Boundaries;

namespace PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time with a start date and an actual end date
    /// </summary>
    public class StartEndingOpenPeriod : DoubleBoundaryInterval<OpenPeriodSet, OpenPeriodListBuilder,IOpenPeriod,DateTime>, IOpenPeriod
    {
        /// <inheritdoc />
        public StartEndingOpenPeriod(Start<DateTime> from, End<DateTime> to):base(from, to)
        {
        }

        /// <inheritdoc />
        protected override IOpenPeriod GetInterval()
        {
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// This <see cref="IOpenPeriod"/> does have an end date
        /// </summary>
        public DateTime? To => EndingBoundary;

        /// <inheritdoc />
        public DateTime Earliest => StartingBoundary;
    }
}
