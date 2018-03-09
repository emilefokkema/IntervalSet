using System;
using IntervalSet.Default;
using IntervalSet.Interval.Boundaries;

namespace IntervalSet.Interval.Default
{
    /// <summary>
    /// A default implementation of an <see cref="IDefaultInterval{T}"/> with a start <typeparamref name="T"/> and an end <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TSet"></typeparam>
    /// <typeparam name="TBuilder"></typeparam>
    public class DefaultStartEndingInterval<TSet, TBuilder, T> : DoubleBoundaryInterval<TSet, TBuilder, IDefaultInterval<T>, T>
        where TSet : IIntervalSet<T>
        where TBuilder : IBuilder<TSet, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="DefaultStartEndingInterval{TSet, TBuilder, T}"/> with a given <see cref="Boundaries.Start{T}"/> and <see cref="Boundaries.End{T}"/>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public DefaultStartEndingInterval(Start<T> from, End<T> to):base(from, to)
        {
        }

        /// <inheritdoc />
        protected override IDefaultInterval<T> GetInterval()
        {
            return this;
        }
    }

    /// <summary>
    /// A default implementation of <see cref="DefaultStartEndingInterval{TSet,TBuilder,T}"/> where <c>TSet</c> is <see cref="DefaultIntervalSet{TBuilder,T}"/>
    /// </summary>
    public class DefaultStartEndingInterval<TBuilder, T> : DefaultStartEndingInterval<DefaultIntervalSet<TBuilder, T>, TBuilder, T>
        where TBuilder : IBuilder<DefaultIntervalSet<TBuilder, T>, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="DefaultStartEndingInterval{TBuilder, T}"/> with a given <see cref="Boundaries.Start{T}"/> and <see cref="Boundaries.End{T}"/>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public DefaultStartEndingInterval(Start<T> from, End<T> to) : base(from, to)
        {
        }
    }

    /// <summary>
    /// A default implementation of <see cref="DefaultStartEndingInterval{TBuilder,T}"/> where <c>TBuilder</c> is <see cref="DefaultBuilder{T}"/>
    /// </summary>
    public class DefaultStartEndingInterval<T> : DefaultStartEndingInterval<DefaultIntervalSet<T>, DefaultBuilder<T>, T>
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="DefaultStartEndingInterval{T}"/> with a given <see cref="Boundaries.Start{T}"/> and <see cref="Boundaries.End{T}"/>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public DefaultStartEndingInterval(Start<T> from, End<T> to) : base(from, to)
        {
        }
    }
}
