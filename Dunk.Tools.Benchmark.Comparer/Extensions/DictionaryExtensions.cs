using System;
using System.Collections.Generic;

namespace Dunk.Tools.Benchmark.Comparer.Extensions
{
    /// <summary>
    /// Provides a series of extension methods for a <see cref="IDictionary{TKey, TValue}"/> instance.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Attempts to get the value associated with the key. If the key is not represent returns a default result.
        /// </summary>
        /// <typeparam name="TKey">The type of keys stored in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values stored in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <returns>
        /// The value associated with the key if the key was found; otherwise returns default instance of <typeparamref name="TValue"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="dictionary"/> or <paramref name="key"/> parameter was null.</exception>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return GetValueOrDefault(dictionary, key, k => default(TValue));
        }

        /// <summary>
        /// Attempts to get the value associated with the key. If the key is not represent returns a default result.
        /// </summary>
        /// <typeparam name="TKey">The type of keys stored in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values stored in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="defaultValue">A default value of TValue.</param>
        /// <returns>
        /// The value associated with the key if the key was found; otherwise returns <paramref name="defaultValue"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="dictionary"/> or <paramref name="key"/> parameter was null.</exception>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            return GetValueOrDefault(dictionary, key, k => defaultValue);
        }

        /// <summary>
        /// Attempts to get the value associated with the key. If the key is not represent returns a default result.
        /// </summary>
        /// <typeparam name="TKey">The type of keys stored in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values stored in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="defaultGenerator">A delegate that creates a default value based on the key.</param>
        /// <returns>
        /// The value associated with the key if the key was found; otherwise returns the result of the defaultGenerator delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="dictionary"/>, <paramref name="key"/> or <paramref name="defaultGenerator"/> parameter was null.</exception>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> defaultGenerator)
        {
            dictionary.ThrowIfNull(nameof(dictionary));
            key.ThrowIfNull(nameof(key));
            defaultGenerator.ThrowIfNull(nameof(defaultGenerator));

            TValue value;
            if (dictionary.TryGetValue(key, out value))
            {
                return value;
            }
            return defaultGenerator(key);
        }

        /// <summary>
        /// Attempts to get the value associated with the key and convert it to a specified type. If the key is not present returns a default result.
        /// </summary>
        /// <typeparam name="TKey">The type of keys stored in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values stored in the dictionary.</typeparam>
        /// <typeparam name="TResult">The type to convert to.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="defaultResult">A default value of TResult.</param>
        /// <returns>
        /// The converted value associated with the key if the key was found; otherwise returns returns <paramref name="defaultResult"/>.
        /// </returns>
        public static TResult GetAndConvertValueOrDefault<TKey, TValue, TResult>(this IDictionary<TKey, TValue> dictionary, TKey key, TResult defaultResult)
            where TResult : class
        {
            return GetAndConvertValueOrDefault(dictionary, key, k => defaultResult, o => o as TResult);
        }

        /// <summary>
        /// Attempts to get the value associated with the key and convert it to a specified type. If the key is not present returns a default result.
        /// </summary>
        /// <typeparam name="TKey">The type of keys stored in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values stored in the dictionary.</typeparam>
        /// <typeparam name="TResult">The type to convert to.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="defaultResult">A default value of TResult.</param>
        /// <param name="converter">A delegate that converts the value to TResult.</param>
        /// <returns>
        /// The converted value associated with the key if the key was found; otherwise returns returns <paramref name="defaultResult"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="dictionary"/>, <paramref name="key"/> or <paramref name="converter"/> parameter was null.</exception>
        public static TResult GetAndConvertValueOrDefault<TKey, TValue, TResult>(this IDictionary<TKey, TValue> dictionary, TKey key, TResult defaultResult, Func<TValue, TResult> converter)
        {
            return GetAndConvertValueOrDefault(dictionary, key, k => defaultResult, converter);
        }

        /// <summary>
        /// Attempts to get the value associated with the key and convert it to a specified type. If the key is not present returns a default result.
        /// </summary>
        /// <typeparam name="TKey">The type of keys stored in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values stored in the dictionary.</typeparam>
        /// <typeparam name="TResult">The type to convert to.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="defaultGenerator">A delegate that creates a default value based on the key.</param>
        /// <param name="converter">A delegate that converts the value to TResult.</param>
        /// <returns>
        /// The converted value associated with the key if the key was found; otherwise returns the result of the defaultGenerator delegate.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="dictionary"/>, <paramref name="key"/>, <paramref name="defaultGenerator"/> or <paramref name="converter"/>parameter was null.</exception>
        public static TResult GetAndConvertValueOrDefault<TKey, TValue, TResult>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TResult> defaultGenerator, Func<TValue, TResult> converter)
        {
            dictionary.ThrowIfNull(nameof(dictionary));
            key.ThrowIfNull(nameof(key));
            defaultGenerator.ThrowIfNull(nameof(defaultGenerator));
            converter.ThrowIfNull(nameof(converter));

            TValue value;
            if (dictionary.TryGetValue(key, out value))
            {
                return converter(value);
            }
            return defaultGenerator(key);
        }
    }
}
