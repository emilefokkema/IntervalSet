using System;
using System.Collections.Generic;
using System.Linq;
using IntervalSet.PeriodSet.Period;
using IntervalSet.PeriodSet.Period.Boundaries;
using IntervalSet.PeriodSet.Period.Boundaries.Kind;

namespace IntervalSet.PeriodSet
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
            Start start = new Start(from, Inclusivity.Inclusive);
            if (to.HasValue)
            {
                return new BoundedPeriodListBuilder().MakeStartingPeriod(start).End(new End(to.Value, Inclusivity.Exclusive));
            }
            return new BoundedPeriodListBuilder().MakeStartingPeriod(start);
        }

        /// <summary>
        /// The end date of the last <see cref="IBoundedPeriod"/> in this <see cref="BoundedPeriodSet"/>
        /// </summary>
        public DateTime To => PeriodList.Last().To;

        /// <inheritdoc />
        public DateTime Earliest => PeriodList.First().Earliest;
    }
}
