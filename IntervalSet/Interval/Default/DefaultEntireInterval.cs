using System;
using IntervalSet.Default;

namespace IntervalSet.Interval.Default
{
    /// <summary>
    /// A default impementation of an <see cref="IIntervalSet{T}"/> representing the entire <typeparamref name="T"/> space
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultEntireInterval<TSet, TIntervalBuilder, T> : Interval<TSet, TIntervalBuilder, IDefaultInterval<T>, T>
        where TSet : IIntervalSet<T>
        where TIntervalBuilder : IIntervalBuilder<IDefaultInterval<T>, T>, ISetBuilder<TSet, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        protected override IDefaultInterval<T> GetInterval()
        {
            return this;
        }
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
