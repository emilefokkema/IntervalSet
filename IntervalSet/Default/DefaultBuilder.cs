using System;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Default;

namespace IntervalSet.Default
{
    /// <summary>
    /// A default implementation of <see cref="IBuilder{TInterval,T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultBuilder<T> : Builder<IDefaultInterval<T>, T>
        where T : IComparable<T>, IEquatable<T>
    {
        /// <inheritdoc />
        public override IDefaultInterval<T> MakeStartingInterval<TBuilder>()
        {
            return new DefaultEntireInterval<TBuilder,T>();
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeStartingInterval<TBuilder>(Start<T> from)
        {
            return new DefaultStartingInterval<TBuilder,T>(from);
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeEndingInterval<TBuilder>(End<T> end)
        {
            return new DefaultEndingInterval<TBuilder,T>(end);
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeStartEndingInterval<TBuilder>(Start<T> from, End<T> to)
        {
            return new DefaultStartEndingInterval<TBuilder,T>(from, to);
        }

        /// <inheritdoc />
        public override IDefaultInterval<T> MakeDegenerate<TBuilder>(Degenerate<T> degenerate)
        {
            return new DefaultDegenerateInterval<TBuilder,T>(degenerate);
        }

        /// <inheritdoc />
        public override Infinity<T> PositiveInfinity => new DefaultInfinity<T>(Sign.Positive);

        /// <inheritdoc />
        public override Infinity<T> NegativeInfinity => new DefaultInfinity<T>(Sign.Negative);
    }
}
