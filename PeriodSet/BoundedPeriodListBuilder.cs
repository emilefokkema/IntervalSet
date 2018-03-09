using System;
using System.Collections.Generic;
using IntervalSet.Default;
using IntervalSet.Interval.Default;

namespace PeriodSet
{
    /// <inheritdoc />
    public class BoundedPeriodListBuilder : AbstractDefaultBuilder<BoundedPeriodSet, BoundedPeriodListBuilder, DateTime>
    {
        public override BoundedPeriodSet MakeSet(IList<IDefaultInterval<DateTime>> intervals)
        {
            return new BoundedPeriodSet(intervals);
        }

        public override IDefaultInterval<DateTime> MakeNonEmptySet(IList<IDefaultInterval<DateTime>> intervals)
        {
            return new NonEmptyBoundedPeriodSet(intervals);
        }

        /// <inheritdoc />
        protected override DateTime PositiveInfinity => DateTime.MaxValue;

        /// <inheritdoc />
        protected override DateTime NegativeInfinity => DateTime.MinValue;
    }
}
