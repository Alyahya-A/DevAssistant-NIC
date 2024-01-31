namespace Dev.Assistant.Business.Core.Models;

public class Event
{
    public Event(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }

    //public EventStatus Status { get; set; }
}

public enum EventStatus
{
    All, // used only in admin app
    Succeed,
    Failed,
    InputError,
    Clicked,
    Called // if its one status there is no Succeed or Failed...
}