using System;
using IntervalSet.Interval.Boundaries;

namespace PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time for which the start and end dates are the same
    /// </summary>
    public class DegenerateBoundedPeriod : SingleBoundaryPeriod<BoundedPeriodSet, BoundedPeriodListBuilder, IStartingBoundedPeriod, IBoundedPeriod>, IBoundedPeriod
    {
        /// <summary>
        /// Initializes a new <see cref="DegenerateBoundedPeriod"/> based on a degenerate <see cref="Boundary{T}"/>
        /// </summary>
        /// <param name="boundary"></param>
        public DegenerateBoundedPeriod(Degenerate<DateTime> boundary):base(boundary)
        {
        }

        /// <inheritdoc />
        protected override IBoundedPeriod GetPeriod()
        {
            return this;
        }

        /// <inheritdoc />
        public DateTime Earliest => Boundary.Location;

        /// <inheritdoc />
        public DateTime To => Boundary.Location;
    }
}
