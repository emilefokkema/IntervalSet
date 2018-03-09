using System;
using System.Collections.Generic;
using System.Globalization;
using IntervalSet;
using IntervalSet.Default;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Boundaries.Kind;
using IntervalSet.Interval.Default;

namespace PeriodSet
{
    /// <summary>
    /// A <see cref="BoundedPeriodSet"/> that contains at least one <see cref="IDefaultInterval{T}"/>
    /// </summary>
    public class NonEmptyBoundedPeriodSet : DefaultNonEmptyIntervalSet<BoundedPeriodSet, BoundedPeriodListBuilder, DateTime>
    {
        /// <inheritdoc />
        public NonEmptyBoundedPeriodSet(IIntervalSet<DateTime> set):base(set)
        {
        }

        /// <inheritdoc />
        public NonEmptyBoundedPeriodSet(IList<IDefaultInterval<DateTime>> list):base(list)
        {
        }

        /// <inheritdoc />
        public NonEmptyBoundedPeriodSet(DateTime from, DateTime? to = null):base(MakePeriod(from, to))
        {
        }

        private static IDefaultInterval<DateTime> MakePeriod(DateTime from, DateTime? to)
        {
            Start<DateTime> start = new Start<DateTime>(from, Inclusivity.Inclusive);
            if (to.HasValue)
            {
                if (to.Value == from)
                {
                    return new DefaultDegenerateInterval<DateTime>(new Degenerate<DateTime>(from));
                }
                return new DefaultStartEndingInterval<DateTime>(start, new End<DateTime>(to.Value, Inclusivity.Exclusive));
            }
            return new DefaultStartingInterval<DateTime>(start);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return ToString("d", CultureInfo.CurrentCulture);
        }
    }
}
