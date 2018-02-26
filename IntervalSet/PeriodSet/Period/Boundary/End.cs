using System;

namespace IntervalSet.PeriodSet.Period.Boundary
{
    public class End : Boundary
    {
        public End(DateTime date, Inclusivity inclusivity) : base(date, new BoundaryKind(BoundaryDirection.End, inclusivity))
        {
        }
    }
}
