using System;
using System.Collections.Generic;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time with a start date and <c>(DateTime?)null</c> as end date (i.e. no end date)
    /// </summary>
    public class StartingOpenPeriod : StartingPeriod<OpenPeriodSet, OpenPeriodListBuilder, IOpenPeriod>, IOpenPeriod, IStartingPeriod<IOpenPeriod>
    {
        /// <inheritdoc />
        public StartingOpenPeriod(DateTime from) : base(from)
        {
        }

        /// <inheritdoc />
        protected override IOpenPeriod GetPeriod()
        {
            return this;
        }

        /// <summary>
        /// Creates an <see cref="IOpenPeriod"/> that represents the result of ending this <see cref="StartingOpenPeriod"/>
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IOpenPeriod End(DateTime date)
        {
            return new StartEndingOpenPeriod(Earliest, date);
        }

        /// <inheritdoc />
        protected override OpenPeriodSet MakeSet(IList<IOpenPeriod> list)
        {
            return new OpenPeriodSet(list);
        }

        /// <summary>
        /// Positive infitity represented as <c>(DateTime?)null</c>
        /// </summary>
        public DateTime? To => null;
    }
}
