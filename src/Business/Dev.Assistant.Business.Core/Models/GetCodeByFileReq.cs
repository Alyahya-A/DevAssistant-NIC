namespace Dev.Assistant.Business.Core.Models;

public class GetCodeByFileReq
{
    public string Code { get; set; }

    public bool IsRemotePath { get; set; }

    public string Path { get; set; }

    public string BranchName { get; set; }

}