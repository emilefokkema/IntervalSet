using System;
using IntervalSet.Default;
using IntervalSet.Interval.Boundaries;

namespace IntervalSet.Interval.Default
{
    /// <summary>
    /// A default implementation of an <see cref="IDefaultInterval{T}"/> with a start <typeparamref name="T"/> and an end <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultStartEndingInterval<TSet, TIntervalBuilder, T> : DoubleBoundaryInterval<TSet, TIntervalBuilder, IDefaultInterval<T>, T>
        where TSet : IIntervalSet<T>
        where TIntervalBuilder : IIntervalBuilder<IDefaultInterval<T>, T>, ISetBuilder<TSet, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="DefaultStartEndingInterval{T}"/> with a given <see cref="Boundaries.Start{T}"/> and <see cref="Boundaries.End{T}"/>
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

    public class DefaultStartEndingInterval<TIntervalBuilder, T> : DefaultStartEndingInterval<DefaultIntervalSet<TIntervalBuilder, T>, TIntervalBuilder, T>
        where TIntervalBuilder : IIntervalBuilder<IDefaultInterval<T>, T>, ISetBuilder<DefaultIntervalSet<TIntervalBuilder, T>, IDefaultInterval<T>, T>, new()
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
