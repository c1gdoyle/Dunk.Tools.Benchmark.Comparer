using System.Collections.Generic;
using Dunk.Tools.Benchmark.Comparer.Extensions;

namespace Dunk.Tools.Benchmark.Comparer.Utils
{
    /// <summary>
    /// An helper class that converts a string representation of a numerical value 
    /// and applies any unit-conversion logic. 
    /// </summary>
    /// <remarks>
    /// This is intended to convert time and memory units to a consistent baseline for comparison.
    /// i.e. - For time-units we use ns (nano-seconds) as the base
    ///     s (second) = 1*1000000000
    ///     ms (milli-second) = 1*1000000
    ///     us (micro-second) = 1*1000
    ///     ns (nano-second) = 1
    ///     
    ///     - For memory we use B (Bytes) as the base
    ///     GB (Giga-Byte) 1*1000000000
    ///     MB (Mega-Byte) 1*1000000
    ///     KB (Kilo-Byte) 1*1000
    ///     B (Byte) 1
    /// 
    /// </remarks>
    public static class UnitConversionHelper
    {
        private static readonly Dictionary<string, decimal> TimeMultipliers = new Dictionary<string, decimal>
        {
            { "s", 1000000000},
            { "ms", 1000000},
            { "us", 1000},
            { "ns", 1}
        };

        private static readonly Dictionary<string, decimal> MemoryMultipliers = new Dictionary<string, decimal>
        {
            { "GB", 1000000000},
            { "MB", 1000000},
            { "KB", 1000},
            { "B", 1}
        };

        public static decimal? ConvertValue(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                string[] tokens = value.Split(" ");

                if (tokens.Length == 1)
                {
                    return tokens[0].ParseNullable<decimal>(decimal.TryParse);
                }
                else if (tokens.Length == 2)
                {
                    decimal? d = tokens[0].ParseNullable<decimal>(decimal.TryParse);
                    decimal multiple = DetermineMultipler(tokens[1]);

                    return d * multiple;
                }
            }
            return null;
        }

        private static decimal DetermineMultipler(string unit)
        {
            decimal multiplier;
            if (TimeMultipliers.TryGetValue(unit, out multiplier) ||
                MemoryMultipliers.TryGetValue(unit, out multiplier))
            {
                return multiplier;
            }
            return 1;
        }
    }
}
