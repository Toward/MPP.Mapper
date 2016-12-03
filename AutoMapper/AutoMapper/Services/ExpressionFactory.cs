using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.Contracts.Models;
using AutoMapper.Contracts.Services;

namespace AutoMapper.Services
{
    internal class ExpressionFactory : IExpressionFactory
    {
        #region Public Methods

        public Expression<Func<TSource, TDestination>> CreateExpression<TSource, TDestination>
            (IEnumerable<IMappingPair> mappingPropertiesPair) where TDestination : new()
        {
            if (mappingPropertiesPair == null)
                throw new ArgumentNullException(nameof(mappingPropertiesPair));

            var parameter = Expression.Parameter(typeof(TSource), "source");
            var assignments = mappingPropertiesPair.Select(propertyPair =>
           {
               var propertry = Expression.Property(parameter, propertyPair.SourceProperty);
               var convertExpression = Expression.Convert(propertry, propertyPair.DestinationProperty.PropertyType);
               return Expression.Bind(propertyPair.DestinationProperty, convertExpression);
           });

            var body = Expression.MemberInit(Expression.New(typeof(TDestination)), assignments);
            return Expression.Lambda<Func<TSource, TDestination>>(body, parameter);
        }

        #endregion
    }
}
