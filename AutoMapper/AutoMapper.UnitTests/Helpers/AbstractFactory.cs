using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper.Contracts.Models;
using AutoMapper.Contracts.Services;
using AutoMapper.Models;
using AutoMapper.Services;

namespace AutoMapper.UnitTests.Helpers
{
    internal abstract class AbstractFactory<TSource,TDestination>
        where TDestination : new()
    {
        #region Abstract Methods

        internal abstract IEnumerable<IMappingPair> CreateMappingPairs();
        internal abstract Expression<Func<TSource, TDestination>> CreateExpression();
        internal abstract Func<TSource, TDestination> CreateFunction(bool withConfiguration = false);
        internal abstract TypePair CreateTypePair();
        internal abstract Source CreateSource();
        internal abstract Destination CreateDestination(bool withConfig = false);
        internal abstract IFunctionFactory CreateMockFunctionFactory();
        internal abstract IMapperConfiguration CreateMockConfiguration();
        internal abstract IFunctionCache CreateMockFunctionCache(Func<TSource, TDestination> function = null);

        #endregion

        #region Internal Methods

        internal ExpressionFactory CreateExpressionFactory()
        {
            return new ExpressionFactory();
        }

        internal FunctionCache CreateFunctionCache()
        {
            return new FunctionCache();
        }

        internal FunctionFactory CreateFunctionFactory(IMapperConfiguration configuration = null)
        {
            return new FunctionFactory(configuration);
        }

        internal FunctionFactoryWithCaching CreateFactoryWithCaching(IMapperConfiguration configuration = null)
        {
            return new FunctionFactoryWithCaching(configuration);
        }

        internal MapperConfiguration CreateConfiguration()
        {
            return new MapperConfiguration();
        }

        internal Mapper CreateMapper(IMapperConfiguration configuration = null)
        {
            return new Mapper(configuration);
        }

        #endregion
    }
}
