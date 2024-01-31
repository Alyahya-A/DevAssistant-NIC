namespace Dev.Assistant.Configuration;

/// <summary>
/// Represents base application settings.
/// </summary>
public abstract class BaseAppsettings
{
    /// <summary>
    /// Gets or sets the application version.
    /// </summary>
    public string AppVersion { get; set; }

    /// <summary>
    /// Gets or sets the IG template version for API project.
    /// </summary>
    public double ApiIGTemplateVersion { get; set; }

    /// <summary>
    /// Gets or sets the IG template version for MicroServices project.
    /// </summary>
    public double MicroIGTemplateVersion { get; set; }

    /// <summary>
    /// Gets or sets the staff information version.
    /// </summary>
    public double StaffInfoVersion { get; set; }
}
