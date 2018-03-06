using System;
using System.Runtime.Serialization;

namespace IntervalSet.Interval.Boundaries.Kind
{
    /// <summary>
    /// A <see cref="BoundaryKind"/> of a <c>T</c> that is a member of an open subset of an <see cref="IIntervalSet{T}"/>
    /// </summary>
    [Serializable]
    public class ContinuationKind : BoundaryKind
    {
        /// <summary>
        /// Initializes a new <see cref="ContinuationKind"/>
        /// </summary>
        public ContinuationKind():base(BoundaryDirection.Start | BoundaryDirection.End, Inclusivity.Inclusive)
        {
        }

        /// <summary>
        /// Deserializes a <see cref="ContinuationKind"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public ContinuationKind(SerializationInfo info, StreamingContext context):base(info, context)
        {
        }
    }
}
