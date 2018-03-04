using System;
using IntervalSet.Interval.Boundaries;

namespace PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time with an end date and <see cref="DateTime.MinValue"/> as start date (i.e. no start date)
    /// </summary>
    public class EndingOpenPeriod : SingleBoundaryPeriod<OpenPeriodSet, OpenPeriodListBuilder, IStartingOpenPeriod, IOpenPeriod>, IOpenPeriod
    {
        /// <summary>
        /// Initializes a new <see cref="EndingOpenPeriod"/> based on a given <see cref="End{T}"/>
        /// </summary>
        /// <param name="end"></param>
        public EndingOpenPeriod(End<DateTime> end):base(end)
        {
        }

        /// <inheritdoc />
        public DateTime Earliest => DateTime.MinValue;

        /// <inheritdoc />
        public DateTime? To => Boundary.Location;

        /// <inheritdoc />
        protected override IOpenPeriod GetPeriod()
        {
            return this;
        }
    }
}
