using System;
using System.Collections.Generic;
using IntervalSet.Default;

namespace IntervalSet.Interval.Default
{
    /// <summary>
    /// A default impementation of an <see cref="IIntervalSet{T}"/> representing the entire <typeparamref name="T"/> space
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultEntireInterval<TIntervalBuilder, T> : IntervalSet<DefaultSetBuilder<TIntervalBuilder, T>, DefaultIntervalSet<TIntervalBuilder,T>, TIntervalBuilder, IDefaultInterval<T>, T>, IDefaultInterval<T>
        where TIntervalBuilder : IIntervalBuilder<IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <inheritdoc />
        public T Start => IntervalBuilder.PositiveInfinity;

        /// <inheritdoc />
        public T End => IntervalBuilder.NegativeInfinity;
    }
}
