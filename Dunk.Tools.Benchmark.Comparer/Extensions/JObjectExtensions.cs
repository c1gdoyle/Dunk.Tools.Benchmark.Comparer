using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Dunk.Tools.Benchmark.Comparer.Extensions
{
    /// <summary>
    /// Provides extension methods for a <see cref="JObject"/> instance.
    /// </summary>
    public static class JObjectExtensions
    {
        /// <summary>
        /// Converts a <see cref="JObject"/> instance into a dictionary.
        /// </summary>
        /// <param name="obj">The JSON object to convert.</param>
        /// <returns>
        /// A dictionary contains the JSON details.
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> was null.</exception>
        public static Dictionary<string, object> ToDictionary(this JObject obj)
        {
            obj.ThrowIfNull(nameof(obj));

            var results = obj.ToObject<Dictionary<string, object>>();

            var jObjectKeys = results.Where(kvp => kvp.Value.GetType() == typeof(JObject))
                .Select(kvp => kvp.Key)
                .ToList();
            var jArrayKeys = results.Where(kvp => kvp.Value.GetType() == typeof(JArray))
                .Select(kvp => kvp.Key)
                .ToList();

            jArrayKeys
                .ForEach(key => results[key] = ((JArray)results[key]).Values().Select(x => ((JValue)x).Value).ToArray());
            jObjectKeys
                .ForEach(key => results[key] = ToDictionary(results[key] as JObject));

            return results;
        }
    }
}
