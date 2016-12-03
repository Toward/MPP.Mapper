using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper.Contracts.Services;

namespace AutoMapper.Services
{
    public class MapperConfiguration: IMapperConfiguration
    {
        #region Internal Members

        internal Dictionary<PropertyInfo, PropertyInfo> ConfigDictionary { get; set; }

        #endregion

        #region Ctor

        public MapperConfiguration()
        {
            ConfigDictionary = new Dictionary<PropertyInfo, PropertyInfo>();
        }

        #endregion

        #region Public Methods

        public IMapperConfiguration Register<TSource, TDestination>(Expression<Func<TSource, object>> sourceAccessor,
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

            if (!ConfigDictionary.ContainsKey(sourceProperty))
            {
                ConfigDictionary.Add(sourceProperty, destinationProperty);
            }
            else
            {
                ConfigDictionary[sourceProperty] = destinationProperty;
            }

            return this;
        }

        public PropertyInfo GetDestinationProperty(PropertyInfo sourcePropertyInfo)
        {
            if (sourcePropertyInfo == null)
                throw new ArgumentNullException(nameof(sourcePropertyInfo));

            PropertyInfo result;
            return (ConfigDictionary.TryGetValue(sourcePropertyInfo, out result))
                ? result
                : null;
        }

        #endregion

        #region Private Methods

        private PropertyInfo GetPropertyInfo<TSource, TPropertyType>(Expression<Func<TSource, TPropertyType>> propertyAccessor)
        {
            var propertyAccessExpression = ((UnaryExpression)propertyAccessor.Body).Operand as MemberExpression;
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
