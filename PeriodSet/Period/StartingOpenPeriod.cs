using System;
using IntervalSet.Interval.Boundaries;

namespace PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time with a start date and <c>(DateTime?)null</c> as end date (i.e. no end date)
    /// </summary>
    public class StartingOpenPeriod : SingleBoundaryPeriod<OpenPeriodSet, OpenPeriodListBuilder, IStartingOpenPeriod, IOpenPeriod>, IStartingOpenPeriod
    {
        private Start<DateTime> _start;
        /// <inheritdoc />
        public StartingOpenPeriod(Start<DateTime> from) : base(from)
        {
            _start = from;
        }

        /// <inheritdoc />
        protected override IOpenPeriod GetPeriod()
        {
            return this;
        }

        /// <inheritdoc />
        public IOpenPeriod End(End<DateTime> end)
        {
            return new StartEndingOpenPeriod(_start, end);
        }

        /// <summary>
        /// Positive infitity represented as <c>(DateTime?)null</c>
        /// </summary>
        public DateTime? To => null;

        /// <inheritdoc />
        public DateTime Earliest => Boundary.Location;
    }
}
