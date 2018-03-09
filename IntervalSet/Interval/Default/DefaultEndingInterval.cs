using System;
using IntervalSet.Default;
using IntervalSet.Interval.Boundaries;

namespace IntervalSet.Interval.Default
{
    /// <summary>
    /// A default implementation of an <see cref="IDefaultInterval{T}"/> consisting of only an end <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TSet"></typeparam>
    /// <typeparam name="TBuilder"></typeparam>
    public class DefaultEndingInterval<TSet, TBuilder,T> : SingleBoundaryInterval<TSet, TBuilder, IDefaultInterval<T>, T>
        where TSet : IIntervalSet<T>
        where TBuilder : IBuilder<TSet, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="DefaultEndingInterval{TSet, TIntervalBuilder, T}"/> with a given <see cref="Boundaries.End{T}"/>
        /// </summary>
        /// <param name="end"></param>
        public DefaultEndingInterval(End<T> end):base(end)
        {
        }

        /// <inheritdoc />
        protected override IDefaultInterval<T> GetInterval()
        {
            return this;
        }
    }

    /// <summary>
    /// A default implementation of an <see cref="DefaultEndingInterval{TSet,TIntervalBuilder,T}"/> where <c>TSet</c> is <see cref="DefaultIntervalSet{TIntervalBuilder,T}"/>
    /// </summary>
    public class DefaultEndingInterval<TIntervalBuilder, T> : DefaultEndingInterval<DefaultIntervalSet<TIntervalBuilder, T>, TIntervalBuilder, T>
        where TIntervalBuilder : IBuilder<DefaultIntervalSet<TIntervalBuilder, T>, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="DefaultEndingInterval{TIntervalBuilder, T}"/> with a given <see cref="Boundaries.End{T}"/>
        /// </summary>
        /// <param name="end"></param>
        public DefaultEndingInterval(End<T> end) : base(end)
        {
        }
    }

    /// <summary>
    /// A default implementation of <see cref="DefaultEndingInterval{TIntervalBuilder,T}"/> where <c>TIntervalBuilder</c> is <see cref="DefaultBuilder{T}"/>
    /// </summary>
    public class DefaultEndingInterval<T> : DefaultEndingInterval<DefaultIntervalSet<T>, DefaultBuilder<T>, T>
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="DefaultEndingInterval{T}"/> with a given <see cref="Boundaries.End{T}"/>
        /// </summary>
        /// <param name="end"></param>
        public DefaultEndingInterval(End<T> end) : base(end)
        {
        }
    }
}
