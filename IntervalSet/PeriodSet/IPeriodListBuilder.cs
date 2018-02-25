using System;
using System.Collections.Generic;

namespace IntervalSet.PeriodSet
{
    /// <summary>
    /// Builds up a list of periods
    /// </summary>
    /// <typeparam name="TPeriod"></typeparam>
    public interface IPeriodListBuilder<out TPeriod>
    {
        /// <summary>
        /// Given a list of boundaries, returns a period for every two <see cref="DateTime"/>s
        /// between which a predicate is true (as returned by <paramref name="trueEverywhereBetween"/>)
        /// </summary>
        /// <param name="changes">all the <see cref="DateTime"/>s between which to evaluate <paramref name="trueEverywhereBetween"/></param>
        /// <param name="trueEverywhereBetween">return whether the predicate is true between two given boundaries</param>
        IEnumerable<TPeriod> InverseOfBoolean(IEnumerable<DateTime> changes,
            Func<DateTime, DateTime, bool> trueEverywhereBetween);

        /// <summary>
        /// Given a list of <see cref="DateTime"/>s on which the value of a predicate changes, returns a period
        /// for every <see cref="DateTime"/> t1 on which the value of <paramref name="predicate"/> switches from
        /// <c>false</c> to <c>true</c> and <see cref="DateTime"/> t2 for which it switches back to <c>false</c>
        /// </summary>
        /// <param name="changes">a list of dates on which to update the value of the predicate</param>
        /// <param name="predicate">returns the updated value of the predicate on the given date</param>
        /// <returns></returns>
        IEnumerable<TPeriod> InverseOfBoolean(IList<DateTime> changes,
            Func<DateTime, bool> predicate);

        /// <summary>
        /// Returns a <typeparamref name="TPeriod"/> starting at <paramref name="from"/> and ending at <paramref name="to"/>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        TPeriod MakePeriod(DateTime from, DateTime to);

        /// <summary>
        /// Returns a <typeparamref name="TPeriod"/> starting at <paramref name="from"/>
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        TPeriod MakePeriod(DateTime from);
    }
}
