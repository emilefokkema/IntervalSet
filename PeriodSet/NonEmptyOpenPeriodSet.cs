using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PeriodSet.Period;
using PeriodSet.Period.Boundaries;
using PeriodSet.Period.Boundaries.Kind;

namespace PeriodSet
{
    /// <summary>
    /// An <see cref="OpenPeriodSet"/> that contains at least one <see cref="IOpenPeriod"/>
    /// </summary>
    public class NonEmptyOpenPeriodSet : NonEmptyPeriodSet<OpenPeriodSet, OpenPeriodListBuilder, IStartingOpenPeriod, IOpenPeriod>, IOpenPeriod
    {
        /// <inheritdoc />
        public NonEmptyOpenPeriodSet(IPeriodSet set):base(set)
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
            Start start = new Start(from, Inclusivity.Inclusive);
            if (to.HasValue)
            {
                if (to.Value == from)
                {
                    return new OpenPeriodListBuilder().MakeDegenerate(new Degenerate(from));
                }
                return new OpenPeriodListBuilder().MakeStartingPeriod(start).End(new End(to.Value, Inclusivity.Exclusive));
            }
            return new OpenPeriodListBuilder().MakeStartingPeriod(start);
        }

        /// <summary>
        /// De end date (if any) of the last <see cref="IOpenPeriod"/> in this <see cref="NonEmptyOpenPeriodSet"/>
        /// </summary>
        public DateTime? To => PeriodList.Last().To;

        /// <inheritdoc />
        public DateTime Earliest => PeriodList.First().Earliest;

        /// <inheritdoc />
        public override string ToString()
        {
            return ToString("d", CultureInfo.CurrentCulture);
        }
    }
}
