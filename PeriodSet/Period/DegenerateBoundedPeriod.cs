using System;
using System.Collections.Generic;
using IntervalSet.Interval;
using IntervalSet.Interval.Boundaries;

namespace PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time for which the start and end dates are the same
    /// </summary>
    public class DegenerateBoundedPeriod : SingleBoundaryInterval<BoundedPeriodSet, BoundedPeriodListBuilder, IBoundedPeriod, DateTime>, IBoundedPeriod
    {
        /// <summary>
        /// Initializes a new <see cref="DegenerateBoundedPeriod"/> based on a degenerate <see cref="Boundary{T}"/>
        /// </summary>
        /// <param name="boundary"></param>
        public DegenerateBoundedPeriod(Degenerate<DateTime> boundary):base(boundary)
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
        public DateTime Earliest => Boundary.Location;

        /// <inheritdoc />
        public DateTime To => Boundary.Location;
    }
}
