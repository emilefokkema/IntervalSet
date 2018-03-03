using System;
using System.Collections.Generic;
using IntervalSet.PeriodSet.Period;
using IntervalSet.PeriodSet.Period.Boundaries.Kind;

namespace IntervalSet.PeriodSet
{
    /// <summary>
    /// Base class for implementations of <see cref="IPeriodSet"/> that do not represent the entire <see cref="DateTime"/> space
    /// </summary>
    /// <typeparam name="TSet"></typeparam>
    /// <typeparam name="TBuilder"></typeparam>
    /// <typeparam name="TStartingPeriod"></typeparam>
    /// <typeparam name="TPeriod"></typeparam>
    public abstract class EmptyPeriodSet<TSet, TBuilder, TStartingPeriod, TPeriod> : PeriodSet<TSet, TBuilder, TStartingPeriod, TPeriod>
        where TSet : IPeriodSet
        where TBuilder : IBuilder<TSet, TPeriod, TStartingPeriod>, new()
        where TStartingPeriod : class, TPeriod, IStartingPeriod<TPeriod>
    {
        /// <inheritdoc />
        public override bool ContainsNegativeInfinity()
        {
            return false;
        }

        /// <inheritdoc />
        public override bool ContainsDate(DateTime date)
        {
            return false;
        }

        /// <inheritdoc />
        public override bool ContainsPeriod(DateTime from, DateTime to)
        {
            return false;
        }

        /// <inheritdoc />
        public override bool IsNonEmpty(out TPeriod nonEmpty)
        {
            nonEmpty = default(TPeriod);
            return false;
        }

        /// <inheritdoc />
        public override bool IsEmpty => true;

        /// <inheritdoc />
        public override int PeriodCount => 0;

        /// <inheritdoc />
        public override BoundaryKind Cross(DateTime date)
        {
            return null;
        }

        /// <inheritdoc />
        public override IEnumerable<TT> Select<TT>(Func<TPeriod, TT> selector)
        {
            yield break;
        }

        /// <inheritdoc />
        public override void ForEach(Action<TPeriod> what)
        {
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return 0;
        }

        /// <inheritdoc />
        public override string ToString(string format, IFormatProvider provider)
        {
            return "(empty)";
        }
    }
}
