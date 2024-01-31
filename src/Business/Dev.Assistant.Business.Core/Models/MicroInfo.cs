using Dev.Assistant.Business.Core.Utilities;

namespace Dev.Assistant.Business.Core.Models;

public class MicroInfo
{
    public string MicroRootName { get; set; }

    public string MicroServiceName { get; set; }

    public string MicroDescEn { get; set; }

    public string CategoryName { get; set; }

    public string GetRemotePath() => $"{DevOpsClient.MicroProjectName}/{DevOpsClient.GetMicroScopePath(MicroServiceName)}Service.cs";

}