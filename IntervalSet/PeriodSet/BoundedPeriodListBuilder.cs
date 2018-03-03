using System.Collections.Generic;
using IntervalSet.PeriodSet.Period;
using IntervalSet.PeriodSet.Period.Boundaries;

namespace IntervalSet.PeriodSet
{
    /// <inheritdoc />
    public class BoundedPeriodListBuilder : Builder<BoundedPeriodSet, IBoundedPeriod, IStartingBoundedPeriod>
    {
        /// <inheritdoc />
        public override IStartingBoundedPeriod MakeStartingPeriod(Start from)
        {
            return new StartingBoundedPeriod(from);
        }

        /// <inheritdoc />
        public override IStartingBoundedPeriod MakeStartingPeriod()
        {
            return new EntireBoundedPeriod();
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
