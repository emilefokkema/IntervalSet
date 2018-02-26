using System;

namespace IntervalSet.PeriodSet.Period.Boundary
{
    public class StartEnd : Boundary
    {
        public StartEnd(DateTime date) : base(date, BoundaryKind.Start | BoundaryKind.End, Inclusivity.Inclusive)
        {
        }
    }
}
