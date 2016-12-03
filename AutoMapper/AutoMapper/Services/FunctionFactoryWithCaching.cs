using System;
using AutoMapper.Contracts.Services;

namespace AutoMapper.Services
{
    internal class FunctionFactoryWithCaching : IFunctionFactory
    {
        #region Internal Members

        internal IFunctionFactory FunctionFactory { get; set; }
        internal IFunctionCache FunctionCache { get; set; }

        #endregion

        #region Ctor

        internal FunctionFactoryWithCaching(IMapperConfiguration mapperConfiguration = null)
        {
            FunctionFactory = new FunctionFactory(mapperConfiguration);
            FunctionCache = new FunctionCache();
        }

        #endregion

        #region Public Methods

        public Func<TSource, TDestination> CreateFunction<TSource, TDestination>() where TDestination : new()
        {
            if (FunctionCache.Contains<TSource, TDestination>())
                return FunctionCache.GetValue<TSource, TDestination>();

            var function = FunctionFactory.CreateFunction<TSource, TDestination>();
            FunctionCache.Add(function);
            return function;
        }

        #endregion

    }
}
