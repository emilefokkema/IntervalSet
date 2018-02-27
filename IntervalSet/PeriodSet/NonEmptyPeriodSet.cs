using System;
using System.Collections.Generic;
using System.Linq;
using IntervalSet.PeriodSet.Period;

namespace IntervalSet.PeriodSet
{
    /// <summary>
    /// A <see cref="MultiplePeriodSet{TSet,TNonEmptySet,TListBuilder,TPeriod}"/> that contains at least one <typeparamref name="TPeriod"/>
    /// </summary>
    /// <typeparam name="TSet"></typeparam>
    /// <typeparam name="TNonEmptySet"></typeparam>
    /// <typeparam name="TPeriod"></typeparam>
    /// <typeparam name="TListBuilder"></typeparam>
    public abstract class NonEmptyPeriodSet<TSet, TNonEmptySet, TListBuilder, TStartingPeriod, TPeriod> : MultiplePeriodSet<TSet, TNonEmptySet, TListBuilder, TStartingPeriod, TPeriod>
        where TSet : IPeriodSet
        where TListBuilder : IPeriodListBuilder<TPeriod, TStartingPeriod>, new()
        where TPeriod : IPeriodSet
        where TStartingPeriod : TPeriod, IStartingPeriod<TPeriod>
    {
        /// <inheritdoc/>
        protected NonEmptyPeriodSet(IList<TPeriod> list):base(list)
        {
            CheckNonEmpty();
        }

        /// <inheritdoc />
        protected NonEmptyPeriodSet(IPeriodSet set):base(set)
        {
            CheckNonEmpty();
        }

        private void CheckNonEmpty()
        {
            if (!PeriodList.Any())
            {
                throw new InvalidCastException($"cannot initialize a non-empty period with an empty period");
            }
        }

        /// <inheritdoc />
        public override bool IsEmpty => false;
    }
}
