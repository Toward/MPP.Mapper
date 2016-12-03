using AutoMapper.UnitTests.Helpers;
using NUnit.Framework;

namespace AutoMapper.UnitTests.Tests
{
    [TestFixture]
    public class FunctionFactoryTests
    {
        #region Private Members

        private readonly AbstractFactory<Source, Destination> _testsFactory = new TestsFactory();

        #endregion

        #region Tests

        [Test]
        public void CreateFunction_WithoutConfiguration_ReturnNewMappingObject()
        {
            var functionFactory = _testsFactory.CreateFunctionFactory();
            var source = _testsFactory.CreateSource();
            var expectedObject = _testsFactory.CreateDestination();

            var mappingFunction = functionFactory.CreateFunction<Source, Destination>();

            Assert.AreEqual(mappingFunction(source), expectedObject);
        }

        [Test]
        public void CreateFunction_WithConfiguration_ReturnNewMappingObject()
        {
            var configuration = _testsFactory.CreateMockConfiguration();
            var functionFactory = _testsFactory.CreateFunctionFactory(configuration);
            var source = _testsFactory.CreateSource();
            var expectedObject = _testsFactory.CreateDestination(true);

            var mappingFunction = functionFactory.CreateFunction<Source, Destination>();

            Assert.AreEqual(mappingFunction(source), expectedObject);
        }

        #endregion

    }
}
