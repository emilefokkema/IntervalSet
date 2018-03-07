using System;
using System.Collections.Generic;
using IntervalSet;

namespace PeriodSet.Period
{
    /// <summary>
    /// The entire <see cref="DateTime"/> space, represented as an <see cref="IOpenPeriod"/> with <see cref="DateTime.MinValue"/> as start date and <c>(DateTime?)null</c> as end date
    /// </summary>
    public class EntireOpenPeriod : IntervalSet<OpenPeriodSet, OpenPeriodListBuilder, IOpenPeriod,DateTime>, IOpenPeriod
    {
        protected override OpenPeriodSet MakeSet(IList<IOpenPeriod> intervals)
        {
            return new OpenPeriodSet(intervals);
        }

        /// <inheritdoc />
        public DateTime Earliest => DateTime.MinValue;

        /// <inheritdoc />
        public DateTime? To => null;
    }
}
