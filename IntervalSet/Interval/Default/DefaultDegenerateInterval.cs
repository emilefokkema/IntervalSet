using System;
using System.Collections.Generic;
using IntervalSet.Default;
using IntervalSet.Interval.Boundaries;

namespace IntervalSet.Interval.Default
{
    /// <summary>
    /// A default implementation of an <see cref="IIntervalSet{T}"/> consisting of a single <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultDegenerateInterval<TBuilder, T> : SingleBoundaryInterval<DefaultIntervalSet<TBuilder, T>, TBuilder, IDefaultInterval<T>, T>, IDefaultInterval<T>
        where TBuilder : IIntervalBuilder<IDefaultInterval<T>, T>, new()
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

        protected override DefaultIntervalSet<TBuilder,T> MakeSet(IList<IDefaultInterval<T>> intervals)
        {
            return new DefaultIntervalSet<TBuilder,T>(intervals);
        }

        /// <inheritdoc />
        public bool HasEnd => true;

        /// <inheritdoc />
        public T End => Boundary.Location;

        /// <inheritdoc />
        public T Start => Boundary.Location;

        /// <inheritdoc />
        public bool HasStart => true;
    }
}
