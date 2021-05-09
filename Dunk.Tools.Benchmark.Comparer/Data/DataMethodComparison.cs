using System.Collections.Generic;

namespace Dunk.Tools.Benchmark.Comparer.Data
{
    /// <summary>
    /// Encapsulates the details of a data comparison for single benchmark method.
    /// </summary>
    internal class DataMethodComparison : IDataMethodComparison
    {
        /// <summary>
        /// Gets or sets the name of the method.
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Gets or sets the data metric comparisons keyed by metric(column) name.
        /// </summary>
        public Dictionary<string, DataMetricComparison> DataComparisonsByName { get; set; }
    }
}
