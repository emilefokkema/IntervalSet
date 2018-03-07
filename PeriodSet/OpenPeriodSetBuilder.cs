using System;
using System.Collections.Generic;
using IntervalSet;
using PeriodSet.Period;

namespace PeriodSet
{
    public class OpenPeriodSetBuilder : ISetBuilder<OpenPeriodSet, IOpenPeriod, DateTime>
    {
        public OpenPeriodSet MakeSet(IList<IOpenPeriod> intervals)
        {
            return new OpenPeriodSet(intervals);
        }

        public IOpenPeriod MakeNonEmptySet(IList<IOpenPeriod> intervals)
        {
            return new NonEmptyOpenPeriodSet(intervals);
        }
    }
}
