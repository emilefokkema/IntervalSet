using System;
using IntervalSet.Interval;
using IntervalSet.Interval.Boundaries;

namespace PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time with a start date and <see cref="DateTime.MaxValue"/> as end date (i.e. no end date)
    /// </summary>
    public class StartingBoundedPeriod : SingleBoundaryInterval<BoundedPeriodSet, BoundedPeriodListBuilder, IStartingBoundedPeriod, IBoundedPeriod, DateTime>, IStartingBoundedPeriod
    {
        private readonly Start<DateTime> _start;

        /// <inheritdoc />
        public StartingBoundedPeriod(Start<DateTime> from):base(from)
        {
            _start = from;
        }

        /// <inheritdoc />
        protected override IBoundedPeriod GetInterval()
        {
            return this;
        }

        /// <inheritdoc />
        public IBoundedPeriod MakeEndingInterval(End<DateTime> end)
        {
            return new StartEndingBoundedPeriod(_start, end);
        }

        /// <inheritdoc />
        /// <summary>
        /// Positive infinity represted as a <see cref="DateTime"/>
        /// </summary>
        public DateTime To => DateTime.MaxValue;

        /// <inheritdoc />
        public DateTime Earliest => Boundary.Location;
    }
}
