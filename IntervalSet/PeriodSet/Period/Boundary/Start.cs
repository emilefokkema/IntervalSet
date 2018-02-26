using System;

namespace IntervalSet.PeriodSet.Period.Boundary
{
    public class Start : Boundary
    {
        public Start(DateTime date, Inclusivity inclusivity) : base(date, BoundaryKind.Start, inclusivity)
        {
        }
    }
}
