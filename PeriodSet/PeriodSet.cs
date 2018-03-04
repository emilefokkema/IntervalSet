using System;
using System.Collections.Generic;
using System.Linq;
using PeriodSet.Period;
using PeriodSet.Period.Boundaries;
using PeriodSet.Period.Boundaries.Kind;

namespace PeriodSet
{
    /// <summary>
    /// A base class for implementations of <see cref="IPeriodSet"/>
    /// </summary>
    /// <typeparam name="TSet">the type of this implementation</typeparam>
    /// <typeparam name="TBuilder">the type of <see cref="Builder{TSet,TPeriod,TStartingPeriod}"/> for this implementation</typeparam>
    /// <typeparam name="TPeriod">the kind of connected period of time for this implementation</typeparam>
    /// <typeparam name="TStartingPeriod"></typeparam>
    public abstract class PeriodSet<TSet, TBuilder, TStartingPeriod, TPeriod> : IEnumerablePeriodSet<TPeriod>, IPeriodSet<TSet>, IEmptyOrNot<TPeriod>
        where TSet : IPeriodSet
        where TBuilder : IBuilder<TSet, TPeriod, TStartingPeriod>, new()
        where TStartingPeriod : class, TPeriod, IStartingPeriod<TPeriod>
    {   
        /// <summary>
        /// The <typeparamref name="TBuilder"/> for this instance
        /// </summary>
        protected readonly TBuilder Builder = new TBuilder();

        /// <inheritdoc />
        public virtual bool ContainsDate(DateTime date)
        {
            return true;
        }

        /// <inheritdoc />
        public virtual bool ContainsNegativeInfinity()
        {
            return true;
        }

        /// <summary>
        /// Returns a <typeparamref name="TPeriod"/> that can serve as the start of this <see cref="IPeriodSet"/>
        /// </summary>
        /// <returns></returns>
        protected TStartingPeriod GetStart()
        {
            if (ContainsNegativeInfinity())
            {
                return Builder.MakeStartingPeriod();
            }

            return null;
        }

        /// <inheritdoc />
        public virtual bool ContainsPeriod(DateTime from, DateTime to)
        {
            return true;
        }

        /// <inheritdoc />
        public virtual IEnumerable<Boundary> Boundaries { get { yield break; } }

        /// <inheritdoc />
        public TSet Where(Func<DateTime, bool> trueFrom, IList<DateTime> changes = null)
        {
            if (changes != null && changes.Count > 0)
            {
                List<Boundary> whereBoundaries = changes.Select(c =>
                    trueFrom(c) && ContainsDate(c) ? (Boundary)new Start(c, Inclusivity.Inclusive) : new End(c, Inclusivity.Exclusive)).ToList();
                return Builder.MakeSet(Builder.Build(whereBoundaries, null).ToList());
            }

            return Builder.MakeSet(Builder.Build(Boundaries.ToList(), GetStart()).ToList());
        }

        /// <inheritdoc />
        public TSet Where(Func<DateTime, DateTime, bool> trueEverywhereBetween, IList<DateTime> changes = null)
        {
            changes = (changes ?? new List<DateTime>()).Concat(Boundaries.Select(b => b.Date)).OrderBy(d => d).ToList();
            
                List<Boundary> boundaries = new List<Boundary>();
                foreach (Tuple<DateTime,DateTime> tuple in changes.Zip(changes.Skip(1), (d1,d2)=>new Tuple<DateTime,DateTime>(d1,d2)))
                {
                    if (trueEverywhereBetween(tuple.Item1,tuple.Item2) && ContainsPeriod(tuple.Item1,tuple.Item2))
                    {
                        boundaries.Add(new Start(tuple.Item1, Inclusivity.Inclusive));
                        boundaries.Add(new End(tuple.Item2, Inclusivity.Exclusive));
                    }
                }
                return Builder.MakeSet(Builder.Build(boundaries, null).ToList());
           
        }

        /// <inheritdoc />
        public TSet Minus(IPeriodSet other)
        {
            List<Boundary> minusBoundaries = Boundaries.Where(b => !other.ContainsDate(b.Date))
                .Concat(MinusBoundaries(other)).ToList();
            return Builder.MakeSet(Builder.Build(minusBoundaries, MinusStart(other)).ToList());
        }

        private TStartingPeriod MinusStart(IPeriodSet other)
        {
            if (other.ContainsNegativeInfinity())
            {
                return null;
            }

            return GetStart();
        }

        private TStartingPeriod PlusStart(IPeriodSet other)
        {
            if (other.ContainsNegativeInfinity())
            {
                return Builder.MakeStartingPeriod();
            }

            return GetStart();
        }

        private TStartingPeriod CrossStart(IPeriodSet other)
        {
            if (!other.ContainsNegativeInfinity())
            {
                return null;
            }

            return GetStart();
        }

        private IEnumerable<Boundary> MinusBoundaries(IPeriodSet other)
        {
            foreach (Boundary otherBoundary in other.Boundaries)
            {
                BoundaryKind minusKind = Cross(otherBoundary.Date)?.Minus(otherBoundary.Kind);
                if (minusKind != null)
                {
                    yield return new Boundary(otherBoundary.Date, minusKind);
                }
            }
        }

        private static IEnumerable<Boundary> PlusBoundaries(IPeriodSet one, IPeriodSet other)
        {
            foreach (Boundary otherBoundary in other.Boundaries)
            {
                BoundaryKind cross = one.Cross(otherBoundary.Date);
                if (cross == null)
                {
                    yield return otherBoundary;
                }
                else
                {
                    BoundaryKind plusKind = cross.Plus(otherBoundary.Kind);
                    yield return new Boundary(otherBoundary.Date, plusKind);
                }
            }
        }

        private static IEnumerable<Boundary> CrossBoundaries(IPeriodSet one, IPeriodSet other)
        {
            foreach (Boundary otherBoundary in other.Boundaries)
            {
                BoundaryKind cross = one.Cross(otherBoundary.Date)?.Cross(otherBoundary.Kind);
                if (cross != null)
                {
                    yield return new Boundary(otherBoundary.Date, cross);
                }
            }
        }

        /// <inheritdoc />
        public TSet Plus(IPeriodSet other)
        {
            List<Boundary> plusBoundaries = PlusBoundaries(this, other).Concat(PlusBoundaries(other, this)).ToList();
            return Builder.MakeSet(Builder.Build(plusBoundaries, PlusStart(other)).ToList());
        }

        /// <inheritdoc />
        public TSet Cross(IPeriodSet other)
        {
            List<Boundary> crossBoundaries = CrossBoundaries(this, other).Concat(CrossBoundaries(other, this)).ToList();
            return Builder.MakeSet(Builder.Build(crossBoundaries, CrossStart(other)).ToList());
        }

        IPeriodSet IPeriodSet.Where(Func<DateTime, bool> trueFrom, IList<DateTime> changes)
        {
            return Where(trueFrom, changes);
        }

        IPeriodSet IPeriodSet.Where(Func<DateTime, DateTime, bool> trueEverywhereBetween, IList<DateTime> changes)
        {
            return Where(trueEverywhereBetween, changes);
        }

        IPeriodSet IPeriodSet.Minus(IPeriodSet other)
        {
            return Minus(other);
        }

        IPeriodSet IPeriodSet.Plus(IPeriodSet other)
        {
            return Plus(other);
        }

        IPeriodSet IPeriodSet.Cross(IPeriodSet other)
        {
            return Cross(other);
        }

        /// <inheritdoc />
        public virtual bool IsNonEmpty(out TPeriod nonEmpty)
        {
            nonEmpty = GetStart();
            return true;
        }

        /// <inheritdoc />
        public virtual bool IsEmpty => false;

        /// <inheritdoc />
        public virtual int PeriodCount => 1;

        /// <inheritdoc />
        public virtual BoundaryKind Cross(DateTime date)
        {
            return new ContinuationKind();
        }

        /// <inheritdoc />
        public bool Intersects(IPeriodSet other)
        {
            return !other.Cross(this).IsEmpty;
        }

        /// <inheritdoc />
        public virtual IEnumerable<TT> Select<TT>(Func<TPeriod, TT> selector) where TT : class
        {
            yield return selector(GetStart());
        }

        /// <inheritdoc />
        public virtual void ForEach(Action<TPeriod> what)
        {
            what(GetStart());
        }

        /// <inheritdoc />
        public bool Equals(IPeriodSet other)
        {
            if (other == null)
            {
                return false;
            }
            return Minus(other).IsEmpty && other.Minus(this).IsEmpty;
        }

        /// <inheritdoc />
        public override bool Equals(object other)
        {
            if (other is IPeriodSet)
            {
                return Equals((IPeriodSet)other);
            }
            return false;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return int.MaxValue;
        }

        /// <inheritdoc />
        public virtual string ToString(string format, IFormatProvider provider)
        {
            return "(-Infinity, Infinity)";
        }
    }
}
