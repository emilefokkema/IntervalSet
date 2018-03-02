using System;
using System.Collections.Generic;
using IntervalSet.PeriodSet.Period.Boundaries;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time with a start date and <c>(DateTime?)null</c> as end date (i.e. no end date)
    /// </summary>
    public class StartingOpenPeriod : SingleBoundedPeriod<OpenPeriodSet, OpenPeriodListBuilder, StartingOpenPeriod, IOpenPeriod>, IOpenPeriod, IStartingPeriod<IOpenPeriod>
    {
        /// <inheritdoc />
        public StartingOpenPeriod(Boundary boundary) : base(boundary)
        {
        }

        /// <inheritdoc />
        protected override IOpenPeriod GetPeriod()
        {
            return this;
        }

        /// <inheritdoc />
        public IOpenPeriod End(Boundary end)
        {
            return new StartEndingOpenPeriod(Boundary, end);
        }

        /// <inheritdoc />
        protected override OpenPeriodSet MakeSet(IList<IOpenPeriod> list)
        {
            return new OpenPeriodSet(list);
        }

        /// <summary>
        /// Positive infitity represented as <c>(DateTime?)null</c>
        /// </summary>
        public DateTime? To => null;

        /// <inheritdoc />
        public DateTime Earliest => Boundary.Date;
    }
}
