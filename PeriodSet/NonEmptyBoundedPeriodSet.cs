using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using IntervalSet;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Boundaries.Kind;
using PeriodSet.Period;

namespace PeriodSet
{
    /// <summary>
    /// A <see cref="BoundedPeriodSet"/> that contains at least one <see cref="IBoundedPeriod"/>
    /// </summary>
    public class NonEmptyBoundedPeriodSet : NonEmptyIntervalSet<BoundedPeriodSetBuilder, BoundedPeriodSet, BoundedPeriodListBuilder,IBoundedPeriod,DateTime>, IBoundedPeriod
    {
        /// <inheritdoc />
        public NonEmptyBoundedPeriodSet(IIntervalSet<DateTime> set):base(set)
        {
        }

        /// <inheritdoc />
        public NonEmptyBoundedPeriodSet(IList<IBoundedPeriod> list):base(list)
        {
        }

        /// <inheritdoc />
        public NonEmptyBoundedPeriodSet(DateTime from, DateTime? to = null):base(MakePeriod(from, to))
        {
        }

        private static IBoundedPeriod MakePeriod(DateTime from, DateTime? to)
        {
            Start<DateTime> start = new Start<DateTime>(from, Inclusivity.Inclusive);
            if (to.HasValue)
            {
                if (to.Value == from)
                {
                    return new DegenerateBoundedPeriod(new Degenerate<DateTime>(from));
                }
                return new StartEndingBoundedPeriod(start, new End<DateTime>(to.Value, Inclusivity.Exclusive));
            }
            return new StartingBoundedPeriod(start);
        }

        /// <summary>
        /// The end date of the last <see cref="IBoundedPeriod"/> in this <see cref="BoundedPeriodSet"/>
        /// </summary>
        public DateTime To => IntervalList.Last().To;

        /// <inheritdoc />
        public DateTime Earliest => IntervalList.First().Earliest;

        /// <inheritdoc />
        public override string ToString()
        {
            return ToString("d", CultureInfo.CurrentCulture);
        }
    }
}
