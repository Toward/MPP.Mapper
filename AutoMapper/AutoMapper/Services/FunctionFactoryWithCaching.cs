using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.Contracts.Models;
using AutoMapper.Contracts.Services;
using AutoMapper.Models;

namespace AutoMapper.Services
{
    internal class FunctionFactoryWithCaching: IFunctionFactory
    {
        #region Private Members

        internal IFunctionFactory FunctionFactory { get; set; }
        internal IFunctionCache FunctionCache { get; set; }

        #endregion

        internal FunctionFactoryWithCaching(IMapperConfiguration mapperConfiguration = null)
        {
            FunctionFactory = new FunctionFactory(mapperConfiguration);
            FunctionCache = new FunctionCache();
        }

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
