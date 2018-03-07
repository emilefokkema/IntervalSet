using System;
using System.Collections.Generic;
using IntervalSet.Interval;
using IntervalSet.Interval.Boundaries;

namespace PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time with a start date and <c>(DateTime?)null</c> as end date (i.e. no end date)
    /// </summary>
    public class StartingOpenPeriod : SingleBoundaryInterval<OpenPeriodSet, OpenPeriodListBuilder, IOpenPeriod,DateTime>, IOpenPeriod
    {
        /// <inheritdoc />
        public StartingOpenPeriod(Start<DateTime> from) : base(from)
        {
        }

        /// <inheritdoc />
        protected override IOpenPeriod GetInterval()
        {
            return this;
        }

        protected override OpenPeriodSet MakeSet(IList<IOpenPeriod> intervals)
        {
            return new OpenPeriodSet(intervals);
        }

        /// <summary>
        /// Positive infitity represented as <c>(DateTime?)null</c>
        /// </summary>
        public DateTime? To => null;

        /// <inheritdoc />
        public DateTime Earliest => Boundary.Location;
    }
}
