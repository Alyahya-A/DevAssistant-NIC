using Dev.Assistant.Configuration;
using Newtonsoft.Json;
using Serilog;
using System.Diagnostics;

// Initialize and start the batch process
Startup();

void Startup()
{
    try
    {
        // Configure Serilog for logging
        ConfigureLogging();

        Log.Logger.Information("==================== Batch START ====================");

        Console.WriteLine("DevAssistant batch is used to update DevAssistant.exe app automatically");

        // Read and validate command-line arguments
        (string executablePath, string appServerPath) = ReadAndValidateCommandLineArgs();

        // Update Consts.AppServerPath
        Consts.AppServerPath = appServerPath;

        // Read server app settings
        var serverAppsettings = ReadServerAppsettings();

        if (serverAppsettings == null)
        {
            Log.Logger.Error("Server appsettings is null!");

            HandleBatchException(new Exception("Server appsettings can not be null"));

            return;
        }

        // Read local app settings
        var localAppsettings = ReadLocalAppsettings();

        // Perform app update if server settings are available
        PerformAppUpdate(serverAppsettings, localAppsettings, executablePath);

        // Restart the application
        RestartApplication(executablePath);
    }
    catch (Exception ex)
    {
        HandleBatchException(ex);
    }
    finally
    {
        Log.Logger.Information("==================== Batch END ====================");
    }
}

void ConfigureLogging()
{
    Log.Logger = new LoggerConfiguration()
        .WriteTo.File(@$"{Consts.AppTempPath}\BatchLogs\Logs.log",
                      outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u4}]: {Message:lj}{NewLine}{Exception}",
                      rollingInterval: RollingInterval.Day, shared: true)
        .CreateLogger();
}



LocalAppsettings ReadLocalAppsettings()
{
    if (File.Exists(Consts.AppsettingsFileLocalPath))
    {
        Log.Logger.Information("Reading local appsettings");
        using var file = File.OpenText(Consts.AppsettingsFileLocalPath);

        return JsonConvert.DeserializeObject<LocalAppsettings>(file.ReadToEnd());
    }
    else
    {
        Log.Logger.Information("Couldn't find local appsettings");
        return null;
    }
}

(string, string) ReadAndValidateCommandLineArgs()
{
    string executablePath = GetCommandLineArg(1);
    string appServerPath = GetCommandLineArg(2);

    ValidateCommandLineArg(executablePath, "Executable path");
    ValidateCommandLineArg(appServerPath, "Appsettings path");

    // Verify access to server directory
    if (!Directory.Exists(appServerPath))
    {
        throw new Exception("9000: Couldn't find app directory in the server or the server location directory cannot be reached. Check your connection.");
    }

    return (executablePath, appServerPath);
}

string GetCommandLineArg(int index) => Environment.GetCommandLineArgs().Length >= index + 1 ? Environment.GetCommandLineArgs()[index] : string.Empty;

void ValidateCommandLineArg(string arg, string argName)
{
    if (string.IsNullOrWhiteSpace(arg))
    {
        string errorMessage = $"Invalid or empty {argName}.";
        Log.Logger.Information(errorMessage);
        Console.WriteLine(errorMessage);
        throw new Exception("This .exe (batch) file is run by DevAssistant.exe app only.");
    }
}

ServerAppsettings ReadServerAppsettings()
{
    if (File.Exists(Consts.AppsettingsFileServerPath))
    {
        using var file = File.OpenText(Consts.AppsettingsFileServerPath);
        return JsonConvert.DeserializeObject<ServerAppsettings>(file.ReadToEnd());
    }
    else
    {
        Log.Logger.Information($"Couldn't found server appsettings at {Consts.AppsettingsFileServerPath}");
        return null;
    }
}

void PerformAppUpdate(ServerAppsettings serverSettings, LocalAppsettings localSettings, string executablePath)
{
    Console.WriteLine($"A New Update (v{serverSettings.AppVersion}) Available for the App! Updating...");

    Log.Logger.Information($"Updating from \"{Consts.AppExeServerPath}\" start...");

    UpdateApp(Consts.AppExeServerPath, executablePath);

    // Update Local appsettings
    UpdateLocalAppsettings(localSettings, serverSettings);
}

void UpdateLocalAppsettings(LocalAppsettings localSettings, ServerAppsettings serverSettings)
{
    Log.Logger.Information("Updating local appsettings");

    localSettings ??= new LocalAppsettings();
    localSettings.AppVersion = serverSettings.AppVersion;
    localSettings.UpdateSettings = true; // To update the appsettings when the app opened again after the app updated (See App.Home.cs "if (localAppsettings.UpdateSettings)")

    var json = JsonConvert.SerializeObject(localSettings, Formatting.Indented);
    File.WriteAllText(Consts.AppsettingsFileLocalPath, json);

    Log.Logger.Information("Local Appsettings updated");
}

void UpdateApp(string appExeServerPath, string appExeLocalPath)
{
    if (!File.Exists(appExeServerPath))
        throw new Exception("9003: Couldn't find .exe file in the server");

    if (File.Exists(appExeLocalPath))
        File.Delete(appExeLocalPath);

    File.Copy(appExeServerPath, appExeLocalPath);
}

void RestartApplication(string executablePath)
{
    Log.Logger.Information("Restarting the App...");
    Console.WriteLine("Restarting the App...");

    using var process = new Process { StartInfo = new(executablePath) };

    process.Start();
}


void HandleBatchException(Exception ex)
{
    Log.Logger.Error(ex, "An error occurred while updating the app");

    Console.Error.WriteLine("An error occurred while updating the app. Check log file.");
    Console.WriteLine("Press enter key to close this window :(~");
    Console.ReadLine();
}