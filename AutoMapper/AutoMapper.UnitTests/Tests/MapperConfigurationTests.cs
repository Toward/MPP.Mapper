using System;
using System.Reflection;
using AutoMapper.UnitTests.Helpers;
using NUnit.Framework;

namespace AutoMapper.UnitTests.Tests
{
    [TestFixture]
    public class MapperConfigurationTests
    {
        #region Private Members

        private readonly AbstractFactory<Source, Destination> _testsFactory = new TestsFactory();

        #endregion

        #region Tests

        [Test]
        public void Register_ByDefault_AddToDictionary()
        {
            var configuration = _testsFactory.CreateConfiguration();
            var expectedDictionaryKey = typeof(Source).GetProperty("FirstProperty");
            var expectedDictionaryValue = typeof(Destination).GetProperty("ThirdProperty");

            configuration.Register<Source, Destination>(source => source.FirstProperty, destination => destination.ThirdProperty);

            Assert.IsTrue(configuration.ConfigDictionary.ContainsKey(expectedDictionaryKey));
            Assert.AreEqual(configuration.ConfigDictionary[expectedDictionaryKey], expectedDictionaryValue);
        }

        [Test]
        public void Register_NullParameters_ThrownArgumentNullException()
        {
            var configuration = _testsFactory.CreateConfiguration();

            Assert.Catch<ArgumentNullException>(() => configuration.Register<Source, Destination>(null, null));
        }

        [Test]
        public void Register_NotConvertibleTypes_ThrownArgumentException()
        {
            var configuration = _testsFactory.CreateConfiguration();

            var exception = Assert.Catch<ArgumentException>(() => configuration.Register<Source, Destination>(source => source.FirstProperty, destination => destination.FourthProperty));

            StringAssert.Contains(exception.Message, "Incompatible types.");
        }

        [Test]
        public void Register_ReadonlyDestinationProperty_ThrownArgumentException()
        {
            var configuration = _testsFactory.CreateConfiguration();

            var exception = Assert.Catch<ArgumentException>(() => configuration.Register<Source, Destination>(source => source.FirstProperty, destination => destination.FieldWithoutSetter));

            StringAssert.Contains(exception.Message, "Destination property doesn't have setter.");
        }

        [Test]
        public void GetMappingPair_NotContainedInDictionary_ReturnNull()
        {
            var configuration = _testsFactory.CreateConfiguration();

            var destinationProperty = configuration.GetDestinationProperty(typeof(Source).GetProperty("SecondProperty"));

            Assert.Null(destinationProperty);
        }

        [Test]
        public void GetMappingPair_ContainedInDictionary_ReturnNull()
        {
            var configuration = _testsFactory.CreateConfiguration();
            var expectedSourceProperty = GetProperty<Source>("FirstProperty");
            var expectedDestinationProperty = GetProperty<Destination>("ThirdProperty");

            configuration.ConfigDictionary.Add(expectedSourceProperty, expectedDestinationProperty);


            var destinationProperty = configuration.GetDestinationProperty(expectedSourceProperty);

            Assert.AreEqual(destinationProperty, expectedDestinationProperty);
        }

        #endregion

        #region Private Methods

        private PropertyInfo GetProperty<T>(string name)
        {
            return typeof(T).GetProperty(name);
        }

        #endregion
    }
}
