using System;
using System.Collections.Generic;
using System.Linq;
using IntervalSet.Interval;

namespace IntervalSet
{
    /// <summary>
    /// A <see cref="MultipleIntervalSet{TSet,TBuilder,TStartingInterval,TInterval,T}"/> that contains at least one <typeparamref name="TInterval"/>
    /// </summary>
    public abstract class NonEmptyIntervalSet<TSet, TBuilder, TStartingInterval, TInterval, T> : MultipleIntervalSet<TSet, TBuilder, TStartingInterval, TInterval, T>
        where TSet : IIntervalSet<T>
        where TBuilder : IBuilder<TSet, TInterval, TStartingInterval, T>, new()
        where TInterval : IIntervalSet<T>
        where TStartingInterval : class, TInterval, IStartingInterval<TInterval, T>
        where T : IComparable<T>, IEquatable<T>, IFormattable
    {
        /// <inheritdoc/>
        protected NonEmptyIntervalSet(IList<TInterval> list) : base(list)
        {
            CheckNonEmpty();
        }

        /// <inheritdoc />
        protected NonEmptyIntervalSet(IIntervalSet<T> set) : base(set)
        {
            CheckNonEmpty();
        }

        private void CheckNonEmpty()
        {
            if (!IntervalList.Any())
            {
                throw new InvalidCastException($"cannot initialize a non-empty interval set with an empty interval set");
            }
        }

        /// <inheritdoc />
        public override bool IsEmpty => false;
    }
}
