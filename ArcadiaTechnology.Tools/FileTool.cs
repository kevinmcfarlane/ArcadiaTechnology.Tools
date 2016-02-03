using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ArcadiaTechnology.Tools
{
    /// <summary>
    /// File handling helpers.
    /// </summary>
    public static class FileTool
    {
        /// <summary>
        /// Returns a file list from the current directory matching the given search pattern.
        /// </summary>
        /// <param name="directory">The directory to search.</param>
        /// <param name="searchPattern">The search string, such as "*.mp3;*.wma".</param>
        /// <returns>An array of <see cref="FileInfo" /> instances from the current directory matching the given searchPattern.</returns>
        /// <remarks>
        /// This is an extension to <see cref="System.IO.DirectoryInfo.GetFiles()" /> where it is not possible to combine search patterns having
        /// multiple extensions, e.g., retrieving both *.mp3 and *.wma files in one call.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Search Pattern is null or empty.</exception>
        public static FileInfo[] GetFiles(DirectoryInfo directory, string searchPattern)
        {
            // Allow these separators, though clients should really stick with a semi-colon
            char[] searchPatternDelimiter = new char[] { ' ', ';', ',' };

            if (directory == null) throw new ArgumentNullException("directory", "Directory is null.");
            if (string.IsNullOrWhiteSpace(searchPattern)) throw new ArgumentNullException("searchPattern", "Search Pattern is null or empty.");

            string[] patterns = searchPattern.Split(searchPatternDelimiter, StringSplitOptions.RemoveEmptyEntries);
            List<FileInfo> files = new List<FileInfo>();

            foreach (string pattern in patterns)
            {
                FileInfo[] filesForCurrentPattern = directory.GetFiles(pattern);
                files.AddRange(filesForCurrentPattern);
            }

            return files.ToArray();
        }

        /// <summary>
        /// Compares two files for identity using their MD5 hashes.
        /// </summary>
        /// <param name="path1">A relative or absolute path for file 1.</param>
        /// <param name="path2">A relative or absolute path for file 2.</param>
        /// <returns>True if the files are identical, false otherwise.</returns>
        public static bool CompareFiles(string path1, string path2)
        {
            string hash1 = String.Empty;
            string hash2 = String.Empty;

            using (FileStream stream1 = new FileStream(path1, FileMode.Open, FileAccess.Read))
            {
                using (FileStream stream2 = new FileStream(path2, FileMode.Open, FileAccess.Read))
                {
                    hash1 = ComputeHash(stream1);
                    hash2 = ComputeHash(stream2);
                }
            }

            return hash1 == hash2;
        }

        /// <summary>
        /// Computes an MD5 hash code for the specified stream.
        /// </summary>
        /// <param name="stream">An instance of a <c>Stream</c>.</param>
        /// <returns>A hash code converted to a string.</returns>
        private static string ComputeHash(Stream stream)
        {
            StringBuilder result = new StringBuilder();

            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                byte[] hash = md5.ComputeHash(stream);
                foreach (byte b in hash)
                    result.Append(b.ToString("x2"));
            }

            return result.ToString();
        }
    }
}