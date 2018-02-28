using System;
using IntervalSet.PeriodSet.Period;
using IntervalSet.PeriodSet.Period.Boundaries;
using IntervalSet.PeriodSet.Period.Boundaries.Kind;

namespace IntervalSet.PeriodSet
{
    /// <inheritdoc />
    public class BoundedPeriodListBuilder : PeriodListBuilder<IBoundedPeriod, StartingBoundedPeriod>
    {
        /// <inheritdoc />
        public override Start MakeStartingBoundary(DateTime from)
        {
            return new Start(from, Inclusivity.Inclusive);
        }

        /// <inheritdoc />
        public override End MakeEndingBoundary(DateTime to)
        {
            return new End(to, Inclusivity.Exclusive);
        }

        /// <inheritdoc />
        public override StartingBoundedPeriod MakeStartingPeriod(Boundary from)
        {
            return new StartingBoundedPeriod(from);
        }

        /// <inheritdoc />
        public override IBoundedPeriod MakeDegenerate(Degenerate degenerate)
        {
            return new StartingBoundedPeriod(degenerate);
        }
    }
}
