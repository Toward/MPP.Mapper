using System;
using System.Collections.Generic;
using AutoMapper.Contracts.Models;
using AutoMapper.Contracts.Services;
using AutoMapper.Models;

namespace AutoMapper.Services
{
    internal class FunctionCache : IFunctionCache
    {
        #region Private Members

        internal Dictionary<TypePair, Delegate> Cache { get; set; }

        #endregion

        #region Ctor

        public FunctionCache()
        {
            Cache = new Dictionary<TypePair, Delegate>();
        }

        #endregion

        #region Public Methods

        public void Add<TSource, TDestination>(Func<TSource, TDestination> cachingFunction)
        {
            if(cachingFunction == null)
                throw new ArgumentNullException();

            Cache.Add(new TypePair(typeof(TSource),typeof(TDestination)), cachingFunction);
        }

        public bool Contains<TSource, TDestination>()
        {
            return Cache.ContainsKey(new TypePair(typeof(TSource), typeof(TDestination)));
        }

        public Func<TSource, TDestination> GetValue<TSource, TDestination>()
        {
            var typePair = new TypePair(typeof(TSource), typeof(TDestination));
            return (Cache.ContainsKey(typePair))
                ? (Func<TSource,TDestination>)Cache[typePair]
                : null;
        }

        #endregion
    }
}
