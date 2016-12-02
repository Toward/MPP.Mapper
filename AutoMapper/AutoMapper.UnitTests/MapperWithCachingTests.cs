using System;
using AutoMapper.Contracts.Services;
using AutoMapper.Services;
using NUnit.Framework;

namespace AutoMapper.UnitTests
{
    [TestFixture]
    public class MapperWithCachingTests
    {
        #region Tests

        [Test]
        public void Map_NullParameter_TrownArgumentNullException()
        {
            var mapper = CreateMapper();

            Assert.Catch<ArgumentNullException>(() => mapper.Map<Source, Destination>(null));
        }

        [Test]
        public void Map_ContainsInCache_Return()
        {
            var mapper = CreateMapper();

            Assert.Catch<ArgumentNullException>(() => mapper.Map<Source, Destination>(null));
        }

        #endregion

        #region Factories

        private MapperWithCaching CreateMapper(IMapper mapper = null)
        {
            return new MapperWithCaching(mapper);
        }

        #endregion
    }
}
