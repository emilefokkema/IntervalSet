using System;

namespace IntervalSet.PeriodSet
{
    [Flags]
    public enum BoundaryKind
    {
        None = 0,
        Start = 1,
        End = 2
    }

    public abstract class Boundary
    {
        public DateTime Date { get; }

        public BoundaryKind Kind { get; }

        protected Boundary(DateTime date, BoundaryKind kind)
        {
            Date = date;
            Kind = kind;
        }
    }

    public class Start : Boundary
    {
        public Start(DateTime date):base(date, BoundaryKind.Start)
        {
        }
    }

    public class End : Boundary
    {
        public End(DateTime date):base(date, BoundaryKind.End)
        {
        }
    }

    public class StartEnd : Boundary
    {
        public StartEnd(DateTime date):base(date, BoundaryKind.Start | BoundaryKind.End)
        {
        }
    }
}
