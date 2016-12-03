using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.Contracts.Models;
using AutoMapper.Contracts.Services;
using AutoMapper.Models;

namespace AutoMapper.Services
{
    internal class FunctionFactory : IFunctionFactory
    {
        #region Private Members

        private readonly IExpressionFactory _expressionFactory = new ExpressionFactory();
        private readonly IMapperConfiguration _mapperConfiguration;

        #endregion

        #region Ctor

        internal FunctionFactory(IMapperConfiguration mapperConfiguration = null)
        {
            _mapperConfiguration = mapperConfiguration ?? new MapperConfiguration();
        }

        #endregion

        #region Public Methods

        public Func<TSource, TDestination> CreateFunction<TSource, TDestination>() where TDestination : new()
        {
            var propertyPairs = GetProperties(typeof(TSource), typeof(TDestination));
            var lambda = _expressionFactory.CreateExpression<TSource, TDestination>(propertyPairs);
            return lambda.Compile();
        }

        #endregion

        #region Private Methods

        private IEnumerable<IMappingPair> GetProperties(Type sourceType, Type destinationType)
        {
            var sourceProperties = sourceType.GetProperties();
            var destinationProperties = destinationType.GetProperties();
            var result = new List<IMappingPair>();
            foreach (var sourceProperty in sourceProperties)
            {
                var destinationProperty = _mapperConfiguration.GetDestinationProperty(sourceProperty);

                if (destinationProperty == null)
                {
                    destinationProperty = destinationProperties.
                        FirstOrDefault(propertyInfo => propertyInfo.Name == sourceProperty.Name);
                    if (destinationProperty != null && destinationProperty.CanWrite &&
                        TypeConvertionTable.CanConvertWithoutDataLoss(sourceProperty.PropertyType, destinationProperty.PropertyType))
                        result.Add(new MappingPair(sourceProperty, destinationProperty));
                }
                else
                {
                    result.Add(new MappingPair(sourceProperty, destinationProperty));
                }
            }
            return result;
        }

        #endregion
    }
}
