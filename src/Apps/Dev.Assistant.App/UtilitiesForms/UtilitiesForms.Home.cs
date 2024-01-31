using AuthServer;
using Dev.Assistant.Business.Core.Extensions;
using Dev.Assistant.Business.Core.Models;
using Dev.Assistant.Business.Core.Services;
using Dev.Assistant.Business.Core.Utilities;
using Dev.Assistant.Business.Decoder.Services;
using Dev.Assistant.Configuration;
using Serilog;
using System.Text;

// Error Code start 7300

namespace Dev.Assistant.App.UtilitiesForms;

public partial class UtilitiesFormsHome : UserControl
{
    public readonly AppHome appHome;
    private readonly List<string> servicesNames = new();
    private string dbName;
    private string contractName;
    private const string _prefixText = "ws_";
    private const string _getCommentLabel = "Get Comment Only";
    private const string _getTablesLabel = "Get Tables";
    private const string _loadingLabel = "Loading...";
    private bool _isLoading = false;

    private readonly ListPDSTablesService _listTables;

    public UtilitiesFormsHome(AppHome appHome)
    {
        InitializeComponent();

        this.appHome = appHome;

        _listTables = new ListPDSTablesService(this);

        // Get User Settings -- START

        MicroPathTxt.Text = Consts.UserSettings.MicroServicePath;

        //MicroPathTxt.Enabled = false;
        ActiveControl = Input;

        SetDirectoryDialog();

        // user must click edit button to edit the path

        // Get User Settings -- END

        dbName = "ODSASIS";

        DeleteBtnTip.SetToolTip(DeleteBtn, "Enter the index or the micro name to delete it from the above list");
        ClearBtnTip.SetToolTip(ClearBtn, "Delete all services from the list");

        DeleteBtnTip.InitialDelay = 100;
        ClearBtnTip.InitialDelay = 100;

        if (string.IsNullOrWhiteSpace(Input.Text))
        {
            AddBtn.Text = "Browse...";
        }
        else
        {
            AddBtn.Text = "Add";
        }

        //Input.Text = @"C:\Users\al_ya\source\repos\DevAssistant\Dev.Assistant.App\Assets\Nic.Apis.Bayan\BusinessLayer\Person\MicroGetWeaponsLicenseInfo.cs";
    }

    private void SetDirectoryDialog()
    {
        if (string.IsNullOrWhiteSpace(MicroPathTxt.Text) || !Directory.Exists(MicroPathTxt.Text))
        {
            using var dialog = new EnterMicroPath();
            var result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                MicroPathTxt.Text = dialog.microPath;

                Consts.UserSettings.MicroServicePath = dialog.microPath;
            }
        }

        if (!Directory.Exists(MicroPathTxt.Text))
        {
            MicroPathLabel.ForeColor = Color.Red;
            MicroPathTxt.Enabled = true;
            ActiveControl = MicroPathTxt;
            //EditSaveBtn.Text = "Save";

            appHome.LogError("Invalid path. Please enter your MicroServiceCore path");
        }
    }

    private void GetValueBtn_Click(object sender, EventArgs e)
    {
        appHome.LogEvent(DevEvents.UGetTableNames, EventStatus.Clicked);

        if (_isLoading)
            return;

        _isLoading = true;
        GetValueBtn.Text = _loadingLabel;

        // clear the table
        Clear();

        try
        {
            if (string.IsNullOrWhiteSpace(Input.Text) && servicesNames.Count == 0)
            {
                ServiceNameErrMeg.Text = "Please enter service name or service path";

                throw new DevAssistantException("Please enter service name or service path", 7304);
            }
            else if (!string.IsNullOrWhiteSpace(Input.Text))
            {
                if (servicesNames.Count > 0)
                {
                    ServiceNameErrMeg.Text = "Please add it to the list or clear the list";

                    throw new DevAssistantException("Please add it to the list or clear the list", 7305);
                }
            }

            if (!MicroProjectRadBtn.Checked)
            {
                //var serviceInfo = FileService.GetServiceInfo(Input.Text);

                //if (string.IsNullOrWhiteSpace(serviceInfo.ContractCode))
                //{
                //    if (servicesNames.Count == 1)
                //    {
                //        serviceInfo = FileService.GetServiceInfo(servicesNames[0]);
                //    }
                //}

                //if (!string.IsNullOrWhiteSpace(serviceInfo.ContractCode))
                //{
                //    appHome.LogSuccess($"Gotten the contract name form the path. Contract name: {serviceInfo.ContractCode}");

                //    ContractNameInput.Text = $"{_prefixText}{serviceInfo.ContractCode}";
                //}
                //else
                //{
                //    //ContractNameInput.Text = string.Empty;
                //}

                if (string.IsNullOrWhiteSpace(ContractNameInput.Text) || ContractNameInput.Text.Equals(_prefixText))
                {
                    ContractNameErrMeg.Text = "Please enter the contract username";

                    ActiveControl = ContractNameInput;

                    throw new DevAssistantException("Please enter the contract username", 7306);
                }
            }

            GetTables();

            appHome.LogEvent(DevEvents.UGetTableNames, EventStatus.Succeed);
        }
        catch (DevAssistantException ex)
        {
            appHome.LogError(ex, DevEvents.UGetTableNames, EventStatus.Failed);
        }
        catch (Exception ex)
        {
            appHome.LogError(new DevAssistantException(ex.Message, 7307));
            appHome.LogEvent(DevEvents.UGetTableNames, EventStatus.Failed, ex: ex);
        }

        _isLoading = false;
        GetValueBtn.Text = _getTablesLabel;
    }

    private void GetTables()
    {
        Log.Logger.Information("Get Service Names Based on User Inputs (BL, Controller, etc..) -- START");

        if (Input.Text.ToLower().Contains("allmicro"))
        {
            Log.Logger.Information("Get PDS tables for All Micro");

            var folders = Directory.GetDirectories(Consts.UserSettings.MicroServicePath);

            foreach (var folder in folders)
            {
                int index = folder.LastIndexOf("\\");
                var folderName = folder[(index + 1)..]; // may MicroName

                if (!folderName.StartsWith("Micro") || folderName.Contains('.') || folderName.Contains('$') || ValidationService.IsExcludedMicro(folderName))
                {
                    continue;
                }

                AddService(folderName);
            }
        }

        string path = Input.Text;

        if (path.Contains(@"C:\"))
        {
            path = path.Replace(@"\", @"/");
        }

        if (path.Contains("Models"))
        {
            path = path.Replace("Models", "BusinessLayer");
        }

        try
        {
            if (!string.IsNullOrWhiteSpace(ContractNameInput.Text) && ContractNameInput.Text.Equals(_prefixText))
            {
                contractName = "<ContractName>";
            }
            else
            {
                // remove the prefix "ws_"
                contractName = ContractNameInput.Text[3..];
            }

            Log.Logger.Information("Get Micros Names From BL File -- START");
            /******  Get Micros Names From BL File -- START  ******/

            if (path.Contains("BusinessLayer"))
            {
                //appHome.LogInfo("This is a BusinessLayer file. Getting Micros & Root names from the file....");
                Log.Logger.Information("Get PDS tables from BusinessLayer");

                var micros = ModelExtractionService.GetMicroNamesFromBL(path);

                if (micros.Count > 0)
                {
                    appHome.LogInfo($"{micros.Count} micro(s) or root service(s) found in the BusinessLayer file. Added to the above list");

                    foreach (var micro in micros)
                    {
                        AddService(micro);
                    }

                    // in case BL has sql
                    AddService(path);
                }
                else
                {
                    appHome.LogInfo($"No micros found in {path}");
                }
            }
            else if (path.EndsWith("Controller.cs"))
            {
                PrepareServicesFromController(path);
            }

            if (servicesNames.Count > 0)
            {
                Log.Logger.Information("Get PDS tables based on services that added to the list. Count {a}", servicesNames.Count);

                // if path null we must also check if there is any controller in the list
                var newServicesNames = new List<string>();
                newServicesNames.AddRange(servicesNames);

                ClearList(false);

                foreach (var msName in newServicesNames)
                {
                    if (msName.EndsWith("Controller.cs"))
                    {
                        PrepareServicesFromController(msName);
                    }
                    else
                    {
                        AddService(msName);
                    }
                }

                List<string> micros = new();

                foreach (var msName in servicesNames)
                {
                    string tempName = msName.Trim();

                    if (tempName.Contains(@"\Models\"))
                    {
                        tempName = tempName.Replace(@"\Models\", @"\BusinessLayer\");
                    }

                    if (tempName.Contains("\\BusinessLayer") || tempName.Contains("/BusinessLayer"))
                    {
                        //appHome.LogInfo($"The following path is a BusinessLayer file {tempName}");
                        //appHome.LogInfo("Getting Micros names from the file....");

                        var tempMicros = ModelExtractionService.GetMicroNamesFromBL(tempName);

                        if (tempMicros.Count > 0)
                        {
                            appHome.LogInfo($"{tempMicros.Count} micro(s) or root service(s) found in the BusinessLayer file. Added to the above list");

                            micros.AddRange(tempMicros);
                        }
                        else
                        {
                            appHome.LogInfo("No micros found");
                        }
                    }
                }

                foreach (var micro in micros)
                {
                    AddService(micro);
                }
            }

            /******  Get Micros Names From BL File -- END  ******/

            Log.Logger.Information("Get Service Names Based on User Inputs (BL, Controller, etc..) -- END -- Num of Services {a}", servicesNames.Count);

            Log.Logger.Information("Get PDS tables...");

            if (servicesNames.Count > 1) // more than one serivce
            {
                var tempServices = new List<PdsTableOfService>();

                foreach (var msName in servicesNames)
                {
                    string tempName = msName.Trim();

                    if (tempName.Contains(@"\Models\"))
                    {
                        tempName = tempName.Replace(@"\Models\", @"\BusinessLayer\");
                    }

                    var tempService = _listTables.ListTablesService(tempName);

                    tempServices.Add(tempService);
                }

                // Convert tempServices (list of PdsTableOfServices) to one object PdsTableOfService
                var serviceInfo = new PdsTableOfService
                {
                    Name = "MultiServices",
                    Tables = new List<string>()
                };

                foreach (var service in tempServices)
                {
                    if (string.IsNullOrWhiteSpace(service.Name))
                        continue;

                    serviceInfo.Tables.Add($"{service.Name} -- START"); // will not copied to Clipboard.

                    serviceInfo.Tables.AddRange(service.Tables);

                    serviceInfo.Tables.Add($"{service.Name} -- END"); // will not copied
                }

                if (serviceInfo.Tables != null && serviceInfo.Tables.Count > 0)
                {
                    appHome.LogEvent(DevEvents.UGetTableNames, EventStatus.Called, $"[{serviceInfo.Tables.Count} Table(s) Found] {ContractNameInput.Text}: {Input.Text}");

                    ShowTableNamesResult(serviceInfo);
                }
            }
            else // one service mode
            {
                if (servicesNames.Count == 1)
                    path = servicesNames[0];

                var serviceInfo = _listTables.ListTablesService(path);

                serviceInfo.Tables.Insert(0, $"{serviceInfo.Name} -- START");
                serviceInfo.Tables.Add($"{serviceInfo.Name} -- END");

                if (serviceInfo.Tables != null && serviceInfo.Tables.Count > 0)
                {
                    appHome.LogEvent(DevEvents.UGetTableNames, EventStatus.Called, $"[{serviceInfo.Tables.Count} Table(s) Found] {ContractNameInput.Text}: {Input.Text}");

                    ShowTableNamesResult(serviceInfo);
                }
            }
        }
        catch (DevAssistantException)
        {
            // clear the table
            Clear();
            //appHome.LogError(ex);
            throw;
        }
        catch (Exception)
        {
            // clear the table
            Clear();

            //appHome.LogError(new DevAssistantException(ex.Message, 7300));
            throw;
        }
    }

    private void PrepareServicesFromController(string path)
    {
        Log.Logger.Information("Get PDS tables for a Controller");

        var serviceInfo = FileService.GetServiceInfo(path);
        string contractPath = Path.Combine(path[..path.LastIndexOf("Nic.Apis")], serviceInfo.Namespace);

        if (serviceInfo.Namespace.IsNamespaceNpv())
        {
            Log.Logger.Information("Npv Controller: {a}", serviceInfo.ContractCode);

            //appHome.LogInfo($"Generate IGs for {serviceInfo.ContractCode}...");

            string[] controllers = Directory.GetDirectories(Path.Combine(contractPath, "Models"));

            foreach (var folder in controllers)
            {
                foreach (var file in Directory.GetFiles(folder, "*.cs"))
                {
                    AddService(file);
                }
            }
        }
        else
        {
            Log.Logger.Information("API Controller: {a}", serviceInfo.ControllerName);

            var controllerFilesPath = Path.Combine(contractPath, "Models", serviceInfo.ControllerName);

            foreach (var file in Directory.GetFiles(controllerFilesPath, "*.cs"))
            {
                AddService(file);
            }
        }
    }

    private void ShowTableNamesResult(PdsTableOfService serviceInfo)
    {
        // Add Option if there is -- START
        appHome.LogTrace("Check Options -- START");

        if (IncludeDisplayIrregularCheck.Checked && !MicroProjectRadBtn.Checked)
        {
            appHome.LogInfo("Include DisplayIrregular tables (for ID && OperatorID) is checked! Adding DisplayIrregular Tables");

            serviceInfo.Tables.Add("Include DisplayIrregular tables -- START"); // will not copied to Clipboard.

            //if (!serviceInfo.Tables.Contains("IFR700_PERSON"))
            serviceInfo.Tables.Add("IFR700_PERSON");

            //if (!serviceInfo.Tables.Contains("IFR701_C_PERSON_XT"))
            serviceInfo.Tables.Add("IFR701_C_PERSON_XT");

            //if (!serviceInfo.Tables.Contains("SRR220_LK_SEC_LOCA"))
            serviceInfo.Tables.Add("SRR220_LK_SEC_LOCA");

            //if (!serviceInfo.Tables.Contains("IFR702_A_PERSON_XT"))
            serviceInfo.Tables.Add("IFR702_A_PERSON_XT");

            //if (!serviceInfo.Tables.Contains("IFR703_V_PERSON_XT"))
            serviceInfo.Tables.Add("IFR703_V_PERSON_XT");

            //if (!serviceInfo.Tables.Contains("IFR706_P_PERSON_XT"))
            serviceInfo.Tables.Add("IFR706_P_PERSON_XT");

            serviceInfo.Tables.Add("Include DisplayIrregular tables -- END"); // will not copied to Clipboard.
        }

        if (BirthDateCheck.Checked && !MicroProjectRadBtn.Checked)
        {
            appHome.LogInfo("Include BirthDate validation tables  is checked! Adding BirthDate Tables");

            serviceInfo.Tables.Add("Include BirthDate validation tables -- START"); // will not copied to Clipboard.

            //if (!serviceInfo.Tables.Contains("IFR700_PERSON"))
            serviceInfo.Tables.Add("IFR700_PERSON");

            //if (!serviceInfo.Tables.Contains("IFR701_C_PERSON_XT"))
            serviceInfo.Tables.Add("IFR701_C_PERSON_XT");

            serviceInfo.Tables.Add("Include BirthDate validation tables -- END"); // will not copied to Clipboard.
        }

        if (ServiceAccessCheck.Checked && !MicroProjectRadBtn.Checked)
        {
            appHome.LogInfo("Include MicroListPersonServiceAccessRecords tables is checked! Adding ServiceAccessRecords Tables");

            serviceInfo.Tables.Add("Include MicroListPersonServiceAccessRecords tables -- START"); // will not copied to Clipboard.

            var accessRecordsTables = _listTables.ListTablesService("MicroListPersonServiceAccessRecords");

            serviceInfo.Tables.AddRange(accessRecordsTables.Tables);

            serviceInfo.Tables.Add("Include MicroListPersonServiceAccessRecords tables -- END"); // will not copied to Clipboard.

            //if (!serviceInfo.Tables.Contains("IFR700_PERSON"))
            //    serviceInfo.Tables.Add("IFR700_PERSON");

            //if (!serviceInfo.Tables.Contains("IFR701_C_PERSON_XT"))
            //    serviceInfo.Tables.Add("IFR701_C_PERSON_XT");

            //if (!serviceInfo.Tables.Contains("IFR702_A_PERSON_XT"))
            //    serviceInfo.Tables.Add("IFR702_A_PERSON_XT");

            //if (!serviceInfo.Tables.Contains("IFR703_V_PERSON_XT"))
            //    serviceInfo.Tables.Add("IFR703_V_PERSON_XT");

            //if (!serviceInfo.Tables.Contains("IFR706_P_PERSON_XT"))
            //    serviceInfo.Tables.Add("IFR706_P_PERSON_XT");

            //if (!serviceInfo.Tables.Contains("PT001_PUBLIC_USER"))
            //    serviceInfo.Tables.Add("PT001_PUBLIC_USER");

            //if (!serviceInfo.Tables.Contains("PT002_PUB_SECTOR"))
            //    serviceInfo.Tables.Add("PT002_PUB_SECTOR");

            //if (!serviceInfo.Tables.Contains("PT007_MOI_AUTH_P_U"))
            //    serviceInfo.Tables.Add("PT007_MOI_AUTH_P_U");

            //if (!serviceInfo.Tables.Contains("PT010_EXTRN_SERVIC"))
            //    serviceInfo.Tables.Add("PT010_EXTRN_SERVIC");

            //if (!serviceInfo.Tables.Contains("PT011_AUTH_EXN_SRV"))
            //    serviceInfo.Tables.Add("PT011_AUTH_EXN_SRV");

            //if (!serviceInfo.Tables.Contains("SRR220_LK_SEC_LOCA"))
            //    serviceInfo.Tables.Add("SRR220_LK_SEC_LOCA");
        }

        StringBuilder copiedText = new();

        if (AddCommentAuthBox.Checked && !MicroProjectRadBtn.Checked)
        {
            appHome.LogInfo("Add comment \"In AuthServer: Create a new user...\" is checked! Adding the comments");

            copiedText = GetInAuthServerComment();

            copiedText.AppendLine("");
            copiedText.AppendLine("==================================================");
            copiedText.AppendLine("");
        }

        appHome.LogTrace("Check Options -- END");
        // Add Option -- END

        ShowResult(serviceInfo);

        serviceInfo.Tables.Sort();

        // Copy -- START
        HashSet<string> tables = new(); // Get only tables, no duplicates

        var numServices = 0;

        foreach (var name in serviceInfo.Tables)
        {
            if (!name.Contains("-- START") && !name.Contains("-- END")) // will not copied if contains
                tables.Add(name);
            else if (!name.StartsWith("Include "))
                numServices++;
        }

        if (numServices == 0) // in one service mode
            numServices = 2;

        NumServicesMsg.Text = $"{numServices / 2} service(s) and {tables.Count} table(s)";

        //                                     //        ODSASIS          //              OLSDB (Absher)
        string dbNames = dbName == "ODSASIS" ? "ODSASIS and ODSASIS_VS" : "ODSASIS, ODSASIS_VS, OLSDB and OLSDB_VS";

        copiedText.AppendLine($"The following tables are missing in PDS ({dbNames}):");
        copiedText.AppendLine("");
        copiedText.AppendLine("Tables:");
        //copiedText.Append("  - "); // for the first table
        copiedText.AppendLine(string.Join(Environment.NewLine, tables)); // to list all tables as bellow:
        copiedText.AppendLine("");
        // Ex:
        // Tables:
        // - Table1
        // - Table2

        copiedText.AppendLine("");
        copiedText.AppendLine($"Please grant SELECT permission to the following PDS users on PDS ({dbNames}):");
        copiedText.AppendLine("");
        copiedText.AppendLine("- Integration users:");
        copiedText.AppendLine("MOIAPP01qa");
        copiedText.AppendLine("ODSAPP01qa");

        if (!MicroProjectRadBtn.Checked)
            copiedText.AppendLine($"ws_{contractName}");

        copiedText.AppendLine("");
        copiedText.AppendLine("- Production users:");

        if (MicroProjectRadBtn.Checked)
        {
            copiedText.AppendLine("MOIAPP01");
            copiedText.AppendLine("GWSAPP01");
        }
        else
        {
            copiedText.AppendLine($"ws_{contractName}");
        }

        copiedText.AppendLine("");

        Clipboard.SetText(copiedText.ToString());
        NoteLabel.ForeColor = Color.Green;

        appHome.LogSuccess("Copied Successfully!");
        // Copy -- END
    }

    private void ShowResult(PdsTableOfService serviceInfo)
    {
        // Table -- START
        ResultGridView.Columns.Add("", "Result");

        ResultGridView.Rows.Add(serviceInfo.Name);

        foreach (string tbName in serviceInfo.Tables)
        {
            ResultGridView.Rows.Add(tbName);
        }

        var fontSize = 9;

        DataGridViewCellStyle style = new();
        Font font = new("Calibri Light", fontSize, FontStyle.Bold);
        style.Font = font;

        ResultGridView.Columns[0].DefaultCellStyle = style;

        ResultGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;

        ResultGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        // Table -- END
    }

    private void OLSDB_CheckedChanged(object sender, EventArgs e)
    {
        if (OLSDB.Checked)
            dbName = "OLSDB";
    }

    private void ODSASIS_CheckedChanged(object sender, EventArgs e)
    {
        if (ODSASIS.Checked)
            dbName = "ODSASIS";
    }

    private void AddBtn_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Input.Text))
        {
            //appHome.LogWarning($"Please enter a valid value");
            //return;

            Input.Text = FileService.FolderPickerDialog(title: "Select the file path:", initialDirectory: Consts.UserSettings.UtilitiesServicePath, isFilePicker: true);

            if (!string.IsNullOrWhiteSpace(Input.Text))
            {
                AddBtn.Text = "Add";
                Consts.UserSettings.UtilitiesServicePath = Input.Text;
            }
        }

        //              .cs
        if (Input.Text.Contains('.') && !File.Exists(Input.Text))
        {
            appHome.LogError($"The given path \"{Input.Text}\" is not existing on disk");
            return;
        }
        else if (!Input.Text.Contains('.') && !Directory.Exists(Input.Text))
        {
            if (!Directory.Exists(Path.Combine(MicroPathTxt.Text, Input.Text)))
            {
                appHome.LogError($"The given path \"{Input.Text}\" is not existing on disk");

                throw new DevAssistantException($"The given path \"{Input.Text}\" is not existing on disk", 9999);
                return;
            }
        }

        AddService(Input.Text.Trim());

        if (servicesNames.Count == 1)
        {
            SetContractUsernameFormPath();
        }
    }

    private void AddBtn_Click1(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Input.Text))
        {
            //appHome.LogWarning($"Please enter a valid value");
            //return;

            Input.Text = FileService.FolderPickerDialog(title: "Select the file path:", initialDirectory: Consts.UserSettings.UtilitiesServicePath, isFilePicker: true);

            if (!string.IsNullOrWhiteSpace(Input.Text))
            {
                AddBtn.Text = "Add";
                Consts.UserSettings.UtilitiesServicePath = Input.Text;
            }
        }

        //              .cs
        if (Input.Text.Contains('.') && !File.Exists(Input.Text))
        {
            appHome.LogError($"The given path \"{Input.Text}\" is not existing on disk");
            return;
        }
        else if (!Input.Text.Contains('.') && !Directory.Exists(Input.Text))
        {
            appHome.LogError($"The given path \"{Input.Text}\" is not existing on disk");
            return;
        }

        if (!string.IsNullOrWhiteSpace(Input.Text))
        {
            AddService(Input.Text.Trim());
        }
    }

    private void AddService(string serviceName)
    {
        if (!servicesNames.Exists(sev => sev.ToLower() == serviceName.ToLower()))
        {
            servicesNames.Add(serviceName);

            MSNames.Text += $"{servicesNames.Count}- {serviceName}";

            MSNames.Text += Environment.NewLine;

            Input.Text = null;
            ActiveControl = Input;

            //appHome.LogSuccess($"{(serviceName.Length > 120 ? $"{serviceName[..120]}..." : serviceName)} has been added successfully!");
        }
        else
        {
            //appHome.LogError($"{serviceName} is already added to the list");
        }
    }

    private void DeleteBtn_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Input.Text))
        {
            appHome.LogWarning($"Please enter a valid value");
            return;
        }

        var serviceName = Input.Text.Trim();

        if (!string.IsNullOrWhiteSpace(Input.Text))
        {
            if (int.TryParse(serviceName, out int index))
            {
                if (index > 0 && index <= servicesNames.Count)
                {
                    serviceName = servicesNames[index - 1];

                    servicesNames.RemoveAt(index - 1);
                }
                else
                {
                    appHome.LogError($"Index: {index} not found");
                    ActiveControl = Input;
                    return;
                }
            }
            else if (servicesNames.Exists(sev => sev.ToLower() == serviceName.ToLower()))
            {
                servicesNames.RemoveAll(sev => sev.ToLower() == serviceName.ToLower());
            }
            else
            {
                appHome.LogError($"{serviceName} is not in the list");
                ActiveControl = Input;
                return;
            }

            appHome.LogSuccess($"{serviceName} deleted successfully!");
            Input.Text = null;
            ActiveControl = Input;

            MSNames.Clear();

            for (int i = 0; i < servicesNames.Count; i++)
            {
                MSNames.Text += $"{i + 1}- {servicesNames[i]}";

                MSNames.Text += Environment.NewLine;
            }
        }
    }

    private void ClearBtn_Click(object sender, EventArgs e)
    {
        ClearList();
    }

    private void ClearList(bool enableLog = true)
    {
        if (servicesNames.Count == 0)
        {
            if (enableLog)
                appHome.LogWarning("The List is empty!");

            return;
        }

        servicesNames.Clear();
        MSNames.Clear();

        if (enableLog)
            appHome.LogSuccess("Cleaned Successfully!");
    }

    private void Clear()
    {
        ServiceNameErrMeg.Text = string.Empty;
        ContractNameErrMeg.Text = string.Empty;

        ResultGridView.Rows.Clear();
        ResultGridView.Columns.Clear();
        ResultGridView.Refresh();
        NumServicesMsg.Text = string.Empty;
    }

    private void EditSaveBtn_Click(object sender, EventArgs e)
    {
        if (MicroPathTxt.Enabled == false)
        {
            MicroPathTxt.Enabled = true;
            ActiveControl = MicroPathTxt;
            //EditSaveBtn.Text = "Save";
        }
        else
        {
            if (Directory.Exists(MicroPathTxt.Text))
            {
                MicroPathTxt.Enabled = false;
                //EditSaveBtn.Text = "Edit";

                Consts.UserSettings.MicroServicePath = MicroPathTxt.Text;
                //Directory.SetCurrentDirectory(MicroPathTxt.Text);

                ActiveControl = Input;

                MicroPathLabel.ForeColor = Color.Black;
                appHome.LogSuccess("The given path is saved successfully!: " + MicroPathTxt.Text);
            }
            else
            {
                appHome.LogError("The given path is not existing on disk: " + MicroPathTxt.Text);
            }
        }
    }

    public Property AskUserToSelectProp(List<Property> properties)
    {
        using var dialog = new SelectProp(properties);
        var result = dialog.ShowDialog();

        if (result == DialogResult.OK)
        {
            return dialog.selectedProp;
        }

        return null;
    }

    private void GetIDTypesBtn_Click(object sender, EventArgs e)
    {
        if (_isLoading)
            return;

        _isLoading = true;
        GetIDTypesBtn.Text = _loadingLabel;
        GetIDTypesBtn.Font = new Font(GetIDTypesBtn.Font.FontFamily, 12);

        ServiceNameErrMeg.Text = string.Empty;

        Clear();

        Event ev = null;

        try
        {
            // for getting only auth comment
            if (AddCommentAuthBox.Checked)
            {
                ev = DevEvents.UGetAuthComment;

                appHome.LogEvent(ev, EventStatus.Clicked);

                if (string.IsNullOrWhiteSpace(ContractNameInput.Text) || ContractNameInput.Text.Equals(_prefixText))
                {
                    ContractNameErrMeg.Text = "Please enter the contract username";

                    throw new DevAssistantException("Please enter the contract username", 7302);
                }

                appHome.LogTrace("Copying comment \"In AuthServer: Create a new user...\"!");

                Clipboard.SetText(GetInAuthServerComment().ToString());

                appHome.LogSuccess("Copied Successfully!");
            }
            else
            {
                ev = DevEvents.UGetIDTypes;

                appHome.LogEvent(ev, EventStatus.Clicked);

                if (string.IsNullOrWhiteSpace(Input.Text))
                {
                    ServiceNameErrMeg.Text = "Please enter MicroSerivce name";

                    throw new DevAssistantException("Please enter MicroSerivce name", 7303);
                }

                var serviceInfo = _listTables.ListTablesService(Input.Text, new ListTablesServiceOptions { OnlyIdTypes = true });

                if (serviceInfo.Tables != null && serviceInfo.Tables.Count > 0)
                {
                    ShowResult(serviceInfo);
                }
            }

            appHome.LogEvent(ev, EventStatus.Succeed);
        }
        catch (DevAssistantException ex)
        {
            appHome.LogError(ex, ev, EventStatus.Failed);
        }
        catch (Exception ex)
        {
            appHome.LogEvent(ev, EventStatus.Failed, ex: ex);
            appHome.LogError(new DevAssistantException(ex.Message, 7301));
        }

        if (AddCommentAuthBox.Checked)
        {
            GetIDTypesBtn.Text = _getCommentLabel;
            GetIDTypesBtn.Font = new Font(GetIDTypesBtn.Font.FontFamily, 8);
        }
        else
        {
            GetIDTypesBtn.Text = "Get ID Types";
            GetIDTypesBtn.Font = new Font(GetIDTypesBtn.Font.FontFamily, 12);
        }

        _isLoading = false;
    }

    private StringBuilder GetInAuthServerComment()
    {
        string contractName = ContractNameInput.Text[3..];

        appHome.LogTrace("Calling AuthServer to get contract Name and NameTr -- START");

        UserReturnModel contractUser = null;
        try
        {
            appHome.LogInfo("Calling AuthServer. Please wait....");

            contractUser = AuthService.GetAccountInfo($"Nic_{contractName}");
            contractUser.ServiceContract = contractUser.UserName[(contractUser.UserName.IndexOf("_") + 1)..];

            appHome.LogSuccess($"The user found. \"{contractUser.NameTr}\"");
        }
        catch (Exception ex)
        {
            appHome.LogError("An exception has occurred!");

            if (ex.InnerException is ApiException<string> apiException && apiException.Result is not null)
            {
                appHome.LogError(apiException.Result + $". Nic_{contractName}");
            }

            contractUser = new()
            {
                Email = $"{contractName}@{contractName}.com",
                UserName = $"Nic_{contractName}",
                Name = "<Please provide Arabic name>",
                NameTr = "<Please provide Enghish name>",
                ServiceContract = contractName
            };
        }

        appHome.LogTrace("Calling AuthServer to get contract Name and NameTr -- END");

        appHome.LogInfo($"Contarct name: {contractUser.ServiceContract}. Username: {contractUser.UserName}");

        StringBuilder copiedText = new();

        copiedText.AppendLine("In AuthServer :");
        copiedText.AppendLine("");
        copiedText.AppendLine($"Create a new user {contractUser.UserName}:");
        copiedText.AppendLine("{");
        copiedText.AppendLine($"    \"Email\": \"{contractUser.Email}\",");
        copiedText.AppendLine($"    \"Username\": \"{contractUser.UserName}\",");
        copiedText.AppendLine($"    \"Name\": \"{contractUser.Name}\",");
        copiedText.AppendLine($"    \"NameTr\": \"{contractUser.NameTr}\",");
        copiedText.AppendLine("    \"Password\": \"Aa@123456\",");
        copiedText.AppendLine("    \"ConfirmPassword\": \"Aa@123456\",");
        copiedText.AppendLine("    \"OperatorID\": ***,");
        copiedText.AppendLine($"    \"ServiceContract\": \"{contractUser.ServiceContract}\"");
        copiedText.AppendLine("}");

        copiedText.AppendLine($", and create a new role {contractUser.ServiceContract}:");
        copiedText.AppendLine("{");
        copiedText.AppendLine($"    \"Name\": \"{contractUser.ServiceContract}\",");
        copiedText.AppendLine("    \"OperatorID\": ***");
        copiedText.AppendLine("}");

        if (contractUser.UserName.EndsWith("Npv") || contractUser.Roles.ToList().Exists(r => r.ToLower().Equals("npv")))
        {
            copiedText.AppendLine($", and assign 2 roles for {contractUser.UserName} user:");
            copiedText.AppendLine($"Role 1:");
            copiedText.AppendLine("{");
            copiedText.AppendLine($"    \"Username\": \"{contractUser.UserName}\",");
            copiedText.AppendLine($"    \"RoleToAssign\": \"{contractUser.ServiceContract}\",");
            copiedText.AppendLine("    \"OperatorID\": ***");
            copiedText.AppendLine("}");

            copiedText.AppendLine($"Role 2:");
            copiedText.AppendLine("{");
            copiedText.AppendLine($"    \"Username\": \"{contractUser.UserName}\",");
            copiedText.AppendLine($"    \"RoleToAssign\": \"Npv\",");
            copiedText.AppendLine("    \"OperatorID\": ***");
            copiedText.AppendLine("}");
        }
        else
        {
            copiedText.AppendLine(", and assign the user to the following role:");
            copiedText.AppendLine("{");
            copiedText.AppendLine($"    \"Username\": \"{contractUser.UserName}\",");
            copiedText.AppendLine($"    \"RoleToAssign\": \"{contractUser.ServiceContract}\",");
            copiedText.AppendLine("    \"OperatorID\": ***");
            copiedText.AppendLine("}");
        }

        if (contractUser.UserName.EndsWith("Npv"))
        {
            copiedText.AppendLine(", and add the contract PDS connection to DbConfig:");
            copiedText.AppendLine("{");
            copiedText.AppendLine("    \"AppName\": \"Npv\",");
            copiedText.AppendLine($"    \"Key\": \"ContractOptions:{contractUser.ServiceContract.Replace("Npv", "")}:ConnectionStrings:PdsConnectionString\",");
            copiedText.AppendLine("    \"Value\": \"Server=****;Database=ODSASIS;User ID=***;Password=***;Connect Timeout=30\",");
            copiedText.AppendLine("    \"IsProtected\": true,");
            copiedText.AppendLine("    \"OperatorID\": ***");
            copiedText.AppendLine("}");
        }
        else
        {
            copiedText.AppendLine(", and add the contract PDS connection to DbConfig:");
            copiedText.AppendLine("{");
            copiedText.AppendLine($"    \"AppName\": \"{contractUser.ServiceContract}\",");
            copiedText.AppendLine("    \"Key\": \"ContractOptions:ConnectionStrings:PdsConnectionString\",");
            copiedText.AppendLine("    \"Value\": \"Server=****;Database=ODSASIS;User ID=***;Password=***;Connect Timeout=30\",");
            copiedText.AppendLine("    \"IsProtected\": true,");
            copiedText.AppendLine("    \"OperatorID\": ***");
            copiedText.AppendLine("}");
        }

        // Add ContractOptions key for Npv contract
        if (contractUser.UserName.EndsWith("Npv"))
        {
            copiedText.AppendLine("");
            copiedText.AppendLine("==================================================");
            copiedText.AppendLine("");
            copiedText.AppendLine("Please add the following keys in Npv's appsettings.json under ContractOptions:");
            copiedText.Append($"\"{contractUser.ServiceContract.Replace("Npv", "")}\": ");
            copiedText.AppendLine("{");
            copiedText.AppendLine("    \"LocationID\": <value>,");
            copiedText.AppendLine("    \"BusinessOwnerCode\": <value>");
            copiedText.AppendLine("}");
            copiedText.AppendLine("");
            copiedText.AppendLine("Please note that values can be different in Production environment ");
        }

        return copiedText;
    }

    private void ContractNameInput_TextChanged(object sender, EventArgs e)
    {
        if (ContractNameInput.Text.Contains(' '))
        {
            ContractNameInput.Text = ContractNameInput.Text.Replace(" ", "");
            ContractNameInput.SelectionStart = ContractNameInput.Text.Length;

            ContractNameErrMeg.Text = "Space not allowed!";
            //appHome.LogError("Space not allowed!");
            return;
        }

        if (!string.IsNullOrWhiteSpace(ContractNameInput.Text))
            ContractNameErrMeg.Text = string.Empty;

        if (!ContractNameInput.Text.StartsWith(_prefixText))
        {
            ContractNameInput.Text = _prefixText;
            ContractNameInput.SelectionStart = ContractNameInput.Text.Length;
        }
        else if (ContractNameInput.Text.Equals($"{_prefixText}Twk", StringComparison.InvariantCultureIgnoreCase))
        {
            ContractNameInput.Text = $"{_prefixText}EstishrafBeResponsible";
            ContractNameInput.SelectionStart = ContractNameInput.Text.Length;

            appHome.LogError("The production username for Twk contract is ws_EstishrafBeResponsible");
        }
    }

    private void ContractNameInput_Enter(object sender, EventArgs e)
    {
        ContractNameInput.SelectionStart = ContractNameInput.Text.Length;
    }

    private void AddCommentAuthBox_CheckedChanged(object sender, EventArgs e)
    {
        if (AddCommentAuthBox.Checked)
        {
            GetIDTypesBtn.Text = _getCommentLabel;
            GetIDTypesBtn.Font = new Font(GetIDTypesBtn.Font.FontFamily, 8);
        }
        else
        {
            GetIDTypesBtn.Text = "Get ID Types";
            GetIDTypesBtn.Font = new Font(GetIDTypesBtn.Font.FontFamily, 12);
        }
    }

    private void BrowseBtn_Click(object sender, EventArgs e)
    {
        try
        {
            MicroPathTxt.Text = FileService.FolderPickerDialog(title: "Select the MicroServices path (Root folder):", initialDirectory: Consts.UserSettings.MicroServicePath);

            if (string.IsNullOrWhiteSpace(MicroPathTxt.Text))
            {
                if (string.IsNullOrWhiteSpace(Consts.UserSettings.MicroServicePath))
                {
                    MicroPathLabel.ForeColor = Color.Red;
                    ActiveControl = MicroPathTxt;

                    Consts.UserSettings.MicroServicePath = "C:\\";
                    //Directory.SetCurrentDirectory("C:\\");

                    throw new DevAssistantException("The returned path is empty or invalid. Please enter your MicroServiceCore path", 7308);
                }
                else
                {
                    MicroPathTxt.Text = Consts.UserSettings.MicroServicePath;

                    appHome.LogInfo($"The returned path is empty or invalid. Setting your previous path {Consts.UserSettings.MicroServicePath}");
                }
            }

            MicroPathLabel.ForeColor = Color.Black;

            Consts.UserSettings.MicroServicePath = MicroPathTxt.Text;
            //Directory.SetCurrentDirectory(MicroPathTxt.Text);

            ActiveControl = Input;

            appHome.LogSuccess("The given path is saved successfully!: " + MicroPathTxt.Text);
        }
        catch (DevAssistantException ex)
        {
            appHome.LogError(ex);
        }
        catch (Exception ex)
        {
            appHome.LogError(new DevAssistantException(ex.Message, 7309));
        }
    }

    private void Input_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Input.Text))
        {
            AddBtn.Text = "Browse...";
        }
        else
        {
            AddBtn.Text = "Add";

            SetContractUsernameFormPath();
        }
    }

    private void SetContractUsernameFormPath()
    {
        if (!MicroProjectRadBtn.Checked)
        {
            var serviceInfo = FileService.GetServiceInfo(Input.Text);

            if (string.IsNullOrWhiteSpace(serviceInfo.ContractCode))
            {
                if (servicesNames.Count == 1)
                {
                    serviceInfo = FileService.GetServiceInfo(servicesNames[0]);
                }
            }

            if (!string.IsNullOrWhiteSpace(serviceInfo.ContractCode) && (!contractName?.Equals(serviceInfo.ContractCode) ?? true))
            {
                var dialogInfo = new DevDialogInfo
                {
                    Title = $"We got the contract username form the path:",
                    Message = $"Do you want to continue and set contract username as: ws_{serviceInfo.ContractCode}?",
                    Buttons = MessageBoxButtons.YesNo,
                    BoxIcon = MessageBoxIcon.Question
                };

                DialogResult result = DevDialog.Show(dialogInfo);

                if (result == DialogResult.Yes)
                {
                    appHome.LogSuccess($"Gotten the contract name form the path. Contract name: {serviceInfo.ContractCode}");

                    ContractNameInput.Text = $"{_prefixText}{serviceInfo.ContractCode}";
                    contractName = serviceInfo.ContractCode;
                }
            }
        }
    }

    private void radioButton1_CheckedChanged(object sender, EventArgs e)
    {
    }

    private void MicroProjectRadBtn_CheckedChanged(object sender, EventArgs e)
    {
        if (MicroProjectRadBtn.Checked)
        {
            dbName = "OLSDB";
            ContractNameInput.Enabled = false;
            OptionsBox.Enabled = false;
        }
        else
        {
            ContractNameInput.Enabled = true;
            OptionsBox.Enabled = true;
        }
    }
}