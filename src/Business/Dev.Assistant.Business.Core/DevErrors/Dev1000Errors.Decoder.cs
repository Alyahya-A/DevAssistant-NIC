using Dev.Assistant.Business.Core.Models;

namespace Dev.Assistant.Business.Core.DevErrors;

/// <summary>
/// Contains error definitions for Decoder-related operations.
/// </summary>
public static partial class DevErrors
{
    public static class Decoder
    {
        /// <summary>
        /// Error when a property does not start with a capital letter.
        /// </summary>
        /// <param name="propName">Property name.</param>
        /// <param name="objType">Type of the object (e.g., "Class name" or "Property").</param>
        /// <returns>A new DevAssistantException instance with the specified message and error code.</returns>
        public static DevAssistantException E1415PropertyStartCapital(string propName, string objType)
            => new($"Property must start with a capital letter. {objType} must be PascalCase",
                $"Property must start with a capital letter. {objType} must be PascalCase. Please check \"{propName}\" {objType}",
                1415);

        /// <summary>
        /// Error when there are more than two capital letters next to each other.
        /// </summary>
        /// <param name="objName">Property name.</param>
        /// <param name="objType">Type of the object (e.g., "Class name" or "Property").</param>
        /// <returns>A new DevAssistantException instance with the specified message and error code.</returns>
        public static DevAssistantException E1416MoreThanTwoCapitals(string objName, string objType)
            => new($"There are more than two capital letters next to each other. {objType} must be PascalCase",
                $"There are more than two capital letters next to each other. {objType} must be PascalCase. Please check \"{objName}\" {objType}",
                1416);


        /// <summary>
        /// Error when the last three letters of a name are capital letters.
        /// </summary>
        /// <param name="objName">Property name.</param>
        /// <param name="objType">Type of the object (e.g., "Class name" or "Property").</param>
        /// <returns>A new DevAssistantException instance with the specified message and error code.</returns>
        public static DevAssistantException E1417MoreThanTwoCapitals(string objName, string objType)
            => new($"There are more than two capital letters next to each other. {objType} must be PascalCase",
                $"There are more than two capital letters next to each other. {objType} must be PascalCase. Please check \"{objName}\" {objType}",
                1417);

        /// <summary>
        /// Error when more than two capital letters are found consecutively within a name.
        /// </summary>
        /// <param name="objName">Property name.</param>
        /// <param name="objType">Type of the object (e.g., "Class name" or "Property").</param>
        /// <returns>A new DevAssistantException instance with the specified message and error code.</returns>
        public static DevAssistantException E1418MoreThanTwoCapitals(string objName, string objType)
            => new($"There are more than two capital letters next to each other. {objType} must be PascalCase",
                $"There are more than two capital letters next to each other. {objType} must be PascalCase. Please check \"{objName}\" {objType}",
                1418);

        /// <summary>
        /// Error when the English XML documentation does not end with "<br/>".
        /// </summary>
        /// <param name="propName">Property name.</param>
        /// <param name="className">Class name.</param>
        /// <returns>A new DevAssistantException instance with the specified message and error code.</returns>
        public static DevAssistantException E1419XmlDocEnglishLineBreak(string propName, string className)
            => new("The English XML documentation must end with \"<br/>\" (New line)",
                $"The English XML documentation must end with \"<br/>\" (New line). Please check \"{propName}\" property in {className}",
                1419);

        /// <summary>
        /// Error for incorrect languages order in XML documentation.
        /// </summary>
        /// <param name="propName">Property name.</param>
        /// <param name="className">Class name.</param>
        /// <returns>A new DevAssistantException instance with the specified message and error code.</returns>
        public static DevAssistantException E1426InvalidXmlLanguageOrder(string propName, string className)
            => new($"The languages order in XML documentation is incorrect. Ensure that the first lines are in English only. Please check \"{propName}\" property in {className}", 1426);

        /// <summary>
        /// Error when the first XML line is not in English.
        /// </summary>
        /// <param name="propName">Property name.</param>
        /// <param name="className">Class name.</param>
        /// <returns>A new DevAssistantException instance with the specified message and error code.</returns>
        public static DevAssistantException E1427FirstXmlLineNotEnglish(string propName, string className)
            => new($"The first XML line must be in English only. Please check \"{propName}\" property in {className}", 1427);


    }
}
