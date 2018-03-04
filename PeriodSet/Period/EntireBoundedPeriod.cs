using System;
using IntervalSet;
using IntervalSet.Interval.Boundaries;

namespace PeriodSet.Period
{
    /// <summary>
    /// The entire <see cref="DateTime"/> space, represented as an <see cref="IBoundedPeriod"/> with <see cref="DateTime.MinValue"/> as start date and <see cref="DateTime.MaxValue"/> as end date
    /// </summary>
    public class EntireBoundedPeriod : IntervalSet<BoundedPeriodSet, BoundedPeriodListBuilder, IStartingBoundedPeriod, IBoundedPeriod, DateTime>, IStartingBoundedPeriod
    {
        /// <inheritdoc />
        public override bool ContainsNegativeInfinity()
        {
            return true;
        }

        /// <inheritdoc />
        public DateTime Earliest => DateTime.MinValue;

        /// <inheritdoc />
        public DateTime To => DateTime.MaxValue;

        /// <inheritdoc />
        public IBoundedPeriod End(End<DateTime> end)
        {
            return new EndingBoundedPeriod(end);
        }
    }
}
