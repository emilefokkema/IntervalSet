using System;
using IntervalSet.Default;
using IntervalSet.Interval.Boundaries;

namespace IntervalSet.Interval.Default
{
    /// <summary>
    /// A default implementation of an <see cref="IDefaultInterval{T}"/> consisting of only an end <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultEndingInterval<T> : SingleBoundaryInterval<DefaultIntervalSet<T>, DefaultBuilder<T>, IDefaultInterval<T>, T>, IDefaultInterval<T>
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

        /// <inheritdoc />
        public bool HasEnd => true;

        /// <inheritdoc />
        public bool HasStart => false;

        /// <inheritdoc />
        public T End => Boundary.Location;

        /// <inheritdoc />
        public T Start => default(T);
    }
}
