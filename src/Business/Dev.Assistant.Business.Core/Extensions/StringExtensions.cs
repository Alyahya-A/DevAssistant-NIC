using System.Globalization;
using System.Text.RegularExpressions;

namespace Dev.Assistant.Business.Core.Extensions;

/// <summary>
/// Provides extension methods for string operations.
/// </summary>
public static class StringExtensions
{
    private static readonly Regex regex = new(@"\s+");
    private static readonly Regex regRemoveNums = new(@"[\d-]");
    private static readonly Regex isArabicRegex = new(@"\p{IsArabic}");

    /// <summary>
    /// Remove all white spaces and replace the space with a specified string (replacement).
    /// </summary>
    /// <param name="value">String value</param>
    /// <param name="replacement">String to replace white spaces. Default is an empty string.</param>
    /// <returns>New string without spaces. Example: "Hello Word" --> "HelloWord"</returns>
    public static string RemoveWhiteSpaces(this string value, string replacement = "") => regex.Replace(value, replacement).Trim();

    /// <summary>
    /// Remove all numbers from the string and replace the space with a specified string (replacement).
    /// </summary>
    /// <param name="value">String value</param>
    /// <param name="replacement">String to replace white spaces. Default is an empty string.</param>
    /// <returns>New string without numbers. Example: "CP002Timestamp" --> "CPTimestamp"</returns>
    public static string RemoveNumbers(this string value, string replacement = "") => regRemoveNums.Replace(value, replacement);

    /// <summary>
    /// Remove required characters (e.g., '*') from the string.
    /// </summary>
    /// <param name="value">String value</param>
    /// <param name="replacement">String to replace removed characters. Default is an empty string.</param>
    /// <returns>New string without required characters. Example: "*Hello*" --> "Hello"</returns>
    public static string RemoveRequired(this string value, string replacement = "") => value.Replace("*", "").Trim();

    /// <summary>
    /// Remove dashes from the string.
    /// </summary>
    /// <param name="value">String value</param>
    /// <param name="replacement">String to replace dashes. Default is an empty string.</param>
    /// <returns>New string without dashes. Example: "Hello-World" --> "HelloWorld"</returns>
    public static string RemoveDash(this string value, string replacement = "") => value.Replace("-", replacement).Trim();

    /// <summary>
    /// Remove appended lines (e.g., newline characters, tabs) from the string.
    /// </summary>
    /// <param name="value">String value</param>
    /// <param name="replacement">String to replace removed characters. Default is an empty string.</param>
    /// <returns>New string without appended lines. Example: "Hello\nWorld" --> "HelloWorld"</returns>
    public static string RemoveAppendedLine(this string value) => value.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace("<br/>", "").Trim();

    /// <summary>
    /// Capitalize each word in the string.
    /// </summary>
    /// <param name="value">String value</param>
    /// <returns>Capitalized string. Example: "to title CACSE" --> "To Title Case"</returns>
    public static string ToTitleCase(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value));
        }
        else
        {
            // Ex. bayan coNTract >> Bayan Contract
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower()).Trim();
        }
    }

    /// <summary>
    /// Capitalize the first letter of the string.
    /// </summary>
    /// <param name="value">String value</param>
    /// <returns>Capitalized string. Example: "get Person info" --> "Get person info"</returns>
    public static string FirstCharToUpper(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));

        return string.Concat(value[0].ToString().ToUpper(), value.AsSpan(1)).Trim();
    }

    /// <summary>
    /// Split the string by capital letters.
    /// </summary>
    /// <param name="value">String value</param>
    /// <returns>String with spaces between words. Example: "ThisIsAString" --> "This Is A String"</returns>
    public static string SplitByCapitalLetter(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));

        // Split by capital letter
        var pattern = new Regex(@"
                            (?<=[A-Z])(?=[A-Z][a-z]) |
                            (?<=[^A-Z])(?=[A-Z])  |
                            (?<=[^A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);
        //pattern = new Regex(@"(?<!^)(?=[A-Z])");

        value = pattern.Replace(value, " ");

        // if the string contains a number
        if (value.Any(char.IsDigit))
        {
            // To convert LKMV2 0 9 to LKMV209
            string newValue = string.Empty;

            foreach (var ch in value)
            {
                if (char.IsDigit(ch))
                    newValue += $"$$${ch}";
                else
                    newValue += ch;
            }

            // in the first num case (LKMV2 it will be LKMV$2) then other numbers like ' 0' will be ' $0'
            value = newValue.Replace(" $$$", "").Replace("$$$", "");
        }

        return value.Trim();
    }

    /// <summary>
    /// Get a short Arabic description for specific values.
    /// </summary>
    /// <param name="value">String value</param>
    /// <returns>Short Arabic description. Example: "PersonID" --> "رقم هوية الشخص"</returns>
    public static string ShortDescAr(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));

        if (value.RemoveRequired().Equals("PersonID", StringComparison.InvariantCultureIgnoreCase))
        {
            return "رقم هوية الشخص";
        }
        else if (value.RemoveRequired().Trim().Equals("OwnerID", StringComparison.InvariantCultureIgnoreCase))
        {
            return "رقم هوية المالك";
        }
        else if (value.RemoveRequired().Trim().Equals("SponsorID", StringComparison.InvariantCultureIgnoreCase))
        {
            return "رقم هوية الكفيل";
        }
        else if (value.RemoveRequired().Trim().Equals("FirmID", StringComparison.InvariantCultureIgnoreCase))
        {
            return "رقم هوية المنشآة";
        }
        else if (value.RemoveRequired().Trim().Equals("ID", StringComparison.InvariantCultureIgnoreCase))
        {
            return "رقم الهوية";
        }
        else if (value.RemoveRequired().Trim().Equals("OperatorID", StringComparison.InvariantCultureIgnoreCase))
        {
            return "رقم هوية المشغل";
        }
        else if (value.RemoveRequired().Trim().Equals("BirthDate", StringComparison.InvariantCultureIgnoreCase))
        {
            return "تاريخ الميلاد";
        }
        else if (value.RemoveRequired().Trim().Equals("Lang", StringComparison.InvariantCultureIgnoreCase))
        {
            return "اللغة";
        }

        return null;
    }

    /// <summary>
    /// Check if the string represents a data type.
    /// </summary>
    /// <param name="value">String value</param>
    /// <returns>True if the string represents a data type; otherwise, false.</returns>
    public static bool IsDataType(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value));
        }

        // TODO: Implement....
        // I forgot why I created this extension :)
        return false;
    }

    /// <summary>
    /// Check if the string contains Arabic characters.
    /// </summary>
    /// <param name="value">String value</param>
    /// <returns>True if the string contains Arabic characters; otherwise, false.</returns>
    public static bool IsArabic(this string value) => DetermineLanguage(value);

    /// <summary>
    /// Check if the string represents a namespace related to Npv.
    /// </summary>
    /// <param name="value">String value</param>
    /// <returns>True if the string represents a namespace related to Npv; otherwise, false.</returns>
    public static bool IsNamespaceNpv(this string value) => !value.Contains("Nic.Apis.NpvPortal") && value.StartsWith("Nic.Apis.Npv", StringComparison.CurrentCultureIgnoreCase);



    private static bool DetermineLanguage(string text)
    {
        // Simple heuristic: If the text contains more Arabic characters, consider it Arabic; otherwise, English
        int arabicCount = text.Count(c => c >= 0x600 && c <= 0x6FF);
        return arabicCount > text.Length / 2;
    }
}