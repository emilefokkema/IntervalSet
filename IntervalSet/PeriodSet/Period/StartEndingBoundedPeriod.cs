﻿using System;
using IntervalSet.PeriodSet.Period.Boundaries;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time with a start date and an end date
    /// </summary>
    public class StartEndingBoundedPeriod : DoubleBoundedPeriod<BoundedPeriodSet, BoundedPeriodListBuilder, StartingBoundedPeriod,IBoundedPeriod>, IBoundedPeriod
    {
        /// <inheritdoc />
        public StartEndingBoundedPeriod(Boundary from, Boundary to):base(from, to)
        {
        }

        /// <inheritdoc />
        protected override IBoundedPeriod GetPeriod()
        {
            return this;
        }

        /// <inheritdoc />
        public DateTime To => Max.Date;

        /// <inheritdoc />
        public DateTime Earliest => Min.Date;
    }
}
