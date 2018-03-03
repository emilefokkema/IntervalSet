using System;
using IntervalSet.PeriodSet.Period.Boundaries;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time with a start date and <see cref="DateTime.MaxValue"/> as end date (i.e. no end date)
    /// </summary>
    public class StartingBoundedPeriod : SingleBoundedPeriod<BoundedPeriodSet, BoundedPeriodListBuilder, StartingBoundedPeriod, IBoundedPeriod>, IBoundedPeriod, IStartingPeriod<IBoundedPeriod>
    {
        /// <inheritdoc />
        public StartingBoundedPeriod(Start from):base(from)
        {
        }

        /// <inheritdoc />
        protected override IBoundedPeriod GetPeriod()
        {
            return this;
        }

        /// <inheritdoc />
        public IBoundedPeriod End(End end)
        {
            return new StartEndingBoundedPeriod(Boundary, end);
        }

        /// <inheritdoc />
        /// <summary>
        /// Positive infinity represted as a <see cref="DateTime"/>
        /// </summary>
        public DateTime To => DateTime.MaxValue;

        /// <inheritdoc />
        public DateTime Earliest => Boundary.Date;
    }
}
