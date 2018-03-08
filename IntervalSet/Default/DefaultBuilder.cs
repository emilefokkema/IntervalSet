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
    public abstract class DefaultBuilder<TSet, TIntervalBuilder,T> : IntervalBuilder<IDefaultInterval<T>, T>, ISetBuilder<TSet, IDefaultInterval<T>, T>
        where TSet : IIntervalSet<T>
        where TIntervalBuilder : IIntervalBuilder<IDefaultInterval<T>, T>, ISetBuilder<TSet, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        public abstract TSet MakeSet(IList<IDefaultInterval<T>> intervals);
       

        public IDefaultInterval<T> MakeNonEmptySet(IList<IDefaultInterval<T>> intervals)
        {
            return new DefaultNonEmptyIntervalSet<TSet, TIntervalBuilder, T>(intervals);
        }
        /// <inheritdoc />
        public override IDefaultInterval<T> MakeStartingInterval()
        {
            return new DefaultEntireInterval<TSet, TIntervalBuilder, T>();
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeStartingInterval(Start<T> from)
        {
            return new DefaultStartingInterval<TSet, TIntervalBuilder, T>(from);
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeEndingInterval(End<T> end)
        {
            return new DefaultEndingInterval<TSet, TIntervalBuilder, T>(end);
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeStartEndingInterval(Start<T> from, End<T> to)
        {
            return new DefaultStartEndingInterval<TSet, TIntervalBuilder, T>(from, to);
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeDegenerate(Degenerate<T> degenerate)
        {
            return new DefaultDegenerateInterval<TSet, TIntervalBuilder, T>(degenerate);
        }

        /// <inheritdoc />
        public override Infinity<T> PositiveInfinity => new DefaultInfinity<T>(Sign.Positive);

        /// <inheritdoc />
        public override Infinity<T> NegativeInfinity => new DefaultInfinity<T>(Sign.Negative);
    }

    public class DefaultBuilder<TIntervalBuilder, T> : DefaultBuilder<DefaultIntervalSet<TIntervalBuilder, T>, TIntervalBuilder, T>
        where TIntervalBuilder : IIntervalBuilder<IDefaultInterval<T>, T>, ISetBuilder<DefaultIntervalSet<TIntervalBuilder, T>, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        public override DefaultIntervalSet<TIntervalBuilder, T> MakeSet(IList<IDefaultInterval<T>> intervals)
        {
            return new DefaultIntervalSet<TIntervalBuilder, T>(intervals);
        }
    }

    public class DefaultBuilder<T> : DefaultBuilder<DefaultBuilder<T>, T>
        where T : IComparable<T>, IEquatable<T>
    {
    }
}
