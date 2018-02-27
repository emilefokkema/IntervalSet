namespace IntervalSet.PeriodSet.Period.Boundary
{
    public class Continuation : BoundaryKind
    {
        public Continuation():base(BoundaryDirection.Start | BoundaryDirection.End, Inclusivity.Inclusive)
        {
        }
    }
}
