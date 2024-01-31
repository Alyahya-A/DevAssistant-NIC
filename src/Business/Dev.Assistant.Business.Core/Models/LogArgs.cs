namespace Dev.Assistant.Business.Core.Models;

public class LogArgs
{
    public LogDelegate LogInfo { get; set; }

    public LogDelegate LogSuccess { get; set; }

    public LogDelegate LogTrace { get; set; }

    public LogDelegate LogWarning { get; set; }

    public LogEvent LogEvent { get; set; }

    public LogError LogError { get; set; }
}


public delegate void LogDelegate(string log);

public delegate void LogEvent(Event ev, EventStatus status, string message = "", Exception ex = null);

public delegate void LogError(string log, Event ev = null, EventStatus status = EventStatus.InputError);