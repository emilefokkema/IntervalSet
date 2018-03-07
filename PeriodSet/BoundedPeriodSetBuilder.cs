using System;
using System.Collections.Generic;
using IntervalSet;
using PeriodSet.Period;

namespace PeriodSet
{
    public class BoundedPeriodSetBuilder : ISetBuilder<BoundedPeriodSet, IBoundedPeriod, DateTime>
    {
        public BoundedPeriodSet MakeSet(IList<IBoundedPeriod> intervals)
        {
            return new BoundedPeriodSet(intervals);
        }

        public IBoundedPeriod MakeNonEmptySet(IList<IBoundedPeriod> intervals)
        {
            return new NonEmptyBoundedPeriodSet(intervals);
        }
    }
}
