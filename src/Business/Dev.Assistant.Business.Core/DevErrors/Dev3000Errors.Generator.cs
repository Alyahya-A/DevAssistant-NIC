using Dev.Assistant.Business.Core.Models;

namespace Dev.Assistant.Business.Core.DevErrors;

/// <summary>
/// Contains error definitions for Converter-related operations.
/// </summary>
public static partial class DevErrors
{
    public static class Generator
    {
        /// <summary>
        /// Error when the provided path is invalid or empty.
        /// </summary>
        public static DevAssistantException E3001InvalidPath => new("Invalid or empty path provided.", 3001);
    }
}