using System.IO;
using System.Security.Cryptography;

namespace Dunk.Tools.Benchmark.Comparer.Test.TestUtils
{
    internal static class FileHelper
    {
        public static byte[] GetFileHash(string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Open))
            using (var hashProvider = new MD5CryptoServiceProvider())
            {
                return hashProvider.ComputeHash(stream);
            }
        }
    }
}
