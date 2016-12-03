using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper.Contracts.Models;
using AutoMapper.Contracts.Services;
using AutoMapper.Models;
using NSubstitute;

namespace AutoMapper.UnitTests.Helpers
{
    internal class TestsFactory : AbstractFactory<Source, Destination>
    {
        #region Internal methods

        internal override IEnumerable<IMappingPair> CreateMappingPairs()
        {
            var mappingPair = Substitute.For<IMappingPair>();
            mappingPair.SourceProperty = typeof(Source).GetProperty("FirstProperty");
            mappingPair.DestinationProperty = typeof(Destination).GetProperty("ThirdProperty");
            return new[] { mappingPair };
        }

        internal override Expression<Func<Source, Destination>> CreateExpression()
        {
            return source => new Destination
            {
                ThirdProperty = source.FirstProperty
            };
        }

        internal override Func<Source, Destination> CreateFunction(bool withConfiguration = false)
        {
            if (withConfiguration == false)
            {
                return source => new Destination
                {
                    FirstProperty = source.FirstProperty,
                    SecondProperty = source.SecondProperty
                };
            }

            return source => new Destination
            {
                ThirdProperty = source.FirstProperty,
                SecondProperty = source.SecondProperty
            };
        }

        internal override TypePair CreateTypePair()
        {
            return new TypePair(typeof(Source), typeof(Destination));
        }

        internal override Source CreateSource()
        {
            return new Source
            {
                FirstProperty = 1000,
                SecondProperty = "Snow",
                ThirdProperty = 10.201,
                FourthProperty = 1
            };
        }

        internal override Destination CreateDestination(bool withConfig = false)
        {
            var source = CreateSource();
            var function = CreateFunction(withConfig);
            return function(source);
        }

        internal override IMapperConfiguration CreateMockConfiguration()
        {
            var configuration = Substitute.For<IMapperConfiguration>();
            configuration
                .GetDestinationProperty(typeof(Source).GetProperty("FirstProperty"))
                .Returns(typeof(Destination).GetProperty("ThirdProperty"));
            return configuration;
        }

        internal override IFunctionFactory CreateMockFunctionFactory()
        {
            var factory = Substitute.For<IFunctionFactory>();
            factory
                .CreateFunction<Source, Destination>()
                .Returns(CreateFunction());
            return factory;
        }

        internal override IFunctionCache CreateMockFunctionCache(Func<Source, Destination> function = null)
        {
            var cache = Substitute.For<IFunctionCache>();
            if (function == null)
                return cache;

            cache
                .Contains<Source, Destination>()
                .Returns(true);
            cache
                .GetValue<Source, Destination>()
                .Returns(CreateFunction());
            return cache;
        }

        #endregion
    }
}
