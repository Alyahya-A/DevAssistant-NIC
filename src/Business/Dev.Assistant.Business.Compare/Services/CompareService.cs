// Error code start with 2000

using Dev.Assistant.Business.Compare.Models;
using Dev.Assistant.Business.Compare.Utilities;
using Dev.Assistant.Business.Core.DevErrors;
using Dev.Assistant.Business.Core.Models;
using Dev.Assistant.Business.Decoder.Models;
using Dev.Assistant.Business.Decoder.Services;
using Newtonsoft.Json.Linq;
using Serilog;

namespace Dev.Assistant.Business.Compare.Services;

/// <summary>
/// Service for comparing JSON objects based on specified options.
/// </summary>
public partial class CompareService
{
    #region Public Methods

    /// <summary>
    /// Compares two JSON strings based on specified options.
    /// </summary>
    /// <param name="input1">The first JSON string to compare.</param>
    /// <param name="input2">The second JSON string to compare.</param>
    /// <param name="options">Options for the JSON comparison.</param>
    /// <returns><c>true</c> if the JSON objects are equal; otherwise, <c>false</c>.</returns>
    public static bool CompareJson(string input1, string input2, CompareJsonOptions options = null)
    {
        Log.Logger.Information("CompareJson Called");

        try
        {
            options ??= new CompareJsonOptions();

            // Validate inputs
            if (string.IsNullOrWhiteSpace(input1) || string.IsNullOrWhiteSpace(input2))
            {
                throw DevErrors.Compare.E2001InvalidJsonInput;
            }

            JObject json1 = ParseJson(input1, options.ToLowerCase);
            JObject json2 = ParseJson(input2, options.ToLowerCase);

            ValidateKeysCount(json1, json2);

            // If OnlyKeys is true, compare sorted keys; otherwise, perform a deep comparison.
            return options.OnlyKeys ? CompareKeys(json1, json2) : JToken.DeepEquals(json1, json2);
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "An unexpected error occurred in CompareJson.");
            throw;
        }
    }

    /// <summary>
    /// Compares two models (C# or VB).
    /// </summary>
    /// <param name="input1">The first model string to compare.</param>
    /// <param name="input2">The second model string to compare.</param>
    /// <returns></returns>
    public static CompareModelResult CompareModel(string input1, string input2)
    {
        Log.Logger.Information("CompareJson Called");

        try
        {
            List<ClassModel> preClasses = ModelExtractionService.GetClassesByCode(input1);
            List<ClassModel> currentClasses = ModelExtractionService.GetClassesByCode(input2);

            return new CompareModelResult
            {
                Models1 = preClasses,
                Models2 = currentClasses
            };
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "An unexpected error occurred in CompareModel.");

            throw;
        }
    }

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Parses a JSON string into a JObject based on specified options.
    /// </summary>
    private static JObject ParseJson(string json, bool toLowerCase) => toLowerCase ? JObject.Parse(json.Trim().ToLower()) : JObject.Parse(json.Trim());

    /// <summary>
    /// Validates the count of keys in two JSON objects.
    /// </summary>
    /// <param name="json1">The first JObject to compare.</param>
    /// <param name="json2">The second JObject to compare.</param>
    /// <exception cref="DevAssistantException">Thrown if the count of keys in the JSON objects does not match.</exception>
    private static void ValidateKeysCount(JObject json1, JObject json2)
    {
        // Extract the keys from each JObject.
        var keys1 = json1.Properties().Select(prop => prop.Name).ToList();
        var keys2 = json2.Properties().Select(prop => prop.Name).ToList();

        // Compare the count of keys in the two JObjects.
        if (keys1.Count != keys2.Count)
        {
            throw DevErrors.Compare.E2002KeysCountMismatch;
        }
    }

    /// <summary>
    /// Compares the keys of two JSON objects.
    /// </summary>
    /// <remarks>
    /// This method sorts the keys of both JSON objects using the JsonKeyComparer and then
    /// compares them to check if they are sequence-equal. This ensures that the key order does not affect the comparion result.
    /// </remarks>
    /// <param name="json1">The first JSON object.</param>
    /// <param name="json2">The second JSON object.</param>
    /// <returns>True if the keys of both JSON objects are equal; otherwise, false.</returns>
    private static bool CompareKeys(JObject json1, JObject json2)
    {
        var keys1 = SortKeys(json1);
        var keys2 = SortKeys(json2);

        return keys1.SequenceEqual(keys2);
    }

    /// <summary>
    /// Sorts the keys of a JSON object.
    /// </summary>
    /// <param name="json">The JSON object whose keys are to be sorted.</param>
    /// <returns>A list of sorted keys.</returns>
    /// <remarks>
    /// This method extracts the keys from the JSON object, sorts them using the JsonKeyComparer,
    /// and returns the sorted list of keys. This sorting is case-sensitive, and thats why we have options.ToLowerCase
    /// </remarks>
    private static List<string> SortKeys(JObject json)
    {
        var keys = json.Properties().Select(prop => prop.Name).ToList();
        keys.Sort(new JsonKeyComparer());

        return keys;
    }

    #endregion Private Methods
}