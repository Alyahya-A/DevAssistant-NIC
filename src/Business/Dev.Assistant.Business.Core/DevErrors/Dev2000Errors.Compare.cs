using Dev.Assistant.Business.Core.Models;

namespace Dev.Assistant.Business.Core.DevErrors;

/// <summary>
/// Contains error definitions for Converter-related operations.
/// </summary>
public static partial class DevErrors
{
    public static class Compare
    {
        /// <summary>
        /// Error when the provided JSON inputs are invalid or empty.
        /// </summary>
        public static DevAssistantException E2001InvalidJsonInput => new("Invalid or empty JSON input provided.", 2001);

        /// <summary>
        /// Error when the count of keys in the JSON objects does not match.
        /// </summary>
        public static DevAssistantException E2002KeysCountMismatch => new("The count of keys in the JSON objects does not match.", 2002);
    }
}