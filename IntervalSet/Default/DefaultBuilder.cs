using System;
using System.Collections.Generic;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Boundaries.Kind;
using IntervalSet.Interval.Default;

namespace IntervalSet.Default
{
    /// <summary>
    /// A default implementation of <see cref="IBuilder{TSet,TInterval,T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TSet"></typeparam>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class DefaultBuilder<TSet, TBuilder, T> : Builder<TSet, IDefaultInterval<T>, T>
        where TSet : IIntervalSet<T>
        where TBuilder : IBuilder<TSet, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <inheritdoc />
        protected override T PositiveInfinity => default(T);

        /// <inheritdoc />
        protected override T NegativeInfinity => default(T);

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeStartingInterval()
        {
            return new DefaultEntireInterval<TSet, TBuilder, T>();
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeStartingInterval(Start<T> @from)
        {
            return new DefaultStartingInterval<TSet, TBuilder, T>(from);
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeStartEndingInterval(Start<T> @from, End<T> to)
        {
            return new DefaultStartEndingInterval<T>(from, to);
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeEndingInterval(End<T> end)
        {
            return new DefaultEndingInterval<TSet, TBuilder, T>(end);
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeDegenerate(Degenerate<T> degenerate)
        {
            return new DefaultDegenerateInterval<TSet, TBuilder, T>(degenerate);
        }

        /// <inheritdoc />
        public override Start<T> MakeStart(T @from)
        {
            return new Start<T>(from, Inclusivity.Inclusive);
        }

        /// <inheritdoc />
        public override End<T> MakeEnd(T to)
        {
            return new End<T>(to, Inclusivity.Exclusive);
        }
    }

    /// <summary>
    /// A default implementation of <see cref="DefaultBuilder{TSet,TBuilder,T}"/> where <c>TSet</c> is <see cref="DefaultIntervalSet{TBuilder,T}"/>
    /// </summary>
    public class DefaultBuilder<TBuilder, T> : DefaultBuilder<DefaultIntervalSet<TBuilder, T>, DefaultBuilder<TBuilder, T>, T>
        where TBuilder : IBuilder<DefaultIntervalSet<TBuilder, T>, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <inheritdoc />
        public override DefaultIntervalSet<TBuilder, T> MakeSet(IList<IDefaultInterval<T>> intervals)
        {
            return new DefaultIntervalSet<TBuilder, T>(intervals);
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeNonEmptySet(IList<IDefaultInterval<T>> intervals)
        {
            return new DefaultNonEmptyIntervalSet<TBuilder, T>(intervals);
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeStartingInterval()
        {
            return new DefaultEntireInterval<TBuilder, T>();
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeStartingInterval(Start<T> from)
        {
            return new DefaultStartingInterval<TBuilder, T>(from);
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeEndingInterval(End<T> end)
        {
            return new DefaultEndingInterval<TBuilder, T>(end);
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeStartEndingInterval(Start<T> from, End<T> to)
        {
            return new DefaultStartEndingInterval<TBuilder, T>(from, to);
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeDegenerate(Degenerate<T> degenerate)
        {
            return new DefaultDegenerateInterval<TBuilder, T>(degenerate);
        }
    }

    /// <summary>
    /// A default implementation of <see cref="DefaultBuilder{TBuilder,T}"/> where <c>TBuilder</c> is <see cref="DefaultBuilder{T}"/>
    /// </summary>
    public class DefaultBuilder<T> : DefaultBuilder<DefaultIntervalSet<T>, DefaultBuilder<T>, T>
        where T : IComparable<T>, IEquatable<T>
    {
        /// <inheritdoc />
        public override DefaultIntervalSet<T> MakeSet(IList<IDefaultInterval<T>> intervals)
        {
            return new DefaultIntervalSet<T>(intervals);
        }

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
    }
}
