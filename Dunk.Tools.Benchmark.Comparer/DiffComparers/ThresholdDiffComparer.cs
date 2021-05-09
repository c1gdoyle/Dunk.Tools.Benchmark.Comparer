using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Dunk.Tools.Benchmark.Comparer.Data;
using Dunk.Tools.Benchmark.Comparer.Extensions;
using Dunk.Tools.Benchmark.Comparer.Utils;
using NLog;

namespace Dunk.Tools.Benchmark.Comparer.DiffComparers
{
    /// <summary>
    /// A helper class that compares and outputs a threshold diff comparison 
    /// between benchmark reports.
    /// </summary>
    internal class ThresholdDiffComparer : DiffComparerBase<DataMethodThresholdComparison>
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly Dictionary<string, DataMethodThreshold> _thresholds;

        /// <summary>
        /// Initialises a new default instance of <see cref="ThresholdDiffComparer"/> with 
        /// specified threshold data and a specified directory for writing the output the 
        /// diff reports to.
        /// </summary>
        /// <param name="thresholds"> The threshold data.</param>
        /// <param name="outputDirectory">The directory for writing the output diff reports to.</param>
        /// <exception cref="ArgumentNullException"><paramref name="outputDirectory"/> or <paramref name="thresholds"/> was null.</exception>
        public ThresholdDiffComparer(
            Dictionary<string, DataMethodThreshold> thresholds,
            string outputDirectory)
            : base(outputDirectory)
        {
            thresholds.ThrowIfNull(nameof(thresholds));
            _thresholds = thresholds;
        }

        public override ILogger BaseLogger
        {
            get { return Logger; }
        }

        protected override DataMethodThresholdComparison CompareReportLine(string[] columns, Dictionary<string, int> oldHeaderMap, Dictionary<string, int> newHeaderMap, string[] oldLineData, string[] newLineData)
        {
            DataMethodThresholdComparison comparsion = new DataMethodThresholdComparison
            {
                DataComparisonsByName = new Dictionary<string, DataMetricThresholdComparison>(),
                MethodName = oldLineData[oldHeaderMap["Method"]]
            };

            DataMethodThreshold threshold = DictionaryExtensions.GetValueOrDefault(_thresholds, comparsion.MethodName);

            foreach (string column in columns)
            {
                if (column != "Method")
                {
                    int oldIndex;
                    int newIndex;
                    if (oldHeaderMap.TryGetValue(column, out oldIndex) &&
                        newHeaderMap.TryGetValue(column, out newIndex))
                    {
                        string baseLineValue = oldLineData[oldIndex];
                        string newValue = newLineData[newIndex];
                        decimal? thresholdValue = threshold != null ?
                            DictionaryExtensions.GetValueOrDefault(threshold.ThresholdsByName, column) :
                            null;

                        var metricComparison = new DataMetricThresholdComparison
                        {
                            MetricName = column,
                            BaseValue = UnitConversionHelper.ConvertValue(baseLineValue),
                            NewValue = UnitConversionHelper.ConvertValue(newValue),
                            Threshold = thresholdValue
                        };

                        comparsion.DataComparisonsByName.Add(metricComparison.MetricName, metricComparison);
                    }
                }
            }

            return comparsion;
        }

        protected override void WriteToDiffFile(string[] columns, Dictionary<string, DataMethodThresholdComparison> comparisons, string filePath)
        {
            using (var writer = new StreamWriter(new FileStream(filePath, FileMode.Create)))
            {
                //first write headers to filer
                var diffHeaders = columns
                    .Select(x => x == "Method" ? x : x + " Diff, " + x + " W/I ThresH")
                    .ToArray();

                string headers = string.Join(", ", diffHeaders);

                writer.WriteLine(headers);

                foreach (var kvp in comparisons)
                {
                    var method = kvp.Value;

                    StringBuilder sb = new StringBuilder();
                    sb.Append(method.MethodName);
                    sb.Append(", ");

                    for (int i = 0; i < columns.Length; i++)
                    {
                        string column = columns[i];
                        if (column == "Method")
                        {
                            continue;
                        }

                        DataMetricThresholdComparison metric;
                        if (method.DataComparisonsByName.TryGetValue(column, out metric))
                        {
                            sb.Append(metric.Difference);
                            sb.Append(", ");
                            sb.Append(metric.WithinThreshold);
                            if (i != columns.Length - 1)
                            {
                                sb.Append(", ");
                            }
                        }
                    }
                    writer.WriteLine(sb);
                }
            }
        }
    }
}
