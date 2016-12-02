using System;
using AutoMapper.Contracts.Models;
using AutoMapper.Services;
using NSubstitute;
using NUnit.Framework;

namespace AutoMapper.UnitTests
{
    [TestFixture]
    public class TypeConvertionTableTests
    {
        #region Tests

        [Test]
        public void CanConvertWithoutDataLoss_ConvertibleValueTypes_ReturnTrue()
        {
            var typePair = CreateTypePair(typeof(byte), typeof(int));

            var result = TypeConvertionTable.CanConvertWithoutDataLoss(typePair);

            Assert.IsTrue(result);
        }

        public void CanConvertWithoutDataLoss_NotConvertibleValueTypes_ReturnFalse()
        {
            var typePair = CreateTypePair(typeof(float), typeof(int));

            var result = TypeConvertionTable.CanConvertWithoutDataLoss(typePair);

            Assert.IsFalse(result);
        }

        [Test]
        public void CanConvertWithoutDataLoss_ConvertibleReferenceTypes_ReturnTrue()
        {
            var typePair = CreateTypePair(typeof(Source), typeof(Source));

            var result = TypeConvertionTable.CanConvertWithoutDataLoss(typePair);

            Assert.IsTrue(result);
        }

        [Test]
        public void CanConvertWithoutDataLoss_NonConvertibleReferenceTypes_ReturnFalse()
        {
            var typePair = CreateTypePair(typeof(Source), typeof(Destination));

            var result = TypeConvertionTable.CanConvertWithoutDataLoss(typePair);

            Assert.IsFalse(result);
        }

        [Test]
        public void CanConvertWithoutDataLoss_NullParameter_ThrownArgumentNullException()
        {
            Assert.Catch<ArgumentNullException>(() => TypeConvertionTable.CanConvertWithoutDataLoss(null));
        }
        #endregion

        #region Factory methods

        private ITypePair CreateTypePair(Type sourceType, Type destinationType)
        {
            var typePair = Substitute.For<ITypePair>();
            typePair.SourceType = sourceType;
            typePair.DestinationType = destinationType;
            return typePair;
        }

        #endregion
    }
}
