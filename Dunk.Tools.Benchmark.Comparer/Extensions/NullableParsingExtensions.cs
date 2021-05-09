namespace Dunk.Tools.Benchmark.Comparer.Extensions
{
    /// <summary>
    /// Provides extension methods for parsing a string to a nullable struct.
    /// </summary>
    public static class NullableParsingExtensions
    {
        /// <summary>
        /// Defines the structure of a delegate for parsing a nullable struct 
        /// from a given <see cref="string"/>.
        /// </summary>
        /// <typeparam name="T">The type of struct being parsed.</typeparam>
        /// <param name="s">The string to be parsed.</param>
        /// <param name="value">
        /// When this method returns if parsing was successful will contain a value T equivalent to <param ref="s"/>;
        /// otherwise will be null.
        /// </param>
        /// <returns>
        /// <c>true</c> if parsing was successful; otherwise <c>false</c>.
        /// </returns>
        /// <remarks>
        /// It is expected that the delegate is the matching TryParse for the struct we are attempting 
        /// to parse.
        /// e.g. if we are attempting to parse a <see cref="int"/> the NullableParser should be <see cref="int.TryParse(string, out int)"/>.
        /// </remarks>
        public delegate bool NullableParser<T>(string s, out T value);

        /// <summary>
        /// Attempts to convert a string representation to a value of <typeparamref name="T"/>. 
        /// </summary>
        /// <typeparam name="T">The type of struct being parsed.</typeparam>
        /// <param name="s">The string to be parsed.</param>
        /// <param name="parser">The parser to use to convert to string.</param>
        /// <returns>
        /// A nullable instance of <typeparamref name="T"/>, if parsing was successful will be the equivalent of the string; 
        /// otherwise will be null.
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="parser"/> was null.</exception>
        public static T? ParseNullable<T>(this string s, NullableParser<T> parser)
            where T : struct
        {
            parser.ThrowIfNull(nameof(parser));

            T value;
            if (parser(s, out value))
            {
                return value;
            }
            return null;
        }
    }
}
