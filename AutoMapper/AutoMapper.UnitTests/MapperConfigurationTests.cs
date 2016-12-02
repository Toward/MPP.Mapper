using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper.Services;
using NUnit.Framework;

namespace AutoMapper.UnitTests
{
    [TestFixture]
    public class MapperConfigurationTests
    {
        #region Tests

        [Test]
        public void Register_ByDefault_AddToDictionary()
        {
            var configuration = CreateConfiguration();
            var expectedDictionaryKey = typeof(Source).GetProperty("FirstProperty");
            var expectedDictionaryValue = typeof(Destination).GetProperty("ThirdProperty");

            configuration.Register<Source, Destination>(source => source.FirstProperty, destination => destination.ThirdProperty);

            Assert.IsTrue(configuration.ConfigDictionary.ContainsKey(expectedDictionaryKey));
            Assert.AreEqual(configuration.ConfigDictionary[expectedDictionaryKey], expectedDictionaryValue);
        }

        [Test]
        public void Register_NullParameters_ThrownArgumentNullException()
        {
            var configuration = CreateConfiguration();

            Assert.Catch<ArgumentNullException>(() => configuration.Register<Source, Destination>(null, null));
        }

        [Test]
        public void Register_NotConvertibleTypes_ThrownArgumentException()
        {
            var configuration = CreateConfiguration();

            var exception = Assert.Catch<ArgumentException>(() => configuration.Register<Source, Destination>(source => source.FirstProperty, destination => destination.FourthProperty));

            StringAssert.Contains(exception.Message, "Incompatible types.");
        }

        [Test]
        public void Register_ReadonlyDestinationProperty_ThrownArgumentException()
        {
            var configuration = CreateConfiguration();

            var exception = Assert.Catch<ArgumentException>(() => configuration.Register<Source, Destination>(source => source.FirstProperty, destination => destination.FieldWithoutSetter));

            StringAssert.Contains(exception.Message, "Destination property doesn't have setter.");
        }

        [Test]
        public void GetMappingPair_NotContainedInDictionary_ReturnNull()
        {
            var configuration = CreateConfiguration();
            configuration.ConfigDictionary = CreateDictionary();

            var destinationProperty = configuration.GetDestinationProperty(typeof(Source).GetProperty("SecondProperty"));

            Assert.Null(destinationProperty);
        }

        [Test]
        public void GetMappingPair_ContainedInDictionary_ReturnNull()
        {
            var configuration = CreateConfiguration();
            configuration.ConfigDictionary = CreateDictionary();
            var expectedSourceProperty = typeof(Source).GetProperty("FirstProperty");
            var expectedDestinationProperty = typeof(Destination).GetProperty("ThirdProperty");

            var destinationProperty = configuration.GetDestinationProperty(expectedSourceProperty);

            Assert.AreEqual(destinationProperty, expectedDestinationProperty);
        }

        #endregion

        #region Factory methods

        private MapperConfiguration CreateConfiguration()
        {
            return new MapperConfiguration();
        }

        private Dictionary<PropertyInfo, PropertyInfo> CreateDictionary()
        {
            var dictionary = new Dictionary<PropertyInfo, PropertyInfo>();
            dictionary.Add(typeof(Source).GetProperty("FirstProperty"),
                typeof(Destination).GetProperty("ThirdProperty"));
            return dictionary;
        }

        #endregion
    }
}
