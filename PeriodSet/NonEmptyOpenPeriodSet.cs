using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using IntervalSet;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Boundaries.Kind;
using PeriodSet.Period;

namespace PeriodSet
{
    /// <summary>
    /// An <see cref="OpenPeriodSet"/> that contains at least one <see cref="IOpenPeriod"/>
    /// </summary>
    public class NonEmptyOpenPeriodSet : NonEmptyIntervalSet<OpenPeriodSet, OpenPeriodListBuilder, IOpenPeriod, DateTime>, IOpenPeriod
    {
        /// <inheritdoc />
        public NonEmptyOpenPeriodSet(IIntervalSet<DateTime> set):base(set)
        {
        }

        /// <inheritdoc />
        public NonEmptyOpenPeriodSet(IList<IOpenPeriod> list):base(list)
        {
            
        }

        /// <inheritdoc />
        public NonEmptyOpenPeriodSet(DateTime from, DateTime? to = null):base(MakePeriod(from, to))
        {
        }

        private static IOpenPeriod MakePeriod(DateTime from, DateTime? to)
        {
            Start<DateTime> start = new Start<DateTime>(from, Inclusivity.Inclusive);
            if (to.HasValue)
            {
                if (to.Value == from)
                {
                    return new DegenerateOpenPeriod(new Degenerate<DateTime>(from));
                }
                return new StartEndingOpenPeriod(start, new End<DateTime>(to.Value, Inclusivity.Exclusive));
            }
            return new StartingOpenPeriod(start);
        }

        /// <summary>
        /// De end date (if any) of the last <see cref="IOpenPeriod"/> in this <see cref="NonEmptyOpenPeriodSet"/>
        /// </summary>
        public DateTime? To => IntervalList.Last().To;

        /// <inheritdoc />
        public DateTime Earliest => IntervalList.First().Earliest;

        /// <inheritdoc />
        public override string ToString()
        {
            return ToString("d", CultureInfo.CurrentCulture);
        }
    }
}
