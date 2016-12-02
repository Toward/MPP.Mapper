using System;
using AutoMapper.Contracts.Services;
using AutoMapper.Services;
using NSubstitute;
using NUnit.Framework;

namespace AutoMapper.UnitTests
{
    [TestFixture]
    public class MapperTests
    {
        #region Tests

        [Test]
        public void Map_NullParameter_TrownArgumentNullException()
        {
            var mapper = CreateMapper();

            Assert.Catch<ArgumentNullException>(() => mapper.Map<Source,Destination>(null));
        }

        [Test]
        public void Map_WithoutConfig_ReturnMappingObject()
        {
            var mapper = CreateMapper();
            mapper.Factory = CreateFactory();
            var source = CreateSource();
            var expectedDesrination = CreateDestination();

            var destination = mapper.Map<Source,Destination>(source);

            Assert.AreEqual(destination,expectedDesrination);
        }

        #endregion

        #region Factory methods

        private Mapper CreateMapper(IMapperConfiguration configuration = null)
        {
            return new Mapper(configuration);
        }

        private IFunctionFactory CreateFactory()
        {
            var factory = Substitute.For<IFunctionFactory>();
            factory
                .CreateFunction<Source, Destination>()
                .Returns(CreateFunction());
            return factory;
        }

        private Func<Source, Destination> CreateFunction()
        {
            return source => new Destination
            {
                FirstProperty = source.FirstProperty,
                SecondProperty = source.SecondProperty
            };
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

        private Destination CreateDestination()
        {
            var function = CreateFunction();
            return function(CreateSource());
        }

        #endregion
    }
}
