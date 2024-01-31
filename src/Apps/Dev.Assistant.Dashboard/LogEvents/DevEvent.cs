namespace Dev.Assistant.Dashboard.LogEvents;

public class DevEVent
{
    public DateTime DateTimestamp { get; set; }
    public string EventId { get; set; } = "";
    public string EventName { get; set; } = "";
    public string EventStatus { get; set; } = "";
    public string Message { get; set; } = "";
    public string Exception { get; set; } = "";
}