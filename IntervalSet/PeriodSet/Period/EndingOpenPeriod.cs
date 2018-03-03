using System;
using IntervalSet.PeriodSet.Period.Boundaries;

namespace IntervalSet.PeriodSet.Period
{
    public class EndingOpenPeriod : SingleBoundedPeriod<OpenPeriodSet, OpenPeriodListBuilder, IStartingOpenPeriod, IOpenPeriod>, IOpenPeriod
    {
        public EndingOpenPeriod(End end):base(end)
        {
        }

        public DateTime Earliest => DateTime.MinValue;
        public DateTime? To => Boundary.Date;

        /// <inheritdoc />
        protected override IOpenPeriod GetPeriod()
        {
            return this;
        }
    }
}
