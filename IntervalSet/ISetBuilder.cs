using System;
using System.Collections.Generic;

namespace IntervalSet
{
    public interface ISetBuilder<out TSet, TInterval, T>
        where T : IEquatable<T>, IComparable<T>
    {
        TSet MakeSet(IList<TInterval> intervals);
        TInterval MakeNonEmptySet(IList<TInterval> intervals);
    }
}
