using System;
using System.Collections.Generic;
using IntervalSet.Default;
using IntervalSet.Interval.Boundaries;

namespace IntervalSet.Interval.Default
{
    /// <summary>
    /// A default implementation of an <see cref="IDefaultInterval{T}"/> with only a start <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultStartingInterval<TIntervalBuilder, T> : SingleBoundaryInterval<DefaultIntervalSet<TIntervalBuilder,T>, TIntervalBuilder, IDefaultInterval<T>, T>, IDefaultInterval<T>
        where TIntervalBuilder : IIntervalBuilder<IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="DefaultStartingInterval{T}"/> with a given <see cref="Boundaries.Start{T}"/>
        /// </summary>
        /// <param name="from"></param>
        public DefaultStartingInterval(Start<T> from):base(from)
        {
        }

        /// <inheritdoc />
        protected override IDefaultInterval<T> GetInterval()
        {
            return this;
        }

        protected override DefaultIntervalSet<TIntervalBuilder,T> MakeSet(IList<IDefaultInterval<T>> intervals)
        {
            return new DefaultIntervalSet<TIntervalBuilder,T>(intervals);
        }

        /// <inheritdoc />
        public T End => Builder.PositiveInfinity;

        /// <inheritdoc />
        public T Start => Boundary.Location;
    }
}
