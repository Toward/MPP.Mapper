using System;
using AutoMapper.UnitTests.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace AutoMapper.UnitTests.Tests
{
    [TestFixture]
    public class FunctionFactoryWithCachingTests
    {
        #region Private Members

        private readonly AbstractFactory<Source, Destination> _testsFactory = new TestsFactory();

        #endregion

        #region Tests

        [Test]
        public void CreateFunction_NotContainsInCache_ReturnFunctionFromInternalFactory()
        {
            var factoryWithCaching = _testsFactory.CreateFactoryWithCaching();
            var internalFactory = _testsFactory.CreateMockFunctionFactory();
            factoryWithCaching.FunctionFactory = internalFactory;

            var source = _testsFactory.CreateSource();
            var expectedDestination = _testsFactory.CreateDestination();

            var mappingFunction = factoryWithCaching.CreateFunction<Source, Destination>();

            internalFactory.Received().CreateFunction<Source, Destination>();
            Assert.AreEqual(mappingFunction(source), expectedDestination);
        }

        [Test]
        public void CreateFunction_NotContainsInCache_AddToCahce()
        {
            var factoryWithCaching = _testsFactory.CreateFactoryWithCaching();
            var cache = _testsFactory.CreateMockFunctionCache();
            factoryWithCaching.FunctionCache = cache;

            factoryWithCaching.CreateFunction<Source, Destination>();

            cache.Received().Add(Arg.Any<Func<Source, Destination>>());
        }

        [Test]
        public void CreateFunction_ContainsInCache_ReturnFunctionFromCache()
        {
            var factoryWithCaching = _testsFactory.CreateFactoryWithCaching();
            var function = _testsFactory.CreateFunction();
            var cache = _testsFactory.CreateMockFunctionCache(function);
            factoryWithCaching.FunctionCache = cache;

            var source = _testsFactory.CreateSource();
            var expectedDestination = _testsFactory.CreateDestination();

            var mappingFunction = factoryWithCaching.CreateFunction<Source, Destination>();

            cache.Received().GetValue<Source, Destination>();
            Assert.AreEqual(mappingFunction(source), expectedDestination);
        }

        [Test]
        public void CreateFunction_ContainsInCache_NotAddToCahce()
        {
            var factoryWithCaching = _testsFactory.CreateFactoryWithCaching();
            var function = _testsFactory.CreateFunction();
            var cache = _testsFactory.CreateMockFunctionCache(function);
            factoryWithCaching.FunctionCache = cache;

            factoryWithCaching.CreateFunction<Source, Destination>();

            cache.DidNotReceive().Add(Arg.Any<Func<Source, Destination>>());
        }

        #endregion
    }
}
