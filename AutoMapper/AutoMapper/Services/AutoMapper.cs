using System;
using AutoMapper.Contracts.Models;
using AutoMapper.Contracts.Services;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.Models;

namespace AutoMapper.Services
{
    public class AutoMapper : IMapper
    {
        #region Private Methods

        private readonly IFunctionFactory _functionFactory = new FunctionFactory();
        private readonly MapperConfiguration _mapperConfiguration;

        #endregion

        #region Ctor

        public AutoMapper(MapperConfiguration mapperConfiguration = null)
        {
            _mapperConfiguration = mapperConfiguration ?? new MapperConfiguration();
        }

        #endregion

        #region Public Methods

        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        {
            var propertyPairs = GetProperties(typeof(TSource), typeof(TDestination));
            var mappingFunction = _functionFactory.CreateFunction<TSource, TDestination>(propertyPairs);
            return mappingFunction(source);
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
                var mappingPair = _mapperConfiguration.GetMappingPair(sourceProperty);
                if (mappingPair != null)
                {
                    result.Add(mappingPair);
                }
                else
                {
                    var destinationProperty = destinationProperties.
                        FirstOrDefault(propertyInfo => propertyInfo.Name == sourceProperty.Name);
                    if (destinationProperty != null && destinationProperty.CanWrite &&
                        TypeConvertionTable.CanConvertWithoutDataLoss(sourceProperty.PropertyType, destinationProperty.PropertyType))
                    {
                        result.Add(new MappingPair()
                        {
                            SourceProperty = sourceProperty,
                            DestinationProperty = destinationProperty
                        });
                    }
                }

            }
            return result;
        }

        #endregion
    }
}