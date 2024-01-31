using Dev.Assistant.Business.Core.Extensions;

namespace Dev.Assistant.Business.Core.Models;

public class ServiceInfo
{
    public string Namespace { get; set; } = string.Empty;

    private string _contractCode = string.Empty;

    public string ContractCode
    {
        get => Namespace.IsNamespaceNpv() ? $"{_controllerName.Replace("Controller.cs", "")}Npv" : _contractCode;
        set => _contractCode = value;
    }

    private string _controllerName = string.Empty;

    public string ControllerName
    {
        get => _controllerName.Replace("Controller.cs", "");
        set => _controllerName = value;
    }

    public string ServiceName { get; set; }
    public string ServiceDescEn { get; set; }


    public string GetBusinessLayerPath() => $"/{Namespace}/BusinessLayer/{ControllerName}/{ServiceName}.cs";

    public string GetModelPath() => $"/{Namespace}/Models/{ControllerName}/{ServiceName}.cs";

    public string GetControllerPath() => $"/{Namespace}/Controllers/{ControllerName}Controller.cs";

}