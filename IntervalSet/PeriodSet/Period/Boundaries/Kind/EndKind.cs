namespace IntervalSet.PeriodSet.Period.Boundaries.Kind
{
    /// <summary>
    /// Represents the <see cref="BoundaryKind"/> of a <see cref="Boundary"/> at the end of a connected period of time in an <see cref="IPeriodSet"/>
    /// </summary>
    public class EndKind : BoundaryKind
    {
        /// <summary>
        /// Initializes a new <see cref="EndKind"/>
        /// </summary>
        /// <param name="inclusivity"></param>
        public EndKind(Inclusivity inclusivity):base(BoundaryDirection.End, inclusivity)
        {
        }
    }
}
