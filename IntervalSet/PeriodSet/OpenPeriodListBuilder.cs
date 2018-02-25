using System;
using IntervalSet.PeriodSet.Period;

namespace IntervalSet.PeriodSet
{
    /// <inheritdoc />
    public class OpenPeriodListBuilder : PeriodListBuilder<IOpenPeriod>
    {
        /// <inheritdoc />
        public override IOpenPeriod MakePeriod(DateTime from)
        {
            return new StartingOpenPeriod(from);
        }

        /// <inheritdoc />
        public override IOpenPeriod MakePeriod(DateTime from, DateTime to)
        {
            return new StartEndingOpenPeriod(from, to);
        }
    }
}
