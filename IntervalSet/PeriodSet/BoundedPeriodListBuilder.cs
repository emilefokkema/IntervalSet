using System;
using IntervalSet.PeriodSet.Period;

namespace IntervalSet.PeriodSet
{
    /// <inheritdoc />
    public class BoundedPeriodListBuilder : PeriodListBuilder<IBoundedPeriod>
    {
        /// <inheritdoc />
        public override IBoundedPeriod MakePeriod(DateTime from)
        {
            return new StartingBoundedPeriod(from);
        }

        /// <inheritdoc />
        public override IBoundedPeriod MakePeriod(DateTime from, DateTime to)
        {
            return new StartEndingBoundedPeriod(from, to);
        }
    }
}
