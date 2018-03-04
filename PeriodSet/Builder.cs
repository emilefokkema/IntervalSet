using System;
using System.Collections.Generic;
using System.Linq;
using IntervalSet.Interval.Boundaries;
using PeriodSet.Period;

namespace PeriodSet
{
    /// <inheritdoc />
    public abstract class Builder<TSet, TPeriod, TStartingPeriod> : IBuilder<TSet, TPeriod, TStartingPeriod>
        where TStartingPeriod : class, TPeriod, IStartingPeriod<TPeriod>
    {
        /// <inheritdoc />
        public abstract TPeriod MakeNonEmptySet(IList<TPeriod> periods);

        /// <inheritdoc />
        public abstract TSet MakeSet(IList<TPeriod> periods);

        /// <inheritdoc />
        public abstract TStartingPeriod MakeStartingPeriod(Start<DateTime> from);

        /// <inheritdoc />
        public abstract TStartingPeriod MakeStartingPeriod();

        /// <inheritdoc />
        public abstract TPeriod MakeDegenerate(Degenerate<DateTime> degenerate);

        private List<Boundary<DateTime>> OrderBoundaries(IList<Boundary<DateTime>> boundaries)
        {
            return boundaries
                .GroupBy(b => b.Location)
                .Select(g => new Boundary<DateTime>(g.Key, g.Select(b => b.Kind).Aggregate((k1, k2) => k1.Plus(k2))))
                .Distinct()
                .OrderBy(b => b.Location)
                .ToList();
        }

        /// <inheritdoc />
        public IEnumerable<TPeriod> Build(IList<Boundary<DateTime>> boundaries, TStartingPeriod currentPeriod)
        {
            foreach (Boundary<DateTime> boundary in OrderBoundaries(boundaries))
            {
                if (boundary.IsStart)
                {
                    if (currentPeriod == null)
                    {
                        currentPeriod = MakeStartingPeriod(new Start<DateTime>(boundary));
                    }
                    else
                    {
                        if (boundary.IsEnd && !boundary.Inclusive)
                        {
                            yield return currentPeriod.End(new End<DateTime>(boundary));
                            currentPeriod = MakeStartingPeriod(new Start<DateTime>(boundary));
                        }
                    }
                }
                else
                {
                    if (currentPeriod == null)
                    {
                        if (!boundary.IsEnd)
                        {
                            yield return MakeDegenerate(new Degenerate<DateTime>(boundary.Location));
                        }
                    }
                    else
                    {
                        if (boundary.IsEnd)
                        {
                            yield return currentPeriod.End(new End<DateTime>(boundary));
                            currentPeriod = null;
                        }
                    }
                }
            }

            if (currentPeriod != null)
            {
                yield return currentPeriod;
            }
        }
    }
}
