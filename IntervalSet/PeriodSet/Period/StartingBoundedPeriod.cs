using System;
using System.Collections.Generic;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time with a start date and <see cref="DateTime.MaxValue"/> as end date (i.e. no end date)
    /// </summary>
    public class StartingBoundedPeriod : StartingPeriod<BoundedPeriodSet, BoundedPeriodListBuilder, IBoundedPeriod>, IBoundedPeriod, IStartingPeriod<IBoundedPeriod>
    {
        /// <inheritdoc />
        public StartingBoundedPeriod(DateTime from):base(from)
        {
        }

        /// <inheritdoc />
        protected override IBoundedPeriod GetPeriod()
        {
            return this;
        }

        /// <summary>
        /// Creates an <see cref="IBoundedPeriod"/> that represents the result of ending this <see cref="StartingBoundedPeriod"/>
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IBoundedPeriod End(DateTime date)
        {
            return new StartEndingBoundedPeriod(Earliest, date);
        }

        /// <inheritdoc />
        protected override BoundedPeriodSet MakeSet(IList<IBoundedPeriod> list)
        {
            return new BoundedPeriodSet(list);
        }

        /// <inheritdoc />
        /// <summary>
        /// Positive infinity represted as a <see cref="DateTime"/>
        /// </summary>
        public DateTime To => DateTime.MaxValue;
    }
}
