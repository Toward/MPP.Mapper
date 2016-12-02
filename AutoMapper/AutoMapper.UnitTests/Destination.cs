using System;

namespace AutoMapper.UnitTests
{
    internal sealed class Destination
    {
        public long FirstProperty { get; set; }
        public string SecondProperty { get; set; }
        public float ThirdProperty { get; set; }
        public DateTime FourthProperty { get; set; }
        public int FieldWithoutSetter { get; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj == this)
                return true;
            var destination = obj as Destination;
            return destination != null && Equals(destination);
        }

        private bool Equals(Destination other)
        {
            return FirstProperty == other.FirstProperty && string.Equals(SecondProperty, other.SecondProperty) && ThirdProperty.Equals(other.ThirdProperty) && FourthProperty.Equals(other.FourthProperty) && FieldWithoutSetter == other.FieldWithoutSetter;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = FirstProperty.GetHashCode();
                hashCode = (hashCode*397) ^ (SecondProperty?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ ThirdProperty.GetHashCode();
                hashCode = (hashCode*397) ^ FourthProperty.GetHashCode();
                hashCode = (hashCode*397) ^ FieldWithoutSetter;
                return hashCode;
            }
        }
    }
}
