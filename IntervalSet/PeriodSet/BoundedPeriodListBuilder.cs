using System;

namespace IntervalSet.PeriodSet
{
    /// <inheritdoc />
    public class BoundedPeriodListBuilder : PeriodListBuilder<IBoundedPeriod>
    {
        /// <inheritdoc />
        protected override IBoundedPeriod MakePeriod(DateTime from)
        {
            return new Period(from);
        }

        /// <inheritdoc />
        protected override IBoundedPeriod MakePeriod(DateTime from, DateTime to)
        {
            return new Period(from, to);
        }
    }
}
