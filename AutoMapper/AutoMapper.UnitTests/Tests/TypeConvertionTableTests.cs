using System;
using AutoMapper.Services;
using AutoMapper.UnitTests.Helpers;
using NUnit.Framework;

namespace AutoMapper.UnitTests.Tests
{
    [TestFixture]
    public class TypeConvertionTableTests
    {
        #region Tests

        [Test]
        public void CanConvertWithoutDataLoss_ConvertibleValueTypes_ReturnTrue()
        {
            var result = TypeConvertionTable.CanConvertWithoutDataLoss(typeof(byte), typeof(int));

            Assert.IsTrue(result);
        }

        public void CanConvertWithoutDataLoss_NotConvertibleValueTypes_ReturnFalse()
        {
            var result = TypeConvertionTable.CanConvertWithoutDataLoss(typeof(float), typeof(int));

            Assert.IsFalse(result);
        }

        [Test]
        public void CanConvertWithoutDataLoss_ConvertibleReferenceTypes_ReturnTrue()
        {
            var result = TypeConvertionTable.CanConvertWithoutDataLoss(typeof(Source), typeof(Source));

            Assert.IsTrue(result);
        }

        [Test]
        public void CanConvertWithoutDataLoss_NonConvertibleReferenceTypes_ReturnFalse()
        {
            var result = TypeConvertionTable.CanConvertWithoutDataLoss(typeof(Source), typeof(Destination));

            Assert.IsFalse(result);
        }

        [Test]
        public void CanConvertWithoutDataLoss_NullParameter_ThrownArgumentNullException()
        {
            Assert.Catch<ArgumentNullException>(() => TypeConvertionTable.CanConvertWithoutDataLoss(null,null));
        }

        #endregion
    }
}
