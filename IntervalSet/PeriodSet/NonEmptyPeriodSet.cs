using System;
using System.Collections.Generic;
using System.Linq;
using IntervalSet.PeriodSet.Period;

namespace IntervalSet.PeriodSet
{
    /// <summary>
    /// A <see cref="MultiplePeriodSet{TSet,TBuilder,TStartingPeriod,TPeriod}"/> that contains at least one <typeparamref name="TPeriod"/>
    /// </summary>
    /// <typeparam name="TSet"></typeparam>
    /// <typeparam name="TPeriod"></typeparam>
    /// <typeparam name="TBuilder"></typeparam>
    /// <typeparam name="TStartingPeriod"></typeparam>
    public abstract class NonEmptyPeriodSet<TSet, TBuilder, TStartingPeriod, TPeriod> : MultiplePeriodSet<TSet, TBuilder, TStartingPeriod, TPeriod>
        where TSet : IPeriodSet
        where TBuilder : IBuilder<TSet, TPeriod, TStartingPeriod>, new()
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
