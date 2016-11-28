using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper.Contracts.Models;
using AutoMapper.Models;

namespace AutoMapper.Services
{
    public class MapperConfiguration
    {
        #region Private Members

        private readonly Dictionary<PropertyInfo,PropertyInfo> _configDictionary = new Dictionary<PropertyInfo, PropertyInfo>();

        #endregion

        #region Public Methods

        public MapperConfiguration Register<TSource, TDestination>(Expression<Func<TSource, object>> sourceAccessor,
            Expression<Func<TDestination, object>> destinationAccessor)
        {
            if (sourceAccessor == null)
                throw new ArgumentNullException(nameof(sourceAccessor));
            if (destinationAccessor == null)
                throw new ArgumentNullException(nameof(destinationAccessor));

            var sourceProperty = GetPropertyInfo(sourceAccessor);
            var destinationProperty = GetPropertyInfo(destinationAccessor);

            if (!destinationProperty.CanWrite)
                throw new ArgumentException("Destination property doesn't have setter.");
            if (!TypeConvertionTable.CanConvertWithoutDataLoss(sourceProperty.PropertyType, destinationProperty.PropertyType))
                throw new ArgumentException("Incompatible types.");

            if (!_configDictionary.ContainsKey(sourceProperty))
            {
                _configDictionary.Add(sourceProperty, destinationProperty);
            }
            else
            {
                _configDictionary[sourceProperty] = destinationProperty;
            }

            return this;
        }

        #endregion

        #region Internal Methods

        internal IMappingPair GetMappingPair(PropertyInfo sourcePropertyInfo)
        {
            PropertyInfo destinationPropertyInfo;
            return (_configDictionary.TryGetValue(sourcePropertyInfo, out destinationPropertyInfo))
                ? new MappingPair()
                    {
                        SourceProperty = sourcePropertyInfo,
                        DestinationProperty = destinationPropertyInfo
                    }
                : null;
        }

        #endregion

        #region Private Methods

        private PropertyInfo GetPropertyInfo<TSource, TPropertyType>(Expression<Func<TSource, TPropertyType>> propertyAccessor)
        {
            var propertyAccessExpression = propertyAccessor.Body as MemberExpression;
            if (propertyAccessExpression == null)
                throw new ArgumentException("Expression doesn't represent property accessor.");
            
            var propertyInfo = propertyAccessExpression.Member as PropertyInfo;
            if (propertyInfo == null)
                throw new ArgumentException("Expression doesn't represent property accessor.");

            return propertyInfo;
        }

        #endregion
    }
}
