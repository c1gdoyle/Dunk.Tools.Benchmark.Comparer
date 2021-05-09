using System;
using CommandLine;
using Dunk.Tools.Benchmark.Comparer.Data;
using NLog;

namespace Dunk.Tools.Benchmark.Comparer
{
    static class Program
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            try
            {
                Parser.Default.ParseArguments<CommandLineOptions>(args)
                    .WithParsed(ReportComparer.CompareReports);
            }
            catch (Exception ex)
            {
                Logger.Error($"Error occurred comparing benchmark reports. \r\n{ex}");
            }
        }
    }
}
