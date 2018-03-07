using System;
using System.Collections.Generic;
using IntervalSet.Default;

namespace IntervalSet.Interval.Default
{
    /// <summary>
    /// A default impementation of an <see cref="IIntervalSet{T}"/> representing the entire <typeparamref name="T"/> space
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultEntireInterval<TIntervalBuilder, T> : IntervalSet<DefaultIntervalSet<TIntervalBuilder,T>, TIntervalBuilder, IDefaultInterval<T>, T>, IDefaultInterval<T>
        where TIntervalBuilder : IIntervalBuilder<IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        protected override DefaultIntervalSet<TIntervalBuilder,T> MakeSet(IList<IDefaultInterval<T>> intervals)
        {
            return new DefaultIntervalSet<TIntervalBuilder,T>(intervals);
        }

        /// <inheritdoc />
        public T Start => Builder.PositiveInfinity;

        /// <inheritdoc />
        public T End => Builder.NegativeInfinity;
    }
}
