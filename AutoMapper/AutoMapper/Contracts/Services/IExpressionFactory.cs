using System;
using AutoMapper.Contracts.Models;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AutoMapper.Contracts.Services
{
    internal interface ILambdaFactory
    {
        Expression<Func<TSource, TDestination>> CreateFunction <TSource,TDestination>(IEnumerable<IMappingPair> mappingPropertiesPair) 
            where TDestination: new();
    }
}
