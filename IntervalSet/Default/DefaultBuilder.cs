using System;
using System.Collections.Generic;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Default;

namespace IntervalSet.Default
{
    /// <summary>
    /// A default implementation of <see cref="IBuilder{TSet,TInterval,TStartingInterval,T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultBuilder<T> : Builder<DefaultIntervalSet<T>, IDefaultInterval<T>, IDefaultStartingInterval<T>, T>
        where T : IComparable<T>, IEquatable<T>
    {
        /// <inheritdoc />
        public override IDefaultInterval<T> MakeNonEmptySet(IList<IDefaultInterval<T>> intervals)
        {
            return new DefaultNonEmptyIntervalSet<T>(intervals);
        }

        /// <inheritdoc />
        public override IDefaultStartingInterval<T> MakeStartingInterval()
        {
            return new DefaultEntireInterval<T>();
        }

        /// <inheritdoc />
        public override IDefaultStartingInterval<T> MakeStartingInterval(Start<T> from)
        {
            return new DefaultStartingInterval<T>(from);
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeDegenerate(Degenerate<T> degenerate)
        {
            return new DefaultDegenerateInterval<T>(degenerate);
        }

        /// <inheritdoc />
        public override DefaultIntervalSet<T> MakeSet(IList<IDefaultInterval<T>> intervals)
        {
            return new DefaultIntervalSet<T>(intervals);
        }
    }
}
