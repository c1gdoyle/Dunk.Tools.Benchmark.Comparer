namespace Dunk.Tools.Benchmark.Comparer.Data
{
    /// <summary>
    /// A super-class of <see cref="DataMetricComparison"/> that includes 
    /// details if the difference exceeds a threshold. 
    /// </summary>
    internal class DataMetricThresholdComparison : DataMetricComparison
    {
        /// <summary>
        /// Gets or sets the threshold for this comparison.
        /// </summary>
        public decimal? Threshold { get; set; }

        /// <summary>
        /// Gets whether or not the difference of this comparison is within 
        /// the threshold.
        /// </summary>
        public bool WithinThreshold
        {
            get
            {
                if (Threshold == null)
                {
                    //if no threshold is defiend just assume true
                    return true;
                }

                return Difference <= Threshold;
            }
        }
    }
}
