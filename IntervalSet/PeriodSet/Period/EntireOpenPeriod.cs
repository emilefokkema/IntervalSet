using System;
using IntervalSet.PeriodSet.Period.Boundaries;

namespace IntervalSet.PeriodSet.Period
{
    public class EntireOpenPeriod : PeriodSet<OpenPeriodSet, OpenPeriodListBuilder, IStartingOpenPeriod, IOpenPeriod>, IStartingOpenPeriod
    {
        public override bool ContainsNegativeInfinity()
        {
            return true;
        }

        public DateTime Earliest => DateTime.MinValue;
        public DateTime? To => null;


        public IOpenPeriod End(End end)
        {
            return new EndingOpenPeriod(end);
        }
    }
}
