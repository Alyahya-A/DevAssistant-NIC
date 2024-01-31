namespace Dev.Assistant.Business.Compare.Models;

public class CompareJsonOptions
{
    public CompareJsonOptions()
    {
        ToLowerCase = false;
        OnlyKeys = false;
    }

    public bool ToLowerCase { get; set; }
    public bool OnlyKeys { get; set; }
}