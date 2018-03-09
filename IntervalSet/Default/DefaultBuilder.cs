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
    /// <typeparam name="TSet"></typeparam>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class AbstractDefaultBuilder<TSet, TBuilder, T> : Builder<TSet, IDefaultInterval<T>, T>
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
    }

    /// <summary>
    /// A default implementation of <see cref="AbstractDefaultBuilder{TSet,TBuilder,T}"/> where <c>TSet</c> is <see cref="DefaultIntervalSet{TIntervalBuilder,T}"/>
    /// </summary>
    public class DefaultBuilder<TIntervalBuilder, T> : AbstractDefaultBuilder<DefaultIntervalSet<TIntervalBuilder, T>, DefaultBuilder<TIntervalBuilder, T>, T>
        where TIntervalBuilder : IBuilder<DefaultIntervalSet<TIntervalBuilder, T>, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <inheritdoc />
        public override DefaultIntervalSet<TIntervalBuilder, T> MakeSet(IList<IDefaultInterval<T>> intervals)
        {
            return new DefaultIntervalSet<TIntervalBuilder, T>(intervals);
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeNonEmptySet(IList<IDefaultInterval<T>> intervals)
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

    /// <summary>
    /// A default implementation of <see cref="DefaultBuilder{TIntervalBuilder,T}"/> where <c>TIntervalBuilder</c> is <see cref="DefaultBuilder{T}"/>
    /// </summary>
    public class DefaultBuilder<T> : AbstractDefaultBuilder<DefaultIntervalSet<T>, DefaultBuilder<T>, T>
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
