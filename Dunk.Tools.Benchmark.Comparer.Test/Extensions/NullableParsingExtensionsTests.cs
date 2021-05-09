using Dunk.Tools.Benchmark.Comparer.Extensions;
using NUnit.Framework;

namespace Dunk.Tools.Benchmark.Comparer.Test.Extensions
{
    [TestFixture]
    public class NullableParsingExtensionsTests
    {
        [Test]
        public void ParseNullableReturnsValueIfInt16ParseIsSuccessful()
        {
            const short expected = 2;

            string s = expected.ToString();

            short? value = s.ParseNullable<short>(short.TryParse);

            Assert.IsTrue(value.HasValue);
            Assert.AreEqual(expected, value);
        }

        [Test]
        public void ParseNullableReturnsNullIfInt16ParseIsUnsuccessful()
        {
            string s = "Aardvark".ToString();

            short? value = s.ParseNullable<short>(short.TryParse);

            Assert.IsFalse(value.HasValue);
        }

        [Test]
        public void ParseNullableReturnsValueIfInt32ParseIsSuccessful()
        {
            const int expected = 2;

            string s = expected.ToString();

            int? value = s.ParseNullable<int>(int.TryParse);

            Assert.IsTrue(value.HasValue);
            Assert.AreEqual(expected, value);
        }

        [Test]
        public void ParseNullableReturnsNullIfInt32ParseIsUnsuccessful()
        {
            string s = "Aardvark".ToString();

            int? value = s.ParseNullable<int>(int.TryParse);

            Assert.IsFalse(value.HasValue);
        }

        [Test]
        public void ParseNullableReturnsValueIfInt64ParseIsSuccessful()
        {
            const long expected = 2;

            string s = expected.ToString();

            long? value = s.ParseNullable<long>(long.TryParse);

            Assert.IsTrue(value.HasValue);
            Assert.AreEqual(expected, value);
        }

        [Test]
        public void ParseNullableReturnsNullIfInt64ParseIsUnsuccessful()
        {
            string s = "Aardvark".ToString();

            long? value = s.ParseNullable<long>(long.TryParse);

            Assert.IsFalse(value.HasValue);
        }

        [Test]
        public void ParseNullableReturnsValueIfDoubleParseIsSuccessful()
        {
            const double expected = 2.0;

            string s = expected.ToString();

            double? value = s.ParseNullable<double>(double.TryParse);

            Assert.IsTrue(value.HasValue);
            Assert.AreEqual(expected, value);
        }

        [Test]
        public void ParseNullableReturnsNullIfDoubleParseIsUnsuccessful()
        {
            string s = "Aardvark".ToString();

            double? value = s.ParseNullable<double>(double.TryParse);

            Assert.IsFalse(value.HasValue);
        }

        [Test]
        public void ParseNullableReturnsValueIfFloatParseIsSuccessful()
        {
            const float expected = 2.0f;

            string s = expected.ToString();

            float? value = s.ParseNullable<float>(float.TryParse);

            Assert.IsTrue(value.HasValue);
            Assert.AreEqual(expected, value);
        }

        [Test]
        public void ParseNullableReturnsNullIfFloatParseIsUnsuccessful()
        {
            string s = "Aardvark".ToString();

            float? value = s.ParseNullable<float>(float.TryParse);

            Assert.IsFalse(value.HasValue);
        }

        [Test]
        public void ParseNullableReturnsValueIfDecimalParseIsSuccessful()
        {
            const decimal expected = 2.0m;

            string s = expected.ToString();

            decimal? value = s.ParseNullable<decimal>(decimal.TryParse);

            Assert.IsTrue(value.HasValue);
            Assert.AreEqual(expected, value);
        }

        [Test]
        public void ParseNullableReturnsNullIfDecimalParseIsUnsuccessful()
        {
            string s = "Aardvark".ToString();

            decimal? value = s.ParseNullable<decimal>(decimal.TryParse);

            Assert.IsFalse(value.HasValue);
        }
    }
}
