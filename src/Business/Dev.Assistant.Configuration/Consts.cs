using Dev.Assistant.Configuration.Models;

namespace Dev.Assistant.Configuration;

/// <summary>
/// Constants and configurations for the DevAssistant.
/// </summary>
public static class Consts
{
    /// <summary>
    /// Gets or sets the application version.
    /// </summary>
    public static string AppVersion { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL of AuthServer.
    /// </summary>
    public static string AuthServerUrl { get; set; } = "https://172.29.1.37";

    /// <summary>
    /// Gets or sets the user settings for the application.
    /// </summary>
    public static UserSettings UserSettings { get; set; } = new();

    /// <summary>
    /// Gets or sets the TFS URL.
    /// </summary>
    public static string TfsUrl { get; set; } = "https://tfsnic.nichq.moi.gov/tfs/NIC%20Application%20Projects";

    /// <summary>
    /// Gets or sets the connected server URL.
    /// </summary>
    public static string ConnectedServerUrl { get; set; }

    /// <summary>
    /// Gets or sets the path for temporary files used by the application.
    /// </summary>
    public static string AppTempPath { get => Path.Combine(Path.GetTempPath(), "DevAssistant"); }

    /// <summary>
    /// Gets or sets the app server path.
    /// </summary>
    public static string AppServerPath { get; set; } = @"\\172.29.1.39\d\Alyahya\DevAssistant";

    /// <summary>
    /// Gets or sets the server path for application assets.
    /// </summary>
    public static string AppServerAssetsPath { get => Path.Combine(AppServerPath, "Assets"); }

    /// <summary>
    /// Gets or sets the path for release notes.
    /// </summary>
    public static string AppReleaseNotes { get => Path.Combine(AppServerAssetsPath, "release-notes.md"); }

    /// <summary>
    /// Gets or sets the server path for application logs.
    /// </summary>
    public static string AppLogsServer { get; } = @"\\172.29.1.27\Deployment\DevAssistant\AppLogs";

    /// <summary>
    /// Gets or sets the local path for application assets.
    /// </summary>
    public static string AppLocalAssetsPath { get => Path.Combine(AppTempPath, "Assets"); }

    /// <summary>
    /// Gets or sets the server path for the DevAssistant executable.
    /// </summary>
    public static string AppExeServerPath { get => Path.Combine(AppServerPath, "DevAssistant.exe"); }

    /// <summary>
    /// Gets or sets the server path for the DevAssistantBatch executable.
    /// </summary>
    public static string AppExeBatchPath { get => Path.Combine(AppServerAssetsPath, "DevAssistantBatch.exe"); }

    /// <summary>
    /// Gets or sets the local path for application settings file.
    /// </summary>
    public static string AppsettingsFileLocalPath { get => Path.Combine(AppTempPath, "appsettings.json"); }

    /// <summary>
    /// Gets or sets the server path for application settings file.
    /// </summary>
    public static string AppsettingsFileServerPath { get => Path.Combine(AppServerAssetsPath, "appsettings.json"); }

    /// <summary>
    /// Gets or sets the local path for the API IG Template document.
    /// </summary>
    public static string ApiIGTemplateFileLocalPath { get => Path.Combine(AppLocalAssetsPath, "IG_Template.docx"); }

    /// <summary>
    /// Gets or sets the server path for the API IG Template document.
    /// </summary>
    public static string ApiIGTemplateFileServerPath { get => Path.Combine(AppServerAssetsPath, "IG_Template.docx"); }

    /// <summary>
    /// Gets or sets the local path for the MicroServices IG Template document.
    /// </summary>
    public static string MicroIGTemplateFileLocalPath { get => Path.Combine(AppLocalAssetsPath, "Micro_IG_Template.docx"); }

    /// <summary>
    /// Gets or sets the server path for the MicroServices IG Template document.
    /// </summary>
    public static string MicroIGTemplateFileServerPath { get => Path.Combine(AppServerAssetsPath, "Micro_IG_Template.docx"); }

    /// <summary>
    /// Gets or sets the local path for staff information file.
    /// </summary>
    public static string StaffInfoFileLocalPath { get => Path.Combine(AppLocalAssetsPath, "StaffInfo.json"); }

    /// <summary>
    /// Gets or sets the server path for staff information file.
    /// </summary>
    public static string StaffInfoFileServerPath { get => Path.Combine(AppServerAssetsPath, "StaffInfo.json"); }

    /// <summary>
    /// Gets or sets the list of GCC contracts.
    /// </summary>
    public static List<string> GccContracts { get; set; }

    /// <summary>
    /// Gets or sets the list of excluded microservices.
    /// </summary>
    public static List<string> ExcludedMicros { get; } = new()
    {
        "MicroCommon",
        "MicroApiClient",
        "UnitTestCore",
        "UtilitiesCore",
    };

    /// <summary>
    /// Gets or sets the list of excluded contracts.
    /// </summary>
    public static List<string> ExcludedContracts { get; } = new()
    {
        "Nic.Apis.BackendRest",
        "Nic.Apis.BackendSoap",
        "Nic.Apis.Common",
        "Nic.Apis.Root",
        "Nic.Apis.AuthServer",
    };

    /// <summary>
    /// Gets or sets the list of excluded tables.
    /// </summary>
    public static List<string> ExcludedTables { get; } = new()
    {
        "ZONEDETAILS".ToUpper(),
    };

    /// <summary>
    /// Gets or sets the list of common API exceptions.
    /// </summary>
    public static List<BusinessApiException> ApiCommonExceptions { get; set; }

    /// <summary>
    /// Gets or sets the list of secrets PIN.
    /// </summary>
    public static List<string> SecretsPin { get; set; }

    /// <summary>
    /// Gets or sets all primitive types + non-user classes such as string and DateTime.
    /// </summary>
    public static List<string> PrimitiveTypes { get; } = new()
    {
        "bool",
        "boolean",
        "byte",
        "byte[]",
        "int",
        "integer",
        "long",
        "short",
        "date",
        "time",
        "datetimeoffset",
        "datetime",
        "timeonly",
        "dateonly",
        "char",
        "double",
        "string",
        "decimal",
        "float",
        "dynamic"
    };
}