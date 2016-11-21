using System.Reflection;
using System.Security.Permissions;
using AutoMapper.Contracts.Models;

namespace AutoMapper.Models
{
    internal class MappingPair: IMappingPair
    {
        public PropertyInfo SourceProperty { get; set; }
        public PropertyInfo DestinationProperty { get; set; }
    }
}
