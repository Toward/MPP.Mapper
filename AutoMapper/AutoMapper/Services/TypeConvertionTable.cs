using System;
using System.Collections.Generic;

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

        internal static bool CanConvertWithoutDataLoss(Type sourceType, Type destinationType)
        {
            var typesEquals = sourceType == destinationType;
            return (!typesEquals && sourceType.IsPrimitive && destinationType.IsPrimitive)
                ? ConvertionTable[sourceType].Contains(destinationType)
                : typesEquals;
        }

        #endregion
    }
}
