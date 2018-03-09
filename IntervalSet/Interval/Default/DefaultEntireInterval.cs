using System;
using IntervalSet.Default;

namespace IntervalSet.Interval.Default
{
    /// <summary>
    /// A default impementation of an <see cref="IIntervalSet{T}"/> representing the entire <typeparamref name="T"/> space
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TSet"></typeparam>
    /// <typeparam name="TBuilder"></typeparam>
    public class DefaultEntireInterval<TSet, TBuilder, T> : Interval<TSet, TBuilder, IDefaultInterval<T>, T>
        where TSet : IIntervalSet<T>
        where TBuilder : IBuilder<TSet, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <inheritdoc />
        protected override IDefaultInterval<T> GetInterval()
        {
            return this;
        }
    }

    /// <summary>
    /// A default impementation of <see cref="DefaultEntireInterval{TSet,TBuilder,T}"/> where <c>TSet</c> is <see cref="DefaultIntervalSet{TBuilder,T}"/>
    /// </summary>
    public class DefaultEntireInterval<TBuilder, T> : DefaultEntireInterval<DefaultIntervalSet<TBuilder, T>, TBuilder, T>
        where TBuilder : IBuilder<DefaultIntervalSet<TBuilder, T>, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
    }

    /// <summary>
    /// A default impementation of <see cref="DefaultEntireInterval{TBuilder,T}"/> where <c>TBuilder</c> is <see cref="DefaultBuilder{T}"/>
    /// </summary>
    public class DefaultEntireInterval<T> : DefaultEntireInterval<DefaultIntervalSet<T>, DefaultBuilder<T>, T>
        where T : IComparable<T>, IEquatable<T>
    {
    }
}
