using System;
using IntervalSet.Interval.Boundaries;
using IntervalSet;

namespace PeriodSet.Period
{
    /// <summary>
    /// The entire <see cref="DateTime"/> space, represented as an <see cref="IOpenPeriod"/> with <see cref="DateTime.MinValue"/> as start date and <c>(DateTime?)null</c> as end date
    /// </summary>
    public class EntireOpenPeriod : IntervalSet<OpenPeriodSet, OpenPeriodListBuilder, IOpenPeriod,DateTime>, IOpenPeriod
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
    }
}
