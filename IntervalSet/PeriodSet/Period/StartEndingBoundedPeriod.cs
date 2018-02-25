using System;
using System.Collections.Generic;

namespace IntervalSet.PeriodSet.Period
{
    public class StartEndingBoundedPeriod : StartEndingPeriod<BoundedPeriodSet, BoundedPeriodListBuilder, IBoundedPeriod>, IBoundedPeriod
    {
        public StartEndingBoundedPeriod(DateTime from, DateTime to):base(from, to)
        {
        }

        protected override IBoundedPeriod GetPeriod()
        {
            return this;
        }

        protected override BoundedPeriodSet MakeSet(IList<IBoundedPeriod> list)
        {
            return new BoundedPeriodSet(list);
        }

        public DateTime To => Latest;
    }
}
