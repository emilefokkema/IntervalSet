using System;
using System.Collections.Generic;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// Represents a period of time with a start date and <c>(DateTime?)null</c> as end date (i.e. no end date)
    /// </summary>
    public class StartingOpenPeriod : StartingPeriod<OpenPeriodSet, OpenPeriodListBuilder, IOpenPeriod>, IOpenPeriod
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
