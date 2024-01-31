using Dev.Assistant.Configuration.Models;

namespace Dev.Assistant.Configuration;

/// <summary>
/// Represents server application settings.
/// </summary>
public class ServerAppsettings : BaseAppsettings
{
    /// <summary>
    /// Gets or sets the duration of the session in hours.
    /// </summary>
    public int SessionDuration { get; set; }

    /// <summary>
    /// Gets or sets the log enabling status.
    /// 1 - Enable all, 2 - Disable all, 3 - Only local, 4 - Only server, 999 - Force the app to stop.
    /// </summary>
    public LogStatus EnableLog { get; set; }

    /// <summary>
    /// Gets or sets the TFS URL.
    /// </summary>
    public string TfsUrl { get; set; }

    /// <summary>
    /// Gets or sets AuthServer URL.
    /// </summary>
    public string AuthServerUrl { get; set; }

    /// <summary>
    /// Gets or sets the list of GCC contracts.
    /// </summary>
    public List<string> GccContracts { get; set; }

    /// <summary>
    /// Gets or sets the list of common API exceptions for business logic.
    /// </summary>
    public List<BusinessApiException> ApiCommonExceptions { get; set; }

    /// <summary>
    /// Determines if the provided version string represents an older version than the current this.AppVersion (server).
    /// </summary>
    /// <param name="version">The current AppVersion version string to compare with this.AppVersion (server).</param>
    /// <returns>True if the this.AppVersion (server) version is newer; otherwise, false.</returns>
    public bool IsNewVersion(string version)
    {
        if (string.IsNullOrWhiteSpace(AppVersion) || string.IsNullOrWhiteSpace(version))
            return false;

        try
        {
            // Split the server and incoming version and convert them to integer arrays.
            var serverVersionNumbers = Array.ConvertAll(AppVersion.Split('.'), int.Parse);
            var currentAppVersionNumbers = Array.ConvertAll(version.Split('.'), int.Parse);

            // Loop through each index of the version number, up to the length of the longer version string.
            for (int i = 0; i < Math.Max(serverVersionNumbers.Length, currentAppVersionNumbers.Length); i++)
            {
                // Get the current index of the server/current version, defaulting to 0 if the index is out of bounds.
                int serverVersion = i < serverVersionNumbers.Length ? serverVersionNumbers[i] : 0;
                int currentAppVersion = i < currentAppVersionNumbers.Length ? currentAppVersionNumbers[i] : 0;

                if (serverVersion > currentAppVersion)
                    return true;
                else if (serverVersion < currentAppVersion)
                    return false;
            }

            // If all indexes are equal or we have compared all indexes, return false.
            // It means the server version is not newer than the incoming version.
            return false;
        }
        catch (Exception ex)
        {
            //Log.Logger.Error(ex, "IsNewVersion falied");
            throw;
        }
    }
}

/// <summary>
/// Defines the log status options for the application.
/// </summary>
public enum LogStatus
{
    EnableAll = 1,
    DisableAll = 2,
    OnlyLocal = 3,
    OnlyServer = 4,
    ForceStop = 999
}