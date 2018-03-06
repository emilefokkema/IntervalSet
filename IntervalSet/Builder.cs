using System;
using System.Collections.Generic;
using System.Linq;
using IntervalSet.Interval.Boundaries;

namespace IntervalSet
{
    /// <inheritdoc />
    public abstract class Builder<TInterval, T> : IBuilder<TInterval, T>
        where T : IEquatable<T>, IComparable<T>
    {
        /// <inheritdoc />
        public abstract TInterval MakeStartingInterval<TBuilder>(Start<T> from) where TBuilder : IBuilder<TInterval,T>, new();

        /// <inheritdoc />
        public abstract TInterval MakeEndingInterval<TBuilder>(End<T> end) where TBuilder : IBuilder<TInterval, T>, new();

        /// <inheritdoc />
        public abstract TInterval MakeStartEndingInterval<TBuilder>(Start<T> from, End<T> to) where TBuilder : IBuilder<TInterval, T>, new();

        /// <inheritdoc />
        public abstract TInterval MakeStartingInterval<TBuilder>() where TBuilder : IBuilder<TInterval, T>, new();

        /// <inheritdoc />
        public abstract TInterval MakeDegenerate<TBuilder>(Degenerate<T> degenerate) where TBuilder : IBuilder<TInterval, T>, new();

        private List<Boundary<T>> OrderBoundaries(IList<Boundary<T>> boundaries)
        {
            return boundaries
                .GroupBy(b => b.Location)
                .Select(g => new Boundary<T>(g.Key, g.Select(b => b.Kind).Aggregate((k1, k2) => k1.Plus(k2))))
                .Distinct()
                .OrderBy(b => b.Location)
                .ToList();
        }

        /// <inheritdoc />
        public IEnumerable<TInterval> Build<TBuilder>(IList<Boundary<T>> boundaries, bool containsNegativeInfinity) where TBuilder : IBuilder<TInterval, T>, new()
        {
            bool currentlyTrue = containsNegativeInfinity;
            Start<T> mostRecentStart = null;
            foreach (Boundary<T> boundary in OrderBoundaries(boundaries))
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
                                yield return MakeEndingInterval<TBuilder>(new End<T>(boundary));
                            }
                            else
                            {
                                yield return MakeStartEndingInterval<TBuilder>(mostRecentStart, new End<T>(boundary));
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
                            yield return MakeDegenerate<TBuilder>(new Degenerate<T>(boundary.Location));
                        }
                    }
                    else
                    {
                        if (boundary.IsEnd)
                        {
                            if (mostRecentStart == null)
                            {
                                yield return MakeEndingInterval<TBuilder>(new End<T>(boundary));
                            }
                            else
                            {
                                yield return MakeStartEndingInterval<TBuilder>(mostRecentStart, new End<T>(boundary));
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
                    yield return MakeStartingInterval<TBuilder>(mostRecentStart);
                }
                else
                {
                    yield return MakeStartingInterval<TBuilder>();
                }
            }
        }

        /// <inheritdoc />
        public abstract Infinity<T> PositiveInfinity { get; }

        /// <inheritdoc />
        public abstract Infinity<T> NegativeInfinity { get; }
    }
}
