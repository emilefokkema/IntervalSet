using System;
using System.Collections.Generic;

namespace IntervalSet.PeriodSet
{
    /// <summary>
    /// Builds up a list of periods
    /// </summary>
    /// <typeparam name="TPeriod"></typeparam>
    public interface IPeriodListBuilder<TPeriod>
    {
        /// <summary>
        /// Given a list of periods and a list of boundaries, adds a period to the list of periods for every two <see cref="DateTime"/>s
        /// between which a predicate is true (as returned by <paramref name="trueEverywhereBetween"/>)
        /// </summary>
        /// <param name="list">the list to add periods to</param>
        /// <param name="changes">all the <see cref="DateTime"/>s between which to evaluate <paramref name="trueEverywhereBetween"/></param>
        /// <param name="trueEverywhereBetween">return whether the predicate is true between two given boundaries</param>
        void InverseOfBoolean(IList<TPeriod> list, IEnumerable<DateTime> changes,
            Func<DateTime, DateTime, bool> trueEverywhereBetween);

        /// <summary>
        /// Given a list of periods and a list of <see cref="DateTime"/>s on which the value of a predicate changes, adds a period
        /// to the list of periods for every <see cref="DateTime"/> t1 on which the value of <paramref name="predicate"/> switches from
        /// <c>false</c> to <c>true</c> and <see cref="DateTime"/> t2 for which it switches back to <c>false</c>
        /// </summary>
        /// <param name="list">the list to add periods to</param>
        /// <param name="changes">a list of dates on which to update the value of the predicate</param>
        /// <param name="predicate">returns the updated value of the predicate on the given date</param>
        /// <returns></returns>
        void InverseOfBoolean(IList<TPeriod> list, IList<DateTime> changes,
            Func<DateTime, bool> predicate);

        /// <summary>
        /// Adds to a given list of periods a period starting at <paramref name="from"/> and ending at <paramref name="to"/>
        /// </summary>
        void Add(IList<TPeriod> list, DateTime from, DateTime to);

        /// <summary>
        /// Adds to a given list of periods a period starting at <paramref name="from"/>
        /// </summary>
        void Add(IList<TPeriod> list, DateTime from);
    }
}
