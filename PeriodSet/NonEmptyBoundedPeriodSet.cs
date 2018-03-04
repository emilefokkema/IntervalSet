using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Boundaries.Kind;
using PeriodSet.Period;

namespace PeriodSet
{
    /// <summary>
    /// A <see cref="BoundedPeriodSet"/> that contains at least one <see cref="IBoundedPeriod"/>
    /// </summary>
    public class NonEmptyBoundedPeriodSet : NonEmptyPeriodSet<BoundedPeriodSet, BoundedPeriodListBuilder, IStartingBoundedPeriod,IBoundedPeriod>, IBoundedPeriod
    {
        /// <inheritdoc />
        public NonEmptyBoundedPeriodSet(IPeriodSet set):base(set)
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
                    return new BoundedPeriodListBuilder().MakeDegenerate(new Degenerate<DateTime>(from));
                }
                return new BoundedPeriodListBuilder().MakeStartingPeriod(start).End(new End<DateTime>(to.Value, Inclusivity.Exclusive));
            }
            return new BoundedPeriodListBuilder().MakeStartingPeriod(start);
        }

        /// <summary>
        /// The end date of the last <see cref="IBoundedPeriod"/> in this <see cref="BoundedPeriodSet"/>
        /// </summary>
        public DateTime To => PeriodList.Last().To;

        /// <inheritdoc />
        public DateTime Earliest => PeriodList.First().Earliest;

        /// <inheritdoc />
        public override string ToString()
        {
            return ToString("d", CultureInfo.CurrentCulture);
        }
    }
}
