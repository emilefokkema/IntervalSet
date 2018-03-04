namespace IntervalSet.Interval.Boundaries.Kind
{
    /// <summary>
    /// Represents the <see cref="BoundaryKind"/> of a <see cref="Boundary{T}"/> at the start of an interval in an <see cref="IIntervalSet{T}"/>
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
