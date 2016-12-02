using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper.Contracts.Models;
using AutoMapper.Services;
using NSubstitute;
using NUnit.Framework;

namespace AutoMapper.UnitTests
{
    [TestFixture]
    public class ExpressionFactoryTests
    {
        #region Tests

        [Test]
        public void CreateFunction_ByDefault_ReturnsNewFunction()
        {
            var factory = CreateFactory();
            var mappingPairs = CreateMappingPairs();
            Expression<Func<Source,Destination>>expectedExpression = source => new Destination
            {
                ThirdProperty = source.FirstProperty
            };

            var expression = factory.CreateExpression<Source, Destination>(mappingPairs);

            Assert.AreEqual(expectedExpression.ToString(),expression.ToString());
        }

        [Test]
        public void CreateFunction_NullParameter_ThrownException()
        {
            var factory = CreateFactory();

            Assert.Catch<ArgumentNullException>(() => factory.CreateExpression<Source, Destination>(null));
        }

        #endregion

        #region Factories for tests

        private ExpressionFactory CreateFactory() => new ExpressionFactory();

        private IEnumerable<IMappingPair> CreateMappingPairs()
        {
            var mappingPair = Substitute.For<IMappingPair>();
            mappingPair.SourceProperty = typeof(Source).GetProperty("FirstProperty");
            mappingPair.DestinationProperty = typeof(Destination).GetProperty("ThirdProperty");
            return new[] { mappingPair };
        }

        #endregion
    }
}
