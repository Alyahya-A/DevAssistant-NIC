namespace Dev.Assistant.Business.Core.Models;

public class ListTablesServiceOptions
{
    public ListTablesServiceOptions()
    {
        OnlyIdTypes = false;
        IsTesting = false;
    }

    /// <summary>
    /// Default Consts.UserSettings.MicroServicePath
    /// </summary>
    public string MicroPath { get; set; }

    /// <summary>
    ///  <br/>
    ///
    /// </summary>
    public bool OnlyIdTypes { get; set; }

    /// <summary>
    ///  <br/>
    ///
    /// </summary>
    public bool IsTesting { get; set; }

    /// <summary>
    ///  <br/>
    ///
    /// </summary>
    public string PropName { get; set; }

    public bool IsRemote { get; set; }
}