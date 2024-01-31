namespace Dev.Assistant.Business.Decoder.Models;

/// <summary>
/// Represents an exception that might be thrown by a microservice.
/// </summary>
public class MicroException
{
    /// <summary>
    /// Error code of the exception.
    /// </summary>
    public int ErrorCode { get; set; }

    /// <summary>
    /// Arabic language error message.
    /// </summary>
    public string ErrorMessageAr { get; set; }

    /// <summary>
    /// English language error message.
    /// </summary>
    public string ErrorMessageEn { get; set; }

    /// <summary>
    /// Placeholder used in the exception for identifying the validation in the code.
    /// </summary>
    public string PlaceHolder { get; set; }
}