using System;
using IntervalSet.PeriodSet.Period.Boundaries;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time with a start date and <c>(DateTime?)null</c> as end date (i.e. no end date)
    /// </summary>
    public class StartingOpenPeriod : SingleBoundaryPeriod<OpenPeriodSet, OpenPeriodListBuilder, IStartingOpenPeriod, IOpenPeriod>, IStartingOpenPeriod
    {
        private Start _start;
        /// <inheritdoc />
        public StartingOpenPeriod(Start from) : base(from)
        {
            _start = from;
        }

        /// <inheritdoc />
        protected override IOpenPeriod GetPeriod()
        {
            return this;
        }

        /// <inheritdoc />
        public IOpenPeriod End(End end)
        {
            return new StartEndingOpenPeriod(_start, end);
        }

        /// <summary>
        /// Positive infitity represented as <c>(DateTime?)null</c>
        /// </summary>
        public DateTime? To => null;

        /// <inheritdoc />
        public DateTime Earliest => Boundary.Date;
    }
}
