using System;
using IntervalSet.PeriodSet.Period.Boundaries;

namespace IntervalSet.PeriodSet.Period
{
    public class EndingBoundedPeriod : SingleBoundedPeriod<BoundedPeriodSet, BoundedPeriodListBuilder, IStartingBoundedPeriod, IBoundedPeriod>, IBoundedPeriod
    {
        public EndingBoundedPeriod(End end):base(end)
        {
        }

        public DateTime To => Boundary.Date;
        public DateTime Earliest => DateTime.MinValue;

        /// <inheritdoc />
        protected override IBoundedPeriod GetPeriod()
        {
            return this;
        }
    }
}
