using System.Reflection;

namespace AutoMapper.Contracts.Models
{
    internal interface IMappingPair
    {
        PropertyInfo SourceProperty { get; set; }
        PropertyInfo DestinationProperty { get; set; }
    }
}
