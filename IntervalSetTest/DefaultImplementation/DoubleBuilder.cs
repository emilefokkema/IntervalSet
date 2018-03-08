using IntervalSet.Default;

namespace IntervalSetTest.DefaultImplementation
{
    public class DoubleBuilder : DefaultBuilder<DoubleBuilder, double>
    {
        protected override double PositiveInfinity => double.PositiveInfinity;
        protected override double NegativeInfinity => double.NegativeInfinity;
    }

}
