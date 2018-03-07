using System;
using System.Collections.Generic;
using System.Linq;

namespace IntervalSet
{
    /// <summary>
    /// A <see cref="MultipleIntervalSet{TSet,TBuilder,TInterval,T}"/> that contains at least one <typeparamref name="TInterval"/>
    /// </summary>
    public abstract class NonEmptyIntervalSet<TSet, TIntervalBuilder, TInterval, T> : MultipleIntervalSet<TSet, TIntervalBuilder, TInterval, T>
        where TSet : IIntervalSet<T>
        where TIntervalBuilder : IIntervalBuilder<TInterval, T>, ISetBuilder<TSet, TInterval, T>, new()
        where TInterval : IIntervalSet<T>
        where T : IComparable<T>, IEquatable<T>
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
