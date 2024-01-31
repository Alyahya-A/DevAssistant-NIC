using Dev.Assistant.Business.Core.Extensions;

namespace Dev.Assistant.Business.Decoder.Models;

/// <summary>
/// This represent interface for bellow classes. INicServiceFile must not be used outside this file
/// </summary>
public class NicServiceFile
{
    /// <summary>
    /// FileName
    /// </summary>
    public string ServiceName { get; set; }

    public NicServiceFilePath FilePath { get; set; }

    public string Code { get; set; }

    public NicServiceFileType FileType { get; set; }

    public string Namespace { get; set; } = string.Empty;

    public string ContractCode { get; set; }

    private string _controllerName;

    // In Npv contracts there is no controllers. So the controller will be one file and its name will be same as contract code
    public string ControllerName
    {
        get => Namespace.IsNamespaceNpv() && FileType == NicServiceFileType.Controller
            ? ContractCode.Replace("Npv", "") : _controllerName;
        set => _controllerName = value;
    }

    public string FullControllerName { get => $"{ControllerName}Controller"; }

    //public string ServiceName { get; set; }

    public string GetRemoteRootPath() => $"/Nic.Apis.{ContractCode}/BusinessLayer/{ControllerName}/{ServiceName}.cs";


}

public class NicServiceFilePath
{
    public string Path { get; set; }

    /// <summary>
    /// True: Remote path from TFS <br/>
    /// False: Local path from user machine
    /// </summary>
    public bool IsRemotePath { get; set; } = false;

    /// <summary>
    /// Must be supllied if IsRemotePath true
    /// </summary>
    public string BranchName { get; set; }

}

public enum NicServiceFileType
{
    BusinessLayer,
    Controller,
    Model,
    Root
}