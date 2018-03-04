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
    public class DefaultNonEmptyIntervalSet<T> : NonEmptyIntervalSet<DefaultIntervalSet<T>, DefaultBuilder<T>, IDefaultStartingInterval<T>, IDefaultInterval<T>, T>, IDefaultInterval<T>
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
        public bool HasStart => IntervalList.First().HasStart;

        /// <inheritdoc />
        public bool HasEnd => IntervalList.Last().HasEnd;

        /// <inheritdoc />
        public T Start {
            get
            {
                if (HasStart)
                {
                    return IntervalList.First().Start;
                }

                return default(T);
            }
        }

        /// <inheritdoc />
        public T End {
            get
            {
                if (HasEnd)
                {
                    return IntervalList.Last().End;
                }

                return default(T);
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }
    }
}
