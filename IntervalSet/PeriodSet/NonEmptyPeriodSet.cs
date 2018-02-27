using System;
using System.Collections.Generic;
using System.Linq;

namespace IntervalSet.PeriodSet
{
    /// <summary>
    /// A <see cref="MultiplePeriodSet{TSet,TNonEmptySet,TListBuilder,TPeriod}"/> that contains at least one <typeparamref name="TPeriod"/>
    /// </summary>
    /// <typeparam name="TSet"></typeparam>
    /// <typeparam name="TNonEmptySet"></typeparam>
    /// <typeparam name="TPeriod"></typeparam>
    /// <typeparam name="TListBuilder"></typeparam>
    public abstract class NonEmptyPeriodSet<TSet, TNonEmptySet, TListBuilder, TPeriod> : MultiplePeriodSet<TSet, TNonEmptySet, TListBuilder, TPeriod>
        where TSet : IPeriodSet
        where TListBuilder : IPeriodListBuilder<TPeriod>, new()
        where TPeriod : IPeriodSet
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
