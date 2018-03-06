using System;
using IntervalSet.Default;

namespace IntervalSet.Interval.Default
{
    /// <summary>
    /// A default impementation of an <see cref="IIntervalSet{T}"/> representing the entire <typeparamref name="T"/> space
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultEntireInterval<T> : IntervalSet<DefaultIntervalSet<T>, DefaultBuilder<T>, IDefaultInterval<T>, T>, IDefaultInterval<T>
        where T : IComparable<T>, IEquatable<T>
    {
        /// <inheritdoc />
        public bool HasEnd => false;

        /// <inheritdoc />
        public bool HasStart => false;

        /// <inheritdoc />
        public T Start => default(T);

        /// <inheritdoc />
        public T End => default(T);
    }
}
