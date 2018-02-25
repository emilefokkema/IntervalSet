using System;
using System.Collections.Generic;
using System.Linq;

namespace IntervalSet.PeriodSet
{
    /// <inheritdoc />
    /// <typeparam name="TPeriod">the type of period to add to a list</typeparam>
    public abstract class PeriodListBuilder<TPeriod> : IPeriodListBuilder<TPeriod>
    {
        /// <summary>
        /// Given <see cref="DateTime"/>s <paramref name="from"/> and <paramref name="to"/>, returns a <typeparamref name="TPeriod"/> starting
        /// at <paramref name="from"/> and ending at <paramref name="to"/>
        /// </summary>
        public abstract TPeriod MakePeriod(DateTime from, DateTime to);

        /// <summary>
        /// Given <see cref="DateTime"/> <paramref name="from"/>, returns a <typeparamref name="TPeriod"/> starting
        /// at <paramref name="from"/>
        /// </summary>
        public abstract TPeriod MakePeriod(DateTime from);
        
        /// <inheritdoc />
        public void InverseOfBoolean(IList<TPeriod> list, IEnumerable<DateTime> changes,
            Func<DateTime, DateTime, bool> trueEverywhereBetween)
        {
            changes = changes.OrderBy(d => d).ToList();
            if (changes.Count() < 2)
            {
                return;
            }
            foreach (Tuple<DateTime, DateTime> fromTo in changes.Zip(changes.Skip(1), (f, t) => new Tuple<DateTime, DateTime>(f, t)))
            {
                if (trueEverywhereBetween(fromTo.Item1, fromTo.Item2))
                {
                    list.Add(MakePeriod(fromTo.Item1, fromTo.Item2));
                }
            }
        }

        /// <inheritdoc />
        public void InverseOfBoolean(IList<TPeriod> list, IList<DateTime> changes,
            Func<DateTime, bool> predicate)
        {
            bool currentlyTrue = false;
            DateTime? beginningBoundary = null;
            foreach (DateTime change in changes.OrderBy(d => d))
            {
                if (predicate(change) && change != DateTime.MaxValue)
                {
                    if (!currentlyTrue)
                    {
                        beginningBoundary = change;
                    }
                    currentlyTrue = true;
                }
                else
                {
                    if (currentlyTrue)
                    {
                        list.Add(MakePeriod(beginningBoundary.Value, change));
                        beginningBoundary = null;
                    }
                    currentlyTrue = false;
                }
            }
            if (currentlyTrue)
            {
                list.Add(MakePeriod(beginningBoundary.Value));
            }
        }
    }
}
