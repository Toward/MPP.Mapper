using System;
using System.Collections.Generic;
using AutoMapper.Contracts.Models;

namespace AutoMapper.Services
{
    internal static class TypeConvertionTable
    {
        #region Private Members

        private static readonly Dictionary<Type, HashSet<Type>> ConvertionTable = new Dictionary<Type, HashSet<Type>>()
        {
            { typeof(byte), new HashSet<Type>
                            {
                                 typeof(short),
                                 typeof(ushort),
                                 typeof(int),
                                 typeof(uint),
                                 typeof(long),
                                 typeof(ulong),
                                 typeof(float),
                                 typeof(double),
                                 typeof(decimal)
                            }
            },
            { typeof(sbyte), new HashSet<Type>
                            {
                                typeof(short),
                                typeof(int),
                                typeof(long),
                                typeof(float),
                                typeof(double),
                                typeof(decimal)
                            }
            },
            { typeof(short), new HashSet<Type>
                            {
                                typeof(int),
                                typeof(long),
                                typeof(float),
                                typeof(double),
                                typeof(decimal)
                            }
            },
            { typeof(ushort), new HashSet<Type>
                            {
                                typeof(int),
                                typeof(uint),
                                typeof(long),
                                typeof(ulong),
                                typeof(float),
                                typeof(double),
                                typeof(decimal)
                            }
            },
            { typeof(int), new HashSet<Type>
                            {
                                typeof(long),
                                typeof(float),
                                typeof(double),
                                typeof(decimal)
                            }
            },
            { typeof(uint), new HashSet<Type>
                            {
                                typeof(long),
                                typeof(ulong),
                                typeof(float),
                                typeof(double),
                                typeof(decimal)
                            }
            },
            { typeof(long), new HashSet<Type>
                            {
                                typeof(float),
                                typeof(double),
                                typeof(decimal)
                            }
            },
            { typeof(char), new HashSet<Type>
                            {
                                typeof(ushort),
                                typeof(int),
                                typeof(uint),
                                typeof(long),
                                typeof(ulong),
                                typeof(float),
                                typeof(double),
                                typeof(decimal)
                            }
            },
            { typeof(float), new HashSet<Type>
                            {
                                typeof(double)
                            }
            },
            { typeof(ulong), new HashSet<Type>
                            {
                                typeof(float),
                                typeof(double),
                                typeof(decimal)
                            }
            },
        };

        #endregion

        #region Internal Methods

        internal static bool CanConvertWithoutDataLoss(ITypePair typePair)
        {
            if (typePair == null)
                throw new ArgumentNullException(nameof(typePair));
            var typesEquals = typePair.SourceType == typePair.DestinationType;
            return (!typesEquals && typePair.SourceType.IsPrimitive && 
                typePair.DestinationType.IsPrimitive && ConvertionTable.ContainsKey(typePair.SourceType))
                ? ConvertionTable[typePair.SourceType].Contains(typePair.DestinationType)
                : typesEquals;
        }

        #endregion
    }
}
