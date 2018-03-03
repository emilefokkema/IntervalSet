using System.Collections.Generic;
using System.Linq;
using IntervalSet.PeriodSet.Period;
using IntervalSet.PeriodSet.Period.Boundaries;

namespace IntervalSet.PeriodSet
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
        public abstract TStartingPeriod MakeStartingPeriod(Start from);

        /// <inheritdoc />
        public abstract TStartingPeriod MakeStartingPeriod();

        /// <inheritdoc />
        public abstract TPeriod MakeDegenerate(Degenerate degenerate);

        private List<Boundary> OrderBoundaries(IList<Boundary> boundaries)
        {
            return boundaries
                .GroupBy(b => b.Date)
                .Select(g => new Boundary(g.Key, g.Select(b => b.Kind).Aggregate((k1, k2) => k1.Plus(k2))))
                .Distinct()
                .OrderBy(b => b.Date)
                .ToList();
        }

        /// <inheritdoc />
        public IEnumerable<TPeriod> Build(IList<Boundary> boundaries, TStartingPeriod currentPeriod)
        {
            foreach (Boundary boundary in OrderBoundaries(boundaries))
            {
                if (boundary.IsStart)
                {
                    if (currentPeriod == null)
                    {
                        currentPeriod = MakeStartingPeriod(new Start(boundary));
                    }
                    else
                    {
                        if (boundary.IsEnd && !boundary.Inclusive)
                        {
                            yield return currentPeriod.End(new End(boundary));
                            currentPeriod = MakeStartingPeriod(new Start(boundary));
                        }
                    }
                }
                else
                {
                    if (currentPeriod == null)
                    {
                        if (!boundary.IsEnd)
                        {
                            yield return MakeDegenerate(new Degenerate(boundary.Date));
                        }
                    }
                    else
                    {
                        if (boundary.IsEnd)
                        {
                            yield return currentPeriod.End(new End(boundary));
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
