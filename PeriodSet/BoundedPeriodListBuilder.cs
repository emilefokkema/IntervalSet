using System;
using System.Collections.Generic;
using IntervalSet.Default;
using IntervalSet.Interval.Default;

namespace PeriodSet
{
    /// <inheritdoc />
    public class BoundedPeriodListBuilder : DefaultBuilder<BoundedPeriodSet, BoundedPeriodListBuilder, DateTime>
    {
        /// <inheritdoc />
        public override BoundedPeriodSet MakeSet(IList<IDefaultInterval<DateTime>> intervals)
        {
            return new BoundedPeriodSet(intervals);
        }

        /// <inheritdoc />
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
