using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using IntervalSet.Interval.Default;

namespace IntervalSet.Default
{
    /// <summary>
    /// A default implementation of an <see cref="IIntervalSet{T}"/> that contains at least one <see cref="IDefaultInterval{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultNonEmptyIntervalSet<TSet, TIntervalBuilder, T> : NonEmptyIntervalSet<TSet, TIntervalBuilder, IDefaultInterval<T>, T>, IDefaultInterval<T>
        where TSet : IIntervalSet<T>
        where TIntervalBuilder : IIntervalBuilder<IDefaultInterval<T>, T>, ISetBuilder<TSet, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="DefaultNonEmptyIntervalSet{T}"/> with a given non-empty list of <see cref="IDefaultInterval{T}"/>
        /// </summary>
        /// <param name="intervals"></param>
        public DefaultNonEmptyIntervalSet(IList<IDefaultInterval<T>> intervals):base(intervals)
        {
        }

        /// <inheritdoc />
        public T Start => IntervalList.First().Start;

        /// <inheritdoc />
        public T End => IntervalList.Last().End;

        /// <inheritdoc />
        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }
    }

    public class DefaultNonEmptyIntervalSet<TIntervalBuilder, T> : DefaultNonEmptyIntervalSet<DefaultIntervalSet<TIntervalBuilder, T>, TIntervalBuilder, T>
        where TIntervalBuilder : IIntervalBuilder<IDefaultInterval<T>, T>, ISetBuilder<DefaultIntervalSet<TIntervalBuilder, T>, IDefaultInterval<T>, T>, new()
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
