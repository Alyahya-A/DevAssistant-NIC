namespace Dev.Assistant.Business.Core.Models;

public class DevAssistantException : Exception
{
    public DevAssistantException(string message, int code) : base(message) => Code = code;

    public DevAssistantException(string shortMessage, string longMessage, int code) : base(longMessage)
    {
        Code = code;
        _shortMessage = shortMessage;
    }

    public int Code { get; set; }

    private string _shortMessage;

    public override string ToString()
    {
        return string.Format("[{0}] {1}", Code, base.Message);
    }

    //                           $"      * There are more than 2 capital letter next to each other. {objType} must be PascalCase.";
    public string ToRemarks() => $"      * {_shortMessage}.";

    public string ToLongRemarks() => $"{base.Message} [{Code}]{Environment.NewLine}";
}