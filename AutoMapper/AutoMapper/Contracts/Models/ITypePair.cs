using System;

namespace AutoMapper.Contracts.Models
{
    internal interface ITypePair
    {
        Type SourceType { get; set; }
        Type DestinationType { get; set; }
    }
}
