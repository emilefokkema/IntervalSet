using System;
using System.Collections.Generic;
using IntervalSet;
using IntervalSet.Interval.Boundaries;
using PeriodSet.Period;

namespace PeriodSet
{
    /// <inheritdoc />
    public class OpenPeriodListBuilder : Builder<OpenPeriodSet, IOpenPeriod, DateTime>
    {

        /// <inheritdoc />
        public override IOpenPeriod MakeStartingInterval(Start<DateTime> from)
        {
            return new StartingOpenPeriod(from);
        }

        /// <inheritdoc />
        public override IOpenPeriod MakeStartingInterval()
        {
            return new EntireOpenPeriod();
        }

        /// <inheritdoc />
        public override IOpenPeriod MakeDegenerate(Degenerate<DateTime> degenerate)
        {
            return new DegenerateOpenPeriod(degenerate);
        }

        /// <inheritdoc />
        public override IOpenPeriod MakeEndingInterval(End<DateTime> end)
        {
            return new EndingOpenPeriod(end);
        }

        /// <inheritdoc />
        public override IOpenPeriod MakeStartEndingInterval(Start<DateTime> from, End<DateTime> to)
        {
            return new StartEndingOpenPeriod(from, to);
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
