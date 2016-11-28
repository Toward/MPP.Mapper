using System;

namespace AutoMapper.Contracts.Services
{
    internal interface IFunctionCache
    {
        void Add<TSorce,TDestination>(Func<TSorce, TDestination> cachingFunction);
        bool Contains<TSorce>();
        Func<TSorce, TDestination> GetValue<TSorce, TDestination>();
    }
}
