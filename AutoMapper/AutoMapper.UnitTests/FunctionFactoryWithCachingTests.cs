using System;
using System.CodeDom;
using AutoMapper.Contracts.Services;
using AutoMapper.Services;
using NSubstitute;
using NUnit.Framework;

namespace AutoMapper.UnitTests
{
    [TestFixture]
    public class FunctionFactoryWithCachingTests
    {
        #region Tests

        [Test]
        public void CreateFunction_NotContainsInCache_ReturnFunctionFromInternalFactory()
        {
            var factoryWithCaching = CreateFactory();
            var internalFactory = CreateInternalFactory();
            factoryWithCaching.FunctionFactory = internalFactory;

            var source = CreateSource();
            var expectedDestination = CreateDestination();

            var mappingFunction = factoryWithCaching.CreateFunction<Source, Destination>();

            internalFactory.Received().CreateFunction<Source, Destination>();
            Assert.AreEqual(mappingFunction(source),expectedDestination);
        }

        [Test]
        public void CreateFunction_NotContainsInCache_AddToCahce()
        {
            var factoryWithCaching = CreateFactory();
            var cache = CreateFunctionCache(true);
            factoryWithCaching.FunctionCache = cache;

            factoryWithCaching.CreateFunction<Source, Destination>();

            cache.Received().Add(Arg.Any<Func<Source,Destination>>());
        }

        [Test]
        public void CreateFunction_ContainsInCache_ReturnFunctionFromCache()
        {
            var factoryWithCaching = CreateFactory();
            var cache = CreateFunctionCache();
            factoryWithCaching.FunctionCache = cache;

            var source = CreateSource();
            var expectedDestination = CreateDestination();

            var mappingFunction = factoryWithCaching.CreateFunction<Source, Destination>();

            cache.Received().GetValue<Source, Destination>();
            Assert.AreEqual(mappingFunction(source), expectedDestination);
        }

        [Test]
        public void CreateFunction_ContainsInCache_NotAddToCahce()
        {
            var factoryWithCaching = CreateFactory();
            var cache = CreateFunctionCache();
            factoryWithCaching.FunctionCache = cache;

            factoryWithCaching.CreateFunction<Source, Destination>();

            cache.DidNotReceive().Add(Arg.Any<Func<Source, Destination>>());
        }

        #endregion

        #region Factories

        private FunctionFactoryWithCaching CreateFactory(IMapperConfiguration configuration = null)
        {
            return new FunctionFactoryWithCaching(configuration);
        }

        private IFunctionFactory CreateInternalFactory()
        {
            var factory = Substitute.For<IFunctionFactory>();
            factory
                .CreateFunction<Source, Destination>()
                .Returns(CreateFunction());
            return factory;
        }

        private IFunctionCache CreateFunctionCache(bool isEmpty = false)
        {
            var cache = Substitute.For<IFunctionCache>();
            if (!isEmpty)
            {
                cache
                    .Contains<Source, Destination>()
                    .Returns(true);
                cache
                    .GetValue<Source, Destination>()
                    .Returns(CreateFunction());
            }            
            return cache;
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
            var mappingFunction = CreateFunction();
            var source = CreateSource();
            return mappingFunction(source);
        }

        #endregion
    }
}
