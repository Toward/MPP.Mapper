using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.Contracts.Models;
using AutoMapper.Contracts.Services;

namespace AutoMapper.Services
{
    internal class FunctionFactory : IFunctionFactory
    {
        #region Internal Methods

        public Func<TSource, TDestination> CreateFunction<TSource, TDestination>(IEnumerable<IMappingPair> mappingPropertiesPair)
            where TDestination : new()
        {
            var parameter = Expression.Parameter(typeof(TSource), "source");
            var assignments = mappingPropertiesPair.Select( propertyPair =>
            {
                var propertry = Expression.Property(parameter, propertyPair.SourceProperty);
                var convertExpression = Expression.Convert(propertry, propertyPair.DestinationProperty.PropertyType);
                return Expression.Bind(propertyPair.DestinationProperty, convertExpression);
            });

            var body = Expression.MemberInit(Expression.New(typeof(TDestination)), assignments);
            return Expression.Lambda<Func<TSource, TDestination>>(body, parameter).Compile();
        }

        #endregion
    }
}
