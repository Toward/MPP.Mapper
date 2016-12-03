using System;
using AutoMapper.UnitTests.Helpers;
using NUnit.Framework;

namespace AutoMapper.UnitTests.Tests
{
    [TestFixture]
    public class ExpressionFactoryTests
    {
        #region Private Members

        private readonly AbstractFactory<Source, Destination> _testsFactory = new TestsFactory();

        #endregion

        #region Tests

        [Test]
        public void CreateFunction_ByDefault_ReturnsNewFunction()
        {
            var factory = _testsFactory.CreateExpressionFactory();
            var mappingPairs = _testsFactory.CreateMappingPairs();
            var expectedExpression = _testsFactory.CreateExpression();

            var expression = factory.CreateExpression<Source, Destination>(mappingPairs);

            Assert.AreEqual(expectedExpression.ToString(), expression.ToString());
        }

        [Test]
        public void CreateFunction_NullParameter_ThrownException()
        {
            var factory = _testsFactory.CreateExpressionFactory();

            Assert.Catch<ArgumentNullException>(() => factory.CreateExpression<Source, Destination>(null));
        }

        #endregion
    }
}
