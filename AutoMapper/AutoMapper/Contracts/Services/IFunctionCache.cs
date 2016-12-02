using System;

namespace AutoMapper.Contracts.Services
{
    internal interface IFunctionCache
    {
        void Add<TSource,TDestination>(Func<TSource, TDestination> cachingFunction);
        bool Contains<TSource,TDestination>();
        Func<TSource, TDestination> GetValue<TSource, TDestination>();
    }
}
