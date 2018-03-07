using System;
using System.Collections.Generic;
using IntervalSet;

namespace PeriodSet.Period
{
    /// <summary>
    /// The entire <see cref="DateTime"/> space, represented as an <see cref="IBoundedPeriod"/> with <see cref="DateTime.MinValue"/> as start date and <see cref="DateTime.MaxValue"/> as end date
    /// </summary>
    public class EntireBoundedPeriod : IntervalSet<BoundedPeriodSetBuilder, BoundedPeriodSet, BoundedPeriodListBuilder, IBoundedPeriod, DateTime>, IBoundedPeriod
    {

        /// <inheritdoc />
        public DateTime Earliest => DateTime.MinValue;

        /// <inheritdoc />
        public DateTime To => DateTime.MaxValue;
    }
}
