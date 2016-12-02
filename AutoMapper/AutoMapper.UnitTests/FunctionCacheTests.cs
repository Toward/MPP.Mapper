using System;
using System.Collections.Generic;
using AutoMapper.Contracts.Models;
using AutoMapper.Models;
using AutoMapper.Services;
using NSubstitute;
using NUnit.Framework;

namespace AutoMapper.UnitTests
{
    [TestFixture]
    public class FunctionCacheTests
    {
        #region Test

        [Test]
        public void Add_NullParameter_ThrownArgumentNullException()
        {
            var cache = CreateCache();

            Assert.Catch<ArgumentNullException>(() => cache.Add<Source, Destination>(null));
        }

        [Test]
        public void Add_ByDefault_AddToCache()
        {
            var cache = CreateCache();
            cache.Cache = CreateDictionary();
            var expectedKey = CreateTypePair();
            var expectedLambda = CreateFunction();

            cache.Add(expectedLambda);

            Assert.IsTrue(cache.Cache.ContainsKey(expectedKey));
            Assert.AreEqual(cache.Cache[expectedKey], expectedLambda);
        }

        [Test]
        public void Contains_NotContainsInCache_ReturnFalse()
        {
            var cache = CreateCache();
            cache.Cache = CreateDictionary();
            var key = CreateTypePair();

            Assert.IsFalse(cache.Contains<Source,Destination>());
        }

        [Test]
        public void Contains_ContainsInCache_ReturnTrue()
        {
            var cache = CreateCache();
            var expectedLambda = CreateFunction();
            cache.Cache = CreateDictionary(expectedLambda);

            Assert.IsTrue(cache.Contains<Source,Destination>());
        }

        [Test]
        public void GetValue_NotContainsInCache_ReturNull()
        {
            var cache = CreateCache();
            cache.Cache = CreateDictionary();

            var function = cache.GetValue<Source, Destination>();

            Assert.Null(function);
        }

        [Test]
        public void GetValue_ContainsInCache_ReturnFunction()
        {
            var cache = CreateCache();
            var expectedLambda = CreateFunction();
            cache.Cache = CreateDictionary(expectedLambda);

            var function = cache.GetValue<Source, Destination>();

            Assert.AreEqual(expectedLambda, function);
        }

        #endregion

        #region Factories

        private FunctionCache CreateCache()
        {
            return new FunctionCache();
        }

        private Dictionary<TypePair, Delegate> CreateDictionary(Func<Source, Destination> function = null)
        {
            var dictionary = new Dictionary<TypePair, Delegate>();
            if (function != null)
                dictionary.Add(CreateTypePair(), function);
            return dictionary;
        }

        private TypePair CreateTypePair()
        {
            //var typePair = Substitute.For<ITypePair>();
            //typePair.SourceType = typeof(Source);
            //typePair.DestinationType = typeof(Destination);
            //return typePair;
            return new TypePair(typeof(Source), typeof(Destination));
        }

        private Func<Source, Destination> CreateFunction()
        {
            return source => new Destination
            {
                SecondProperty = source.SecondProperty
            };
        }

        #endregion
    }
}
