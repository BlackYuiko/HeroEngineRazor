using System.IO;

namespace HeroEngine.Core.Data
{
    /// <summary>
    /// Holds global path configurations for the engine's file persistence.
    /// </summary>
    public static class PathConfig
    {
        public static string DataFolderPath { get; set; } = string.Empty;

        /// <summary>
        /// Gets the full path for a specific file within the Data folder.
        /// </summary>
        public static string GetFilePath(string fileName)
        {
            if (string.IsNullOrEmpty(DataFolderPath))
            {
                throw new DirectoryNotFoundException("DataFolderPath has not been initialized. Please set it at application startup.");
            }

            if (!Directory.Exists(DataFolderPath))
            {
                Directory.CreateDirectory(DataFolderPath);
            }

            return Path.Combine(DataFolderPath, fileName);
        }
    }
}