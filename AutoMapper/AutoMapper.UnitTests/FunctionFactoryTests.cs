using System;
using System.Reflection;
using AutoMapper.Contracts.Services;
using AutoMapper.Services;
using NSubstitute;
using NUnit.Framework;

namespace AutoMapper.UnitTests
{
    [TestFixture]
    public class FunctionFactoryTests
    {
        #region Tests

        [Test]
        public void Map_WithoutConfiguration_ReturnNewMappingObject()
        {
            var functionFactory = CreateFactory();
            var source = CreateSource();
            var expectedObject = CreateDestination(true);

            var mappingFunction = functionFactory.CreateFunction<Source, Destination>();

            Assert.AreEqual(mappingFunction(source), expectedObject);
        }

        [Test]
        public void Map_WithConfiguration_ReturnNewMappingObject()
        {
            var configuration = CreateConfiguration();
            var functionFactory = CreateFactory(configuration);
            var source = CreateSource();
            var expectedObject = CreateDestination(false);

            var mappingFunction = functionFactory.CreateFunction<Source, Destination>();

            Assert.AreEqual(mappingFunction(source), expectedObject);
        }

        #endregion


        #region Factories

        private FunctionFactory CreateFactory(IMapperConfiguration configuration = null)
        {
            return new FunctionFactory(configuration);
        }

        private IMapperConfiguration CreateConfiguration()
        {
            var configuration = Substitute.For<IMapperConfiguration>();
            configuration
                .GetDestinationProperty(typeof(Source).GetProperty("FirstProperty"))
                .Returns(typeof(Destination).GetProperty("ThirdProperty"));
            return configuration;
        }

        private Source CreateSource()
        {
            return new Source
            {
                FirstProperty = 1000,
                SecondProperty = "Snow",
                ThirdProperty = 10.201,
                FourthProperty = 1
            };
        }

        private Destination CreateDestination(bool withoutConfig)
        {
            var source = CreateSource();
            return (withoutConfig)
                ? new Destination
                {
                    FirstProperty = source.FirstProperty,
                    SecondProperty = source.SecondProperty
                }
                : new Destination
                {
                    ThirdProperty = source.FirstProperty,
                    SecondProperty = source.SecondProperty
                };
        }

        #endregion
    }
}
