using System;
using System.Collections.Generic;
using IntervalSet.Default;
using IntervalSet.Interval.Boundaries;

namespace IntervalSet.Interval.Default
{
    /// <summary>
    /// A default implementation of an <see cref="IDefaultInterval{T}"/> consisting of only an end <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultEndingInterval<TBuilder,T> : SingleBoundaryInterval<DefaultIntervalSet<TBuilder,T>, TBuilder, IDefaultInterval<T>, T>, IDefaultInterval<T>
        where TBuilder : IIntervalBuilder<IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="DefaultEndingInterval{T}"/> with a given <see cref="Boundaries.End{T}"/>
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

        protected override DefaultIntervalSet<TBuilder,T> MakeSet(IList<IDefaultInterval<T>> intervals)
        {
            return new DefaultIntervalSet<TBuilder,T>(intervals);
        }

        /// <inheritdoc />
        public T End => Boundary.Location;

        /// <inheritdoc />
        public T Start => Builder.NegativeInfinity;
    }
}
