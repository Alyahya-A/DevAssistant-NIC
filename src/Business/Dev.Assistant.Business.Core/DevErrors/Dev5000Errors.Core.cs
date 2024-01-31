using Dev.Assistant.Business.Core.Models;

namespace Dev.Assistant.Business.Core.DevErrors;

/// <summary>
/// Contains error definitions for Converter-related operations.
/// </summary>
public static partial class DevErrors
{
    public static class Core
    {
        /// <summary>
        /// Error when the provided path is invalid or empty.
        /// </summary>
        public static DevAssistantException E5000InvalidPath => new("Invalid or empty path provided.", 5000);

        /// <summary>
        /// Error when the specified path does not exist on disk.
        /// </summary>
        public static DevAssistantException E5001PathNotExist => new("The specified path does not exist on disk.", 5001);

        /// <summary>
        /// Error when the specified folder path is empty.
        /// </summary>
        public static DevAssistantException E5002EmptyFolderPath => new("The specified folder path is empty.", 5002);
    }
}