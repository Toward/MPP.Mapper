using System;
using AutoMapper.Contracts.Services;

namespace AutoMapper.Services
{
    internal class FunctionCache : IFunctionCache
    {
        #region Public Methods

        public void Add<TSorce, TDestination>(Func<TSorce, TDestination> cachingFunction)
        {
            throw new NotImplementedException();
        }

        public bool Contains<TSorce>()
        {
            throw new NotImplementedException();
        }

        public Func<TSorce, TDestination> GetValue<TSorce, TDestination>()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
