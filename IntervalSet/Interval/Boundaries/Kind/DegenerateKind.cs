using System;
using System.Runtime.Serialization;

namespace IntervalSet.Interval.Boundaries.Kind
{
    /// <summary>
    /// The <see cref="Boundary{T}"/> in question is not a limit point of the <see cref="IIntervalSet{T}"/> it is a <see cref="Boundary{T}"/> of
    /// </summary>
    [Serializable]
    public class DegenerateKind : BoundaryKind
    {
        /// <summary>
        /// Initializes a new <see cref="DegenerateKind"/>
        /// </summary>
        public DegenerateKind():base(BoundaryDirection.None, Inclusivity.Inclusive)
        {
        }

        public DegenerateKind(SerializationInfo info, StreamingContext context):base(info, context)
        {
        }
    }
}
