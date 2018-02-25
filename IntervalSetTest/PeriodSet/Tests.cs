using IntervalSet.PeriodSet;
using NUnit.Framework;
using System;

namespace IntervalSetTest.PeriodSet
{
    [TestFixture]
    public class Tests
    {
        protected DateTime startOne, startTwo, startThree, startFour, startFive, startSix, startSeven, startEight;

        protected OpenPeriodSet one, two, three, four, empty;

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            startOne = new DateTime(2016, 1, 1);
            startTwo = new DateTime(2016, 2, 1);
            startThree = new DateTime(2016, 3, 1);
            startFour = new DateTime(2016, 4, 1);
            startFive = new DateTime(2016, 5, 1);
            startSix = new DateTime(2016, 6, 1);
            startSeven = new DateTime(2016, 7, 1);
            startEight = new DateTime(2016, 8, 1);
            one = new Period(startOne, startTwo);
            two = new Period(startTwo, startThree);
            three = new Period(startThree, startFour);
            four = new Period(startFour, startFive);
            empty = OpenPeriodSet.Empty;
        }
    }
}
