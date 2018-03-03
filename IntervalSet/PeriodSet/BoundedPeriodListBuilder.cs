using System.Collections.Generic;
using IntervalSet.PeriodSet.Period;
using IntervalSet.PeriodSet.Period.Boundaries;

namespace IntervalSet.PeriodSet
{
    /// <inheritdoc />
    public class BoundedPeriodListBuilder : Builder<BoundedPeriodSet, IBoundedPeriod, StartingBoundedPeriod>
    {
        /// <inheritdoc />
        public override StartingBoundedPeriod MakeStartingPeriod(Start from)
        {
            return new StartingBoundedPeriod(from);
        }

        /// <inheritdoc />
        public override IBoundedPeriod MakeDegenerate(Degenerate degenerate)
        {
            return new DegenerateBoundedPeriod(degenerate);
        }

        /// <inheritdoc />
        public override BoundedPeriodSet MakeSet(IList<IBoundedPeriod> periods)
        {
            return new BoundedPeriodSet(periods);
        }

        /// <inheritdoc />
        public override IBoundedPeriod MakeNonEmptySet(IList<IBoundedPeriod> list)
        {
            return new NonEmptyBoundedPeriodSet(list);
        }
    }
}
