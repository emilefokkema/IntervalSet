using System;
using IntervalSet.PeriodSet.Period.Boundaries;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time with a start date and <see cref="DateTime.MaxValue"/> as end date (i.e. no end date)
    /// </summary>
    public class StartingBoundedPeriod : SingleBoundaryPeriod<BoundedPeriodSet, BoundedPeriodListBuilder, IStartingBoundedPeriod, IBoundedPeriod>, IStartingBoundedPeriod
    {
        private readonly Start _start;

        /// <inheritdoc />
        public StartingBoundedPeriod(Start from):base(from)
        {
            _start = from;
        }

        /// <inheritdoc />
        protected override IBoundedPeriod GetPeriod()
        {
            return this;
        }

        /// <inheritdoc />
        public IBoundedPeriod End(End end)
        {
            return new StartEndingBoundedPeriod(_start, end);
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
