namespace Dev.Assistant.Business.Decoder.Models;

/// <summary>
/// This file info, may a MicroModel or a service
/// </summary>
public class FileInfoModel
{
    public string Name { get; set; }
    public bool IsMicroInBayan { get; set; }
    public List<ClassModel> Models { get; set; }
    public string Controller { get; set; }

    public string ContractCode { get; set; }
}