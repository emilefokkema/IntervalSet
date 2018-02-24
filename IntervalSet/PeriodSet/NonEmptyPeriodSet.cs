using System;
using System.Collections.Generic;
using System.Linq;

namespace IntervalSet.PeriodSet
{
    /// <summary>
    /// A <see cref="PeriodSet{TSet,TNonEmptySet,TListBuilder,TPeriod}"/> that contains at least one <typeparamref name="TPeriod"/>
    /// </summary>
    /// <typeparam name="TSet"></typeparam>
    /// <typeparam name="TNonEmptySet"></typeparam>
    /// <typeparam name="TPeriod"></typeparam>
    /// <typeparam name="TListBuilder"></typeparam>
    public abstract class NonEmptyPeriodSet<TSet, TNonEmptySet, TListBuilder, TPeriod> : PeriodSet<TSet, TNonEmptySet, TListBuilder, TPeriod>, INonEmptyPeriod
        where TSet : IPeriodSet
        where TListBuilder : IPeriodListBuilder<TPeriod>, new()
        where TPeriod : INonEmptyPeriod
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

        /// <inheritdoc />
        protected NonEmptyPeriodSet(DateTime from, DateTime? to = null):base(from, to)
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

        /// <inheritdoc/>
        public DateTime Earliest => PeriodList.OrderBy(p => p.Earliest).First().Earliest;

        /// <inheritdoc cref="IPeriodSet.IsEmpty"/>
        public override bool IsEmpty => false;
       
    }
}
