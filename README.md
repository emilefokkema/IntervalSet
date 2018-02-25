# IntervalSet
### Some base classes for performing algebra on sets of disjoint intervals of the IComparable&lt;T&gt; line

The defining characteristic of `IComparable<Thing>`s is that they form a line. [Intervals](https://en.wikipedia.org/wiki/Interval_(mathematics)) are subsets of this line,
as are sets consisting of a single `Thing`.

This project considers interval sets. An example of an interval set is
```
[a, b) + [c, d) + [e,e]
```
(where `a`, `b`, `c`, `d` and `e` are `Thing`s) which is the union of the set of `Thing`s that are greater than or equal to
`a` and less than `b`, the set of `Thing`s that are greater than or equal to `c` and less than `d` and the set containing only `e`.

Another interval set is
```
[b,c)
```
When we join these two interval sets, the result is
```
[a,d) + [e,e]
```
Similarly, when we take `[a,d)` and we subtract `[b,c)` from it, the result is `[a,b) + [c,d)`.

See [BoundedPeriodSet](IntervalSet/PeriodSet/BoundedPeriodSet.cs) for an implementation of this using `DateTime` as `Thing`
