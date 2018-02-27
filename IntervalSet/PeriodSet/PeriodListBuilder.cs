using System;
using System.Collections.Generic;
using System.Linq;
using IntervalSet.PeriodSet.Period;

namespace IntervalSet.PeriodSet
{
    /// <inheritdoc />
    public abstract class PeriodListBuilder<TPeriod, TStartingPeriod> : IPeriodListBuilder<TPeriod>
        where TStartingPeriod : class, TPeriod, IStartingPeriod<TPeriod>
    {
        /// <inheritdoc />
        public TPeriod MakePeriod(DateTime from, DateTime to)
        {
            return MakeStartingPeriod(from).End(to);
        }

        /// <inheritdoc />
        public TPeriod MakePeriod(DateTime from)
        {
            return MakeStartingPeriod(from);
        }
        
        /// <summary>
        /// Returns a <typeparamref name="TStartingPeriod"/> starting at <paramref name="from"/>
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        protected abstract TStartingPeriod MakeStartingPeriod(DateTime from);
        
        /// <inheritdoc />
        public IEnumerable<TPeriod> InverseOfBoolean(IEnumerable<DateTime> changes,
            Func<DateTime, DateTime, bool> trueEverywhereBetween)
        {
            List<TPeriod> result = new List<TPeriod>();
            changes = changes.OrderBy(d => d).ToList();
            if (changes.Count() < 2)
            {
                return result;
            }
            foreach (Tuple<DateTime, DateTime> fromTo in changes.Zip(changes.Skip(1), (f, t) => new Tuple<DateTime, DateTime>(f, t)))
            {
                if (trueEverywhereBetween(fromTo.Item1, fromTo.Item2))
                {
                    result.Add(MakePeriod(fromTo.Item1, fromTo.Item2));
                }
            }

            return result;
        }

        /// <inheritdoc />
        public IEnumerable<TPeriod> InverseOfBoolean(IList<DateTime> changes,
            Func<DateTime, bool> predicate)
        {
            List<TPeriod> result = new List<TPeriod>();
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
                        result.Add(start.End(change));
                        start = null;
                    }
                }
            }
            if (start != null)
            {
                result.Add(start);
            }

            return result;
        }
    }
}
