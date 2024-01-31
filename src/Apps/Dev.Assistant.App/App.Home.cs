using Dev.Assistant.App.Extensions;
using Dev.Assistant.Business.Core.Enums;
using Dev.Assistant.Business.Core.Extensions;
using Dev.Assistant.Business.Core.Models;
using Dev.Assistant.Business.Core.Utilities;
using Dev.Assistant.Configuration;
using Newtonsoft.Json;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using System.Diagnostics;
using Timer = System.Windows.Forms.Timer;

namespace Dev.Assistant.App;

public partial class AppHome : Form
{
    private Reviewme.ReviewmeHome _reviewmeApp;
    private UtilitiesOps.UtilitiesFormsHome _utilitiesForms;
    private MyWork.MyWorkHome _myWorkApp;

    private PullRequests.PullRequestsHome _pullRequestsApp;
    private Staff.StaffHome _staffApp;
    private TasksBoard.TasksBoardHome _tasksBoardApp;

    private ServerAppsettings _serverAppsettings;

    private DevApp _selectedApp;

    /// <summary>
    /// 1- Enable all
    /// 2- disable all
    /// 3- only local
    /// 4- only server
    /// 999- Force the app to stop
    /// </summary>
    private LogStatus _enableLog;

    private List<DevAppInfo> _includedApps = new();

    private Timer _sessionTimer;
    private Timer _checkUpdateTimer;
    private DateTime _appStartedAt;
    private int _sessionDurationH;
    private bool _sessionExpired;
    private bool _stopTheApp;

    //const string _utilitiesFormsNote = "[UtilitiesForms]: Note: You can select your BusinessLayer file, then the App will get all the MicroServices names and the BusinessLayer path to add it to the above list.\n                       To get all needed tables from Micros and BusinessLayer file sorted and without duplicated table";

    private const string _utilitiesFormsNote = "[UtilitiesForms]: Note: In RestApi you can select your BusinessLayer file only.\n                         Then the App will get all the MicroServices names and all queries (if there is) from your BusinessLayer.\n                         To make it easier to get all needed tables from Micros and BusinessLayer file sorted and without duplicated tables";

    public AppHome()
    {
        InitializeComponent();

        // Width=880, Height=570erp

        IsMdiContainer = true;

        _appStartedAt = DateTime.Now;
        runningFromToolStripMenuItem.Text = $"Running from {DateTime.Now}";
        lastUpdateCheckToolStripMenuItem.Visible = false;

        TasksboardAppToolStripMenuItem.Visible = false;

        _sessionDurationH = 24; // one day. Default
        _sessionExpired = false;

        Text = $"DevAssistant App v{Consts.AppVersion}";

        // 1- read server config
        // if not exist then bypass (check for update)

        // 2- check update. if avaliable and forceupdate true then show mesage to the user to go the shared folder to update.

        //var config = new ConfigurationBuilder().AddJsonFile("serilog-config.json", optional: false, reloadOnChange: true).Build();
        string outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u4}]: {Message:lj}{NewLine}{Exception}";

        Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Logger(l =>
                {
                    l.WriteTo.File(@$"{Consts.AppTempPath}\AppLogs\Logs.log", outputTemplate: outputTemplate, rollingInterval: RollingInterval.Day, shared: true);
                    l.Filter.ByExcluding(e => e.Properties.ContainsKey("DevEvent") || e.Properties.ContainsKey("DevError"));
                })
                .CreateLogger();

        Log.Logger.Information("");
        Log.Logger.Information("Application START");

        //#if DEBUG
        //            Consts.AppsettingsFileServerPath = @"C:\Project\Personal\DevAssistant\docs\Assets\appsettings.json";
        //#endif

        // default which is 1 (enabled)
        InitializeLogger();

        _stopTheApp = CheckForUpdateAvailable();

#if !DEBUG
#endif

        if (!_stopTheApp)
            InitializeTimer();

        // overwrite the log
        InitializeLogger();

        //if (Consts.UserSettings.UpdateSettings)
        //{
        //Log.Logger.Information("Upgrading UserSettings");

        //UserSettings.Default.Upgrade();

        //foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        //{
        //    foreach (var type in assembly.GetTypes())
        //    {
        //        if (type.Name == "UserSettings" || assembly.GetName().Name == "UserSettings")
        //        {
        //        }
        //        if (type.Name == "UserSettings" && typeof(SettingsBase).IsAssignableFrom(type))
        //        {
        //            var settings = (ApplicationSettingsBase)type.GetProperty("Default").GetValue(null, null);

        //            if (settings != null)
        //            {
        //                settings.Upgrade();
        //                settings.Reload();
        //                settings.Save();
        //            }
        //        }
        //    }

        //}

        ////Consts.UserSettings.UpdateSettings = false;
        //UserSettings.Default.Save();
        ////}

        ReviewmeInd.Visible = false;
        UtilitiesFormsInd.Visible = false;
        PullRequestsInd.Visible = false;
        StaffInd.Visible = false;

        if (!_stopTheApp)
        {
            LogEvent(DevEvents.AppStartup, EventStatus.Succeed, Consts.AppVersion);

            // Get User Settings
            var startupApp = Consts.UserSettings.StartupApp;

            // Get user apps
            CheckIncludedAppByPin();

            // Defind Apps -- START
            var appItems = new List<AppItem>
            {
                //new AppItem { Value = 0, Text = "Select every time", AppType = Apps.None },
                new AppItem { Value = 1, Text = "Reviewme", AppType = DevApp.ReviewmeApp },
                //new AppItem { Value = 2, Text = "Reader", AppType = Apps.ReaderApp },
                new AppItem { Value = 3, Text = "UtilitiesOps", AppType = DevApp.UtilitiesForms },
                new AppItem { Value = 4, Text = "Pull Requests", AppType = DevApp.MyWork },
                new AppItem { Value = 5, Text = "Staff", AppType = DevApp.Staff },
                new AppItem { Value = 6, Text = "MyWork", AppType = DevApp.MyWork },
                //new AppItem { Value = 5, Text = "Pr-Review", AppType = Apps.PrReview },
            };

            // Add included apps to appItems
            foreach (var app in _includedApps)
            {
                appItems.Add(new()
                {
                    Value = app.Value,
                    Text = app.Text,
                    AppType = app.AppType,
                });
            }

            AppsComboBox.DataSource = appItems;
            AppsComboBox.DisplayMember = "Text";
            AppsComboBox.ValueMember = "Value";
            // Defind Apps -- END

            ReviewmeInd.Visible = true; // default app

            // Start the app based on UserSettings StartupApp
            switch (startupApp)
            {
                case 1:
                    OpenReviewmeApp();
                    AppsComboBox.Text = "Reviewme";

                    break;

                //case 2:
                //    _readerApp = new Reader.ReaderHome(this)
                //    {
                //        MdiParent = this
                //    };

                //    _readerApp.Show();

                //    AppsComboBox.Text = "Reader";

                //    ReaderInd.Visible = true;

                //    break;

                case 3:
                    OpenUtilitiesFormsApp();
                    AppsComboBox.Text = "UtilitiesOps";
                    break;

                case 4:
                    OpenPullRequestsApp();
                    AppsComboBox.Text = "Pull Requests";
                    break;

                case 5:
                    OpenStaffApp();
                    AppsComboBox.Text = "Staff";
                    break;

                case 6:
                    OpenMyWorkApp();
                    AppsComboBox.Text = "MyWork";
                    break;


                default:

                    if (_includedApps.TryGetFirst(app => app.Value == startupApp, out var app))
                    {
                        OpenApp(app);
                        AppsComboBox.Text = app.Text;
                        break;
                    }

                    AppsComboBox.Text = "Select a startup app";
                    break;
            }

            //if (_readerApp is null && _reviewmeApp is null && _utilitiesForms is null)
            //{
            //    ShowButtons();
            //}

            toolTip1.SetToolTip(StartupAppLabel, "Select startup application for the next time the app is run");
            toolTip1.SetToolTip(AppsComboBox, "Select startup application for the next time the app is run");
            toolTip1.InitialDelay = 50;

            ActiveControl = null;
        }
        else
        {
            LogEvent(DevEvents.AppStartup, EventStatus.Failed, "A New Update Available. So the app stopped!");
        }
    }

    private void OpenApp(DevAppInfo app)
    {
        Log.Logger.Information("Open {a} App", app.Text);

        if (app.Text.Equals("Tasksboard"))
            OpenTasksboardApp();
        else if (app.Text.Equals("PrReview"))
            OpenPullRequestsApp();
    }

    private void CheckIncludedAppByPin()
    {
        Log.Logger.Information("Check Included Apps");

#if DEBUG
        Log.Logger.Information("Your are in Debug mode.....");
        Log.Logger.Information("You have access to TasksboardApp");

        TasksboardAppToolStripMenuItem.Visible = true;

        _includedApps.Add(new()
        {
            Value = 20,
            Text = "Tasksboard",
            AppType = DevApp.TasksBoard
        });

#endif
        if (Consts.SecretsPin is not null and { Count: > 0 })
        {
            foreach (var pin in Consts.SecretsPin)
            {
                if (pin.Equals(Pin.IncludeTasksboardApp))
                {
                    Log.Logger.Information("You have access to TasksboardApp");

                    TasksboardAppToolStripMenuItem.Visible = true;

                    _includedApps.Add(new()
                    {
                        Value = 20,
                        Text = "Tasksboard",
                        AppType = DevApp.TasksBoard
                    });
                }
            }
        }
    }

    private void InitializeLogger()
    {
        DisposeLogger();

        if (_enableLog is 0)
            _enableLog = LogStatus.EnableAll; // default

#if DEBUG
        /// <summary>
        /// 1- Enable all
        /// 2- disable all
        /// 3- only local
        /// 4- only server
        /// 999- Force the app to stop
        /// </summary>
        _enableLog = LogStatus.OnlyLocal;
#endif

        string outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u4}]: {Message:lj}{NewLine}{Exception}";
        string outputTemplateServer = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} | {Level:u4} | {DevEvent} | {EventStatus} | {Message:lj} {NewLine}{Exception}";
        string outputTemplateError = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} | {DevEv} | {Message:lj} | {Exception}";

        if (_enableLog != LogStatus.DisableAll)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Logger(l =>
                {
                    if (_enableLog is not (LogStatus.EnableAll or LogStatus.OnlyLocal))
                        return;

                    // Local logs
                    l.WriteTo.File(@$"{Consts.AppTempPath}\AppLogs\Logs.log", outputTemplate: outputTemplate, rollingInterval: RollingInterval.Day, shared: true);

                    //l.Filter.ByExcluding(e => e.Properties.ContainsKey("DevEvent") || e.Properties.ContainsKey("DevError"));
                    l.Filter.ByExcluding(e => e.Properties.ContainsKey("DevEvent"));
                })
                .WriteTo.Logger(l =>
                {
                    if (_enableLog is not (LogStatus.EnableAll or LogStatus.OnlyServer))
                        return;

                    // 252,232
                    // 1073741824L

                    // Server logs
                    l.WriteTo.File(@$"{Consts.AppLogsServer}\Logs.log", outputTemplate: outputTemplateServer, rollingInterval: RollingInterval.Month,
                        shared: true, rollOnFileSizeLimit: true);

                    l.Filter.ByIncludingOnly(e => e.Properties.ContainsKey("DevEvent"));
                })
                .WriteTo.Logger(l =>
                {
                    if (_enableLog is not (LogStatus.EnableAll or LogStatus.OnlyServer))
                        return;

                    // local error logs
                    l.WriteTo.File(@$"{Consts.AppLogsServer}\Errors.log", LogEventLevel.Fatal, outputTemplate: outputTemplateError, rollingInterval: RollingInterval.Month, shared: true);
                    l.Filter.ByIncludingOnly(e => e.Properties.ContainsKey("DevError"));
                })
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Logger initialized successfully!");
        }
        else
        {
            Log.Logger.Information("Overwrite the log");
            Log.Logger = new LoggerConfiguration().CreateLogger();
        }
    }

    private void InitializeTimer()
    {
        _sessionTimer = new Timer
        {
            //Interval = (int)TimeSpan.FromMilliseconds(10000).TotalMilliseconds
            Interval = (int)TimeSpan.FromHours(_sessionDurationH).TotalMilliseconds
        };

        _sessionTimer.Tick += new EventHandler(SessionTimer_Tick);
        _sessionTimer.Enabled = true;

        // each 12h check if there is an update avaliable
        _checkUpdateTimer = new Timer
        {
            //Interval = (int)TimeSpan.FromMilliseconds(10000).TotalMilliseconds
            Interval = (int)TimeSpan.FromHours(12).TotalMilliseconds
        };

        _checkUpdateTimer.Tick += new EventHandler(CheckUpdate_Tick);
        _checkUpdateTimer.Enabled = true;
    }

    private void CheckUpdate_Tick(object sender, EventArgs e)
    {
        Log.Logger.Information("Auto check for update -- START");
        Log.Logger.Information("If there is an update avaliable then stop the app; otherwise, do nothing");

        _checkUpdateTimer.Enabled = false;
        _sessionTimer.Enabled = false;

        lastUpdateCheckToolStripMenuItem.Text = $"Last update check: {DateTime.Now}";
        lastUpdateCheckToolStripMenuItem.Visible = true;

        Log.Logger.Information("Reinitialize the Logger...");

        InitializeLogger();

        _stopTheApp = CheckForUpdateAvailable(true);

        Event evv = DevEvents.SessionExpired;

        using (LogContext.PushProperty("EventStatus", EventStatus.Clicked))
        using (LogContext.PushProperty("DevEvent", evv.Id))
        {
            Log.Logger.Information($"Auto check for new update... {(_stopTheApp ? "New update avaliable found!" : "No update found!")}");
        }

        if (_stopTheApp)
        {
            Log.Logger.Information("New update avaliable found for the App!");

            // Hide all
            HideFormsExcept();

            DisposeApp();
        }
        else
        {
            Log.Logger.Information("No update found for the App!");

            // run the timer again
            _checkUpdateTimer.Enabled = true;
            _sessionTimer.Enabled = true;
        }

        Log.Logger.Information("Auto check for update -- END");
    }

    private static void DisposeLogger()
    {
        try
        {
            Log.Logger.Information("Disposing, closing and flushing the Logger...");

            Log.CloseAndFlush();
            Log.Logger = null;
        }
        catch (Exception ex)
        {
            Log.Logger.Error("Falied to dispose the logger :(", ex);
        }
    }

    private void SessionTimer_Tick(object sender, EventArgs e)
    {
        _sessionTimer.Enabled = false;
        _checkUpdateTimer.Enabled = false;

        Log.Logger.Information("The session has been expired since the last event on {a}. The app must be restarted", _appStartedAt);

        Event evv = DevEvents.SessionExpired;

        using (LogContext.PushProperty("EventStatus", EventStatus.Clicked))
        using (LogContext.PushProperty("DevEvent", evv.Id))
        {
            Log.Logger.Information("Session has been expired");
        }

        // Hide all
        HideFormsExcept();

        menuStrip1.Enabled = false;
        AppsComboBox.Enabled = false;
        groupBox1.Visible = false;

        AppScreen.Size = new Size(1154, 815);

        // Stop the app and show the message
        UpdateAvailableLabel.Text = "The app session has been expired :(";
        VersionsLabel.Text = $"The session has been expired since the app started on {_appStartedAt}.{Environment.NewLine}Please restart the app and try again";

        UpdateAvailableLabel.Visible = true;
        VersionsLabel.Visible = true;

        UpdateBtn.Visible = false;
        RestartBtn.Visible = true;

        _sessionExpired = true;
        DisposeApp();
    }

    private static void DisposeApp()
    {
        try
        {
            Log.Logger.Information("Disposing the app..");

            ApiClient.Client?.Dispose();
            DevOpsClient.Dispose();

            DisposeLogger();
        }
        catch (Exception ex)
        {
            Log.Logger.Error("Failed to dispose the app :(", ex);
        }
    }

    /// <summary>
    /// Check if there is any update or if we must stop the app
    /// </summary>
    /// <returns>True if there is an update, false if there is no so the app still work</returns>
    private bool CheckForUpdateAvailable(bool autoCheck = false)
    {
        if (Application.ExecutablePath.ToLower().Contains(@"nicdevnfs")
            || Application.ExecutablePath.ToLower().Contains(@"z:\alyahya") // my local Network locations
            || Application.ExecutablePath.ToLower().Contains(@"y:\alyahya") // my local Network locations
            || Application.ExecutablePath.ToLower().Contains(@"devfilesvr01")
            || Application.ExecutablePath.ToLower().Contains("172.29.1.39"))
        {
            Log.Logger.Information("The app is running from the shared folder");

            menuStrip1.Enabled = false;
            AppsComboBox.Enabled = false;
            groupBox1.Visible = false;

            AppScreen.Size = new Size(1154, 815);

            // Stop the app and show the message
            UpdateAvailableLabel.Text = "Move the app out of the shared folder";
            VersionsLabel.Text = "To run the app please move it out from Shared Folder";

            UpdateAvailableLabel.Visible = true;
            VersionsLabel.Visible = true;

            UpdateBtn.Visible = false;

            return true;
        }
        else
        {
            Log.Logger.Information("Read local appsettings from {a} -- START", Consts.AppsettingsFileLocalPath);

            LocalAppsettings localAppsettings = null;

            if (File.Exists(Consts.AppsettingsFileLocalPath))
            {
                Log.Logger.Information("Reading local appsettings");

                using (StreamReader file = File.OpenText(Consts.AppsettingsFileLocalPath))
                {
                    localAppsettings = JsonConvert.DeserializeObject<LocalAppsettings>(file.ReadToEnd());
                }

                Log.Logger.Information("Get AppServerPath from local appsettings");

                if (localAppsettings != null && !string.IsNullOrWhiteSpace(localAppsettings.AppServerPath))
                {
                    Log.Logger.Information("AppServerPath found. Set AppServerPath to {path}", localAppsettings.AppServerPath);

                    string temp = Consts.AppServerPath;

                    Consts.AppServerPath = localAppsettings.AppServerPath;

                    // Check if App has Access to Server based on localAppsettings.AppServerPath
                    if (File.Exists(Consts.AppsettingsFileServerPath)) // Shared folder not working or files missing..
                    {
                        // Go to new shared by hostname
                        Log.Logger.Information("AppServerPath from localAppsettings has access to server");
                    }
                    {
                        // Go back to the default URL
                        Consts.AppServerPath = temp;

                        Log.Logger.Information("AppServerPath from localAppsettings doesn't have access to server. Change it back to the default URL... {a}", Consts.AppServerPath);
                    }
                }
                else
                {
                    Log.Logger.Information("AppServerPath not found. Set AppServerPath to default {path}", Consts.AppServerPath);
                }
            }
            else
            {
                Log.Logger.Information("Local appsettings not found");
            }

            Log.Logger.Information("Read localsettings from  -- END");

            Log.Logger.Information("Check if server appsetting server accessible.. {a}", Consts.AppsettingsFileServerPath);

            // Check if App has Access to Server & Prepare Assets path -- START
            if (!File.Exists(Consts.AppsettingsFileServerPath)) // Shared folder not working or files missing..
            {
                // Go to new shared by hostname
                Consts.AppServerPath = @$"\\devfilesvr01\D\Alyahya\DevAssistant";
                Log.Logger.Information("Couldn't access to shared. Change it to shared folder by new hostname.. {a}", Consts.AppServerPath);

                if (!File.Exists(Consts.AppsettingsFileServerPath))
                {
                    // Go to server .27
                    Consts.AppServerPath = @"\\172.29.1.27\Deployment\DevAssistant";

                    Log.Logger.Information("Couldn't access to shared. Change it to.. {a}", Consts.AppServerPath);

                    // now Consts.AppsettingsFileServerPath will be in 27
                    if (!File.Exists(Consts.AppsettingsFileServerPath))
                    {
                        Log.Logger.Information("Couldn't access to {a}", Consts.AppServerAssetsPath);
                        Log.Logger.Information("CheckForUpdateAvailable STOPED");

                        return false;
                    }
                }
            }
            // Check if App has Access to Server & Prepare Assets path -- END

            Log.Logger.Information("App can access appsetting server from {a}", Consts.AppsettingsFileServerPath);

            // Init the App -- START
            // Create temp directory for DevAssistant app
            if (!Directory.Exists(Consts.AppTempPath))
            {
                Log.Logger.Information("Create the AppTemp directory in {a}", Consts.AppTempPath);
                LogInfo("Create DevAssistant directory...");

                Directory.CreateDirectory(Consts.AppTempPath);
            }

            // Create Assets directory indide temp directory
            if (!Directory.Exists(Consts.AppLocalAssetsPath))
            {
                Log.Logger.Information("Create the AppAssets directory in {a}", Consts.AppLocalAssetsPath);
                LogInfo("Create Assets directory under DevAssistant...");

                Directory.CreateDirectory(Consts.AppLocalAssetsPath);
            }

            if (!File.Exists(Consts.ApiIGTemplateFileLocalPath))
            {
                Log.Logger.Information("Copying the ApiIGTemplateFile to {a}", Consts.ApiIGTemplateFileLocalPath);
                LogInfo("Copying API IG template file...");

                if (File.Exists(Consts.ApiIGTemplateFileServerPath))
                    File.Copy(Consts.ApiIGTemplateFileServerPath, Consts.ApiIGTemplateFileLocalPath);
                else
                    Log.Logger.Information("Couldn't found API IG template file in the server.. {a}", Consts.ApiIGTemplateFileServerPath);
            }

            if (!File.Exists(Consts.MicroIGTemplateFileLocalPath))
            {
                Log.Logger.Information("Copying the MicroIGTemplateFile to {a}", Consts.MicroIGTemplateFileLocalPath);
                LogInfo("Copying MicroServices IG template file...");

                if (File.Exists(Consts.MicroIGTemplateFileServerPath))
                    File.Copy(Consts.MicroIGTemplateFileServerPath, Consts.MicroIGTemplateFileLocalPath);
                else
                    Log.Logger.Information("Couldn't found MicroServices IG template file in the server.. {a}", Consts.MicroIGTemplateFileServerPath);
            }


            if (!File.Exists(Consts.StaffInfoFileLocalPath))
            {
                Log.Logger.Information("Copying the StaffInfoFile {a}", Consts.StaffInfoFileLocalPath);
                LogInfo("Copying Staff info file...");

                if (File.Exists(Consts.StaffInfoFileServerPath))
                    File.Copy(Consts.StaffInfoFileServerPath, Consts.StaffInfoFileLocalPath);
                else
                    Log.Logger.Information("Couldn't found staff info file in the server.. {a}", Consts.StaffInfoFileServerPath);
            }
            // Init the App -- END

            Log.Logger.Information("Reading server appsettings from {a}", Consts.AppsettingsFileServerPath);

            using (StreamReader file = File.OpenText(Consts.AppsettingsFileServerPath))
            {
                _serverAppsettings = JsonConvert.DeserializeObject<ServerAppsettings>(file.ReadToEnd());
            }

            Log.Logger.Information("Reading done!");

            if (_serverAppsettings == null)
            {
                Log.Logger.Information("Server appsettings is null! Stop CheckForUpdateAvailable()");

                return false;
            }

            Log.Logger.Information("Logging Mode: {mode}", _serverAppsettings.EnableLog);

            // Set valuse frmo server -- START
            _enableLog = _serverAppsettings.EnableLog;

            if (!string.IsNullOrWhiteSpace(_serverAppsettings.AuthServerUrl))
                Consts.AuthServerUrl = _serverAppsettings.AuthServerUrl;

            if (_serverAppsettings.GccContracts is not null and { Count: > 0 })
                Consts.GccContracts = _serverAppsettings.GccContracts;
            else
                Consts.GccContracts = new() { "Vio", "Reports", "TravelersInfo" }; // defualt

            if (_serverAppsettings.ApiCommonExceptions is not null and { Count: > 0 })
                Consts.ApiCommonExceptions = _serverAppsettings.ApiCommonExceptions;

            if (!string.IsNullOrWhiteSpace(_serverAppsettings.TfsUrl))
                Consts.TfsUrl = _serverAppsettings.TfsUrl;

            if (_serverAppsettings.SessionDuration > 0)
            {
                // autoCheck mean called by the timer. if _sessionDurationH is changed then update the Interval
                if (autoCheck)
                {
                    // update the timer
                    if (_sessionDurationH != _serverAppsettings.SessionDuration)
                    {
                        Log.Logger.Information("Updating SessionTimer's Interval from {_sessionDurationH} to {_serverAppsettings.SessionDuration}", _sessionDurationH, _serverAppsettings.SessionDuration);

                        _sessionTimer.Interval = (int)TimeSpan.FromHours(_serverAppsettings.SessionDuration).TotalMilliseconds;
                    }
                }

                _sessionDurationH = _serverAppsettings.SessionDuration;
            }
            // Set valuse frmo server -- END

            // get & update (update only after update finish) local Appsettings
            if (localAppsettings != null)
            {
                // This will run after each time the app was updated by user
                if (localAppsettings.UpdateSettings)
                {
                    Log.Logger.Information("Copying values from local appsettings to UserSettings - UpdateSettings is true");

                    Consts.UserSettings.StartupApp = localAppsettings.StartupApp;
                    Consts.UserSettings.MicroServicePath = localAppsettings.MicroServicePath;
                    Consts.UserSettings.UtilitiesServicePath = localAppsettings.UtilitiesServicePath;
                    Consts.UserSettings.WrittenByIG = localAppsettings.WrittenByIG;
                    Consts.UserSettings.IGPathFolder = localAppsettings.IGPathFolder;
                    Consts.UserSettings.LastPathInput = localAppsettings.LastPathInput;
                    Consts.UserSettings.MyWorkSearchCriteria = localAppsettings.MyWorkSearchCriteria;

                    Consts.UserSettings.RFeatureCriteria = localAppsettings.RFeatureCriteria;
                    Consts.UserSettings.RIGSelectedOption = localAppsettings.RIGSelectedOption;
                    Consts.UserSettings.RConverterSelectedOption = localAppsettings.RConverterSelectedOption;
                    Consts.UserSettings.RCompareSelectedOption = localAppsettings.RCompareSelectedOption;
                    Consts.UserSettings.PListAllEmp = localAppsettings.PListAllEmp;
                    Consts.UserSettings.RNdbOpenFileCheckOption = localAppsettings.RNdbOpenFileCheckOption;
                    Consts.UserSettings.StaffSearchBy = localAppsettings.StaffSearchBy;
                    Consts.UserSettings.RIGProjectTypeOption = localAppsettings.RIGProjectTypeOption;
                    //Consts.UserSettings.UpdateSettings = false;

                    localAppsettings.UpdateSettings = false;

                    Log.Logger.Information("Writing local appsettings file after updateing UserSettings and set UpdateSettings to false");

                    string json = JsonConvert.SerializeObject(localAppsettings, Formatting.Indented);
                    File.WriteAllText(Consts.AppsettingsFileLocalPath, json);
                }

                Consts.SecretsPin = localAppsettings.SecretsPin ?? new();

                if (_serverAppsettings.ApiIGTemplateVersion > localAppsettings.ApiIGTemplateVersion)
                {
                    LogEvent(DevEvents.UpdateAssets, EventStatus.Called, $"A New Update Available for the API IG Template! ServerVersion: {_serverAppsettings.ApiIGTemplateVersion}. LocalVersion: {localAppsettings.ApiIGTemplateVersion}. Updating...");

                    LogInfo($"A New Update Available for the API IG Template! ServerVersion: {_serverAppsettings.ApiIGTemplateVersion}. LocalVersion: {localAppsettings.ApiIGTemplateVersion}. Updating...");

                    GetFileFromServer(Consts.ApiIGTemplateFileServerPath, Consts.ApiIGTemplateFileLocalPath);

                    LogSuccess("Updated successfully!");

                    localAppsettings.ApiIGTemplateVersion = _serverAppsettings.ApiIGTemplateVersion;
                    string json = JsonConvert.SerializeObject(localAppsettings, Formatting.Indented);

                    File.WriteAllText(Consts.AppsettingsFileLocalPath, json);

                    Log.Logger.Information("Update ApiIGTemplateVersion in local Appsettings");
                }


                if (_serverAppsettings.MicroIGTemplateVersion > localAppsettings.MicroIGTemplateVersion)
                {
                    LogEvent(DevEvents.UpdateAssets, EventStatus.Called, $"A New Update Available for the Micro IG Template! ServerVersion: {_serverAppsettings.MicroIGTemplateVersion}. LocalVersion: {localAppsettings.ApiIGTemplateVersion}. Updating...");

                    LogInfo($"A New Update Available for the Micro IG Template! ServerVersion: {_serverAppsettings.MicroIGTemplateVersion}. LocalVersion: {localAppsettings.MicroIGTemplateVersion}. Updating...");

                    GetFileFromServer(Consts.MicroIGTemplateFileServerPath, Consts.MicroIGTemplateFileLocalPath);

                    LogSuccess("Updated successfully!");

                    localAppsettings.MicroIGTemplateVersion = _serverAppsettings.MicroIGTemplateVersion;
                    string json = JsonConvert.SerializeObject(localAppsettings, Formatting.Indented);

                    File.WriteAllText(Consts.AppsettingsFileLocalPath, json);

                    Log.Logger.Information("Update MicroIGTemplateVersion in local Appsettings");
                }


                if (_serverAppsettings.StaffInfoVersion > localAppsettings.StaffInfoVersion)
                {
                    LogEvent(DevEvents.UpdateAssets, EventStatus.Called, $"A New Update Available for the Staff Info! ServerVersion: {_serverAppsettings.StaffInfoVersion}. LocalVersion: {localAppsettings.StaffInfoVersion}. Updating...");

                    LogInfo($"A New Update Available for the Staff Info! ServerVersion: {_serverAppsettings.StaffInfoVersion}. LocalVersion: {localAppsettings.StaffInfoVersion}. Updating...");

                    GetFileFromServer(Consts.StaffInfoFileServerPath, Consts.StaffInfoFileLocalPath);

                    LogSuccess("Updated successfully!");

                    localAppsettings.StaffInfoVersion = _serverAppsettings.StaffInfoVersion;
                    string json = JsonConvert.SerializeObject(localAppsettings, Formatting.Indented);

                    File.WriteAllText(Consts.AppsettingsFileLocalPath, json);

                    Log.Logger.Information("Update StaffInfoVersion in local Appsettings");
                }
            }
            else // if Appsettings not exists create one
            {
                Log.Logger.Information("No local appsettings found. Create one...");
                LogInfo("Create local appsettings under DevAssistant...");

                localAppsettings = new()
                {
                    AppVersion = Consts.AppVersion,
                    ApiIGTemplateVersion = _serverAppsettings.ApiIGTemplateVersion,
                    StaffInfoVersion = _serverAppsettings.StaffInfoVersion,
                    MicroServicePath = Consts.UserSettings.MicroServicePath,
                    UtilitiesServicePath = Consts.UserSettings.UtilitiesServicePath,
                    SecretsPin = new(),

                    StartupApp = Consts.UserSettings.StartupApp,
                    WrittenByIG = Consts.UserSettings.WrittenByIG,
                    IGPathFolder = Consts.UserSettings.IGPathFolder,
                    LastPathInput = Consts.UserSettings.LastPathInput,
                    MyWorkSearchCriteria = Consts.UserSettings.MyWorkSearchCriteria,

                    RFeatureCriteria = Consts.UserSettings.RFeatureCriteria,
                    RIGSelectedOption = Consts.UserSettings.RIGSelectedOption,
                    RConverterSelectedOption = Consts.UserSettings.RConverterSelectedOption,
                    RCompareSelectedOption = Consts.UserSettings.RCompareSelectedOption,
                    PListAllEmp = Consts.UserSettings.PListAllEmp,
                    RNdbOpenFileCheckOption = Consts.UserSettings.RNdbOpenFileCheckOption,
                    StaffSearchBy = Consts.UserSettings.StaffSearchBy,
                    RIGProjectTypeOption = Consts.UserSettings.RIGProjectTypeOption,
                };

                string json = JsonConvert.SerializeObject(localAppsettings, Formatting.Indented);
                File.WriteAllText(Consts.AppsettingsFileLocalPath, json);
            }

            // Stop the app
            if (_serverAppsettings.EnableLog == LogStatus.ForceStop && (!Consts.SecretsPin?.Exists(secretPin => secretPin.Equals(Pin.IgnoreForceStopApp)) ?? true))
            {
                Log.Logger.Information("Force the app to stop");

                menuStrip1.Enabled = false;
                AppsComboBox.Enabled = false;
                groupBox1.Visible = false;

                AppScreen.Size = new Size(1154, 815);

                // Stop the app and show the message
                UpdateAvailableLabel.Visible = true;
                UpdateAvailableLabel.Text = "The app is temporarily unavailable :(";

                VersionsLabel.Visible = true;
                VersionsLabel.Text = "Please try again later";

                UpdateBtn.Visible = false;

                return true;
            }

            // Update the app app
            if (_serverAppsettings.IsNewVersion(Consts.AppVersion))
            {
                Log.Logger.Information("A New Update Available for the App! ServerVersion: {a}. LocalVersion: {b}", _serverAppsettings.AppVersion, Consts.AppVersion);
                Log.Logger.Information("Updating...");

                ReviewmeInd.Visible = false;
                UtilitiesFormsInd.Visible = false;
                PullRequestsInd.Visible = false;
                StaffInd.Visible = false;

                menuStrip1.Enabled = false;
                AppsComboBox.Enabled = false;
                groupBox1.Visible = false;

                UpdateBtn.Visible = true;

                AppScreen.Size = new Size(1154, 815);

                // Stop the app and show the message
                UpdateAvailableLabel.Visible = true;

                UpdateAvailableLabel.Text = "A New Update Available for the App!";

                VersionsLabel.Visible = true;
                VersionsLabel.Text = $"Current version: v{Consts.AppVersion}{Environment.NewLine}New version: v{_serverAppsettings.AppVersion}";

                return true;
            }
            else
            {
                Log.Logger.Information("There is no available update found for the App");
            }
        }

        return false;
    }

    private void GetFileFromServer(string sourceFileName, string destFileName)
    {
        Log.Logger.Information("GetFileFromServer -- START");
        Log.Logger.Information("SourceFileName: {a}", sourceFileName);
        Log.Logger.Information("DestFileName: {a}", destFileName);

        // If directory found, means the user can access the server
        if (!Directory.Exists(Consts.AppServerAssetsPath))
            throw new Exception("9000: Couldn't found app directory in the server or the server location directory cannot be reached check your connection");

        if (!File.Exists(sourceFileName))
            throw new Exception("9001: Couldn't found the file in the server");

        // delete the file first
        if (File.Exists(destFileName))
        {
            Log.Logger.Information("Deleteing current version from {a}", destFileName);
            File.Delete(destFileName);
        }

        File.Copy(sourceFileName, destFileName);

        Log.Logger.Information("GetFileFromServer -- END");
    }

    private void AddToPanel(UserControl uc)
    {
        uc.AutoSize = true;
        //uc.AutoScaleMode = AutoScaleMode.gr
        uc.Dock = DockStyle.Fill;
        //uc.Anchor = AnchorStyles.Top & AnchorStyles.Right & AnchorStyles.Bottom & AnchorStyles.Left;

        AppScreen.Controls.Add(uc);

        uc.BringToFront();
    }

    //private void ReaderAppMenuItem_Click(object sender, EventArgs e) => OpenReaderApp();

    //private void ReaderAppBtn_Click(object sender, EventArgs e) => OpenReaderApp();

    //private void OpenReaderApp()
    //{
    //    if (_selectedApp == Apps.ReaderApp)
    //    {
    //        return;
    //    }

    //    _selectedApp = Apps.ReaderApp;

    //    if (_readerApp is null)
    //    {
    //        _readerApp = new Reader.ReaderHome(this)
    //        {
    //            MdiParent = this
    //        };
    //    }

    //    HideButtons();
    //    HideFormsExcept(typeof(Reader.ReaderHome).Name);

    //    _readerApp.Show();
    //}

    private void ReviewmeAppMenuItem_Click(object sender, EventArgs e) => OpenReviewmeApp();

    private void ReviewmeAppBtn_Click(object sender, EventArgs e) => OpenReviewmeApp();

    private void OpenReviewmeApp()
    {
        if (_selectedApp == DevApp.ReviewmeApp || _stopTheApp)
        {
            return;
        }

        _selectedApp = DevApp.ReviewmeApp;

        if (_reviewmeApp is null)
        {
            _reviewmeApp = new Reviewme.ReviewmeHome(this);

            AddToPanel(_reviewmeApp);
        }

        HideFormsExcept(_reviewmeApp, ReviewmeInd);
    }

    private void UtilitiesFormsMenuItem_Click(object sender, EventArgs e) => OpenUtilitiesFormsApp();

    private void OpenUtilitiesFormsApp()
    {
        if (_selectedApp == DevApp.UtilitiesForms || _stopTheApp)
        {
            return;
        }

        _selectedApp = DevApp.UtilitiesForms;

        if (_utilitiesForms is null)
        {
            _utilitiesForms = new UtilitiesOps.UtilitiesFormsHome(this);

            AddToPanel(_utilitiesForms);

            //LogNote(_utilitiesFormsNote);
        }

        HideFormsExcept(_utilitiesForms, UtilitiesFormsInd);
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedApp = (AppItem)AppsComboBox.SelectedItem;

        switch (selectedApp.Value)
        {
            case 1:
                Consts.UserSettings.StartupApp = 1;
                OpenReviewmeApp();

                break;

            //case 2:
            //    Consts.UserSettings.StartupApp = 2;
            //    OpenReaderApp();

            //    break;

            case 3:
                Consts.UserSettings.StartupApp = 3;
                OpenUtilitiesFormsApp();

                break;

            case 4:
                Consts.UserSettings.StartupApp = 4;
                OpenPullRequestsApp();

                break;

            case 5:
                Consts.UserSettings.StartupApp = 5;
                OpenStaffApp();
                break;

            case 6:
                Consts.UserSettings.StartupApp = 6;
                OpenMyWorkApp();
                break;

            default:
                Consts.UserSettings.StartupApp = 0;

                if (_includedApps.TryGetFirst(app => app.Value == selectedApp.Value, out var app))
                {
                    Consts.UserSettings.StartupApp = app.Value;
                    OpenApp(app);
                }

                break;
        }
    }

    private void HideFormsExcept(UserControl control = null, Label indicator = null)
    {
        ActiveControl = null;
        Log.Logger.Information("HideFormsExcept: {control}", control != null ? control.Name : "Hide All");

        // Hide all indicator
        ReviewmeInd.Visible = false;
        UtilitiesFormsInd.Visible = false;
        PullRequestsInd.Visible = false;
        StaffInd.Visible = false;

        // Hide all apps
        _reviewmeApp?.Hide();
        _utilitiesForms?.Hide();
        _myWorkApp?.Hide();
        _pullRequestsApp?.Hide();
        _staffApp?.Hide();
        _tasksBoardApp?.Hide();

        if (control != null)
            control.Show();
        else
            Log.Logger.Information("Hide all apps");

        if (indicator != null)
            indicator.Visible = true;
        else
            Log.Logger.Information("Hide all indicators");
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        Log.Logger.Information("Closing the App");

        try
        {
            LogEvent(DevEvents.ClosingApp, EventStatus.Called, Consts.AppVersion);

            Consts.UserSettings.Save();
            DisposeApp();

            Log.Logger.Information("Application CLOSED");
        }
        catch (Exception)
        {
            LogError("Error while closing the app!", DevEvents.AppStartup, EventStatus.Failed);
        }
    }

    private void MyWorkMenuItem_Click(object sender, EventArgs e) => OpenMyWorkApp();

    private void OpenMyWorkApp()
    {
        if (_selectedApp == DevApp.MyWork || _stopTheApp)
        {
            return;
        }

        _selectedApp = DevApp.MyWork;

        if (_myWorkApp is null)
        {
            _myWorkApp = new MyWork.MyWorkHome(this);

            AddToPanel(_myWorkApp);
        }

        HideFormsExcept(_myWorkApp);
    }

    private void OpenPullRequestsApp()
    {
        if (_selectedApp == DevApp.PullRequests || _stopTheApp)
        {
            return;
        }

        _selectedApp = DevApp.PullRequests;

        PullRequestsToolStripMenuItem1.Text = "Loading...";

        if (_pullRequestsApp is null)
        {
            _pullRequestsApp = new PullRequests.PullRequestsHome(this);

            AddToPanel(_pullRequestsApp);
        }

        //if (string.IsNullOrWhiteSpace(Consts.UserSettings.Pat))
        //{
        //    _prReviewLoginApp = new PrReview.PrReviewLogin(this);

        //    AddToPanel(_prReviewLoginApp);

        //    HideButtons();
        //    HideFormsExcept(typeof(PrReview.PrReviewLogin).Name);

        //    _prReviewLoginApp.Show();
        //    return;
        //}

        HideFormsExcept(_pullRequestsApp, PullRequestsInd);

        PullRequestsToolStripMenuItem1.Text = "Pull Requests";
    }

    #region Log

    public void LogEvent(Event ev, EventStatus status, string message = "", Exception ex = null)
    {
        ArgumentNullException.ThrowIfNull(ev);

        if (_sessionExpired)
            return;

        if (ex != null)
            LogFatal(ex, ev, message);

        using (LogContext.PushProperty("EventStatus", status))
        using (LogContext.PushProperty("DevEvent", ev.Id))
        {
            if (string.IsNullOrWhiteSpace(message))
                message = $"Event #{ev.Id}";

            Log.Logger.Information(message);
        }

        if (ex == null)
            Log.Logger.Information("{a} - {b} [{c}]", ev.Id, status, message);
    }

    public void LogInfo(string log)
    {
        LoggerText.AppendText($"{DateTime.Now:hh:mm:ss tt}: {log}{Environment.NewLine}", Color.Gray);
    }

    public void LogTrace(string log)
    {
        if (Consts.UserSettings.LogMode == 1)
            LoggerText.AppendText($"{DateTime.Now:hh:mm:ss tt}: {log}{Environment.NewLine}", Color.DarkGray);
    }

    public void LogSuccess(string log)
    {
        LoggerText.AppendText($"{DateTime.Now:hh:mm:ss tt}: {log}{Environment.NewLine}", Color.Green);
    }

    public void LogWarning(string log)
    {
        LoggerText.AppendText($"{DateTime.Now:hh:mm:ss tt}: {log}{Environment.NewLine}", Color.DarkGoldenrod);
    }

    public void LogError(string log, Event ev = null, EventStatus status = EventStatus.InputError)
    {
        if (ev != null)
            LogEvent(ev, status);

        LoggerText.AppendText($"{DateTime.Now:hh:mm:ss tt}: {log}{Environment.NewLine}", Color.Red);
    }

    public void LogError(DevAssistantException ex, Event ev = null, EventStatus status = EventStatus.InputError)
    {
        if (ev != null)
            LogEvent(ev, status, ex: new Exception(ex.ToString()));

        //Log.Logger.Error(ex );

        if (ev == null)
            Log.Logger.Error(ex, $"A business exception has occurred:");

        //_logger.Information(ex.Message);
        LoggerText.AppendText($"{DateTime.Now:hh:mm:ss tt}: [{ex.Code}] {ex.Message}{Environment.NewLine}", Color.Red);
    }

    private void LogFatal(Exception ex, Event ev, string log)
    {
        if (string.IsNullOrWhiteSpace(log))
            log = "An exception has occurred";

        //Log.Logger.Error(ex );
        using (LogContext.PushProperty("DevEv", ev.Id))
        using (LogContext.PushProperty("DevError", ev.Id))
        {
            Log.Logger.Fatal(ex, log);
        }

        Log.Logger.Fatal(ex, log);
    }

    public void LogNote(string log)
    {
        LoggerText.AppendText($"{DateTime.Now:hh:mm:ss tt}: {log}{Environment.NewLine}", Color.MidnightBlue);
    }

    private void AppHome_Load(object sender, EventArgs e)
    {
    }

    #endregion Log

    private void UpdateBtn_Click(object sender, EventArgs e)
    {
        Log.Logger.Information("UpdateBtn clicked!");

        LogEvent(DevEvents.UpdateTheApp, EventStatus.Clicked);

        VersionsLabel.Text = string.Empty;

        if (!File.Exists(Consts.AppExeBatchPath))
        {
            string meg = $"Couldn't found DevAssistant.exe at \"{Consts.AppExeBatchPath}\"";
            VersionsLabel.Text = meg;

            LogEvent(DevEvents.UpdateTheApp, EventStatus.Failed, meg);

            VersionsLabel.ForeColor = Color.Red;
            return;
        }

        // Copying values to local appsettings -- START
        if (File.Exists(Consts.AppsettingsFileLocalPath))
        {
            Log.Logger.Information("Reading local appsettings for copying values UserSettings");

            LocalAppsettings localAppsettings = null;

            using (StreamReader file = File.OpenText(Consts.AppsettingsFileLocalPath))
            {
                localAppsettings = JsonConvert.DeserializeObject<LocalAppsettings>(file.ReadToEnd());
            }

            if (localAppsettings == null)
                return;

            localAppsettings.StartupApp = Consts.UserSettings.StartupApp;
            localAppsettings.MicroServicePath = Consts.UserSettings.MicroServicePath;
            localAppsettings.UtilitiesServicePath = Consts.UserSettings.UtilitiesServicePath;
            localAppsettings.WrittenByIG = Consts.UserSettings.WrittenByIG;
            localAppsettings.IGPathFolder = Consts.UserSettings.IGPathFolder;
            localAppsettings.LastPathInput = Consts.UserSettings.LastPathInput;
            localAppsettings.MyWorkSearchCriteria = Consts.UserSettings.MyWorkSearchCriteria;

            localAppsettings.RFeatureCriteria = Consts.UserSettings.RFeatureCriteria;
            localAppsettings.RIGSelectedOption = Consts.UserSettings.RIGSelectedOption;
            localAppsettings.RConverterSelectedOption = Consts.UserSettings.RConverterSelectedOption;
            localAppsettings.RCompareSelectedOption = Consts.UserSettings.RCompareSelectedOption;
            localAppsettings.StaffSearchBy = Consts.UserSettings.StaffSearchBy;
            localAppsettings.RIGProjectTypeOption = Consts.UserSettings.RIGProjectTypeOption;
            // Set to true to copy values in CheckForUpdateAvailable()
            localAppsettings.UpdateSettings = true;

            string json = JsonConvert.SerializeObject(localAppsettings, Formatting.Indented);
            File.WriteAllText(Consts.AppsettingsFileLocalPath, json);

            Log.Logger.Information("Local appsettings updated");
        }
        // Copying values to local appsettings -- END

        UpdateBtn.Text = "Updating...";
        LogEvent(DevEvents.UpdateTheApp, EventStatus.Succeed, "Updating... Close the app and run batch file");

        Log.Logger.Information("Exit the app and run new process to fire the batch!");

        Application.Exit();
        Close();

        // CommandLineArgs that we must send to our batch
        //                             args[1]                     args[2]
        string arguments = $"{Application.ExecutablePath} {Consts.AppServerPath}";

        Process process = new()
        {
            StartInfo = new(Consts.AppExeBatchPath, arguments)
        };

        process.Start();
        process.Dispose();
    }

    private void RestartBtn_Click(object sender, EventArgs e)
    {
        Log.Logger.Information("Restarting the app");

        Event evv = DevEvents.RestartApp;

        using (LogContext.PushProperty("EventStatus", EventStatus.Succeed))
        using (LogContext.PushProperty("DevEvent", evv.Id))
        {
            Log.Logger.Information("Restarting the app");
        }

        Application.Restart();
        Environment.Exit(0);
    }

    private void pRReviewToolStripMenuItem_Click(object sender, EventArgs e) => OpenPullRequestsApp();

    private void AppsComboBox_SelectionChangeCommitted(object sender, EventArgs e)
    {
    }

    private void ReleaseNotesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        LogEvent(DevEvents.ReleaseNotes, EventStatus.Clicked);

        try
        {
            AppReleaseNotes releaseNotes = new();
            releaseNotes.Show();

            LogEvent(DevEvents.ReleaseNotes, EventStatus.Succeed);
        }
        catch (Exception ex)
        {
            LogError("Couldn't open release notes!");

            LogEvent(DevEvents.ReleaseNotes, EventStatus.Failed, ex.Message);

            throw;
        }
    }

    private void phoneExtsToolStripMenuItem_Click(object sender, EventArgs e) => OpenStaffApp();

    private void OpenStaffApp()
    {
        if (_selectedApp == DevApp.Staff || _stopTheApp)
        {
            return;
        }

        _selectedApp = DevApp.Staff;

        LogEvent(DevEvents.SAppOpned, EventStatus.Called);

        if (_staffApp is null)
        {
            _staffApp = new Staff.StaffHome(this);

            AddToPanel(_staffApp);
        }

        HideFormsExcept(_staffApp, StaffInd);
    }

    private void TasksboardAppToolStripMenuItem_Click(object sender, EventArgs e) => OpenTasksboardApp();

    private void OpenTasksboardApp()
    {
        if (_selectedApp == DevApp.TasksBoard || _stopTheApp)
        {
            return;
        }

        _selectedApp = DevApp.TasksBoard;

        LogEvent(DevEvents.TAppOpned, EventStatus.Called);

        if (_tasksBoardApp is null)
        {
            _tasksBoardApp = new TasksBoard.TasksBoardHome(this);

            AddToPanel(_tasksBoardApp);
        }

        HideFormsExcept(_tasksBoardApp);
    }

    private void pRReviewToolStripMenuItem1_Click(object sender, EventArgs e) => OpenPullRequestsApp();

    private void ClearBtn_Click(object sender, EventArgs e)
    {
        LoggerText.Clear();
    }

    private void SettingsMenuItem_Click(object sender, EventArgs e)
    {

    }
}