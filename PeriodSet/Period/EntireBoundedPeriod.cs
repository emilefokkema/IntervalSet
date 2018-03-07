using System;
using System.Collections.Generic;
using IntervalSet;

namespace PeriodSet.Period
{
    /// <summary>
    /// The entire <see cref="DateTime"/> space, represented as an <see cref="IBoundedPeriod"/> with <see cref="DateTime.MinValue"/> as start date and <see cref="DateTime.MaxValue"/> as end date
    /// </summary>
    public class EntireBoundedPeriod : IntervalSet<BoundedPeriodSet, BoundedPeriodListBuilder, IBoundedPeriod, DateTime>, IBoundedPeriod
    {
        protected override BoundedPeriodSet MakeSet(IList<IBoundedPeriod> intervals)
        {
            return new BoundedPeriodSet(intervals);
        }

        /// <inheritdoc />
        public DateTime Earliest => DateTime.MinValue;

        /// <inheritdoc />
        public DateTime To => DateTime.MaxValue;
    }
}
