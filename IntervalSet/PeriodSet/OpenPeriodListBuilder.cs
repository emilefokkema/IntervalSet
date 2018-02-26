using System;
using IntervalSet.PeriodSet.Period;

namespace IntervalSet.PeriodSet
{
    /// <inheritdoc />
    public class OpenPeriodListBuilder : PeriodListBuilder<IOpenPeriod, StartingOpenPeriod>
    {
        protected override StartingOpenPeriod MakeStartingPeriod(DateTime from)
        {
            return new StartingOpenPeriod(from);
        }
    }
}
