using System;
using System.Collections.Generic;

namespace IntervalSet.PeriodSet.Period
{
    public class StartingBoundedPeriod : StartingPeriod<BoundedPeriodSet, BoundedPeriodListBuilder, IBoundedPeriod>, IBoundedPeriod
    {
        public StartingBoundedPeriod(DateTime from):base(from)
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

        public DateTime To => DateTime.MaxValue;
    }
}
