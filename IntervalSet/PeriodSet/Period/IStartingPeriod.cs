using System;

namespace IntervalSet.PeriodSet.Period
{
    public interface IStartingPeriod<TEndingPeriod>
    {
        TEndingPeriod End(DateTime end);
    }
}
