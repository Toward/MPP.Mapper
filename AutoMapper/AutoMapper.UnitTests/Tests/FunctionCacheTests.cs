using System;
using AutoMapper.UnitTests.Helpers;
using NUnit.Framework;

namespace AutoMapper.UnitTests.Tests
{
    [TestFixture]
    public class FunctionCacheTests
    {
        #region Private Members

        private readonly AbstractFactory<Source, Destination> _testsFactory = new TestsFactory();

        #endregion

        #region Test

        [Test]
        public void Add_NullParameter_ThrownArgumentNullException()
        {
            var cache = _testsFactory.CreateFunctionCache();

            Assert.Catch<ArgumentNullException>(() => cache.Add<Source, Destination>(null));
        }

        [Test]
        public void Add_ByDefault_AddToCache()
        {
            var cache = _testsFactory.CreateFunctionCache();
            var expectedKey = _testsFactory.CreateTypePair();
            var expectedLambda = _testsFactory.CreateFunction();

            cache.Add(expectedLambda);

            Assert.IsTrue(cache.Cache.ContainsKey(expectedKey));
            Assert.AreEqual(cache.Cache[expectedKey], expectedLambda);
        }

        [Test]
        public void Contains_NotContainsInCache_ReturnFalse()
        {
            var cache = _testsFactory.CreateFunctionCache();

            Assert.IsFalse(cache.Contains<Source, Destination>());
        }

        [Test]
        public void Contains_ContainsInCache_ReturnTrue()
        {
            var cache = _testsFactory.CreateFunctionCache();
            var key = _testsFactory.CreateTypePair();
            var function = _testsFactory.CreateFunction();
            cache.Cache.Add(key, function);

            Assert.IsTrue(cache.Contains<Source, Destination>());
        }

        [Test]
        public void GetValue_NotContainsInCache_ReturNull()
        {
            var cache = _testsFactory.CreateFunctionCache();

            var function = cache.GetValue<Source, Destination>();

            Assert.Null(function);
        }

        [Test]
        public void GetValue_ContainsInCache_ReturnFunction()
        {
            var cache = _testsFactory.CreateFunctionCache();
            var key = _testsFactory.CreateTypePair();
            var expectedLambda = _testsFactory.CreateFunction();
            cache.Cache.Add(key, expectedLambda);

            var function = cache.GetValue<Source, Destination>();

            Assert.AreEqual(expectedLambda, function);
        }

        #endregion
    }
}
