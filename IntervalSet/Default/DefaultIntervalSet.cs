using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Boundaries.Kind;
using IntervalSet.Interval.Default;

namespace IntervalSet.Default
{
    /// <summary>
    /// A default implementation of <see cref="IIntervalSet{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TBuilder"></typeparam>
    /// <typeparam name="TSet"></typeparam>
    [Serializable]
    public class DefaultIntervalSet<TSet, TBuilder, T> : MultipleIntervalSet<TSet, TBuilder, IDefaultInterval<T>, T>
        where TSet : IIntervalSet<T>
        where TBuilder : IBuilder<TSet, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new, empty, <see cref="DefaultIntervalSet{TSet,TBuilder,T}"/>
        /// </summary>
        public DefaultIntervalSet()
        {
        }

        /// <summary>
        /// Initializes a new <see cref="DefaultIntervalSet{TSet,TBuilder,T}"/> with a given list of <see cref="IDefaultInterval{T}"/>s
        /// </summary>
        /// <param name="intervals"></param>
        public DefaultIntervalSet(IList<IDefaultInterval<T>> intervals):base(intervals)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="DefaultIntervalSet{TSet,TBuilder,T}"/> based on a given <see cref="IIntervalSet{T}"/>
        /// </summary>
        /// <param name="set"></param>
        public DefaultIntervalSet(IIntervalSet<T> set):base(set)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="DefaultIntervalSet{TSet,TBuilder,T}"/> based on a given <see cref="Start{T}"/>
        /// </summary>
        /// <param name="start"></param>
        public DefaultIntervalSet(Start<T> start):base(start)
        {
        }

        /// <summary>
        /// Deserializes a <see cref="DefaultIntervalSet{TSet,TBuilder,T}"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public DefaultIntervalSet(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="DefaultIntervalSet{TSet,TBuilder,T}"/> with a given start <typeparamref name="T"/> and end <typeparamref name="T"/>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public DefaultIntervalSet(T from, T to):base(new TBuilder().MakeStart(from), new TBuilder().MakeEnd(to))
        {
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        
    }

    /// <summary>
    /// A default implementation of <see cref="DefaultIntervalSet{TSet,TBuilder,T}"/> where <c>TSet</c> is <see cref="DefaultIntervalSet{TBuilder,T}"/>
    /// </summary>
    [Serializable]
    public class DefaultIntervalSet<TBuilder, T> : DefaultIntervalSet<DefaultIntervalSet<TBuilder, T>, TBuilder, T>
        where TBuilder : IBuilder<DefaultIntervalSet<TBuilder, T>, IDefaultInterval<T>, T>, new()
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new, empty, <see cref="DefaultIntervalSet{TBuilder,T}"/>
        /// </summary>
        public DefaultIntervalSet()
        {
        }

        /// <summary>
        /// Initializes a new <see cref="DefaultIntervalSet{TBuilder,T}"/> with a given list of <see cref="IDefaultInterval{T}"/>s
        /// </summary>
        /// <param name="intervals"></param>
        public DefaultIntervalSet(IList<IDefaultInterval<T>> intervals) : base(intervals)
        {
        }

        /// <summary>
        /// Deserializes a <see cref="DefaultIntervalSet{TBuilder,T}"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public DefaultIntervalSet(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="DefaultIntervalSet{TBuilder,T}"/> with a given start <typeparamref name="T"/> and end <typeparamref name="T"/>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public DefaultIntervalSet(T from, T to) : base(from, to)
        {
        }

        /// <summary>
        /// A <see cref="DefaultIntervalSet{TBuilder,T}"/> representing the entire <typeparamref name="T"/> space
        /// </summary>
        public static DefaultIntervalSet<TBuilder,T> All = new DefaultIntervalSet<TBuilder,T>(new List<IDefaultInterval<T>> {new DefaultEntireInterval<TBuilder,T>()});

        /// <summary>
        /// A <see cref="DefaultIntervalSet{TBuilder,T}"/> representing the empty set as a set containing only <typeparamref name="T"/>s
        /// </summary>
        public static DefaultIntervalSet<TBuilder,T> Empty = new DefaultIntervalSet<TBuilder,T>();
    }

    /// <summary>
    /// A default implementation of <see cref="DefaultIntervalSet{TBuilder,T}"/> where <c>TBuilder</c> is <see cref="DefaultBuilder{T}"/>
    /// </summary>
    [Serializable]
    public class DefaultIntervalSet<T> : DefaultIntervalSet<DefaultIntervalSet<T>, DefaultBuilder<T>, T>
        where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new, empty, <see cref="DefaultIntervalSet{T}"/>
        /// </summary>
        public DefaultIntervalSet()
        {
        }

        /// <summary>
        /// Initializes a new <see cref="DefaultIntervalSet{T}"/> with a given list of <see cref="IDefaultInterval{T}"/>s
        /// </summary>
        /// <param name="intervals"></param>
        public DefaultIntervalSet(IList<IDefaultInterval<T>> intervals) : base(intervals)
        {
        }

        /// <summary>
        /// Deserializes a <see cref="DefaultIntervalSet{T}"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public DefaultIntervalSet(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="DefaultIntervalSet{T}"/> with a given start <typeparamref name="T"/> and end <typeparamref name="T"/>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public DefaultIntervalSet(T from, T to) : base(from, to)
        {
        }

        /// <summary>
        /// A <see cref="DefaultIntervalSet{T}"/> representing the entire <typeparamref name="T"/> space
        /// </summary>
        public static DefaultIntervalSet<T> All = new DefaultIntervalSet<T>(new List<IDefaultInterval<T>> { new DefaultEntireInterval<T>() });

        /// <summary>
        /// A <see cref="DefaultIntervalSet{T}"/> representing the empty set as a set containing only <typeparamref name="T"/>s
        /// </summary>
        public static DefaultIntervalSet<T> Empty = new DefaultIntervalSet<T>();
    }
}
