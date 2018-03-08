using System;
using System.Collections.Generic;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Default;

namespace IntervalSet.Default
{
    /// <summary>
    /// A default implementation of <see cref="IIntervalBuilder{TInterval,T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractDefaultBuilder<T> : IntervalBuilder<IDefaultInterval<T>, T>
        where T : IComparable<T>, IEquatable<T>
    {
        /// <inheritdoc />
        public override Infinity<T> PositiveInfinity => new DefaultInfinity<T>(Sign.Positive);

        /// <inheritdoc />
        public override Infinity<T> NegativeInfinity => new DefaultInfinity<T>(Sign.Negative);
    }

    public class DefaultBuilder<TIntervalBuilder, T> : AbstractDefaultBuilder<T>, ISetBuilder<DefaultIntervalSet<TIntervalBuilder, T>, IDefaultInterval<T>, T>
        where TIntervalBuilder : IIntervalBuilder<IDefaultInterval<T>, T>, ISetBuilder<DefaultIntervalSet<TIntervalBuilder, T>, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        public DefaultIntervalSet<TIntervalBuilder, T> MakeSet(IList<IDefaultInterval<T>> intervals)
        {
            return new DefaultIntervalSet<TIntervalBuilder, T>(intervals);
        }

        public IDefaultInterval<T> MakeNonEmptySet(IList<IDefaultInterval<T>> intervals)
        {
            return new DefaultNonEmptyIntervalSet<TIntervalBuilder, T>(intervals);
        }
        /// <inheritdoc />
        public override IDefaultInterval<T> MakeStartingInterval()
        {
            return new DefaultEntireInterval<TIntervalBuilder, T>();
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeStartingInterval(Start<T> from)
        {
            return new DefaultStartingInterval<TIntervalBuilder, T>(from);
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeEndingInterval(End<T> end)
        {
            return new DefaultEndingInterval<TIntervalBuilder, T>(end);
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeStartEndingInterval(Start<T> from, End<T> to)
        {
            return new DefaultStartEndingInterval<TIntervalBuilder, T>(from, to);
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeDegenerate(Degenerate<T> degenerate)
        {
            return new DefaultDegenerateInterval<TIntervalBuilder, T>(degenerate);
        }
    }

    public class DefaultBuilder<T> : AbstractDefaultBuilder<T>, ISetBuilder<DefaultIntervalSet<T>, IDefaultInterval<T>, T>
        where T : IComparable<T>, IEquatable<T>
    {
        public DefaultIntervalSet<T> MakeSet(IList<IDefaultInterval<T>> intervals)
        {
            return new DefaultIntervalSet<T>(intervals);
        }

        public IDefaultInterval<T> MakeNonEmptySet(IList<IDefaultInterval<T>> intervals)
        {
            return new DefaultNonEmptyIntervalSet<T>(intervals);
        }

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
    }
}
