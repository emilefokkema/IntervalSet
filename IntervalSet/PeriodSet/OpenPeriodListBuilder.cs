﻿using System;
using IntervalSet.PeriodSet.Period;

namespace IntervalSet.PeriodSet
{
    /// <inheritdoc />
    public class OpenPeriodListBuilder : PeriodListBuilder<IOpenPeriod>
    {
        /// <inheritdoc />
        public override IOpenPeriod MakePeriod(DateTime from)
        {
            return new Period.Period(from);
        }

        /// <inheritdoc />
        public override IOpenPeriod MakePeriod(DateTime from, DateTime to)
        {
            return new Period.Period(from, to);
        }
    }
}