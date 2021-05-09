namespace Dunk.Tools.Benchmark.Comparer.Data
{
    /// <summary>
    /// An interface for data-objects that represent a data-comparison 
    /// for a single method.
    /// </summary>
    public interface IDataMethodComparison
    {
        /// <summary>
        /// Gets or sets the name of the method.
        /// </summary>
        string MethodName { get; set; }
    }
}
