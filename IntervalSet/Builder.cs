﻿using System;
using System.Collections.Generic;
using System.Linq;
using IntervalSet.Interval.Boundaries;

namespace IntervalSet
{
    /// <inheritdoc />
    public abstract class Builder<TSet, TInterval, T> : IBuilder<TSet, TInterval, T>
        where T : IEquatable<T>, IComparable<T>
    {
        /// <inheritdoc />
        public abstract TSet MakeSet(IList<TInterval> intervals);

        /// <inheritdoc />
        public abstract TInterval MakeNonEmptySet(IList<TInterval> intervals);

        /// <inheritdoc />
        public abstract TInterval MakeStartingInterval(Start<T> from);

        /// <inheritdoc />
        public abstract TInterval MakeEndingInterval(End<T> end);

        /// <inheritdoc />
        public abstract TInterval MakeStartEndingInterval(Start<T> from, End<T> to);

        /// <inheritdoc />
        public abstract TInterval MakeStartingInterval();

        /// <inheritdoc />
        public abstract TInterval MakeDegenerate(Degenerate<T> degenerate);

        /// <inheritdoc />
        public abstract Start<T> MakeStart(T from);

        /// <inheritdoc />
        public abstract End<T> MakeEnd(T to);

        /// <inheritdoc />
        public List<Boundary<T>> GroupBoundaries(IEnumerable<Boundary<T>> boundaries)
        {
            return boundaries
                .GroupBy(b => b.Location)
                .Select(g => new Boundary<T>(g.Key, g.Select(b => b.Kind).Aggregate((k1, k2) => k1.Plus(k2))))
                .Distinct()
                .OrderBy(b => b.Location)
                .ToList();
        }

        /// <inheritdoc />
        public IEnumerable<TInterval> Build(IList<Boundary<T>> boundaries, bool containsNegativeInfinity)
        {
            bool currentlyTrue = containsNegativeInfinity;
            Start<T> mostRecentStart = null;
            foreach (Boundary<T> boundary in GroupBoundaries(boundaries))
            {
                if (boundary.IsStart)
                {
                    if (!currentlyTrue)
                    {
                        mostRecentStart = new Start<T>(boundary);
                    }
                    else
                    {
                        if (boundary.IsEnd && !boundary.Inclusive)
                        {
                            if (mostRecentStart == null)
                            {
                                yield return MakeEndingInterval(new End<T>(boundary));
                            }
                            else
                            {
                                yield return MakeStartEndingInterval(mostRecentStart, new End<T>(boundary));
                            }

                            mostRecentStart = new Start<T>(boundary);
                        }
                    }
                    currentlyTrue = true;
                }
                else
                {
                    if (!currentlyTrue)
                    {
                        if (!boundary.IsEnd)
                        {
                            yield return MakeDegenerate(new Degenerate<T>(boundary.Location));
                        }
                    }
                    else
                    {
                        if (boundary.IsEnd)
                        {
                            if (mostRecentStart == null)
                            {
                                yield return MakeEndingInterval(new End<T>(boundary));
                            }
                            else
                            {
                                yield return MakeStartEndingInterval(mostRecentStart, new End<T>(boundary));
                            }
                            mostRecentStart = null;
                            currentlyTrue = false;
                        }
                    }
                }
            }

            if (currentlyTrue)
            {
                if (mostRecentStart != null)
                {
                    yield return MakeStartingInterval(mostRecentStart);
                }
                else
                {
                    yield return MakeStartingInterval();
                }
            }
        }

        /// <summary>
        /// The <typeparamref name="T"/> that is greater than all <typeparamref name="T"/>s
        /// </summary>
        protected abstract T PositiveInfinity { get; }

        /// <summary>
        /// The <typeparamref name="T"/> that is less than all <typeparamref name="T"/>s
        /// </summary>
        protected abstract T NegativeInfinity { get; }

        /// <inheritdoc />
        public Boundary<T> PositiveInfinityBoundary => new PositiveInfinity<T>(PositiveInfinity);

        /// <inheritdoc />
        public Boundary<T> NegativeInfinityBoundary => new NegativeInfinity<T>(NegativeInfinity);
    }
}
