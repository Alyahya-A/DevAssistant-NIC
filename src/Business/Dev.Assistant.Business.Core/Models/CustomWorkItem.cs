namespace Dev.Assistant.Business.Core.Models;

public class CustomWorkItem
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string DeveloperName { get; set; }
    public string TaskState { get; set; }
    public string ContractName { get; set; }
    public string CrState { get; set; }
    public string Type { get; set; }
    public bool IsIGLinked { get; set; }
    public bool IsTestDataAttached { get; set; }
    public bool IsCmTabEmpty { get; set; }
}