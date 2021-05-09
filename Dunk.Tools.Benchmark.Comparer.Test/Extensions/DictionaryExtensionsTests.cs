using System;
using System.Collections.Generic;
using Dunk.Tools.Benchmark.Comparer.Extensions;
using NUnit.Framework;

namespace Dunk.Tools.Benchmark.Comparer.Test.Extensions
{
    [TestFixture]
    public class DictionaryExtensionsTests
    {
        [Test]
        public void GetValueOrDefaultThrowsIfDictionaryIsNull()
        {
            IDictionary<string, int> dictionary = null;
            string key = "key1";

            Assert.Throws<ArgumentNullException>(() => dictionary.GetValueOrDefault(key));
        }

        [Test]
        public void GetValueOrDefaultThrowsIfKeyIsNull()
        {
            IDictionary<string, int> dictionary = new Dictionary<string, int>();
            string key = null;

            Assert.Throws<ArgumentNullException>(() => dictionary.GetValueOrDefault(key));
        }

        [Test]
        public void GetValueOrDefaultThrowsIfDefaultGeneratorIsNull()
        {
            IDictionary<string, int> dictionary = new Dictionary<string, int>();
            string key = "key1";

            Assert.Throws<ArgumentNullException>(() => dictionary.GetValueOrDefault(key, null));
        }

        [Test]
        public void GetValueOrDefaultReturnsValueIfKeyIsPresent()
        {
            IDictionary<string, int> dictionary = new Dictionary<string, int>
            {
                { "key1", 1 }
            };
            string key = "key1";

            int result = dictionary.GetValueOrDefault(key);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void GetValueOrDefaultReturnsDefaultInstanceOfValueIfKeyIsNotPresent()
        {
            IDictionary<string, int> dictionary = new Dictionary<string, int>
            {
                { "key1", 1 }
            };
            string key = "key2";

            int result = dictionary.GetValueOrDefault(key);

            Assert.AreEqual(default(int), result);
        }

        [Test]
        public void GetValueOrDefaultReturnsSpecifiedDefaultValueIfKeyIsNotPresent()
        {
            const int specifiedDefault = 2;

            IDictionary<string, int> dictionary = new Dictionary<string, int>
            {
                { "key1", 1 }
            };
            string key = "key2";

            int result = dictionary.GetValueOrDefault(key, specifiedDefault);

            Assert.AreEqual(specifiedDefault, result);
        }

        [Test]
        public void GetValueOrDefaultReturnsGeneratedDefaultValueIfKeyIsNotPresent()
        {
            const int specifiedDefault = 2;

            Dictionary<string, int> dictionary = new Dictionary<string, int>
            {
                { "key1", 1 }
            };
            string key = "key2";

            int result = dictionary.GetValueOrDefault(key, k => specifiedDefault);

            Assert.AreEqual(specifiedDefault, result);
        }

        [Test]
        public void GetAndConvertValueOrDefaultThrowsIfDictionaryIsNull()
        {
            IDictionary<string, object> dictionary = null;
            string key = "key1";
            Func<object, string> converter = o => o.ToString();

            Assert.Throws<ArgumentNullException>(() => dictionary.GetAndConvertValueOrDefault(key, string.Empty, converter));
        }

        [Test]
        public void GetAndConvertValueOrDefaultThrowsIfKeyIsNull()
        {
            IDictionary<string, object> dictionary = new Dictionary<string, object>();
            string key = null;
            Func<object, string> converter = o => o.ToString();

            Assert.Throws<ArgumentNullException>(() => dictionary.GetAndConvertValueOrDefault(key, string.Empty, converter));
        }

        [Test]
        public void GetAndConvertValueOrDefaultThrowsIfConverterIsNull()
        {
            IDictionary<string, object> dictionary = new Dictionary<string, object>();
            string key = "key1";
            Func<object, string> converter = null;

            Assert.Throws<ArgumentNullException>(() => dictionary.GetAndConvertValueOrDefault(key, string.Empty, converter));
        }

        [Test]
        public void GetAndConvertValueOrDefaultThrowsIfDefaultGeneratorIsNull()
        {
            IDictionary<string, object> dictionary = new Dictionary<string, object>();
            string key = "Key1";
            Func<string, string> defaultGenerator = null;
            Func<object, string> converter = o => o.ToString();

            Assert.Throws<ArgumentNullException>(() => dictionary.GetAndConvertValueOrDefault(key, defaultGenerator, converter));
        }

        [Test]
        public void GetAndConvertValueOrDefaultReturnsValueIfKeyIsPresent()
        {
            const string expected = "1";
            IDictionary<string, object> dictionary = new Dictionary<string, object>
            {
                { "Key1", 1}
            };
            string key = "Key1";
            Func<object, string> converter = o => Convert.ToString(o);

            string result = dictionary.GetAndConvertValueOrDefault(key, string.Empty, converter);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetAndConvertValueOrDefaultReturnsSpecifiedDefaultIfKeyIsNotPresent()
        {
            const string expected = "foo";
            IDictionary<string, object> dictionary = new Dictionary<string, object>
            {
                { "Key1", 1}
            };
            string key = "Key2";
            Func<object, string> converter = o => Convert.ToString(o);

            string result = dictionary.GetAndConvertValueOrDefault(key, "foo", converter);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetAndConvertValueOrDefaultReturnsGeneratedDefaultIfKeyIsNotPresent()
        {
            const string expected = "Key2";
            IDictionary<string, object> dictionary = new Dictionary<string, object>
            {
                { "Key1", 1}
            };
            string key = "Key2";
            Func<object, string> converter = o => Convert.ToString(o);

            string result = dictionary.GetAndConvertValueOrDefault(key, k => k, converter);

            Assert.AreEqual(expected, result);
        }
    }
}
