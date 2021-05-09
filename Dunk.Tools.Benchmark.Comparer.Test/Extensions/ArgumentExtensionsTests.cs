using System;
using System.Threading.Tasks;
using Dunk.Tools.Benchmark.Comparer.Extensions;
using NUnit.Framework;

namespace Dunk.Tools.Benchmark.Comparer.Test.Extensions
{
    [TestFixture]
    public class ArgumentExtensionsTests
    {
        [Test]
        public void ThrowIfNullDoesNotThrowIfArgumentIsNotNull()
        {
            Task t = new Task(() => { });

            Assert.DoesNotThrow(() => t.ThrowIfNull("param"));
        }

        [Test]
        public void ThrowIfNullDoesThrowIfArgumentIsNull()
        {
            Task t = null;

            Assert.Throws<ArgumentNullException>(() => t.ThrowIfNull("param"));
        }

        [Test]
        public void ThrowIfNullThrowsExceptionWithExpectedDefaultMesssage()
        {
            const string expectedErrorMessage = "param was null (Parameter 'param')";
            string errorMessage = null;

            Task t = null;

            try
            {
                t.ThrowIfNull("param");
            }
            catch (ArgumentNullException aEx)
            {
                errorMessage = aEx.Message;
            }

            Assert.AreEqual(expectedErrorMessage, errorMessage);
        }

        [Test]
        public void ArgThrowIfNullThrowsExceptionWithExpectedSpecifiedMessage()
        {
            const string expectedErrorMessage = "Error occurred, param was null (Parameter 'param')";
            string errorMessage = null;

            Task t = null;

            try
            {
                t.ThrowIfNull("param", "Error occurred, param was null");
            }
            catch (ArgumentNullException aEx)
            {
                errorMessage = aEx.Message;
            }

            Assert.AreEqual(expectedErrorMessage, errorMessage);
        }
    }
}
