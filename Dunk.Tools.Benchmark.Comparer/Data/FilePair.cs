using System.IO;

namespace Dunk.Tools.Benchmark.Comparer.Data
{
    /// <summary>
    /// Encapsualtes the base file and new file to compare against.
    /// </summary>
    internal class FilePair
    {
        /// <summary>
        /// Gets or sets the base file to compare.
        /// </summary>
        public FileInfo BaseFile { get; set; }

        /// <summary>
        /// Gets or sets the new file to compare.
        /// </summary>
        public FileInfo NewFile { get; set; }
    }
}
