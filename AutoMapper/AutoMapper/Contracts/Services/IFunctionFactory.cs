using System;
using AutoMapper.Contracts.Models;
using System.Collections.Generic;

namespace AutoMapper.Contracts.Services
{
    internal interface IFunctionFactory
    {
        Func<TSource, TDestination> CreateFunction <TSource,TDestination>(IEnumerable<IMappingPair> mappingPropertiesPair) 
            where TDestination: new();
    }
}
