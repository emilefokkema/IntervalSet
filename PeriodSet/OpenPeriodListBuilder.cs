using System;
using System.Collections.Generic;
using IntervalSet;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Boundaries.Kind;
using PeriodSet.Period;

namespace PeriodSet
{
    /// <inheritdoc cref="Builder{TSet,TInterval,T}"/>
    public class OpenPeriodListBuilder : Builder<OpenPeriodSet, IOpenPeriod, DateTime>
    {
        /// <inheritdoc />
        public override OpenPeriodSet MakeSet(IList<IOpenPeriod> intervals)
        {
            return new OpenPeriodSet(intervals);
        }

        /// <inheritdoc />
        public override IOpenPeriod MakeNonEmptySet(IList<IOpenPeriod> intervals)
        {
            return new NonEmptyOpenPeriodSet(intervals);
        }
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
        public override Start<DateTime> MakeStart(DateTime @from)
        {
            return new Start<DateTime>(from, Inclusivity.Inclusive);
        }

        /// <inheritdoc />
        public override End<DateTime> MakeEnd(DateTime to)
        {
            return new End<DateTime>(to, Inclusivity.Exclusive);
        }

        /// <inheritdoc />
        protected override DateTime PositiveInfinity => DateTime.MaxValue;

        /// <inheritdoc />
        protected override DateTime NegativeInfinity => DateTime.MinValue;
    }
}
