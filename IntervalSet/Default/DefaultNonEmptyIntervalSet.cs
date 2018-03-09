using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Default;

namespace IntervalSet.Default
{
    /// <summary>
    /// A default implementation of an <see cref="IIntervalSet{T}"/> that contains at least one <see cref="IDefaultInterval{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TSet"></typeparam>
    /// <typeparam name="TBuilder"></typeparam>
    public class DefaultNonEmptyIntervalSet<TSet, TBuilder, T> : NonEmptyIntervalSet<TSet, TBuilder, IDefaultInterval<T>, T>, IDefaultInterval<T>
        where TSet : IIntervalSet<T>
        where TBuilder : IBuilder<TSet, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="DefaultNonEmptyIntervalSet{T}"/> with a given non-empty list of <see cref="IDefaultInterval{T}"/>
        /// </summary>
        /// <param name="intervals"></param>
        public DefaultNonEmptyIntervalSet(IList<IDefaultInterval<T>> intervals):base(intervals)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="DefaultNonEmptyIntervalSet{TSet,TBuilder,T}"/> based on a given <see cref="IIntervalSet{T}"/>
        /// </summary>
        /// <param name="set"></param>
        public DefaultNonEmptyIntervalSet(IIntervalSet<T> set):base(set)
        {
        }

        /// <inheritdoc />
        public Boundary<T> StartingBoundary => IntervalList.First().StartingBoundary;

        /// <inheritdoc />
        public Boundary<T> EndingBoundary => IntervalList.Last().EndingBoundary;

        /// <inheritdoc />
        public T Start => StartingBoundary;

        /// <inheritdoc />
        public T End => EndingBoundary;

        /// <inheritdoc />
        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }
    }

    /// <summary>
    /// A default implementation of <see cref="DefaultNonEmptyIntervalSet{TSet,TBuilder,T}"/> where <c>TSet</c> is <see cref="DefaultIntervalSet{TBuilder,T}"/>
    /// </summary>
    public class DefaultNonEmptyIntervalSet<TBuilder, T> : DefaultNonEmptyIntervalSet<DefaultIntervalSet<TBuilder, T>, TBuilder, T>
        where TBuilder : IBuilder<DefaultIntervalSet<TBuilder, T>, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="DefaultNonEmptyIntervalSet{T}"/> with a given non-empty list of <see cref="IDefaultInterval{T}"/>
        /// </summary>
        /// <param name="intervals"></param>
        public DefaultNonEmptyIntervalSet(IList<IDefaultInterval<T>> intervals) : base(intervals)
        {
        }
    }

    /// <summary>
    /// A default implementation of <see cref="DefaultNonEmptyIntervalSet{TBuilder,T}"/> where <c>TBuilder</c> is <see cref="DefaultBuilder{T}"/>
    /// </summary>
    public class DefaultNonEmptyIntervalSet<T> : DefaultNonEmptyIntervalSet<DefaultIntervalSet<T>, DefaultBuilder<T>, T>
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="DefaultNonEmptyIntervalSet{T}"/> with a given non-empty list of <see cref="IDefaultInterval{T}"/>
        /// </summary>
        /// <param name="intervals"></param>
        public DefaultNonEmptyIntervalSet(IList<IDefaultInterval<T>> intervals) : base(intervals)
        {
        }
    }
}
