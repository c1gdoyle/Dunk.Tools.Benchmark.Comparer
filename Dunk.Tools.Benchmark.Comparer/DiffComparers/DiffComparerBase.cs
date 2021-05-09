using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dunk.Tools.Benchmark.Comparer.Data;
using Microsoft.VisualBasic.FileIO;
using NLog;

namespace Dunk.Tools.Benchmark.Comparer.DiffComparers
{
    /// <summary>
    /// An abstract base class for classes responsible for 
    /// comparing benchmark files and writing the output to 
    /// a specified diff file.
    /// </summary>
    internal abstract class DiffComparerBase<T>
        where T : IDataMethodComparison
    {
        /// <summary>
        /// Initialises a new instance of <see cref="DiffComparerBase{T}"/> with a 
        /// specified directory for writing the output the diff reports to.
        /// </summary>
        /// <param name="outputDirectory">The directory for writing the output diff reports to.</param>
        /// <exception cref="ArgumentNullException"><paramref name="outputDirectory"/> was null or empty.</exception>
        protected DiffComparerBase(string outputDirectory)
        {
            if (string.IsNullOrEmpty(outputDirectory))
            {
                throw new ArgumentNullException(nameof(outputDirectory),
                    $"Unable to initialise DiffComparerBase. {nameof(outputDirectory)} cannot be null or empty");
            }

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            OutputFilePath = Path.Combine(outputDirectory, "DiffReport.csv");
        }

        /// <summary>
        /// Gets the logger used by the base class.
        /// </summary>
        public abstract ILogger BaseLogger { get; }

        /// <summary>
        /// Gets the full file-path to output the diff results to.
        /// </summary>
        protected string OutputFilePath
        {
            get;
            private set;
        }

        /// <summary>
        /// Compares the benchmark reports and writes to a diff file.
        /// </summary>
        /// <param name="columns">The columns to compare.</param>
        /// <param name="filePairs">The file-pairs to compare.</param>
        public void CompareAndWriteDiffFiles(string[] columns, IEnumerable<FilePair> filePairs)
        {
            //Compare the files
            BaseLogger.Info("Beginning file-pair comparisons for benchmark operations");
            Dictionary<string, T> comparisons = CreateComparisons(columns, filePairs);
            BaseLogger.Info(System.Globalization.CultureInfo.InvariantCulture,
                "Completed file-pair comparisons for {0} benchmark operations", comparisons.Count);

            //Write diff to file
            BaseLogger.Info(System.Globalization.CultureInfo.InvariantCulture,
                "Writing diffed results to {0}", OutputFilePath);
            WriteToDiffFile(columns, comparisons, OutputFilePath);
            BaseLogger.Info(System.Globalization.CultureInfo.InvariantCulture,
                "Diff results successfully written to {0}", OutputFilePath);
        }

        protected abstract T CompareReportLine(string[] columns, Dictionary<string, int> oldHeaderMap, Dictionary<string, int> newHeaderMap, string[] oldLineData, string[] newLineData);

        protected abstract void WriteToDiffFile(string[] columns, Dictionary<string, T> comparisons, string filePath);

        private Dictionary<string, T> CreateComparisons(string[] columns, IEnumerable<FilePair> filePairs)
        {
            Dictionary<string, T> comparisonsByMethod = new Dictionary<string, T>();

            foreach (var filePair in filePairs)
            {
                BaseLogger.Info(System.Globalization.CultureInfo.InvariantCulture,
                    "Comparing file-pair:{0}", filePair.BaseFile.Name);

                CreateComparisonsForFilePair(filePair.BaseFile, filePair.NewFile, columns)
                    .ForEach(c =>
                    {
                        if (!string.IsNullOrEmpty(c.MethodName))
                        {
                            comparisonsByMethod.Add(c.MethodName, c);
                        }
                    });
            }

            return comparisonsByMethod;
        }

        private List<T> CreateComparisonsForFilePair(FileInfo baseFile, FileInfo newFile, string[] columns)
        {
            List<T> comparisons = new List<T>();
            using (var oldReader = new StreamReader(baseFile.FullName))
            using (var newReader = new StreamReader(newFile.FullName))
            {
                Dictionary<string, int> oldHeaderMap = GenerateHeaderMap(oldReader.ReadLine());
                Dictionary<string, int> newHeaderMap = GenerateHeaderMap(newReader.ReadLine());

                string oldLine = null;
                string newLine = null;

                while ((oldLine = oldReader.ReadLine()) != null &&
                    (newLine = newReader.ReadLine()) != null)
                {
                    string[] oldLineData = ParseLineData(oldLine);
                    string[] newLineData = ParseLineData(newLine);

                    comparisons.Add(CompareReportLine(columns, oldHeaderMap, newHeaderMap, oldLineData, newLineData));
                }
                return comparisons;
            }
        }

        private static Dictionary<string, int> GenerateHeaderMap(string headerLine)
        {
            return headerLine.Split(",")
                .Select((x, i) => new Tuple<string, int>(x, i))
                .ToDictionary(y => y.Item1, y => y.Item2);
        }

        private static string[] ParseLineData(string rawDataLine)
        {
            using (var parser = new TextFieldParser(new StringReader(rawDataLine)))
            {
                parser.HasFieldsEnclosedInQuotes = true;
                parser.SetDelimiters(",");

                return parser.ReadFields();
            }
        }
    }
}
