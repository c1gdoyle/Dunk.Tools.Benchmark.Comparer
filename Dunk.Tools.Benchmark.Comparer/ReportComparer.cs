using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dunk.Tools.Benchmark.Comparer.Data;
using Dunk.Tools.Benchmark.Comparer.DiffComparers;
using Dunk.Tools.Benchmark.Comparer.Extensions;
using Dunk.Tools.Benchmark.Comparer.Utils;
using Newtonsoft.Json.Linq;
using NLog;

namespace Dunk.Tools.Benchmark.Comparer
{
    /// <summary>
    /// A helper class that compares Benchmark reports.
    /// </summary>
    public static class ReportComparer
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Compares benchmark reports using the specified command-line arguments.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void CompareReports(CommandLineOptions args)
        {
            CheckCommandLineArguments(args);

            //Determine columns
            Logger.Info(System.Globalization.CultureInfo.InvariantCulture,
                "Columns to compare {0}", args.Columns);
            var columns = args.Columns.Split(",");

            //Sort file-pairs
            var filePairs = CreateFilePairs(new DirectoryInfo(args.BaseDirectory), new DirectoryInfo(args.NewDirectory));

            //Parse data thresholds
            Dictionary<string, DataMethodThreshold> thresholds = args.ThresholdDirectory != null ? CreateThresholds(new DirectoryInfo(args.ThresholdDirectory)) : null;

            //Compare file-pairs
            if (thresholds == null)
            {
                var diffComparer = new DefaultDiffComparer(args.OutputDirectory);
                diffComparer.CompareAndWriteDiffFiles(columns, filePairs);
            }
            else
            {

                var thresholdDiffComparer = new ThresholdDiffComparer(thresholds, args.OutputDirectory);
                thresholdDiffComparer.CompareAndWriteDiffFiles(columns, filePairs);
            }
        }
        private static Dictionary<string, DataMethodThreshold> CreateThresholds(DirectoryInfo thresholdDirectory)
        {
            Logger.Info("Parsing threshold data");
            var thresholds = new Dictionary<string, DataMethodThreshold>();

            foreach (var thresholdFile in thresholdDirectory.GetFiles("*.json"))
            {
                var fileThresholds = Newtonsoft.Json.JsonConvert.DeserializeObject(File.ReadAllText(thresholdFile.FullName)) as JArray;
                if (fileThresholds != null)
                {
                    foreach (var fileThresHold in fileThresholds)
                    {
                        var details = JObject.FromObject(fileThresHold)
                            .ToDictionary();

                        DataMethodThreshold threshold = new DataMethodThreshold
                        {
                            MethodName = details["MethodName"].ToString(),
                            ThresholdsByName = details.Where(x => x.Key != "MethodName")
                                .ToDictionary(kvp => kvp.Key, kvp => UnitConversionHelper.ConvertValue(kvp.Value.ToString()))
                        };
                        thresholds[threshold.MethodName] = threshold;
                    }
                }
            }
            Logger.Info(System.Globalization.CultureInfo.InvariantCulture,
                "Parsed {0} thresholds", thresholds.Count);

            return thresholds;
        }

        private static void CheckCommandLineArguments(CommandLineOptions args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args),
                    $"Unable to compare reports {nameof(args)} parameter was null");
            }

            if (!Directory.Exists(args.BaseDirectory))
            {
                throw new DirectoryNotFoundException(
                    $"Unable to compare reports. Base Directory:{args.BaseDirectory} was not found");
            }
            if (!Directory.Exists(args.NewDirectory))
            {
                throw new DirectoryNotFoundException(
                    $"Unable to compare reports. New Directory:{args.NewDirectory} was not found");
            }
            Logger.Info("Base-Directory:{0} and New-Directory:{1} are valid.", args.BaseDirectory, args.NewDirectory);

            if (string.IsNullOrEmpty(args.OutputDirectory))
            {
                args.OutputDirectory = Directory.GetCurrentDirectory();
            }
            Logger.Info(System.Globalization.CultureInfo.InvariantCulture,
                "Output-Directory set to :{0}", args.OutputDirectory);
        }

        private static IList<FilePair> CreateFilePairs(DirectoryInfo baseDirectory, DirectoryInfo newDirectory)
        {
            Logger.Info("Creating file-pairs");
            var pairs = new List<FilePair>();

            foreach (var oldFile in baseDirectory.GetFiles("*-report.csv"))
            {
                FileInfo newFile = new FileInfo(Path.Combine(newDirectory.FullName, oldFile.Name));

                if (newFile.Exists)
                {
                    pairs.Add(new FilePair { BaseFile = oldFile, NewFile = newFile });
                }
            }
            Logger.Info(System.Globalization.CultureInfo.InvariantCulture,
                "Created {0} file-pairs", pairs.Count);
            return pairs;
        }
    }
}
