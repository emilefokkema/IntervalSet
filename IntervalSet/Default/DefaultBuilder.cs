using System;
using System.Collections.Generic;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Default;

namespace IntervalSet.Default
{
    /// <summary>
    /// A default implementation of <see cref="IBuilder{TSet,TInterval,T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultBuilder<T> : Builder<DefaultIntervalSet<T>, IDefaultInterval<T>, T>
        where T : IComparable<T>, IEquatable<T>
    {
        /// <inheritdoc />
        public override IDefaultInterval<T> MakeNonEmptySet(IList<IDefaultInterval<T>> intervals)
        {
            return new DefaultNonEmptyIntervalSet<T>(intervals);
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeStartingInterval()
        {
            return new DefaultEntireInterval<T>();
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeStartingInterval(Start<T> from)
        {
            return new DefaultStartingInterval<T>(from);
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeEndingInterval(End<T> end)
        {
            return new DefaultEndingInterval<T>(end);
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeStartEndingInterval(Start<T> from, End<T> to)
        {
            return new DefaultStartEndingInterval<T>(from, to);
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
