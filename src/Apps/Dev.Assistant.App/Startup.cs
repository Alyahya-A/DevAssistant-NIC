namespace Dev.Assistant.App;

//public static class Startup
//{
//    public static void Init()
//    {
//        try
//        {
//            Log.Logger.Information("==================== Batch START ====================");

//            Console.WriteLine("Checking for updates....");

//            //Log.Logger.Information($"App folder path {Consts.AppTempPath}");

//            // Create temp directory for DevAssistant app
//            if (!Directory.Exists(Consts.AppTempPath))
//            {
//                Log.Logger.Information("Create the AppTemp directory");
//                Directory.CreateDirectory(Consts.AppTempPath);
//            }
//            else
//            {
//                Log.Logger.Information("The AppTemp directory is already created");
//            }

//            // Create Assets directory indide temp directory
//            if (!Directory.Exists(Consts.AppAssetsPath))
//            {
//                Log.Logger.Information("Create the AppAssets directory");
//                Directory.CreateDirectory(Consts.AppAssetsPath);
//            }
//            else
//            {
//                Log.Logger.Information("The AppAssets directory is already created");
//            }

//            Appsettings localAppsettings = null;

//            if (File.Exists(Consts.AppsettingsFileLocalPath))
//            {
//                Log.Logger.Information("Reading local appsettings");

//                using StreamReader file = File.OpenText(Consts.AppsettingsFileLocalPath);
//                localAppsettings = JsonConvert.DeserializeObject<Appsettings>(file.ReadToEnd());
//            }
//            else
//            {
//                Log.Logger.Information("Couldn't found local appsettings");
//            }

//            Appsettings serverAppsettings = null;

//            if (localAppsettings is not null)
//            {
//                // bypass
//                if (!localAppsettings.SecretPin.Equals("U.are.U"))
//                {
//                    if (File.Exists(Consts.AppsettingsFileServerPath))
//                    {
//                        using StreamReader file = File.OpenText(Consts.AppsettingsFileServerPath);
//                        serverAppsettings = JsonConvert.DeserializeObject<Appsettings>(file.ReadToEnd());

//                        bool updateLocalSettings = false;

//                        if (serverAppsettings.IsNewVersion(localAppsettings.AppVersion))
//                        {
//                            Log.Logger.Information($"A New Update Available for the App! ServerVersion: {serverAppsettings.AppVersion}. LocalVersion: {localAppsettings.AppVersion}");

//                            if (serverAppsettings.ForceUpdate)
//                            {
//                                Log.Logger.Information($"Updating...");

//                                Console.WriteLine($"A New Update (v{serverAppsettings.AppVersion}) Available for the App! Updating....");

//                                UpdateApp(Consts.AppExeServerPath, Consts.AppExeLocalPath);

//                                updateLocalSettings = true;
//                            }
//                            else
//                            {
//                                Log.Logger.Information($"Stop updating. ForceUpdate is false");
//                            }
//                        }

//                        if (serverAppsettings.IGTemplateVersion > localAppsettings.IGTemplateVersion)
//                        {
//                            Log.Logger.Information($"A New Update Available for the IG Template! ServerVersion: {serverAppsettings.IGTemplateVersion}. LocalVersion: {localAppsettings.IGTemplateVersion}");

//                            Log.Logger.Information($"Updating...");

//                            GetIGTempleteFileFromServer();
//                            updateLocalSettings = true;
//                        }

//                        // update localAppsettings
//                        if (updateLocalSettings)
//                        {
//                            localAppsettings.AppVersion = serverAppsettings.AppVersion;
//                            localAppsettings.IGTemplateVersion = serverAppsettings.IGTemplateVersion;

//                            string json = JsonConvert.SerializeObject(localAppsettings);

//                            File.WriteAllText(Consts.AppsettingsFileLocalPath, json);

//                            Log.Logger.Information("Update local Appsettings");
//                        }
//                    }
//                    else
//                    {
//                        Log.Logger.Information($"Couldn't found server appsettings.");
//                        Console.Error.WriteLine("Couldn't found server appsettings.");
//                    }
//                }
//                else
//                {
//                    Log.Logger.Information("Bypass. No need to check for app updates");
//                }
//            }
//            else
//            { // if null it means first run
//                Log.Logger.Information("Creating local appsettings");

//                using StreamReader file = File.OpenText(Consts.AppsettingsFileServerPath);
//                serverAppsettings = JsonConvert.DeserializeObject<Appsettings>(file.ReadToEnd());

//                localAppsettings = new()
//                {
//                    AppVersion = serverAppsettings.AppVersion,
//                    IGTemplateVersion = serverAppsettings.IGTemplateVersion,
//                    ForceUpdate = false,
//                    RunDirect = false,
//                    SecretPin = ""
//                };

//                string json = JsonConvert.SerializeObject(localAppsettings);
//                File.WriteAllText(Consts.AppsettingsFileLocalPath, json);
//            }

//            if (!File.Exists(Consts.IGTemplateFileLocalPath))
//            {
//                Log.Logger.Information("Copying the IGTemplateFile");

//                GetIGTempleteFileFromServer();
//            }

//            // Run the App -- START
//            Log.Logger.Information("Run the App...");
//            Log.Logger.Information($"RunDirect is {serverAppsettings?.RunDirect}");

//            Console.WriteLine("Run the App...");

//            Process process = new()
//            {
//                StartInfo = new(serverAppsettings?.RunDirect ?? false ? Consts.AppExeServerPath : Consts.AppExeLocalPath)
//            };
//            // Run the App -- END

//            process.Start();
//            process.Dispose();

//            Log.Logger.Information("==================== Batch END ====================");

//        }
//        catch (Exception ex)
//        {
//            string message = ex.Message
//                .Replace(@"\Deployment\DevAssistant", " **")
//                .Replace(@"\DevAssistant.exe", "*")
//                .Replace(@"\appsettings.json", "**")
//                .Replace(@"\IG_Template.docx", "***");

//            Log.Logger.Error($"Batch Stoped! {message}");

//            Console.Error.WriteLine(message);

//            Console.WriteLine("Press any key to close this window :(~");
//            Console.ReadLine();
//        }
//    }

//    private static void GetIGTempleteFileFromServer()
//    {
//        // Mean the directory is found the user can access the server
//        if (!Directory.Exists(Consts.AppServerPath))
//            throw new Exception("9000: Couldn't found app directory in the server or the server location directory cannot be reached check your connection");

//        if (!File.Exists(Consts.IGTemplateFileServerPath))
//            throw new Exception("9001: Couldn't found IG template file in the server");

//        if (File.Exists(Consts.IGTemplateFileLocalPath))
//            File.Delete(Consts.IGTemplateFileLocalPath);

//        File.Copy(Consts.IGTemplateFileServerPath, Consts.IGTemplateFileLocalPath);
//    }

//    private static void UpdateApp(string appExeServerPath, string appExeLocalPath)
//    {
//        // Mean the directory is found the user can access the server
//        if (!Directory.Exists(Consts.AppServerPath))
//            throw new Exception("9002: Couldn't found app directory in the server or the server location directory cannot be reached check your connection");

//        if (!File.Exists(appExeServerPath))
//            throw new Exception("9003: Couldn't found .exe file in the server");

//        if (File.Exists(appExeLocalPath))
//            File.Delete(appExeLocalPath);

//        File.Copy(appExeServerPath, appExeLocalPath);
//    }
//}