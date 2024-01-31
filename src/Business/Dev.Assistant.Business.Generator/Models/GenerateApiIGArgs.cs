using Dev.Assistant.Business.Core.Enums;
using Dev.Assistant.Business.Decoder.Models;

namespace Dev.Assistant.Business.Generator.Models;

public class GenerateApiIGArgs
{
    // ProjectType projectType, List<NicService> services, double idVersion

    public ProjectType ProjectType { get; set; }

    public List<NicService> Services { get; set; }

    public double IgVersion { get; set; }

    public string WrittenBy { get; set; }

    public bool GenerateMultipleIGs { get; set; }

    public bool OpenJsonAfterCreate { get; set; }

    public bool AddBracket { get; set; }
}
