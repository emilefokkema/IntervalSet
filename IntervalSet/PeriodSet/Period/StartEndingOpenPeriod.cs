using System;
using System.Collections.Generic;

namespace IntervalSet.PeriodSet.Period
{
    public class StartEndingOpenPeriod : StartEndingPeriod<OpenPeriodSet, OpenPeriodListBuilder, IOpenPeriod>, IOpenPeriod
    {
        public StartEndingOpenPeriod(DateTime from, DateTime to):base(from, to)
        {
        }

        protected override IOpenPeriod GetPeriod()
        {
            return this;
        }

        protected override OpenPeriodSet MakeSet(IList<IOpenPeriod> list)
        {
            return new OpenPeriodSet(list);
        }

        public DateTime? To => Latest;
    }
}
