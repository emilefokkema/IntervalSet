using System;
using IntervalSet.PeriodSet.Period.Boundaries;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// The entire <see cref="DateTime"/> space, represented as an <see cref="IBoundedPeriod"/> with <see cref="DateTime.MinValue"/> as start date and <see cref="DateTime.MaxValue"/> as end date
    /// </summary>
    public class EntireBoundedPeriod : PeriodSet<BoundedPeriodSet, BoundedPeriodListBuilder, IStartingBoundedPeriod, IBoundedPeriod>, IStartingBoundedPeriod
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
        public IBoundedPeriod End(End end)
        {
            return new EndingBoundedPeriod(end);
        }
    }
}
