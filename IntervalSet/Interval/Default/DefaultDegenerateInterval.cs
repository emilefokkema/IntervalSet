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
    public class DefaultDegenerateInterval<TIntervalBuilder, T> : SingleBoundaryInterval<DefaultIntervalSet<TIntervalBuilder, T>, TIntervalBuilder, IDefaultInterval<T>, T>, IDefaultInterval<T>
        where TIntervalBuilder : IIntervalBuilder<IDefaultInterval<T>, T>, new()
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

        protected override DefaultIntervalSet<TIntervalBuilder,T> MakeSet(IList<IDefaultInterval<T>> intervals)
        {
            return new DefaultIntervalSet<TIntervalBuilder,T>(intervals);
        }

        /// <inheritdoc />
        public T End => Boundary.Location;

        /// <inheritdoc />
        public T Start => Boundary.Location;
    }
}
