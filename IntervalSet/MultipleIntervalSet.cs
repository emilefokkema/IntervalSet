using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Boundaries.Kind;

namespace IntervalSet
{
    /// <inheritdoc cref="EmptyIntervalSet{TSet,TBuilder,TInterval,T}"/>
    /// <summary>
    /// A subset of the <typeparamref name="T"/> space consisting of zero or more <see cref="IIntervalSet{T}"/>s
    /// </summary>
    public abstract class MultipleIntervalSet<TSet, TIntervalBuilder, TInterval, T> : EmptyIntervalSet<TSet, TIntervalBuilder, TInterval, T>
        where TSet : IIntervalSet<T>
        where TIntervalBuilder : IIntervalBuilder<TInterval, T>, ISetBuilder<TSet, TInterval, T>, new()
        where TInterval : IIntervalSet<T>
        where T : IComparable<T>, IEquatable<T>
    {
        private readonly SerializationInfo _info;
        /// <inheritdoc />
        public override bool ContainsNegativeInfinity()
        {
            return IntervalList.Any(p => p.ContainsNegativeInfinity());
        }

        /// <summary>
        /// The list of <typeparamref name="TInterval"/>s for this instance
        /// </summary>
        protected IList<TInterval> IntervalList => _intervalList;

        private IList<TInterval> _intervalList = new List<TInterval>();

        /// <summary>
        /// Initializes a new <see cref="MultipleIntervalSet{TSet,TBuilder,TInterval,T}"/>
        /// </summary>
        protected MultipleIntervalSet() : this(new List<TInterval>())
        {
        }

        /// <summary>
        /// Initializes a new <see cref="MultipleIntervalSet{TSet,TBuilder,TInterval,T}"/> based on a given <see cref="IIntervalSet{T}"/>
        /// </summary>
        /// <param name="set"></param>
        protected MultipleIntervalSet(IIntervalSet<T> set) : this()
        {
            _intervalList = IntervalBuilder.Build(set.Boundaries.ToList(), set.ContainsNegativeInfinity()).ToList();
        }

        /// <summary>
        /// Deserializes a <see cref="MultipleIntervalSet{TSet,TBuilder,TInterval,T}"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected MultipleIntervalSet(SerializationInfo info, StreamingContext context)
        {
            _info = info;
        }

        /// <summary>
        /// Only create <see cref="IntervalList"/> after deserialization of the boundaries is complete
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized]
        public void OnDeserialization(StreamingContext context)
        {
            _intervalList = MakeIntervals(_info);
        }

        /// <summary>
        /// Initializes a new <see cref="MultipleIntervalSet{TSet,TBuilder,TInterval,T}"/> based on a given list of <typeparamref name="TInterval"/>s
        /// </summary>
        /// <param name="list"></param>
        protected MultipleIntervalSet(IList<TInterval> list)
        {
            _intervalList = list;
        }

        /// <summary>
        /// Initializes a new <see cref="MultipleIntervalSet{TSet,TBuilder,TInterval,T}"/> containing a <typeparamref name="TInterval"/> with a given start and end
        /// </summary>
        protected MultipleIntervalSet(Start<T> from, End<T> to) : this()
        {
            if (from.Location.Equals(to.Location))
            {
                IntervalList.Add(IntervalBuilder.MakeDegenerate(new Degenerate<T>(from.Location)));
            }
            else
            {
                IntervalList.Add(IntervalBuilder.MakeStartEndingInterval(from, to));
            }
        }

        /// <summary>
        /// Initializes a new <see cref="MultipleIntervalSet{TSet,TBuilder,TInterval,T}"/> containing a <typeparamref name="TInterval"/> with a given start
        /// </summary>
        protected MultipleIntervalSet(Start<T> from) : this()
        {
            IntervalList.Add(IntervalBuilder.MakeStartingInterval(from));
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            if (IntervalList.Count == 0)
            {
                return 0;
            }

            if (IntervalList.Count == 1)
            {
                return IntervalList.ElementAt(0).GetHashCode();
            }

            unchecked
            {
                int result = 17;
                foreach (TInterval interval in IntervalList)
                {
                    result = result * 31 + interval.GetHashCode();
                }
                return result;
            }
        }

        /// <inheritdoc />
        public override string ToString(string format, IFormatProvider provider)
        {
            if (IntervalList.Count == 0)
            {
                return base.ToString(format, provider);
            }

            return string.Join(" + ", IntervalList.Select(p => p.ToString(format, provider)));
        }

        /// <inheritdoc />
        public override bool IsNonEmpty(out TInterval nonEmpty)
        {
            if (IntervalList.Any())
            {
                nonEmpty = IntervalBuilder.MakeNonEmptySet(IntervalList);
                return true;
            }
            nonEmpty = default(TInterval);
            return false;
        }

        /// <inheritdoc />
        public override bool IsEmpty => !IntervalList.Any();

        /// <inheritdoc />
        public override int IntervalCount => IntervalList.Count;

        /// <inheritdoc />
        public override bool Contains(T item)
        {
            return IntervalList.Any(p => p.Contains(item));
        }

        /// <inheritdoc />
        public override BoundaryKind Cross(T location)
        {
            return IntervalList.Select(p => p.Cross(location)).FirstOrDefault(b => b != null);
        }

        /// <inheritdoc />
        public override bool ContainsInterval(T from, T to)
        {
            return IntervalList.Any(p => p.ContainsInterval(from, to));
        }

        /// <inheritdoc />
        public override IEnumerable<Boundary<T>> Boundaries => IntervalList.SelectMany(p => p.Boundaries);

        /// <inheritdoc />
        public override IEnumerable<TT> Select<TT>(Func<TInterval, TT> selector)
        {
            return IntervalList.Select(selector);
        }

        /// <inheritdoc />
        public override void ForEach(Action<TInterval> what)
        {
            foreach (TInterval interval in IntervalList)
            {
                what(interval);
            }
        }
    }
}
