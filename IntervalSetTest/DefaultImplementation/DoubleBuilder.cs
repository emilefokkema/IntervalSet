using IntervalSet;
using IntervalSet.Default;

namespace IntervalSetTest.DefaultImplementation
{
    public class DoubleBuilder : DefaultBuilder<DoubleBuilder, double>
    {
        public override Infinity<double> PositiveInfinity => new Infinity<double>(double.PositiveInfinity);
        public override Infinity<double> NegativeInfinity => new Infinity<double>(double.NegativeInfinity);
    }

}
