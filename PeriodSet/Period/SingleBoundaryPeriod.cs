using System;
using System.Collections.Generic;
using IntervalSet;
using IntervalSet.Interval;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Boundaries.Kind;

namespace PeriodSet.Period
{
    /// <summary>
    /// A base class for implementations of an <see cref="IPeriodSet"/> representing period of time with a single boundary
    /// </summary>
    /// <typeparam name="TSet">the kind of <see cref="IPeriodSet"/> that contains this kind of period</typeparam>
    /// <typeparam name="TBuilder">the kind of <see cref="IBuilder{TSet,TInterval,TStartingInterval,T}"/> that will produce this kind of period</typeparam>
    /// <typeparam name="TPeriod">the type of this period</typeparam>
    /// <typeparam name="TStartingPeriod"></typeparam>
    public abstract class SingleBoundaryPeriod<TSet, TBuilder, TStartingPeriod, TPeriod> : PeriodSet<TSet, TBuilder, TStartingPeriod, TPeriod>
        where TSet : IPeriodSet
        where TBuilder : IBuilder<TSet, TPeriod, TStartingPeriod, DateTime>, new()
        where TStartingPeriod : class, TPeriod, IStartingInterval<TPeriod, DateTime>
    {
        /// <summary>
        /// returns a typed version of this instance
        /// </summary>
        /// <returns></returns>
        protected abstract TPeriod GetPeriod();

        /// <inheritdoc />
        public override bool ContainsNegativeInfinity()
        {
            return Boundary.IsEnd;
        }

        /// <summary>
        /// The boundary of this period
        /// </summary>
        public Boundary<DateTime> Boundary { get; }

        /// <summary>
        /// Initializes a new <see cref="SingleBoundaryPeriod{TSet,TBuilder,TStartingPeriod,TPeriod}"/> with a given boundary
        /// </summary>
        /// <param name="boundary"></param>
        protected SingleBoundaryPeriod(Boundary<DateTime> boundary)
        {
            Boundary = boundary;
        }

        /// <inheritdoc />
        public override bool ContainsDate(DateTime date)
        {
            if (date == Boundary.Location)
            {
                return Boundary.Inclusive;
            }
            return date > Boundary.Location && Boundary.IsStart || date < Boundary.Location && Boundary.IsEnd;
        }

        /// <inheritdoc />
        public override bool ContainsPeriod(DateTime from, DateTime to)
        {
            if (from >= Boundary.Location)
            {
                return Boundary.IsStart;
            }
            if (to <= Boundary.Location)
            {
                return Boundary.IsEnd;
            }
            return Boundary.IsContinuation;
        }

        /// <inheritdoc />
        public override IEnumerable<Boundary<DateTime>> Boundaries
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
            if (date == Boundary.Location)
            {
                return Boundary.Kind;
            }
            if (date > Boundary.Location && Boundary.IsStart || date < Boundary.Location && Boundary.IsEnd)
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
