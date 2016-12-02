using System;
using System.Linq.Expressions;
using System.Reflection;

namespace AutoMapper.Contracts.Services
{
    public interface IMapperConfiguration
    {
        IMapperConfiguration Register<TSource, TDestination>(Expression<Func<TSource, object>> sourceAccessor,
            Expression<Func<TDestination, object>> destinationAccessor);

        PropertyInfo GetDestinationProperty(PropertyInfo sourcePropertyInfo);
    }
}
