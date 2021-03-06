﻿using System;
using AutoMapper.Contracts.Services;

namespace AutoMapper.Services
{
    public class Mapper : IMapper
    {
        #region Public Members

        public IFunctionFactory Factory { get; set; }

        #endregion

        #region Ctor

        public Mapper(IMapperConfiguration configuration)
        {
            Factory = new FunctionFactoryWithCaching(configuration);
        }

        #endregion

        #region Public Methods

        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var function = Factory.CreateFunction<TSource, TDestination>();
            return function(source);
        }

        #endregion
    }
}
