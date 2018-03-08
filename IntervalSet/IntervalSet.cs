using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Boundaries.Kind;

namespace IntervalSet
{
    /// <summary>
    /// A base class for implementations of <see cref="IIntervalSet{T}"/>
    /// </summary>
    public abstract class IntervalSet<TSet, TIntervalBuilder, TInterval, T> : IEnumerableIntervalSet<TInterval, T>, IIntervalSet<TSet, T>, IEmptyOrNot<TInterval>
        where TSet : IIntervalSet<T>
        where TIntervalBuilder : IIntervalBuilder<TInterval, T>, ISetBuilder<TSet, TInterval, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// The <typeparamref name="TIntervalBuilder"/> for this instance
        /// </summary>
        protected readonly TIntervalBuilder IntervalBuilder = new TIntervalBuilder();

        /// <inheritdoc />
        public virtual bool Contains(T item)
        {
            return true;
        }

        /// <inheritdoc />
        public virtual bool ContainsNegativeInfinity()
        {
            return true;
        }

        /// <inheritdoc />
        public virtual bool ContainsInterval(T from, T to)
        {
            return true;
        }

        /// <inheritdoc />
        public virtual IEnumerable<Boundary<T>> Boundaries { get { yield break; } }

        /// <inheritdoc />
        public TSet Where(Func<T, bool> trueFrom, IList<T> changes = null)
        {
            if (changes != null && changes.Count > 0)
            {
                List<Boundary<T>> whereBoundaries = changes.Select(c =>
                    trueFrom(c) && Contains(c) ? (Boundary<T>)new Start<T>(c, Inclusivity.Inclusive) : new End<T>(c, Inclusivity.Exclusive)).ToList();
                return IntervalBuilder.MakeSet(IntervalBuilder.Build(whereBoundaries, false).ToList());
            }

            return IntervalBuilder.MakeSet(IntervalBuilder.Build(Boundaries.ToList(), ContainsNegativeInfinity()).ToList());
        }

        /// <inheritdoc />
        public TSet Where(Func<T, T, bool> trueEverywhereBetween, IList<T> changes = null)
        {
            changes = (changes ?? new List<T>()).Concat(Boundaries.Select(b => b.Location)).OrderBy(d => d).ToList();

            List<Boundary<T>> boundaries = new List<Boundary<T>>();
            foreach (Tuple<T, T> tuple in changes.Zip(changes.Skip(1), (d1, d2) => new Tuple<T, T>(d1, d2)))
            {
                if (trueEverywhereBetween(tuple.Item1, tuple.Item2) && ContainsInterval(tuple.Item1, tuple.Item2))
                {
                    boundaries.Add(new Start<T>(tuple.Item1, Inclusivity.Inclusive));
                    boundaries.Add(new End<T>(tuple.Item2, Inclusivity.Exclusive));
                }
            }
            return IntervalBuilder.MakeSet(IntervalBuilder.Build(boundaries, false).ToList());
        }

        /// <inheritdoc />
        public TSet Minus(IIntervalSet<T> other)
        {
            List<Boundary<T>> minusBoundaries = Boundaries.Where(b => !other.Contains(b.Location))
                .Concat(MinusBoundaries(other)).ToList();
            return IntervalBuilder.MakeSet(IntervalBuilder.Build(minusBoundaries, !other.ContainsNegativeInfinity() && ContainsNegativeInfinity()).ToList());
        }

        private IEnumerable<Boundary<T>> MinusBoundaries(IIntervalSet<T> other)
        {
            foreach (Boundary<T> otherBoundary in other.Boundaries)
            {
                BoundaryKind minusKind = Cross(otherBoundary.Location)?.Minus(otherBoundary.Kind);
                if (minusKind != null)
                {
                    yield return new Boundary<T>(otherBoundary.Location, minusKind);
                }
            }
        }

        private static IEnumerable<Boundary<T>> PlusBoundaries(IIntervalSet<T> one, IIntervalSet<T> other)
        {
            foreach (Boundary<T> otherBoundary in other.Boundaries)
            {
                BoundaryKind cross = one.Cross(otherBoundary.Location);
                if (cross == null)
                {
                    yield return otherBoundary;
                }
                else
                {
                    BoundaryKind plusKind = cross.Plus(otherBoundary.Kind);
                    yield return new Boundary<T>(otherBoundary.Location, plusKind);
                }
            }
        }

        private static IEnumerable<Boundary<T>> CrossBoundaries(IIntervalSet<T> one, IIntervalSet<T> other)
        {
            foreach (Boundary<T> otherBoundary in other.Boundaries)
            {
                BoundaryKind cross = one.Cross(otherBoundary.Location)?.Cross(otherBoundary.Kind);
                if (cross != null)
                {
                    yield return new Boundary<T>(otherBoundary.Location, cross);
                }
            }
        }

        /// <inheritdoc />
        public TSet Plus(IIntervalSet<T> other)
        {
            List<Boundary<T>> plusBoundaries = PlusBoundaries(this, other).Concat(PlusBoundaries(other, this)).ToList();
            return IntervalBuilder.MakeSet(IntervalBuilder.Build(plusBoundaries, ContainsNegativeInfinity() || other.ContainsNegativeInfinity()).ToList());
        }

        /// <inheritdoc />
        public TSet Cross(IIntervalSet<T> other)
        {
            List<Boundary<T>> crossBoundaries = CrossBoundaries(this, other).Concat(CrossBoundaries(other, this)).ToList();
            return IntervalBuilder.MakeSet(IntervalBuilder.Build(crossBoundaries, ContainsNegativeInfinity() && other.ContainsNegativeInfinity()).ToList());
        }

        IIntervalSet<T> IIntervalSet<T>.Where(Func<T, bool> trueFrom, IList<T> changes)
        {
            return Where(trueFrom, changes);
        }

        IIntervalSet<T> IIntervalSet<T>.Where(Func<T, T, bool> trueEverywhereBetween, IList<T> changes)
        {
            return Where(trueEverywhereBetween, changes);
        }

        IIntervalSet<T> IIntervalSet<T>.Minus(IIntervalSet<T> other)
        {
            return Minus(other);
        }

        IIntervalSet<T> IIntervalSet<T>.Plus(IIntervalSet<T> other)
        {
            return Plus(other);
        }

        IIntervalSet<T> IIntervalSet<T>.Cross(IIntervalSet<T> other)
        {
            return Cross(other);
        }

        /// <inheritdoc />
        public virtual bool IsNonEmpty(out TInterval nonEmpty)
        {
            nonEmpty = IntervalBuilder.MakeStartingInterval();
            return true;
        }

        /// <inheritdoc />
        public virtual bool IsEmpty => false;

        /// <inheritdoc />
        public virtual int IntervalCount => 1;

        /// <inheritdoc />
        public virtual BoundaryKind Cross(T location)
        {
            return new ContinuationKind();
        }

        /// <inheritdoc />
        public bool Intersects(IIntervalSet<T> other)
        {
            return !other.Cross(this).IsEmpty;
        }

        /// <inheritdoc />
        public virtual IEnumerable<TT> Select<TT>(Func<TInterval, TT> selector) where TT : class
        {
            yield return selector(IntervalBuilder.MakeStartingInterval());
        }

        /// <inheritdoc />
        public virtual void ForEach(Action<TInterval> what)
        {
            what(IntervalBuilder.MakeStartingInterval());
        }

        /// <inheritdoc />
        public bool Equals(IIntervalSet<T> other)
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
            if (other is IIntervalSet<T>)
            {
                return Equals((IIntervalSet<T>)other);
            }
            return false;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return int.MaxValue;
        }

        /// <inheritdoc />
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            List<Boundary<T>> boundaries = Boundaries.ToList();
            info.AddValue("ContainsNegativeInfinity", ContainsNegativeInfinity());
            info.AddValue("Boundaries", boundaries);
        }

        /// <inheritdoc />
        public virtual string ToString(string format, IFormatProvider provider)
        {
            return $"{IntervalBuilder.NegativeInfinityBoundary.ToString(format, provider)}, {IntervalBuilder.PositiveInfinityBoundary.ToString(format, provider)}";
        }

        /// <summary>
        /// Deserializes a list of <typeparamref name="TInterval"/>s
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected static IList<TInterval> MakeIntervals(SerializationInfo info)
        {
            TIntervalBuilder builder = new TIntervalBuilder();
            bool containsNegativeInfinity = (bool)info.GetValue("ContainsNegativeInfinity", typeof(bool));
            List<Boundary<T>> boundaries = (List<Boundary<T>>)info.GetValue("Boundaries", typeof(List<Boundary<T>>));
            return builder.Build(boundaries, containsNegativeInfinity).ToList();
        }
    }
}
