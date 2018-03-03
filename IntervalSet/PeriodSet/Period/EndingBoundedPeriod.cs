using System;
using IntervalSet.PeriodSet.Period.Boundaries;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time with an end date and <see cref="DateTime.MinValue"/> as start date (i.e. no start date)
    /// </summary>
    public class EndingBoundedPeriod : SingleBoundaryPeriod<BoundedPeriodSet, BoundedPeriodListBuilder, IStartingBoundedPeriod, IBoundedPeriod>, IBoundedPeriod
    {
        /// <summary>
        /// Initializes a new <see cref="EndingBoundedPeriod"/> based on a given <see cref="End"/>
        /// </summary>
        /// <param name="end"></param>
        public EndingBoundedPeriod(End end):base(end)
        {
        }

        /// <inheritdoc />
        public DateTime To => Boundary.Date;

        /// <inheritdoc />
        public DateTime Earliest => DateTime.MinValue;

        /// <inheritdoc />
        protected override IBoundedPeriod GetPeriod()
        {
            return this;
        }
    }
}
