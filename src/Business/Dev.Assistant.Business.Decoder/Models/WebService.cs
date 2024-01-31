namespace Dev.Assistant.Business.Decoder.Models;

public class WebService
{
    public string Name { get; set; }
    public string Namespace { get; set; }
    public bool IsVB { get; set; }
    public List<Function> Funcations { get; set; }
}

public class Function
{
    public string FunName { get; set; }

    /// <summary>
    /// Header.
    /// public void MethodName(args) {
    /// </summary>
    public string Definition { get; set; }

    /// <summary>
    /// public, private, protected, internal
    /// </summary>
    public string AccessLevel { get; set; }

    public string ReturnType { get; set; }
    public string Code { get; set; }
    public string Configuration { get; set; }
    public DataBaseType DBName { get; set; }
}
