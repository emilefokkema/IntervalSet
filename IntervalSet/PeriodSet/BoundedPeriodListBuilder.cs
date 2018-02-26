using System;
using IntervalSet.PeriodSet.Period;

namespace IntervalSet.PeriodSet
{
    /// <inheritdoc />
    public class BoundedPeriodListBuilder : PeriodListBuilder<IBoundedPeriod, StartingBoundedPeriod>
    {
        protected override StartingBoundedPeriod MakeStartingPeriod(DateTime from)
        {
            return new StartingBoundedPeriod(from);
        }
    }
}
