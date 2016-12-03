using System;
using AutoMapper.Contracts.Models;

namespace AutoMapper.Models
{
    internal class TypePair : ITypePair
    {
        public Type SourceType { get; set; }
        public Type DestinationType { get; set; }

        internal TypePair(Type sourceType, Type destinationType)
        {
            SourceType = sourceType;
            DestinationType = destinationType;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj == this)
                return true;
            var typePair = obj as TypePair;
            return typePair != null && Equals(typePair);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((SourceType != null ? SourceType.GetHashCode() : 0) * 397) ^
                    (DestinationType != null ? DestinationType.GetHashCode() : 0);
            }
        }

        protected bool Equals(ITypePair other)
        {
            return SourceType == other.SourceType && DestinationType == other.DestinationType;
        }
    }
}
