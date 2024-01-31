using Dev.Assistant.Business.Core.Models;

namespace Dev.Assistant.Business.Decoder.Models;

public class ClassModel
{
    public string Name { get; set; }
    public string ClassDesc { get; set; }
    public string Body { get; set; }
    public string Remarks { get; set; }
    public string LongRemarks { get; set; }
    public List<Property> Properties { get; set; }

    public bool IsVB { get; set; }

    public List<Function> Functions { get; set; }
}