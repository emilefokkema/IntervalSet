using System;
using System.Collections.Generic;
using System.Linq;
using IntervalSet.PeriodSet.Period;
using IntervalSet.PeriodSet.Period.Boundaries;
using IntervalSet.PeriodSet.Period.Boundaries.Kind;

namespace IntervalSet.PeriodSet
{
    /// <inheritdoc />
    public abstract class PeriodListBuilder<TPeriod, TStartingPeriod> : IPeriodListBuilder<TPeriod, TStartingPeriod>
        where TStartingPeriod : class, TPeriod, IStartingPeriod<TPeriod>
    {
       
        /// <inheritdoc />
        public abstract Start MakeStartingBoundary(DateTime from);

        /// <inheritdoc />
        public abstract End MakeEndingBoundary(DateTime to);

        /// <inheritdoc />
        public abstract TStartingPeriod MakeStartingPeriod(Boundary from);

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
        public IEnumerable<TPeriod> Build(IList<Boundary> boundaries)
        {
            TStartingPeriod currentPeriod = null;
            foreach (Boundary boundary in OrderBoundaries(boundaries))
            {
                if (boundary.IsStart)
                {
                    if (currentPeriod == null)
                    {
                        currentPeriod = MakeStartingPeriod(boundary);
                    }
                    else
                    {
                        if (boundary.IsEnd && !boundary.Inclusive)
                        {
                            BoundaryKind startKind = new StartKind(Inclusivity.Exclusive);
                            BoundaryKind endKind = new EndKind(Inclusivity.Exclusive);
                            yield return currentPeriod.End(new Boundary(boundary.Date, endKind));
                            currentPeriod = MakeStartingPeriod(new Boundary(boundary.Date, startKind));
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
                            yield return currentPeriod.End(boundary);
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
