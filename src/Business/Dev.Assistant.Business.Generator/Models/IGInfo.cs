using AuthServer;
using Dev.Assistant.Business.Core.Models;
using Dev.Assistant.Business.Decoder.Models;
using Dev.Assistant.Configuration.Models;

namespace Dev.Assistant.Business.Generator.Models;

public class IGInfo
{
    public UserReturnModel UserInfo { get; set; }

    public ServiceInfo ServiceInfo { get; set; }

    public string WrittenBy { get; set; }

    public double IGVersion { get; set; }

    /// <summary>
    /// Form the namespace
    /// </summary>
    public string ContractCode { get; set; }

    public List<Property> RequestDto { get; set; }
    public List<Property> ResponseDto { get; set; }

    public List<BusinessApiException> ApiErrors { get; set; }
    public List<MicroException> MicrosErrors { get; set; }

}