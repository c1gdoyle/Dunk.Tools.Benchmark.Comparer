using System;

namespace Dunk.Tools.Benchmark.Comparer.Extensions
{
    /// <summary>
    /// Provides a series of extension methods for checking arguments.
    /// </summary>
    public static class ArgumentExtensions
    {
        /// <summary>
        /// Checks if a given argument is null, if so this method will throw.
        /// </summary>
        /// <typeparam name="T">The type of the argument.</typeparam>
        /// <param name="arg">The argument we are checking.</param>
        /// <param name="parameterName">The name of the argument variable.</param>
        /// <exception cref="ArgumentNullException"><paramref name="arg"/> was null.</exception>
        public static void ThrowIfNull<T>(this T arg, string parameterName)
        {
            ThrowIfNull<T>(arg, parameterName, $"{parameterName} was null");
        }

        /// <summary>
        /// Checks if a given argument is null, if so this method will throw with a specified error message.
        /// </summary>
        /// <typeparam name="T">The type of the argument.</typeparam>
        /// <param name="arg">The argument we are checking.</param>
        /// <param name="parameterName">The name of the argument variable.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <exception cref="ArgumentNullException"><paramref name="arg"/> was null.</exception>
        public static void ThrowIfNull<T>(this T arg, string parameterName, string errorMessage)
        {
            if (ReferenceEquals(arg, null))
            {
                throw new ArgumentNullException(parameterName, errorMessage);
            }
        }
    }
}
