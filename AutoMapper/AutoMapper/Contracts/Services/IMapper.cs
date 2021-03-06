﻿namespace AutoMapper.Contracts.Services
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source)
            where TDestination : new();
    }
}
