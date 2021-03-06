﻿using System;
using IntervalSet.Default;
using IntervalSet.Interval.Boundaries;

namespace IntervalSet.Interval.Default
{
    /// <summary>
    /// A default implementation of an <see cref="IDefaultInterval{T}"/> with only a start <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TSet"></typeparam>
    /// <typeparam name="TBuilder"></typeparam>
    public class DefaultStartingInterval<TSet, TBuilder, T> : SingleBoundaryInterval<TSet, TBuilder, IDefaultInterval<T>, T>
        where TSet : IIntervalSet<T>
        where TBuilder : IBuilder<TSet, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="DefaultStartingInterval{TSet, TBuilder, T}"/> with a given <see cref="Boundaries.Start{T}"/>
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
    }

    /// <summary>
    /// A default implementation of <see cref="DefaultStartingInterval{TSet,TBuilder,T}"/> where <c>TSet</c> is <see cref="DefaultIntervalSet{TBuilder,T}"/>
    /// </summary>
    public class DefaultStartingInterval<TBuilder, T> : DefaultStartingInterval<DefaultIntervalSet<TBuilder, T>, TBuilder, T>
        where TBuilder : IBuilder<DefaultIntervalSet<TBuilder, T>, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="DefaultStartingInterval{TBuilder, T}"/> with a given <see cref="Boundaries.Start{T}"/>
        /// </summary>
        /// <param name="from"></param>
        public DefaultStartingInterval(Start<T> from) : base(from)
        {
        }
    }

    /// <summary>
    /// A default implementation of <see cref="DefaultStartingInterval{TBuilder,T}"/> where <c>TBuilder</c> is <see cref="DefaultBuilder{T}"/>
    /// </summary>
    public class DefaultStartingInterval<T> : DefaultStartingInterval<DefaultIntervalSet<T>, DefaultBuilder<T>, T>
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="DefaultStartingInterval{T}"/> with a given <see cref="Boundaries.Start{T}"/>
        /// </summary>
        /// <param name="from"></param>
        public DefaultStartingInterval(Start<T> from) : base(from)
        {
        }
    }
}
