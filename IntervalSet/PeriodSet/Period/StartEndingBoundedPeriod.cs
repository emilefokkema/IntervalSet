using System;
using System.Collections.Generic;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time with a start date and an end date
    /// </summary>
    public class StartEndingBoundedPeriod : StartEndingPeriod<BoundedPeriodSet, BoundedPeriodListBuilder, IBoundedPeriod>, IBoundedPeriod
    {
        /// <inheritdoc />
        public StartEndingBoundedPeriod(DateTime from, DateTime to):base(from, to)
        {
        }

        /// <inheritdoc />
        protected override IBoundedPeriod GetPeriod()
        {
            return this;
        }

        /// <inheritdoc />
        protected override BoundedPeriodSet MakeSet(IList<IBoundedPeriod> list)
        {
            return new BoundedPeriodSet(list);
        }

        /// <inheritdoc />
        public DateTime To => Latest;
    }
}
