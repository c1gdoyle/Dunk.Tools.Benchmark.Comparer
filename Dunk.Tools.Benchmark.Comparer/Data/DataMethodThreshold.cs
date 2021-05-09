using System.Collections.Generic;

namespace Dunk.Tools.Benchmark.Comparer.Data
{
    /// <summary>
    /// Encapsulates details of thresholds for a single benchmark method. 
    /// </summary>
    internal class DataMethodThreshold
    {
        /// <summary>
        /// Gets or sets the name of the method.
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Gets the thresholds keyed by name.
        /// </summary>
        /// <remarks>
        /// It is expected that names match the columns being compared.
        /// </remarks>
        public Dictionary<string, decimal?> ThresholdsByName { get; set; }
    }
}
