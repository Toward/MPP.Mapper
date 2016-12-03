using System;
using AutoMapper.Contracts.Models;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AutoMapper.Contracts.Services
{
    internal interface IExpressionFactory
    {
        Expression<Func<TSource, TDestination>> CreateExpression<TSource, TDestination>(IEnumerable<IMappingPair> mappingPropertiesPair)
            where TDestination : new();
    }
}
