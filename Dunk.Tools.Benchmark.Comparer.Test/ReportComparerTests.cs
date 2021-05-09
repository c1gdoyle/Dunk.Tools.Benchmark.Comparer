using System;
using System.IO;
using System.Linq;
using Dunk.Tools.Benchmark.Comparer.Data;
using Dunk.Tools.Benchmark.Comparer.Test.TestUtils;
using NUnit.Framework;

namespace Dunk.Tools.Benchmark.Comparer.Test
{
    [TestFixture]
    public class ReportComparerTests
    {
        [Test]
        public void ReportComparerThrowsIfCommandLineArgumentsAreNull()
        {
            Assert.Throws<ArgumentNullException>(() => ReportComparer.CompareReports(null));
        }

        [Test]
        public void ReportComparerThrowsIfCommandLineArgumentsBaseDirectoryDoesNotExist()
        {
            var options = new CommandLineOptions
            {
                BaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports//Aardvark"),
                NewDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports//Latest")
            };

            Assert.Throws<DirectoryNotFoundException>(() => ReportComparer.CompareReports(options));
        }

        [Test]
        public void ReportComparerThrowsIfCommandLineArgumentsBaseDirectoryIsNull()
        {
            var options = new CommandLineOptions
            {
                BaseDirectory = null,
                NewDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports//Latest")
            };

            Assert.Throws<DirectoryNotFoundException>(() => ReportComparer.CompareReports(options));
        }

        [Test]
        public void ReportComparerThrowsIfCommandLineArgumentsNewDirectoryDoesNotExist()
        {
            var options = new CommandLineOptions
            {
                BaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports//BaseLine"),
                NewDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports//Aardvark")
            };

            Assert.Throws<DirectoryNotFoundException>(() => ReportComparer.CompareReports(options));
        }

        [Test]
        public void ReportComparerThrowsIfCommandLineArgumentsNewDirectoryIsNull()
        {
            var options = new CommandLineOptions
            {
                BaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports//BaseLine"),
                NewDirectory = null
            };

            Assert.Throws<DirectoryNotFoundException>(() => ReportComparer.CompareReports(options));
        }

        [Test]
        public void ReportComparerGeneratesDiffFile()
        {
            var options = new CommandLineOptions
            {
                BaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports//BaseLine"),
                NewDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports//Latest"),
                Columns = "Method,Mean,Median,Max,Gen 0,Gen 1,Gen 2,Allocated"
            };

            ReportComparer.CompareReports(options);

            Assert.IsTrue(File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DiffReport.csv")));
        }

        [Test]
        public void ReportComparerGeneratesExpectedDiffFile()
        {
            var options = new CommandLineOptions
            {
                BaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports//BaseLine"),
                NewDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports//Latest"),
                Columns = "Method,Mean,Median,Max,Gen 0,Gen 1,Gen 2,Allocated"
            };

            ReportComparer.CompareReports(options);

            byte[] resultHash = FileHelper.GetFileHash(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DiffReport.csv"));
            byte[] exepectedHash = FileHelper.GetFileHash(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports//Expected//DefaultDiffReport.csv"));

            Assert.IsTrue(Enumerable.SequenceEqual(exepectedHash, resultHash));
        }

        [Test]
        public void ReportComparerGeneratesDiffFileInSpecifiedOutputFolder()
        {
            string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DiffReports");

            var options = new CommandLineOptions
            {
                BaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports//BaseLine"),
                NewDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports//Latest"),
                OutputDirectory = outputPath,
                Columns = "Method,Mean,Median,Max,Gen 0,Gen 1,Gen 2,Allocated"
            };

            ReportComparer.CompareReports(options);

            Assert.IsTrue(File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DiffReports//DiffReport.csv")));
        }

        [Test]
        public void ReportComparerGeneratesExpectedDiffFileInSpecifiedOutputFolder()
        {
            string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DiffReports");

            var options = new CommandLineOptions
            {
                BaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports//BaseLine"),
                NewDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports//Latest"),
                OutputDirectory = outputPath,
                Columns = "Method,Mean,Median,Max,Gen 0,Gen 1,Gen 2,Allocated"
            };

            ReportComparer.CompareReports(options);

            byte[] resultHash = FileHelper.GetFileHash(Path.Combine(outputPath, "DiffReport.csv"));
            byte[] exepectedHash = FileHelper.GetFileHash(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports//Expected//DefaultDiffReport.csv"));

            Assert.IsTrue(Enumerable.SequenceEqual(exepectedHash, resultHash));
        }

        [Test]
        public void ReportComparerGeneratesThresholdDiffFile()
        {
            var options = new CommandLineOptions
            {
                BaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports//BaseLine"),
                NewDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports//Latest"),
                ThresholdDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports//Thresholds"),
                Columns = "Method,Mean,Median,Max,Gen 0,Gen 1,Gen 2,Allocated"
            };

            ReportComparer.CompareReports(options);

            Assert.IsTrue(File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DiffReport.csv")));
        }

        [Test]
        public void ReportComparerGeneratesExpectedThresholdDiffFile()
        {
            var options = new CommandLineOptions
            {
                BaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports//BaseLine"),
                NewDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports//Latest"),
                ThresholdDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports//Thresholds"),
                Columns = "Method,Mean,Median,Max,Gen 0,Gen 1,Gen 2,Allocated"
            };

            ReportComparer.CompareReports(options);

            byte[] resultHash = FileHelper.GetFileHash(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DiffReport.csv"));
            byte[] exepectedHash = FileHelper.GetFileHash(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports//Expected//ThresholdDiffReport.csv"));

            Assert.IsTrue(Enumerable.SequenceEqual(exepectedHash, resultHash));
        }

        [TearDown]
        public void Cleanup()
        {
            string defaultDiffFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DiffReport.csv");

            string specifiedDiffFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DiffReports");
            string specifiedDiffFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DiffReports//DiffReport.csv");

            if (File.Exists(defaultDiffFile))
            {
                File.Delete(defaultDiffFile);
            }
            if (File.Exists(specifiedDiffFile))
            {
                File.Delete(specifiedDiffFile);
            }
            if (Directory.Exists(specifiedDiffFolder))
            {
                Directory.Delete(specifiedDiffFolder);
            }
        }
    }
}
