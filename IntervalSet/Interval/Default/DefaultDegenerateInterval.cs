using System;
using IntervalSet.Default;
using IntervalSet.Interval.Boundaries;

namespace IntervalSet.Interval.Default
{
    /// <summary>
    /// A default implementation of an <see cref="IIntervalSet{T}"/> consisting of a single <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TSet"></typeparam>
    /// <typeparam name="TBuilder"></typeparam>
    public class DefaultDegenerateInterval<TSet, TBuilder, T> : SingleBoundaryInterval<TSet, TBuilder, IDefaultInterval<T>, T>
        where TSet : IIntervalSet<T>
        where TBuilder : IBuilder<TSet, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="DefaultDegenerateInterval{TSet, TIntervalBuilder, T}"/> based on a given <typeparamref name="T"/>
        /// </summary>
        /// <param name="boundary"></param>
        public DefaultDegenerateInterval(Degenerate<T> boundary):base(boundary)
        {
        }

        /// <inheritdoc />
        protected override IDefaultInterval<T> GetInterval()
        {
            return this;
        }
    }

    /// <summary>
    /// A default implementation of <see cref="DefaultDegenerateInterval{TSet,TIntervalBuilder,T}"/> where <c>TSet</c> is <see cref="DefaultIntervalSet{TIntervalBuilder,T}"/>
    /// </summary>
    public class DefaultDegenerateInterval<TIntervalBuilder, T> : DefaultDegenerateInterval<DefaultIntervalSet<TIntervalBuilder, T>, TIntervalBuilder, T>
        where TIntervalBuilder : IBuilder<DefaultIntervalSet<TIntervalBuilder, T>, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="DefaultDegenerateInterval{TIntervalBuilder, T}"/> based on a given <typeparamref name="T"/>
        /// </summary>
        /// <param name="boundary"></param>
        public DefaultDegenerateInterval(Degenerate<T> boundary) : base(boundary)
        {
        }
    }

    /// <summary>
    /// A default implementation of <see cref="DefaultDegenerateInterval{TIntervalBuilder,T}"/> where <c>TIntervalBuilder</c> is <see cref="DefaultBuilder{T}"/>
    /// </summary>
    public class DefaultDegenerateInterval<T> : DefaultDegenerateInterval<DefaultIntervalSet<T>, DefaultBuilder<T>, T>
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="DefaultDegenerateInterval{T}"/> based on a given <typeparamref name="T"/>
        /// </summary>
        /// <param name="boundary"></param>
        public DefaultDegenerateInterval(Degenerate<T> boundary) : base(boundary)
        {
        }
    }
}
