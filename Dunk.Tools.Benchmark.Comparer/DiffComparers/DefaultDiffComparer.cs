using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Dunk.Tools.Benchmark.Comparer.Data;
using Dunk.Tools.Benchmark.Comparer.Utils;
using NLog;

namespace Dunk.Tools.Benchmark.Comparer.DiffComparers
{
    /// <summary>
    /// A helper class that compares and outputs a simple diff comparison 
    /// between benchmark reports.
    /// </summary>
    internal class DefaultDiffComparer : DiffComparerBase<DataMethodComparison>
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initialises a new default instance of <see cref="DefaultDiffComparer"/> with a 
        /// specified directory for writing the output the diff reports to.
        /// </summary>
        /// <param name="outputDirectory">The directory for writing the output diff reports to.</param>
        public DefaultDiffComparer(string outputDirectory)
            : base(outputDirectory)
        {
        }

        public override ILogger BaseLogger
        {
            get { return Logger; }
        }

        protected override DataMethodComparison CompareReportLine(string[] columns, Dictionary<string, int> oldHeaderMap, Dictionary<string, int> newHeaderMap, string[] oldLineData, string[] newLineData)
        {
            DataMethodComparison comparsion = new DataMethodComparison
            {
                DataComparisonsByName = new Dictionary<string, DataMetricComparison>(),
                MethodName = oldLineData[oldHeaderMap["Method"]]
            };

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

                        var metricComparison = new DataMetricComparison
                        {
                            MetricName = column,
                            BaseValue = UnitConversionHelper.ConvertValue(baseLineValue),
                            NewValue = UnitConversionHelper.ConvertValue(newValue)
                        };

                        comparsion.DataComparisonsByName.Add(metricComparison.MetricName, metricComparison);
                    }
                }
            }

            return comparsion;
        }

        protected override void WriteToDiffFile(string[] columns, Dictionary<string, DataMethodComparison> comparisons, string filePath)
        {
            using (var writer = new StreamWriter(new FileStream(filePath, FileMode.Create)))
            {
                //first write headers to filer
                var diffHeaders = columns
                    .Select(x => x == "Method" ? x : x + " Diff")
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

                        DataMetricComparison metric;
                        if (method.DataComparisonsByName.TryGetValue(column, out metric))
                        {
                            sb.Append(metric.Difference);
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
