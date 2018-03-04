using System;
using IntervalSet.Interval;
using IntervalSet.Interval.Boundaries;

namespace PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time for which the start and end dates are the same
    /// </summary>
    public class DegenerateOpenPeriod : SingleBoundaryInterval<OpenPeriodSet, OpenPeriodListBuilder, IStartingOpenPeriod, IOpenPeriod, DateTime>, IOpenPeriod
    {
        /// <summary>
        /// Initializes a new <see cref="DegenerateOpenPeriod"/> based on a degenerate <see cref="Boundary{T}"/>
        /// </summary>
        /// <param name="boundary"></param>
        public DegenerateOpenPeriod(Degenerate<DateTime> boundary):base(boundary)
        {
        }

        /// <inheritdoc />
        protected override IOpenPeriod GetInterval()
        {
            return this;
        }

        /// <inheritdoc />
        public DateTime? To => Boundary.Location;

        /// <inheritdoc />
        public DateTime Earliest => Boundary.Location;
    }
}
