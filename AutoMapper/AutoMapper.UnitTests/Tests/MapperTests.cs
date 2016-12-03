using System;
using AutoMapper.UnitTests.Helpers;
using NUnit.Framework;

namespace AutoMapper.UnitTests.Tests
{
    [TestFixture]
    public class MapperTests
    {
        #region Private Members

        private readonly AbstractFactory<Source, Destination> _testsFactory = new TestsFactory();

        #endregion

        #region Tests

        [Test]
        public void Map_NullParameter_TrownArgumentNullException()
        {
            var mapper = _testsFactory.CreateMapper();

            Assert.Catch<ArgumentNullException>(() => mapper.Map<Source, Destination>(null));
        }

        [Test]
        public void Map_WithoutConfig_ReturnMappingObject()
        {
            var mapper = _testsFactory.CreateMapper();
            mapper.Factory = _testsFactory.CreateMockFunctionFactory();
            var source = _testsFactory.CreateSource();
            var expectedDesrination = _testsFactory.CreateDestination();

            var destination = mapper.Map<Source, Destination>(source);

            Assert.AreEqual(destination, expectedDesrination);
        }

        #endregion
    }
}
