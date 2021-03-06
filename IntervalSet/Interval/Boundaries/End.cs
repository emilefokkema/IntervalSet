﻿using System;
using System.Runtime.Serialization;
using IntervalSet.Interval.Boundaries.Kind;

namespace IntervalSet.Interval.Boundaries
{
    /// <summary>
    /// A <see cref="Boundary{T}"/> at the end of a period
    /// </summary>
    [Serializable]
    public class End<T> : Boundary<T>
        where T : IEquatable<T>
    {
        /// <summary>
        /// Initializes a new <see cref="End{T}"/> based on a given <paramref name="location"/> and <paramref name="inclusivity"/>
        /// </summary>
        /// <param name="location"></param>
        /// <param name="inclusivity"></param>
        public End(T location, Inclusivity inclusivity):base(location, new EndKind(inclusivity))
        {
        }

        /// <summary>
        /// Initializes a new <see cref="End{T}"/> base on a given <see cref="Boundary{T}"/>
        /// </summary>
        /// <param name="boundary"></param>
        public End(Boundary<T> boundary):this(boundary.Location, boundary.Kind.Inclusivity)
        {
        }

        /// <summary>
        /// Deserializes an <see cref="End{T}"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public End(SerializationInfo info, StreamingContext context):base(info, context)
        {
        }
    }
}
