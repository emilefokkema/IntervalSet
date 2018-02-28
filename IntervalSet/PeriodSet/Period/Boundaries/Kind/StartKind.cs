namespace IntervalSet.PeriodSet.Period.Boundaries.Kind
{
    /// <summary>
    /// Represents the <see cref="BoundaryKind"/> of a <see cref="Boundary"/> at the start of a connected period of time in an <see cref="IPeriodSet"/>
    /// </summary>
    public class StartKind : BoundaryKind
    {
        /// <summary>
        /// Initializes a new <see cref="StartKind"/>
        /// </summary>
        /// <param name="inclusivity"></param>
        public StartKind(Inclusivity inclusivity):base(BoundaryDirection.Start, inclusivity)
        {
        }
    }
}
