using System;
using System.Collections.Generic;
using AutoMapper.Models;
using AutoMapper.Services;
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

            Assert.Catch<ArgumentNullException>(() => cache.Add<Source,Destination>(null,null));
        }

        [Test]
        public void Add_ByDefault_AddToCache()
        {
            var cache = CreateCache();
            cache.Cache = CreateDictionary();
            var expectedKey = CreateTypePair();
            var expectedLambda = CreateFunction();

            cache.Add(expectedKey,expectedLambda);
                          
            Assert.IsTrue(cache.Cache.ContainsKey(expectedKey));
            Assert.AreEqual(cache.Cache[expectedKey],expectedLambda);
        }

        [Test]
        public void Contains_NotContainsInCache_ReturnFalse()
        {
            var cache = CreateCache();
            cache.Cache = CreateDictionary();
            var key = CreateTypePair();

            Assert.IsFalse(cache.Contains(key));
        }

        [Test]
        public void Contains_ContainsInCache_ReturnTrue()
        {
            var cache = CreateCache();
            var expectedLambda = CreateFunction();
            cache.Cache = CreateDictionary(expectedLambda);
            var key = CreateTypePair();

            Assert.IsTrue(cache.Contains(key));
        }

        [Test]
        public void GetValue_NotContainsInCache_ReturNull()
        {
            var cache = CreateCache();
            cache.Cache = CreateDictionary();
            var key = CreateTypePair();

            var function = cache.GetValue<Source, Destination>(key);

            Assert.Null(function);
        }

        [Test]
        public void GetValue_ContainsInCache_ReturnFunction()
        {
            var cache = CreateCache();
            var expectedLambda = CreateFunction();
            cache.Cache = CreateDictionary(expectedLambda);
            var key = CreateTypePair();

            var function = cache.GetValue<Source, Destination>(key);

            Assert.AreEqual(expectedLambda,function);
        }

        #endregion

        #region Factories

        private FunctionCache CreateCache()
        {
            return new FunctionCache();
        }

        private Dictionary<TypePair, Delegate> CreateDictionary(Func<Source,Destination> function = null)
        {
            var dictionary = new Dictionary<TypePair, Delegate>();
            if (function != null)
                dictionary.Add(CreateTypePair(),function);
            return dictionary;
        }

        private TypePair CreateTypePair()
        {
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
