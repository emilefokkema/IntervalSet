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
        /// Initializes a new <see cref="DefaultEndingInterval{TSet, TBuilder, T}"/> with a given <see cref="Boundaries.End{T}"/>
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
    /// A default implementation of an <see cref="DefaultEndingInterval{TSet,TBuilder,T}"/> where <c>TSet</c> is <see cref="DefaultIntervalSet{TBuilder,T}"/>
    /// </summary>
    public class DefaultEndingInterval<TBuilder, T> : DefaultEndingInterval<DefaultIntervalSet<TBuilder, T>, TBuilder, T>
        where TBuilder : IBuilder<DefaultIntervalSet<TBuilder, T>, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="DefaultEndingInterval{TBuilder, T}"/> with a given <see cref="Boundaries.End{T}"/>
        /// </summary>
        /// <param name="end"></param>
        public DefaultEndingInterval(End<T> end) : base(end)
        {
        }
    }

    /// <summary>
    /// A default implementation of <see cref="DefaultEndingInterval{TBuilder,T}"/> where <c>TBuilder</c> is <see cref="DefaultBuilder{T}"/>
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
