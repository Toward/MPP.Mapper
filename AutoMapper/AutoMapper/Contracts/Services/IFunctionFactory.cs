using System;

namespace AutoMapper.Contracts.Services
{
    public interface IFunctionFactory
    {
        Func<TSource, TDestination> CreateFunction<TSource, TDestination>()
            where TDestination : new();
    }
}
