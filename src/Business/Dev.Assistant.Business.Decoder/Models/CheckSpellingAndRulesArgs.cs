using Dev.Assistant.Business.Core.Models;

namespace Dev.Assistant.Business.Decoder.Models;

public class CheckSpellingAndRulesArgs
{
    // List<ClassModel> models, Event ev, bool alwaysShowDialog, string nameSpace, Form CheckSpellingDialog, Func< string> func

    public List<ClassModel> Models { get; set; }

    public Event Event { get; set; }

    public bool AlwaysShowDialog { get; set; }

    public string Namespace { get; set; }
}

