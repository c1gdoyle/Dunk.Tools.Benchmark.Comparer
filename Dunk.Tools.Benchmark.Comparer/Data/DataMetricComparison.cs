namespace Dunk.Tools.Benchmark.Comparer.Data
{
    /// <summary>
    /// Encapsulates the details of a comparison of a single metric.
    /// </summary>
    internal class DataMetricComparison
    {
        /// <summary>
        /// Gets or sets the column name of this comparison.
        /// </summary>
        public string MetricName { get; set; }

        /// <summary>
        /// Gets or sets the baseline value of the metric.
        /// </summary>
        public decimal? BaseValue { get; set; }

        /// <summary>
        /// Gets or sets the new value of the metric.
        /// </summary>
        public decimal? NewValue { get; set; }

        /// <summary>
        /// Gets or sets the difference between the new value and the baseline value.
        /// </summary>
        public decimal? Difference
        {
            get { return NewValue - BaseValue; }
        }
    }
}
