using System;
using IntervalSet.Interval;
using IntervalSet.Interval.Boundaries;

namespace PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time with an end date and <see cref="DateTime.MinValue"/> as start date (i.e. no start date)
    /// </summary>
    public class EndingBoundedPeriod : SingleBoundaryInterval<BoundedPeriodSet, BoundedPeriodListBuilder, IBoundedPeriod, DateTime>, IBoundedPeriod
    {
        /// <summary>
        /// Initializes a new <see cref="EndingBoundedPeriod"/> based on a given <see cref="End{T}"/>
        /// </summary>
        /// <param name="end"></param>
        public EndingBoundedPeriod(End<DateTime> end):base(end)
        {
        }

        /// <inheritdoc />
        public DateTime To => Boundary.Location;

        /// <inheritdoc />
        public DateTime Earliest => DateTime.MinValue;

        /// <inheritdoc />
        protected override IBoundedPeriod GetInterval()
        {
            return this;
        }
    }
}
