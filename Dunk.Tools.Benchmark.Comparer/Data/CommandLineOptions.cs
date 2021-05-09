using CommandLine;

namespace Dunk.Tools.Benchmark.Comparer.Data
{
    /// <summary>
    /// Encapsulates command-line details.
    /// </summary>
    public class CommandLineOptions
    {
        /// <summary>
        /// Gets or sets the file directory containing the base Benchmark reports.
        /// </summary>
        [Option("base", HelpText = "Path to the folder/file with base results.")]
        public string BaseDirectory { get; set; }

        /// <summary>
        /// Gets or sets the file directory containing the new Benchmark reports to
        /// compare against the Base.
        /// </summary>
        [Option("new", HelpText = "Path to the folder/file with the new results.")]
        public string NewDirectory { get; set; }

        /// <summary>
        /// Gets or sets the file directory to output the diffed results to.
        /// </summary>
        [Option("out", HelpText = "Path to the folder/file to output the diffed results", Required = false)]
        public string OutputDirectory { get; set; }

        /// <summary>
        /// Gets or sets the file directory containing the JSON threshold files, if any.
        /// </summary>
        [Option("threshold", HelpText = "Path to the folder containing the threshold JSON files", Required = false)]
        public string ThresholdDirectory { get; set; }

        /// <summary>
        /// Gets or sets the columns to compare.
        /// </summary>
        [Option("columns", HelpText = "Comma-separated list of columns to compare", Default = "Method,Mean,Median,Max,Gen 0,Gen 1,Gen 2,Allocated")]
        public string Columns { get; set; }
    }
}
