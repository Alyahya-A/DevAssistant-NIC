namespace Dev.Assistant.Configuration;

/// <summary>
/// Represents local application settings.
/// </summary>
public class LocalAppsettings : BaseAppsettings
{
    /// <summary>
    /// Gets or sets the app server path.
    /// Default is null, if null app server path will be shard folder or 27 server
    /// </summary>
    public string AppServerPath { get; set; }

    /// <summary>
    /// Gets or sets the list of secret pins for various functions.
    /// </summary>
    public List<string> SecretsPin { get; set; }

    /// <summary>
    /// Gets or sets the microservice path.
    /// </summary>
    public string MicroServicePath { get; set; }

    /// <summary>
    /// Gets or sets the utilities service path.
    /// </summary>
    public string UtilitiesServicePath { get; set; }

    /// <summary>
    /// Gets or sets the startup application identifier.
    /// </summary>
    public int StartupApp { get; set; }

    /// <summary>
    /// Gets or sets the identifier for 'WrittenByIG'.
    /// </summary>
    public string WrittenByIG { get; set; }

    /// <summary>
    /// Gets or sets the IG path folder.
    /// </summary>
    public string IGPathFolder { get; set; }

    /// <summary>
    /// Gets or sets the last path input.
    /// </summary>
    public string LastPathInput { get; set; }

    /// <summary>
    /// Gets or sets the My Work search criteria.
    /// </summary>
    public int MyWorkSearchCriteria { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to update settings.
    /// </summary>
    public bool UpdateSettings { get; set; }

    /// <summary>
    /// Gets or sets the RFeature criteria.
    /// </summary>
    public int RFeatureCriteria { get; set; }

    /// <summary>
    /// Gets or sets the RIG selected option.
    /// </summary>
    public int RIGSelectedOption { get; set; }

    /// <summary>
    /// Gets or sets the RConverter selected option.
    /// </summary>
    public int RConverterSelectedOption { get; set; }

    /// <summary>
    /// Gets or sets the RCompare selected option.
    /// </summary>
    public int RCompareSelectedOption { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to list all employees in PList.
    /// </summary>
    public bool PListAllEmp { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the RNdb open file check option is enabled.
    /// </summary>
    public bool RNdbOpenFileCheckOption { get; set; }

    /// <summary>
    /// Gets or sets the staff search criteria.
    /// </summary>
    public string StaffSearchBy { get; set; }

    /// <summary>
    /// Gets or sets the RIG project type option.
    /// </summary>
    public int RIGProjectTypeOption { get; set; }
}

/// <summary>
/// Provides constant pins for specific application functions.
/// </summary>
public static class Pin
{
    public const string IgnoreForceStopApp = "U.are.U";
    public const string IncludeTasksboardApp = "123";
    //public const string BypassCSharpStandards = "123";
}