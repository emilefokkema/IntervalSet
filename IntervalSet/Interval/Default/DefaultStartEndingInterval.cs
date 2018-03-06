﻿using System;
using IntervalSet.Default;
using IntervalSet.Interval.Boundaries;

namespace IntervalSet.Interval.Default
{
    /// <summary>
    /// A default implementation of an <see cref="IDefaultInterval{T}"/> with a start <typeparamref name="T"/> and an end <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultStartEndingInterval<T> : DoubleBoundaryInterval<DefaultIntervalSet<T>, DefaultBuilder<T>, IDefaultInterval<T>, T>, IDefaultInterval<T>
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

        /// <inheritdoc />
        public T End => Max.Location;

        /// <inheritdoc />
        public T Start => Min.Location;

        /// <inheritdoc />
        public bool HasStart => true;

        /// <inheritdoc />
        public bool HasEnd => true;
    }
}