using Dev.Assistant.Business.Core.Models;

namespace Dev.Assistant.Business.Core.DevErrors;

/// <summary>
/// Contains error definitions for Converter-related operations.
/// </summary>
public static partial class DevErrors
{
    public static class Converter
    {
        /// <summary>
        /// Represents an error when the input is null.
        /// </summary>
        public static DevAssistantException E4001InputIsNull => new("Input is null!", 4001);

        /// <summary>
        /// Represents an error for an invalid SQL statement.
        /// </summary>
        public static DevAssistantException E4002InvalidSqlStatement => new("Invalid SQL statement!", 4002);

        /// <summary>
        /// Represents an error when no SQL statement is found.
        /// </summary>
        public static DevAssistantException E4003NoSqlStatementFound => new("No SQL statement found! :(. Try to rename the string variable to 'cmdtxt'", 4003);

        /// <summary>
        /// Represents an error when input is null or empty.
        /// </summary>
        public static DevAssistantException E4101InputIsNullOrEmpty => new("Input is null or empty!", 4101);

        /// <summary>
        /// Represents an error for an invalid SQL statement due to missing keywords.
        /// </summary>
        public static DevAssistantException E4102InvalidSqlKeyword => new("Invalid SQL statement. Must contain at least one keyword (Select, From, Where, etc.)", 4102);

        /// <summary>
        /// Represents an error when the query is already cleaned.
        /// </summary>
        public static DevAssistantException E4103QueryAlreadyCleaned => new("The query is already CLEANED!", 4103);

        /// <summary>
        /// Represents an error when the query cannot be cleaned.
        /// </summary>
        public static DevAssistantException E4104CannotCleanQuery => new("Couldn't clean the query!", 4104);

        /// <summary>
        /// Error when XML to JSON conversion fails.
        /// </summary>
        public static DevAssistantException E5001XmlToJsonConversionError => new("Couldn't convert XML to JSON!", 5001);

        /// <summary>
        /// Error when JSON to XML conversion fails.
        /// </summary>
        public static DevAssistantException E5002JsonToXmlConversionError => new("Couldn't convert JSON to XML!", 5002);
    }
}