using System;
using System.Collections.Generic;
using System.Linq;
using IntervalSet.PeriodSet.Period;

namespace IntervalSet.PeriodSet
{
    /// <inheritdoc />
    public abstract class PeriodListBuilder<TPeriod, TStartingPeriod> : IPeriodListBuilder<TPeriod, TStartingPeriod>
        where TStartingPeriod : class, TPeriod, IStartingPeriod<TPeriod>
    {
        /// <summary>
        /// Returns a <typeparamref name="TStartingPeriod"/> starting at <paramref name="from"/>
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public abstract TStartingPeriod MakeStartingPeriod(DateTime from);
        
        /// <inheritdoc />
        public IEnumerable<TPeriod> InverseOfBoolean(IEnumerable<DateTime> changes,
            Func<DateTime, DateTime, bool> trueEverywhereBetween)
        {
            changes = changes.OrderBy(d => d).ToList();
            if (changes.Count() < 2)
            {
                yield break;
            }
            foreach (Tuple<DateTime, DateTime> fromTo in changes.Zip(changes.Skip(1), (f, t) => new Tuple<DateTime, DateTime>(f, t)))
            {
                if (trueEverywhereBetween(fromTo.Item1, fromTo.Item2))
                {
                    yield return MakeStartingPeriod(fromTo.Item1).End(fromTo.Item2);
                }
            }
        }

        /// <inheritdoc />
        public IEnumerable<TPeriod> InverseOfBoolean(IList<DateTime> changes,
            Func<DateTime, bool> predicate)
        {
            TStartingPeriod start = null;
            foreach (DateTime change in changes.OrderBy(d => d))
            {
                if (predicate(change))
                {
                    if (start == null)
                    {
                        start = MakeStartingPeriod(change);
                    }
                }
                else
                {
                    if (start != null)
                    {
                        yield return start.End(change);
                        start = null;
                    }
                }
            }
            if (start != null)
            {
                yield return start;
            }
        }
    }
}
