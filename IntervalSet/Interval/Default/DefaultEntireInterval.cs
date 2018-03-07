using System;
using System.Collections.Generic;
using IntervalSet.Default;

namespace IntervalSet.Interval.Default
{
    /// <summary>
    /// A default impementation of an <see cref="IIntervalSet{T}"/> representing the entire <typeparamref name="T"/> space
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultEntireInterval<TBuilder, T> : IntervalSet<DefaultIntervalSet<TBuilder,T>, TBuilder, IDefaultInterval<T>, T>, IDefaultInterval<T>
        where TBuilder : IIntervalBuilder<IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        protected override DefaultIntervalSet<TBuilder,T> MakeSet(IList<IDefaultInterval<T>> intervals)
        {
            return new DefaultIntervalSet<TBuilder,T>(intervals);
        }
        /// <inheritdoc />
        public bool HasEnd => false;

        /// <inheritdoc />
        public bool HasStart => false;

        /// <inheritdoc />
        public T Start => Builder.PositiveInfinity;

        /// <inheritdoc />
        public T End => Builder.NegativeInfinity;
    }
}
