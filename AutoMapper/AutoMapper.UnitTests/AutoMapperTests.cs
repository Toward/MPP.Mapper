using System;
using AutoMapper.Services;
using Mapper = AutoMapper.Services.AutoMapper;
using NUnit.Framework;

namespace AutoMapper.UnitTests
{
    [TestFixture]
    public class AutoMapperTests
    {
        #region Tests

        [Test]
        public void Map_NullParameter_ThrownArgumentNullException()
        {
            var mapper = CreateMapper();

            Assert.Catch<ArgumentNullException>(() => mapper.Map<Source, Destination>(null));
        }

        [Test]
        public void Map_WithoutConfiguration_ReturnNewMappingObject()
        {
            var mapper = CreateMapper();
            var source = CreateSource();
            var expectedObject = CreateDestination(true);

            var mappingObject = mapper.Map<Source, Destination>(source);

            Assert.AreEqual(mappingObject,expectedObject);
        }

        [Test]
        public void Map_WithConfiguration_ReturnNewMappingObject()
        {
            var configuration = CreateConfiguration();
            var mapper = CreateMapper(configuration);
            var source = CreateSource();
            var expectedObject = CreateDestination(false);

            var mappingObject = mapper.Map<Source, Destination>(source);

            Assert.AreEqual(mappingObject, expectedObject);
        }

        #endregion


        #region Factories

        private Mapper CreateMapper(MapperConfiguration configuration = null)
        {
            return new Mapper(configuration);
        }

        private MapperConfiguration CreateConfiguration()
        {
            var configuration = new MapperConfiguration();
            configuration
                .Register<Source, Destination>(source => source.FirstProperty, destination => destination.ThirdProperty);
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
