using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using FluentAssertions;
using IntervalSet;
using IntervalSet.Default;
using Newtonsoft.Json;
using NUnit.Framework;
using IntBuilder = IntervalSet.Default.DefaultBuilder<int>;
using DoubleBuilder = IntervalSet.Default.DefaultBuilder<double>;
using IntSet = IntervalSet.Default.DefaultIntervalSet<IntervalSet.Default.DefaultBuilder<int>, int>;
using DoubleSet = IntervalSet.Default.DefaultIntervalSet<IntervalSet.Default.DefaultBuilder<double>, double>;
using StringSet = IntervalSet.Default.DefaultIntervalSet<IntervalSet.Default.DefaultBuilder<string>, string>;
using IntervalSet.Interval.Boundaries;
using IntervalSet.Interval.Boundaries.Kind;
using IntervalSet.Interval.Default;
using Newtonsoft.Json.Serialization;

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
            TestSetSerialization(new DefaultIntervalSet<DefaultBuilder<ComparableType>,ComparableType>(new ComparableType{Value = 3}, new ComparableType{Value = 4}));
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

        private void TestSetSerialization<TBuilder, T>(DefaultIntervalSet<TBuilder,T> set) where T:IComparable<T>,IEquatable<T> where TBuilder : IBuilder<IDefaultInterval<T>,T>,new()
        {
            TestSetSerializationJson(set);
            TestSetSerializationBinary(set);
        }

        private void TestSetSerializationJson<TBuilder, T>(DefaultIntervalSet<TBuilder, T> set) where T : IComparable<T>, IEquatable<T> where TBuilder : IBuilder<IDefaultInterval<T>, T>, new()
        {
            string serialized = JsonConvert.SerializeObject(set);
            DefaultIntervalSet<TBuilder,T> deserialized = JsonConvert.DeserializeObject<DefaultIntervalSet<TBuilder,T>>(serialized);
            deserialized.Should().Be(set);
        }

        private void TestSetSerializationBinary<TBuilder, T>(DefaultIntervalSet<TBuilder, T> set) where T : IComparable<T>, IEquatable<T> where TBuilder : IBuilder<IDefaultInterval<T>, T>, new()
        {
            Stream stream = new MemoryStream();
            new BinaryFormatter().Serialize(stream, set);
            stream.Position = 0;
            DefaultIntervalSet<TBuilder,T> deserialized = (DefaultIntervalSet<TBuilder,T>) new BinaryFormatter().Deserialize(stream);
            deserialized.Should().Be(set);
        }
    }
}
