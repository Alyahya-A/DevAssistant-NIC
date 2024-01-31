namespace Dev.Assistant.Business.Decoder.Models;

/// <summary>
/// The represent a service, such as Rest, SOAP or Micro.
/// </summary>
public class NicService
{
    public string Namespace { get; set; }

    public string ContractCode { get; set; }

    public string ControllerName { get; set; }

    public string ServiceName { get; set; }

    //public NicServiceRemoteInfo RemoteInfo { get; set; }

    //public List<PrFile> Files { get; set; }

    private NicServiceFile _businessLayer;

    public NicServiceFile BusinessLayer
    {
        get => _businessLayer;
        set
        {
            if (value != null)
            {
                value.Namespace = $"{Namespace}.BusinssLayer";
                value.ContractCode = ContractCode;
                value.ControllerName = ControllerName;
                value.ServiceName = ServiceName;

                _businessLayer = value;
            }
        }
    }

    private NicServiceFile _controller;

    public NicServiceFile Controller
    {
        get => _controller;
        set
        {
            if (value != null)
            {
                value.Namespace = $"{Namespace}.Controllers";
                value.ContractCode = ContractCode;
                value.ControllerName = ControllerName;
                value.ServiceName = ServiceName;

                _controller = value;
            }
        }
    }

    private NicServiceFile _model;

    public NicServiceFile Model
    {
        get => _model;
        set
        {
            if (value != null)
            {
                value.Namespace = $"{Namespace}.Models";
                value.ContractCode = ContractCode;
                value.ControllerName = ControllerName;
                value.ServiceName = ServiceName;

                _model = value;
            }
        }
    }
}

public class NicServiceRemoteInfo
{
    public int PullRequestId { get; set; }
    public string BranchNumber { get; set; }

    // TODO: .
}