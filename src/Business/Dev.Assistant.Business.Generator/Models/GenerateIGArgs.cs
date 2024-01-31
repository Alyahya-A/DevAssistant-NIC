using Dev.Assistant.Business.Core.Enums;
using Dev.Assistant.Business.Decoder.Models;

namespace Dev.Assistant.Business.Generator.Models;

public class GenerateIGArgs
{
    // Only used in API GenIG
    public ProjectType ProjectType { get; set; }

    public List<NicService> Services { get; set; }

    public bool GenerateMultipleIGs { get; set; }

    public bool OpenJsonAfterCreate { get; set; }

    public bool AddBracket { get; set; }


    // Only used on Micro GenIG
    public string MicroPath { get; set; }


    // Common for Micro & API
    public double IgVersion { get; set; }

    public string WrittenBy { get; set; }
}
