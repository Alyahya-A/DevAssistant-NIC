using System.Text.Json.Serialization;

namespace Dev.Assistant.Configuration.Models;

public class BusinessApiException
{
    /// <summary>
    /// Only used to write the line of the validation in appsetting.
    /// So that we can search for this ex in the code
    /// </summary>
    public string PlaceHolder { get; set; }

    public int HttpStatusCode { get; set; }
    public int ErrorCode { get; set; }
    public string ErrorMessageAr { get; set; }
    public string ErrorMessageEn { get; set; }

    /// <summary>
    /// It will be used only if there are error with same code. in DevAssistantException #7106
    /// Format is (Line, File). ex: (129 in Root) or (92 in Business Layer).
    /// if all error in BL the format wii be the line number only. ex 92
    /// </summary>
    [JsonIgnore]
    public string FoundAtLine { get; set; }

    [JsonIgnore]
    public bool AllowDuplicate { get; set; } // because some error in Api has same error code but diff message
}