using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using FluentAssertions;
using IntervalSet.Default;
using Newtonsoft.Json;
using NUnit.Framework;
using IntSet = IntervalSet.Default.DefaultIntervalSet<int>;
using DoubleSet = IntervalSet.Default.DefaultIntervalSet<double>;
using StringSet = IntervalSet.Default.DefaultIntervalSet<string>;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Boundaries.Kind;
using IntervalSetTest.DefaultImplementation;

namespace IntervalSetTest.SerializationTest
{
    [TestFixture]
    public class SerializationTest
    {
        [Serializable]
        private class ComparableType : IComparable<ComparableType>, IEquatable<ComparableType>
        {
            public int Value { get; set; }

            public bool Equals(ComparableType other)
            {
                return Value == other?.Value;
            }

            public int CompareTo(ComparableType other)
            {
                return Value.CompareTo(other.Value);
            }
        }

        private class TypeWithDefaultSet
        {
            public string Name { get; set; }
            public IntSet Integers { get; set; }
            public List<DoubleSet> DoubleList { get; set; }
        }

        [Test]
        public void TestBoundarySerialization()
        {
            Start<double> doubleBoundary = new Start<double>(5, Inclusivity.Inclusive);
            string serialized = JsonConvert.SerializeObject(doubleBoundary);
            Start<double> deserialized = JsonConvert.DeserializeObject<Start<double>>(serialized);
            deserialized.Should().Be(doubleBoundary);
        }

        [Test]
        public void TestSets()
        {
            TestSetSerialization(new IntSet(6,7));
            TestSetSerialization(IntSet.All.Minus(new IntSet(6, 7)));
            TestSetSerialization(IntSet.All);
            TestSetSerialization(new StringSet("a","b"));
            TestSetSerialization(new DefaultIntervalSet<ComparableType>(new ComparableType{Value = 3}, new ComparableType{Value = 4}));
            TestSetSerialization(new FloatSet(5,6));
        }

        [Test]
        public void TestObject()
        {
            DoubleSet double1 = new DoubleSet(0.5, 1.5);
            DoubleSet double2 = new DoubleSet(5.5, 10.5);
            IntSet intSet = new IntSet(6, 7);
            TypeWithDefaultSet thing = new TypeWithDefaultSet
            {
                Name = "John",
                Integers = intSet,
                DoubleList = new List<DoubleSet>{double1, double2}
            };
            string serialized = JsonConvert.SerializeObject(thing);
            TypeWithDefaultSet deserializedThing = JsonConvert.DeserializeObject<TypeWithDefaultSet>(serialized);
            deserializedThing.Name.Should().Be("John");
            deserializedThing.Integers.Should().Be(intSet);
            deserializedThing.DoubleList.Should().Contain(double1);
            deserializedThing.DoubleList.Should().Contain(double2);
        }

        private void TestSetSerialization<TSet>(TSet set) 
        {
            TestSetSerializationJson(set);
            TestSetSerializationBinary(set);
        }

        private void TestSetSerializationJson<TSet>(TSet set) 
        {
            string serialized = JsonConvert.SerializeObject(set);
            TSet deserialized = JsonConvert.DeserializeObject<TSet>(serialized);
            deserialized.Should().Be(set);
        }

        private void TestSetSerializationBinary<TSet>(TSet set)  
        {
            Stream stream = new MemoryStream();
            new BinaryFormatter().Serialize(stream, set);
            stream.Position = 0;
            TSet deserialized = (TSet) new BinaryFormatter().Deserialize(stream);
            deserialized.Should().Be(set);
        }
    }
}
