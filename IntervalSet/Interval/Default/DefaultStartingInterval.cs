using System;
using IntervalSet.Default;
using IntervalSet.Interval.Boundaries;

namespace IntervalSet.Interval.Default
{
    /// <summary>
    /// A default implementation of an <see cref="IDefaultInterval{T}"/> with only a start <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultStartingInterval<T> : SingleBoundaryInterval<DefaultIntervalSet<T>, DefaultBuilder<T>, IDefaultInterval<T>, T>, IDefaultInterval<T>
        where T : IComparable<T>, IEquatable<T>
    {
        private readonly Start<T> _start;

        /// <summary>
        /// Initializes a new <see cref="DefaultStartingInterval{T}"/> with a given <see cref="Boundaries.Start{T}"/>
        /// </summary>
        /// <param name="from"></param>
        public DefaultStartingInterval(Start<T> from):base(from)
        {
            _start = from;
        }

        /// <inheritdoc />
        protected override IDefaultInterval<T> GetInterval()
        {
            return this;
        }

        /// <inheritdoc />
        public T End => default(T);

        /// <inheritdoc />
        public T Start => _start.Location;

        /// <inheritdoc />
        public bool HasEnd => false;

        /// <inheritdoc />
        public bool HasStart => true;
    }
}
