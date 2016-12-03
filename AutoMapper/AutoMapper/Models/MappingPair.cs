using System.Reflection;
using AutoMapper.Contracts.Models;

namespace AutoMapper.Models
{
    internal class MappingPair : IMappingPair
    {
        public PropertyInfo SourceProperty { get; set; }
        public PropertyInfo DestinationProperty { get; set; }

        internal MappingPair(PropertyInfo sourcePropertyInfo, PropertyInfo destinationPropertyInfo)
        {
            SourceProperty = sourcePropertyInfo;
            DestinationProperty = destinationPropertyInfo;
        }
    }
}
