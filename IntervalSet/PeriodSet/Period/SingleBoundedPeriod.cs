using System;
using System.Collections.Generic;
using IntervalSet.PeriodSet.Period.Boundaries;
using IntervalSet.PeriodSet.Period.Boundaries.Kind;

namespace IntervalSet.PeriodSet.Period
{
    /// <summary>
    /// A base class for implementations of an <see cref="IPeriodSet"/> representing period of time with a single boundary
    /// </summary>
    /// <typeparam name="TSet">the kind of <see cref="IPeriodSet"/> that contains this kind of period</typeparam>
    /// <typeparam name="TBuilder">the kind of <see cref="IBuilder{TSet,TPeriod,TStartingPeriod}"/> that will produce this kind of period</typeparam>
    /// <typeparam name="TPeriod">the type of this period</typeparam>
    /// <typeparam name="TStartingPeriod"></typeparam>
    public abstract class SingleBoundedPeriod<TSet, TBuilder, TStartingPeriod, TPeriod> : PeriodSet<TSet, TBuilder, TStartingPeriod, TPeriod>
        where TSet : IPeriodSet
        where TBuilder : IBuilder<TSet, TPeriod, TStartingPeriod>, new()
        where TStartingPeriod : class, TPeriod, IStartingPeriod<TPeriod>
    {
        /// <summary>
        /// returns a typed version of this instance
        /// </summary>
        /// <returns></returns>
        protected abstract TPeriod GetPeriod();

        public override bool ContainsNegativeInfinity()
        {
            return Boundary.IsEnd;
        }

        /// <summary>
        /// The boundary of this period
        /// </summary>
        public Boundary Boundary { get; }

        /// <summary>
        /// Initializes a new <see cref="SingleBoundedPeriod{TSet,TListBuilder,TStartingPeriod,TPeriod}"/> with a given boundary
        /// </summary>
        /// <param name="boundary"></param>
        protected SingleBoundedPeriod(Boundary boundary)
        {
            Boundary = boundary;
        }

        /// <inheritdoc />
        public override bool ContainsDate(DateTime date)
        {
            if (date == Boundary.Date)
            {
                return Boundary.Inclusive;
            }
            return date > Boundary.Date && Boundary.IsStart || date < Boundary.Date && Boundary.IsEnd;
        }

        /// <inheritdoc />
        public override bool ContainsPeriod(DateTime from, DateTime to)
        {
            if (from >= Boundary.Date)
            {
                return ContainsDate(from);
            }
            if (to <= Boundary.Date)
            {
                return ContainsDate(to);
            }
            return Boundary.IsContinuation;
        }

        /// <inheritdoc />
        public override IEnumerable<Boundary> Boundaries
        {
            get { yield return Boundary; }
        }

        /// <inheritdoc />
        public override bool IsNonEmpty(out TPeriod nonEmpty)
        {
            nonEmpty = GetPeriod();
            return true;
        }

        /// <summary>
        /// A connected period of time is never empty
        /// </summary>
        public override bool IsEmpty => false;

        /// <summary>
        /// Namely, this one
        /// </summary>
        public override int PeriodCount => 1;

        /// <inheritdoc />
        public override BoundaryKind Cross(DateTime date)
        {
            if (date == Boundary.Date)
            {
                return Boundary.Kind;
            }
            if (date > Boundary.Date && Boundary.IsStart || date < Boundary.Date && Boundary.IsEnd)
            {
                return new ContinuationKind();
            }
            return null;
        }

        /// <inheritdoc />
        public override IEnumerable<TT> Select<TT>(Func<TPeriod, TT> selector)
        {
            yield return selector(GetPeriod());
        }

        /// <inheritdoc />
        public override void ForEach(Action<TPeriod> what)
        {
            what(GetPeriod());
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Boundary.GetHashCode();
        }

        /// <inheritdoc />
        public override string ToString(string format, IFormatProvider formatProvider)
        {
            string boundaryString = Boundary.ToString(format, formatProvider);
            if (Boundary.IsContinuation)
            {
                return "(-Infinity, Infinity)";
            }
            if (Boundary.IsEnd)
            {
                if (Boundary.IsStart)
                {
                    return "(-Infinity, " + boundaryString + ", Infinity)";
                }
                return "(-Infinity, " + boundaryString;
            }
            if (Boundary.IsStart)
            {
                return boundaryString + ", Infinity)";
            }
            return boundaryString;
        }
    }
}
