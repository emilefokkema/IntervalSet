using System;
using IntervalSet.PeriodSet.Period.Boundaries;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// The entire <see cref="DateTime"/> space, represented as an <see cref="IOpenPeriod"/> with <see cref="DateTime.MinValue"/> as start date and <c>(DateTime?)null</c> as end date
    /// </summary>
    public class EntireOpenPeriod : PeriodSet<OpenPeriodSet, OpenPeriodListBuilder, IStartingOpenPeriod, IOpenPeriod>, IStartingOpenPeriod
    {
        /// <inheritdoc />
        public override bool ContainsNegativeInfinity()
        {
            return true;
        }

        /// <inheritdoc />
        public DateTime Earliest => DateTime.MinValue;

        /// <inheritdoc />
        public DateTime? To => null;

        /// <inheritdoc />
        public IOpenPeriod End(End end)
        {
            return new EndingOpenPeriod(end);
        }
    }
}
