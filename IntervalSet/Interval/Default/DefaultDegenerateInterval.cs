using System;
using IntervalSet.Default;
using IntervalSet.Interval.Boundaries;

namespace IntervalSet.Interval.Default
{
    /// <summary>
    /// A default implementation of an <see cref="IIntervalSet{T}"/> consisting of a single <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultDegenerateInterval<TSet, TIntervalBuilder, T> : SingleBoundaryInterval<TSet, TIntervalBuilder, IDefaultInterval<T>, T>
        where TSet : IIntervalSet<T>
        where TIntervalBuilder : IIntervalBuilder<IDefaultInterval<T>, T>, ISetBuilder<TSet, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="DefaultDegenerateInterval{T}"/> based on a given <typeparamref name="T"/>
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

    public class DefaultDegenerateInterval<TIntervalBuilder, T> : DefaultDegenerateInterval<DefaultIntervalSet<TIntervalBuilder, T>, TIntervalBuilder, T>
        where TIntervalBuilder : IIntervalBuilder<IDefaultInterval<T>, T>, ISetBuilder<DefaultIntervalSet<TIntervalBuilder, T>, IDefaultInterval<T>, T>, new()
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
