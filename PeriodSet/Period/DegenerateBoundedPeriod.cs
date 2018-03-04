using System;
using PeriodSet.Period.Boundaries;

namespace PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time for which the start and end dates are the same
    /// </summary>
    public class DegenerateBoundedPeriod : SingleBoundaryPeriod<BoundedPeriodSet, BoundedPeriodListBuilder, IStartingBoundedPeriod, IBoundedPeriod>, IBoundedPeriod
    {
        /// <summary>
        /// Initializes a new <see cref="DegenerateBoundedPeriod"/> based on a degenerate <see cref="Boundary"/>
        /// </summary>
        /// <param name="boundary"></param>
        public DegenerateBoundedPeriod(Degenerate boundary):base(boundary)
        {
        }

        /// <inheritdoc />
        protected override IBoundedPeriod GetPeriod()
        {
            return this;
        }

        /// <inheritdoc />
        public DateTime Earliest => Boundary.Date;

        /// <inheritdoc />
        public DateTime To => Boundary.Date;
    }
}
