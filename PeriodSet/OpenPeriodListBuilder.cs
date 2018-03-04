using System;
using System.Collections.Generic;
using IntervalSet;
using IntervalSet.Interval.Boundaries;
using PeriodSet.Period;

namespace PeriodSet
{
    /// <inheritdoc />
    public class OpenPeriodListBuilder : Builder<OpenPeriodSet, IOpenPeriod, IStartingOpenPeriod, DateTime>
    {

        /// <inheritdoc />
        public override IStartingOpenPeriod MakeStartingInterval(Start<DateTime> from)
        {
            return new StartingOpenPeriod(from);
        }

        /// <inheritdoc />
        public override IStartingOpenPeriod MakeStartingInterval()
        {
            return new EntireOpenPeriod();
        }

        /// <inheritdoc />
        public override IOpenPeriod MakeDegenerate(Degenerate<DateTime> degenerate)
        {
            return new DegenerateOpenPeriod(degenerate);
        }

        /// <inheritdoc />
        public override OpenPeriodSet MakeSet(IList<IOpenPeriod> periods)
        {
            return new OpenPeriodSet(periods);
        }

        /// <inheritdoc />
        public override IOpenPeriod MakeNonEmptySet(IList<IOpenPeriod> list)
        {
            return new NonEmptyOpenPeriodSet(list);
        }
    }
}
