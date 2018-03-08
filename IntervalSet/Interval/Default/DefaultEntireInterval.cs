using System;
using System.Collections.Generic;
using IntervalSet.Default;
using IntervalSet.Interval.Boundaries;

namespace IntervalSet.Interval.Default
{
    /// <summary>
    /// A default impementation of an <see cref="IIntervalSet{T}"/> representing the entire <typeparamref name="T"/> space
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultEntireInterval<TSet, TIntervalBuilder, T> : IntervalSet<TSet, TIntervalBuilder, IDefaultInterval<T>, T>, IDefaultInterval<T>
        where TSet : IIntervalSet<T>
        where TIntervalBuilder : IIntervalBuilder<IDefaultInterval<T>, T>, ISetBuilder<TSet, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <inheritdoc />
        public Boundary<T> Start => IntervalBuilder.NegativeInfinityBoundary;

        /// <inheritdoc />
        public Boundary<T> End => IntervalBuilder.PositiveInfinityBoundary;
    }

    public class DefaultEntireInterval<TIntervalBuilder, T> : DefaultEntireInterval<DefaultIntervalSet<TIntervalBuilder, T>, TIntervalBuilder, T>
        where TIntervalBuilder : IIntervalBuilder<IDefaultInterval<T>, T>, ISetBuilder<DefaultIntervalSet<TIntervalBuilder, T>, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
    }

    public class DefaultEntireInterval<T> : DefaultEntireInterval<DefaultIntervalSet<T>, DefaultBuilder<T>, T>
        where T : IComparable<T>, IEquatable<T>
    {
    }
}
