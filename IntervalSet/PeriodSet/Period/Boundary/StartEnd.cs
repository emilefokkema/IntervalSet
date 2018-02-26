using System;

namespace IntervalSet.PeriodSet.Period.Boundary
{
    public class StartEnd : Boundary
    {
        public StartEnd(DateTime date) : base(date, new BoundaryKind(BoundaryDirection.Start | BoundaryDirection.End, Inclusivity.Inclusive))
        {
        }
    }
}
