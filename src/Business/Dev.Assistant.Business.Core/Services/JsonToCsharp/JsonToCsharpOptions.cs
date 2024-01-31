namespace Dev.Assistant.Business.Core.Services.JsonToCsharp;

public enum ListType
{
    None,
    List,
    IReadOnlyList
}

public interface IReadOnlyOptions
{
    string NameSpace { get; }
    bool DeclareDataMember { get; }
    ListType ListType { get; }
}

public class JsonToCsharpOptions : IReadOnlyOptions
{
    public string NameSpace { get; set; }
    public bool DeclareDataMember { get; set; }
    public ListType ListType { get; set; }
}