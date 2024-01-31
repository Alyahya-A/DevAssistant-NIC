using Dev.Assistant.Business.Decoder.Models;

namespace Dev.Assistant.Business.Compare.Models;

public class CompareModelResult
{
    /// <summary>
    /// Pre Classes
    /// </summary>
    public List<ClassModel> Models1 { get; set; }

    /// <summary>
    /// Current Classes
    /// </summary>
    public List<ClassModel> Models2 { get; set; }
}
