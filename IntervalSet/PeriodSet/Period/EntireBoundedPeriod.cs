using System;
using IntervalSet.PeriodSet.Period.Boundaries;

namespace IntervalSet.PeriodSet.Period
{
    public class EntireBoundedPeriod : PeriodSet<BoundedPeriodSet, BoundedPeriodListBuilder, IStartingBoundedPeriod, IBoundedPeriod>, IStartingBoundedPeriod
    {
        public override bool ContainsNegativeInfinity()
        {
            return true;
        }

        public DateTime Earliest => DateTime.MinValue;
        public DateTime To => DateTime.MaxValue;


        public IBoundedPeriod End(End end)
        {
            return new EndingBoundedPeriod(end);
        }
    }
}
