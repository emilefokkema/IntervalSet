﻿using System;
using System.Collections.Generic;
using IntervalSet.Interval.Boundaries;
using PeriodSet.Period;

namespace PeriodSet
{
    /// <inheritdoc />
    public class BoundedPeriodListBuilder : Builder<BoundedPeriodSet, IBoundedPeriod, IStartingBoundedPeriod>
    {
        /// <inheritdoc />
        public override IStartingBoundedPeriod MakeStartingPeriod(Start<DateTime> from)
        {
            return new StartingBoundedPeriod(from);
        }

        /// <inheritdoc />
        public override IStartingBoundedPeriod MakeStartingPeriod()
        {
            return new EntireBoundedPeriod();
        }

        /// <inheritdoc />
        public override IBoundedPeriod MakeDegenerate(Degenerate<DateTime> degenerate)
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
