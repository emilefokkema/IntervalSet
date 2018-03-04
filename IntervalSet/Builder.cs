﻿using System;
using System.Collections.Generic;
using System.Linq;
using IntervalSet.Interval;
using IntervalSet.Interval.Boundaries;

namespace IntervalSet
{
    /// <inheritdoc />
    public abstract class Builder<TSet, TInterval, TStartingInterval, T> : IBuilder<TSet, TInterval, TStartingInterval, T>
        where T : IEquatable<T>, IComparable<T>, IFormattable
        where TStartingInterval : class, TInterval, IStartingInterval<TInterval, T>
    {
        /// <inheritdoc />
        public abstract TInterval MakeNonEmptySet(IList<TInterval> intervals);

        /// <inheritdoc />
        public abstract TSet MakeSet(IList<TInterval> intervals);

        /// <inheritdoc />
        public abstract TStartingInterval MakeStartingInterval(Start<T> from);

        /// <inheritdoc />
        public abstract TStartingInterval MakeStartingInterval();

        /// <inheritdoc />
        public abstract TInterval MakeDegenerate(Degenerate<T> degenerate);

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
        public IEnumerable<TInterval> Build(IList<Boundary<T>> boundaries, TStartingInterval currentInterval)
        {
            foreach (Boundary<T> boundary in OrderBoundaries(boundaries))
            {
                if (boundary.IsStart)
                {
                    if (currentInterval == null)
                    {
                        currentInterval = MakeStartingInterval(new Start<T>(boundary));
                    }
                    else
                    {
                        if (boundary.IsEnd && !boundary.Inclusive)
                        {
                            yield return currentInterval.End(new End<T>(boundary));
                            currentInterval = MakeStartingInterval(new Start<T>(boundary));
                        }
                    }
                }
                else
                {
                    if (currentInterval == null)
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
                            yield return currentInterval.End(new End<T>(boundary));
                            currentInterval = null;
                        }
                    }
                }
            }

            if (currentInterval != null)
            {
                yield return currentInterval;
            }
        }
    }
}