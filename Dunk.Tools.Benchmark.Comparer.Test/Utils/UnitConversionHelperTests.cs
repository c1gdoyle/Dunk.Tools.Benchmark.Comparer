using Dunk.Tools.Benchmark.Comparer.Utils;
using NUnit.Framework;

namespace Dunk.Tools.Benchmark.Comparer.Test.Utils
{
    [TestFixture]
    public class UnitConversionHelperTests
    {
        [Test]
        public void UnitConversionHelperReturnsExpectedResultForSeconds()
        {
            const decimal expected = 907400000000m;

            string s = "907.4 s";

            decimal? result = UnitConversionHelper.ConvertValue(s);

            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void UnitConversionHelperReturnsExpectedResultForMilliSeconds()
        {
            const decimal expected = 907400000m;

            string s = "907.4 ms";

            decimal? result = UnitConversionHelper.ConvertValue(s);

            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void UnitConversionHelperReturnsExpectedResultForMicroSeconds()
        {
            const decimal expected = 907400m;

            string s = "907.4 us";

            decimal? result = UnitConversionHelper.ConvertValue(s);

            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void UnitConversionHelperReturnsExpectedResultForNanoSeconds()
        {
            const decimal expected = 907.4m;

            string s = "907.4 ns";

            decimal? result = UnitConversionHelper.ConvertValue(s);

            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void UnitConversionHelperReturnsExpectedResultGigaBytes()
        {
            const decimal expected = 5880000000m;

            string s = "5.88 GB";

            decimal? result = UnitConversionHelper.ConvertValue(s);

            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void UnitConversionHelperReturnsExpectedResultForMegaBytes()
        {
            const decimal expected = 5880000m;

            string s = "5.88 MB";

            decimal? result = UnitConversionHelper.ConvertValue(s);

            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(expected, result);

        }

        [Test]
        public void UnitConversionHelperReturnsExpectedResultForKiloBytes()
        {
            const decimal expected = 5880m;

            string s = "5.88 KB";

            decimal? result = UnitConversionHelper.ConvertValue(s);

            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(expected, result);

        }

        [Test]
        public void UnitConversionHelperReturnsExpectedResultForBytes()
        {
            const decimal expected = 5.88m;

            string s = "5.88 B";

            decimal? result = UnitConversionHelper.ConvertValue(s);

            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void UnitConversionHelperReturnsExpectedResultForUnknownUnit()
        {
            const decimal expected = 5.88m;

            string s = "5.88 Aardvark";

            decimal? result = UnitConversionHelper.ConvertValue(s);

            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void UnitConversionHelperReturnsExpectedResultForNoUnit()
        {
            const decimal expected = 5.88m;

            string s = "5.88";

            decimal? result = UnitConversionHelper.ConvertValue(s);

            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void UnitConversionHelperReturnsNullIfInputIsNullString()
        {
            string s = null;

            decimal? result = UnitConversionHelper.ConvertValue(s);

            Assert.IsFalse(result.HasValue);
        }

        [Test]
        public void UnitConversionHelperReturnsNullIfInputIsEmptyString()
        {
            string s = string.Empty;

            decimal? result = UnitConversionHelper.ConvertValue(s);

            Assert.IsFalse(result.HasValue);
        }

        [Test]
        public void UnitConversionHelperReturnsNullIfInputIsWhiteSpaceString()
        {
            string s = "   ";

            decimal? result = UnitConversionHelper.ConvertValue(s);

            Assert.IsFalse(result.HasValue);
        }
    }
}
