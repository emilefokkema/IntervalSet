using System;
using System.Runtime.Serialization;

namespace IntervalSet.Interval.Boundaries.Kind
{
    /// <summary>
    /// Represents the <see cref="BoundaryKind"/> of a <see cref="Boundary{T}"/> at the end of an interval in an <see cref="IIntervalSet{T}"/>
    /// </summary>
    [Serializable]
    public class EndKind : BoundaryKind
    {
        /// <summary>
        /// Initializes a new <see cref="EndKind"/>
        /// </summary>
        /// <param name="inclusivity"></param>
        public EndKind(Inclusivity inclusivity):base(BoundaryDirection.End, inclusivity)
        {
        }

        public EndKind(SerializationInfo info, StreamingContext context):base(info, context)
        {
        }
    }
}
