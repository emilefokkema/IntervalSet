using System;
using IntervalSet.PeriodSet.Period.Boundaries;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time for which the start and end dates are the same
    /// </summary>
    public class DegenerateOpenPeriod : SingleBoundaryPeriod<OpenPeriodSet, OpenPeriodListBuilder, IStartingOpenPeriod, IOpenPeriod>, IOpenPeriod
    {
        /// <summary>
        /// Initializes a new <see cref="DegenerateOpenPeriod"/> based on a degenerate <see cref="Boundary"/>
        /// </summary>
        /// <param name="boundary"></param>
        public DegenerateOpenPeriod(Degenerate boundary):base(boundary)
        {
        }

        /// <inheritdoc />
        protected override IOpenPeriod GetPeriod()
        {
            return this;
        }

        /// <inheritdoc />
        public DateTime? To => Boundary.Date;

        /// <inheritdoc />
        public DateTime Earliest => Boundary.Date;
    }
}
