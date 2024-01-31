using Dev.Assistant.Business.Core.Enums;

namespace Dev.Assistant.Business.Core.Models;

public class AppItem
{
    public int Value { get; set; }

    public string Text { get; set; }

    public DevApp AppType { get; set; }
}