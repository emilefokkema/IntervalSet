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
    public class DefaultNonEmptyIntervalSet<TBuilder, T> : NonEmptyIntervalSet<DefaultIntervalSet<TBuilder, T>, DefaultBuilder<T>, IDefaultInterval<T>, T>, IDefaultInterval<T>
        where TBuilder : IIntervalBuilder<IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="DefaultNonEmptyIntervalSet{T}"/> with a given non-empty list of <see cref="IDefaultInterval{T}"/>
        /// </summary>
        /// <param name="intervals"></param>
        public DefaultNonEmptyIntervalSet(IList<IDefaultInterval<T>> intervals):base(intervals)
        {
        }

        protected override DefaultIntervalSet<TBuilder,T> MakeSet(IList<IDefaultInterval<T>> intervals)
        {
            return new DefaultIntervalSet<TBuilder,T>(intervals);
        }

        protected override IDefaultInterval<T> MakeNonEmptySet()
        {
            return this;
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
}
