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
    public class FunctionFactoryTests
    {
        #region Tests

        [Test]
        public void CreateFunction_ByDefault_ReturnsNewFunction()
        {
            var mappingPairs = CreateMappingPairs();
            var factory = CreateFactory();

            var result = factory.CreateFunction<Source, Destination>(mappingPairs);

            var paramName = result.Parameters[0].Name;
            var paramType = result.Parameters[0].Type;
            var body = (MemberInitExpression)result.Body;
            var destinationType = body.Type;
            var destinationPropertyName = body.Bindings[0].Member.Name;
            var operand = ((UnaryExpression)((MemberAssignment)body.Bindings[0]).Expression).Operand;
            var sourcePropertyName = ((MemberExpression) operand).Member.Name;

            Assert.AreEqual(paramName, "source");
            Assert.AreEqual(paramType, typeof(Source));
            Assert.AreEqual(destinationType, typeof(Destination));
            Assert.AreEqual(destinationPropertyName, "ThirdProperty");
            Assert.AreEqual(sourcePropertyName, "FirstProperty");
        }

        [Test]
        public void CreateFunction_NullParameter_ThrownException()
        {
            var factory = CreateFactory();

            Assert.Catch<ArgumentNullException>(() => factory.CreateFunction<Source, Destination>(null));
        }

        #endregion

        #region Factories for tests

        private LambdaFactory CreateFactory() => new LambdaFactory();

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
