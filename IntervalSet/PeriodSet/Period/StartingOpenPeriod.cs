using System;
using System.Collections.Generic;

namespace IntervalSet.PeriodSet.Period
{
    public class StartingOpenPeriod : StartingPeriod<OpenPeriodSet, OpenPeriodListBuilder, IOpenPeriod>, IOpenPeriod
    {
        public StartingOpenPeriod(DateTime from) : base(from)
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

        public DateTime? To => null;
    }
}
