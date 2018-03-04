using System;
using IntervalSet.Interval.Boundaries;

namespace PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time with a start date and an end date
    /// </summary>
    public class StartEndingBoundedPeriod : DoubleBoundaryPeriod<BoundedPeriodSet, BoundedPeriodListBuilder, IStartingBoundedPeriod,IBoundedPeriod>, IBoundedPeriod
    {
        /// <inheritdoc />
        public StartEndingBoundedPeriod(Start<DateTime> from, End<DateTime> to):base(from, to)
        {
        }

        /// <inheritdoc />
        protected override IBoundedPeriod GetPeriod()
        {
            return this;
        }

        /// <inheritdoc />
        public DateTime To => Max.Location;

        /// <inheritdoc />
        public DateTime Earliest => Min.Location;
    }
}
