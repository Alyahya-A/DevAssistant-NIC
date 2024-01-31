namespace Dev.Assistant.Business.DevOps.Models;

public class PreparePullRequestOptions
{
    public bool ExcludeBusinessLayer { get; set; }
    public bool ExcludeController { get; set; }
    public bool ExcludeModels { get; set; }
}
