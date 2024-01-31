using Dev.Assistant.Business.Compare.Models;
using Dev.Assistant.Business.Compare.Services;
using Dev.Assistant.Business.Converter.Models;
using Dev.Assistant.Business.Converter.Services;
using Dev.Assistant.Business.Core.Enums;
using Dev.Assistant.Business.Core.Extensions;
using Dev.Assistant.Business.Core.Models;
using Dev.Assistant.Business.Core.Services;
using Dev.Assistant.Business.Core.Services.JsonToCsharp;
using Dev.Assistant.Business.Core.Utilities;
using Dev.Assistant.Business.Decoder.Models;
using Dev.Assistant.Business.Decoder.Services;
using Dev.Assistant.Business.DevOps.Services;
using Dev.Assistant.Business.Generator.Models;
using Dev.Assistant.Business.Generator.Services;
using Dev.Assistant.Configuration;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using Serilog;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

// Error code start with 7000

namespace Dev.Assistant.App.Reviewme;

public partial class ReviewmeHome : UserControl
{
    public readonly AppHome appHome;
    private readonly LogArgs _log;

    private bool _isLoading;
    private const string _loadingLabel = "Loading...";
    private string _getOutputLabel = "Get the Result";
    private const string _prefixText = "V";

    private string _oldUserInput;


    private bool _isVbCodePrev = false;
    private bool _isCSharpCodePrev = false;


    public ReviewmeHome(AppHome appHome)
    {
        InitializeComponent();

        this.appHome = appHome;

        _log = new()
        {
            LogInfo = appHome.LogInfo,
            LogEvent = appHome.LogEvent,
            LogWarning = appHome.LogWarning,
            LogSuccess = appHome.LogSuccess,
            LogError = appHome.LogError,
            LogTrace = appHome.LogTrace,
        };

        //For Testing Only

        //PathInput.Text = @"C:\Project\API\ApiDev\Nic.Apis.Bayan\Models\Person\MicroGetWeaponsLicenseInfo.cs";
        //PathInput.Text = @"C:\Users\al_ya\source\repos\DevAssistant\Dev.Assistant.App\Assets\Nic.Apis.Bayan\Models\Person\MicroGetWeaponsLicenseInfo.cs";
        //GetPropsInfo();
        //PathInput.Text = @"C:\Project\API\Git2\Nic.Apis.Npv\Controllers\MosController.cs";//\EsbCustomsGetSuspectPersonByID.cs";// PathInput.Text.Trim();

        //if (Consts.SecretsPin.Contains(Pin.BypassCSharpStandards))
        //{
        //BypassStandardsGenIGRatBtn.Enabled = true;
        //BypassStandardsGenIGRatBtn.Visible = true;
        //}
        //else
        //{
        //    BypassStandardsGenIGRatBtn.Enabled = false;
        //    BypassStandardsGenIGRatBtn.Visible = false;
        //}

        GetOutput.Text = "Generate";
        _getOutputLabel = "Generate";

        GenerateIGOptions.Visible = true;

        WrittenByInput.Text = Consts.UserSettings.WrittenByIG;
        PathLabel.Text = "Enter Model or BusinessLayer path (or contarct path: for creating all IGs):";

        Input2Label.Visible = false;
        Input2.Visible = false;

        ShowPath();
        UserInput.Enabled = false;
        //UserInput.Size = new Size(838, 413);

        MainSplitContainer.Panel2Collapsed = true;
        SqlOptions.Visible = false;

        #region Selected Service

        int searchCriteria = Consts.UserSettings.RFeatureCriteria;

        GenerateIGRadBtn.Checked = false;           // 0, 1
        GetPropsInfoBtn.Checked = false;            // 2
        CheckRulesRadBtn.Checked = false;           // 3
        GetDbLengthBtn.Checked = false;             // 4
        GetAllMethodsBtn.Checked = false;           // 5
        GetAllSerivcesBtn.Checked = false;          // 6
        ConverterBtn.Checked = false;               // 7
        CompareTRadBtn.Checked = false;             // 8
        GetPackageReferencesBtn.Checked = false;    // 9
        GenerateNdbJsonRadBtn.Checked = false;      // 10

        switch (searchCriteria)
        {
            case 1 or 0:
                GenerateIGRadBtn.Checked = true;
                break;

            case 2:
                GetPropsInfoBtn.Checked = true;
                break;

            case 3:
                CheckRulesRadBtn.Checked = true;
                break;

            case 4:
                GetDbLengthBtn.Checked = true;
                break;

            case 5:
                GetAllMethodsBtn.Checked = true;
                break;

            case 6:
                GetAllSerivcesBtn.Checked = true;
                break;

            case 7:
                ConverterBtn.Checked = true;
                break;

            case 8:
                CompareTRadBtn.Checked = true;
                break;

            case 9:
                GetPackageReferencesBtn.Checked = true;
                break;

            case 10:
                GenerateNdbJsonRadBtn.Checked = true;
                break;
        }

        #endregion Selected Service

        // Severity	Code	Description	Project	File	Line	Suppression State
        //Error CS0121  The call is ambiguous between the following methods or properties: 'ApiCommonCode.ValidateDateFormat(int, int)' and 'ApiCommonCode.ValidateDateFormat(int, int)'    Nic.Apis.Maidan C:\Project\API\Git\Nic.Apis.Maidan\BusinessLayer\Vehicle\ListGeographicalDistributionOfAccidents.cs 141 Active

        string suffixTip = "You can add suffix after the class name to avoid duplicate classes (Error CS0121 \"The call is ambiguous...\") this happened mostly in ServiceBus services.\nIf you add V1 for example, the Attachment class will be AttachmentV1.\nKeep in mind this will be applied for all classes even request and response models.";

        AddSuffixTip.SetToolTip(AddSuffixLabel, suffixTip);
        AddSuffixTip.SetToolTip(suffixTxtBox, suffixTip);

        AddSuffixTip.InitialDelay = 50;
        AddSuffixTip.AutoPopDelay = 20000;

        CSharpConverterTip.SetToolTip(VbToCharpBtn, "You can use it to convert VB to C# or use it to add XML to your C# model");

        CSharpConverterTip.InitialDelay = 50;
        CSharpConverterTip.AutoPopDelay = 10000;
    }

    private void GetOutput_Click(object sender, EventArgs e)
    {
        Event ev = GetCurrentEvent();

        //appHome.LogInfo(Size.ToString());

        appHome.LogEvent(ev, EventStatus.Clicked);

        if (_isLoading)
            return;

        _isLoading = true;
        GetOutput.Text = _loadingLabel;

        ImageResult.Image = null;

        try
        {
            if (string.IsNullOrWhiteSpace(UserInput.Text) && string.IsNullOrWhiteSpace(PathInput.Text))
            {
                if (GetPropsInfoBtn.Checked || GetAllMethodsBtn.Checked)
                {
                    throw new DevAssistantException("Invalid value. Please enter your full code or the file path of code", 7000);
                }
                else
                {
                    throw new DevAssistantException("Invalid value. Please enter a value", 7001);
                }
            }
            else if (!string.IsNullOrWhiteSpace(UserInput.Text) && (PathInput.Visible == true && !string.IsNullOrWhiteSpace(PathInput.Text)))
            {
                if (UserInput.ReadOnly == false)
                    throw new DevAssistantException("Invalid value. Please enter your full code or the file path ONLY", 7002);
            }

            if (CompareTRadBtn.Checked)
            {
                if (!string.IsNullOrWhiteSpace(Input1) && !string.IsNullOrWhiteSpace(NewInput))
                {
                    if (IsModel.Checked)
                        CompareModel();
                    else if (IsJson.Checked)
                        CompareJson();
                    else if (IsSql.Checked)
                        CompareSql();
                    else if (IsText.Checked)
                        CompareText();
                    else
                        throw new Exception(message: "Please select comparison type.");
                }
                else
                {
                    throw new Exception(message: "All fileds are required.");
                }
            }
            else if (ConverterBtn.Checked)
            {
                if (CodeToSqlBtn.Checked)
                    CodeToSql();
                else if (SqlToCodeBtn.Checked)
                    SqlToCode();
                else if (XmlToJsonBtn.Checked)
                    XmlToJson();
                else if (JsonToXmlBtn.Checked)
                    JsonToXml();
                else if (JsonToCsharpBtn.Checked)
                    JsonToCsharp();
                //else if (VbToCSharpBtn.Checked)
                //    VbToCSharpAsync();
                else if (DecryptTokenBtn.Checked)
                    ReadJwtToken();
                else if (Base2ImageBtn.Checked)
                    ConvertBase64ToImage();
                else if (Blob2ImageBtn.Checked)
                    ConvertBlobToImage();
                else if (VbToCharpBtn.Checked)
                    ConvertVbToCharpClass();
            }
            else
            {
                if (GenerateIGRadBtn.Checked)
                {
                    if (GenApiBtn.Checked)
                        PrepareApiService(ProjectType.RestApi);
                    else if (GenMicroBtn.Checked)
                        PrepareMicroService();
                    else
                        throw new DevAssistantException("Please select the project type", 7024);
                }
                else if (GenerateNdbJsonRadBtn.Checked)
                    PrepareApiService(ProjectType.NdbJson);
                else if (GetPropsInfoBtn.Checked)
                    GetPropsInfo();
                else if (GetDbLengthBtn.Checked)
                    GetDbLength();
                else if (GetAllMethodsBtn.Checked)
                    GetAllMethods();
                else if (GetAllSerivcesBtn.Checked)
                    GetAllServices();
                //MatchMicroServicesWithBayan();
                else if (GetPackageReferencesBtn.Checked)
                    GetPackageReferences();
                else if (CheckRulesRadBtn.Checked)
                    CheckRules();
                else if (ReviewReportRadBtn.Checked)
                    ReviewReport();
                //else if (MoveCommonSettingBtn.Checked)
                //    MoveCommonSetting();
                //else if (GitCmdBtn.Checked)
                //{
                //    PrepareGitCmd();
                //}
                else
                    throw new DevAssistantException("Please select a service", 7003);
            }

            appHome.LogEvent(ev, EventStatus.Succeed);
        }
        catch (DevAssistantException error)
        {
            if (ev != null)
                appHome.LogError(error, ev, EventStatus.Failed);
            else
                appHome.LogError(error);
        }
        catch (Exception ex)
        {
            appHome.LogError(new DevAssistantException(ex.Message, 7004));
            appHome.LogEvent(ev, EventStatus.Failed, ex: ex);
        }

        _isLoading = false;
        GetOutput.Text = _getOutputLabel;
    }

    private void ConvertVbToCharpClass()
    {
        var code = GeneralConvertService.ConvertToCSharp(Input1, suffixTxtBox.Text, PrepareXmlBtn.Checked);

        Input2.Text = code;

        Clipboard.SetDataObject(code, true);

        SuccessMessage("VB class was converted to C# class successfully. Code copied successfully");
    }


    private void ReadJwtToken()
    {
        JwtSecurityTokenHandler tokenHandler = new();

        var jwtToken = tokenHandler.ReadJwtToken(UserInput.Text);

        string json = JsonConvert.SerializeObject(jwtToken, Formatting.Indented);

        Input2.Text = json;

        SuccessMessage("Token decrypted successfully. Convert token into an instance of JwtSecurityToken");
    }

    private void ConvertByteArrayToImage()
    {
        string tmp = UserInput.Text.Trim().Replace("[", string.Empty).Replace("]", string.Empty).Replace(" ", string.Empty);
        byte[] photoInList = tmp.Split(',').Select(i => byte.Parse(i)).ToArray();

        using var ms = new MemoryStream(photoInList, 0, photoInList.Length);

        //ms.Write(photoInList, 0, photoInList.Length);

        Bitmap bm = new(ms, false);

        var a = Image.FromStream(ms);

        ImageResult.Image = Image.FromStream(ms);
        //ImageResult.Size = new Size(ImageResult.Width, ImageResult.Height);
    }

    private void ConvertBase64ToImage()
    {
        //byte[] photoInList = Convert.FromBase64String("R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==");
        byte[] photoInList = Convert.FromBase64String(UserInput.Text.Trim());

        using var ms = new MemoryStream(photoInList, 0, photoInList.Length);
        ImageResult.Image = Image.FromStream(ms, true);
    }

    private void ConvertBlobToImage()
    {
        string hex = UserInput.Text.Trim().Replace("  ", string.Empty);

        var bytes = new byte[hex.Length / 2];
        for (var i = 0; i < bytes.Length; i++)
        {
            bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
        }

        using var ms = new MemoryStream(bytes, 0, bytes.Length)
        {
            Position = 0
        };

        ImageResult.Image = Image.FromStream(ms, true);
    }

    private void GetPackageReferences()
    {
        string path = PathInput.Text;

        StringBuilder sb = new();

        string[] folders = { };

        folders = Directory.GetDirectories(PathInput.Text);

        var root = Directory.GetFiles(PathInput.Text, "*", SearchOption.AllDirectories)
            .Where(f => f.Contains("Nic.Apis.Root", StringComparison.CurrentCultureIgnoreCase) && f.EndsWith(".csproj")).FirstOrDefault();
        var rootReferences = GetReferences(root);

        var common = Directory.GetFiles(PathInput.Text, "*", SearchOption.AllDirectories)
          .Where(f => f.Contains("Nic.Apis.Common", StringComparison.CurrentCultureIgnoreCase) && f.EndsWith(".csproj")).FirstOrDefault();
        var commonReferences = GetReferences(common);

        foreach (var folder in folders)
        {
            var contractCode = folder[folder.LastIndexOf("\\")..].Replace("\\", "").Trim();
            //contractCode = contractCode.Replace("Nic.Apis.", "");

            var file = Directory.GetFiles(folder, "*", SearchOption.AllDirectories).Where(f => f.EndsWith(".csproj")).FirstOrDefault();

            if (file != null)
            {
                bool duplicateFound = false;
                List<string> addedRef = new();

                if (contractCode.Equals("Nic.Apis.BackendRest"))
                {
                    XDocument projectDefinition = XDocument.Load(file);

                    var removeRefs = projectDefinition
                      .Element("Project")
                      .Elements("ItemGroup")
                      .Elements("None")
                      .Attributes("Remove")
                      .Select(refElm => refElm.Value).ToList();

                    var anyDuplicate = removeRefs.GroupBy(r => r).Where(g => g.Count() > 1).Select(a => a.Key).ToList();

                    var openApiRefs = projectDefinition
                      .Element("Project")
                      .Elements("ItemGroup")
                      .Elements("OpenApiReference")
                      .ToList();

                    var includeRefs = openApiRefs
                      .Attributes("Include")
                      .Select(refElm => refElm.Value).ToList();

                    foreach (var r in removeRefs)
                    {
                        var names = r.Split('\\');

                        var app = names[0];
                        var transName = names[1];
                        var trnasFile = names[2].Replace(".json", "");

                        if (!addedRef.Contains(r) && (anyDuplicate.Contains(r) || !includeRefs.Contains(r) ||
                            !char.IsUpper(app[0]) || char.IsUpper(app[1]) || !char.IsUpper(transName[0]) || char.IsUpper(transName[1]) || transName != trnasFile))
                        {
                            if (!duplicateFound)
                            {
                                sb.AppendLine("Nic.Apis.BackendRest:");
                                sb.AppendLine("None (Remove) References:");
                            }

                            duplicateFound = true;

                            bool errorAdded = false;

                            if (anyDuplicate.Contains(r))
                            {
                                sb.Append($"{r} [Duplicated]");
                                errorAdded = true;
                            }

                            if (!includeRefs.Contains(r))
                            {
                                if (errorAdded)
                                    sb.Append($" [No OpenApiReference Found]");
                                else
                                    sb.Append($"{Environment.NewLine}{r} [No OpenApiReference Found]");

                                errorAdded = true;
                            }

                            if (!char.IsUpper(app[0]) || char.IsUpper(app[1]) || !char.IsUpper(transName[0]) || char.IsUpper(transName[1]) || transName != trnasFile)
                            {
                                if (errorAdded)
                                    sb.Append($" [Invalid Name Must be PascalCase]");
                                else
                                    sb.Append($"{Environment.NewLine}{r} [Invalid Name Must be PascalCase]");

                                errorAdded = true;
                            }

                            addedRef.Add(r);
                        }
                    }

                    duplicateFound = false;
                    addedRef = new();

                    anyDuplicate = includeRefs.GroupBy(r => r).Where(g => g.Count() > 1).Select(a => a.Key).ToList();

                    foreach (var apiRef in openApiRefs)
                    {
                        var includeValue = apiRef.Attributes("Include").Select(refElm => refElm.Value).FirstOrDefault();
                        var namespaceValue = apiRef.Attributes("Namespace").Select(refElm => refElm.Value).FirstOrDefault();
                        var optionsValue = apiRef.Attributes("Options").Select(refElm => refElm.Value).FirstOrDefault();

                        var names = includeValue.Split('\\');

                        var app = names[0];
                        var transName = names[1];
                        var trnasFile = names[2].Replace(".json", "");

                        if (!addedRef.Contains(includeValue) && (anyDuplicate.Contains(includeValue) || !removeRefs.Contains(includeValue) ||
                            !char.IsUpper(app[0]) || char.IsUpper(app[1]) || !char.IsUpper(transName[0]) || char.IsUpper(transName[1]) || transName != trnasFile) ||
                            optionsValue == null)
                        {
                            if (!duplicateFound)
                            {
                                if (sb.ToString().Contains("Nic.Apis.BackendRest:"))
                                {
                                    sb.AppendLine("");
                                    sb.AppendLine("");
                                    sb.AppendLine("OpenApiReferences:");
                                }
                                else
                                {
                                    sb.AppendLine("Nic.Apis.BackendRest:");
                                }
                            }

                            duplicateFound = true;
                            bool errorAdded = false;

                            if (anyDuplicate.Contains(includeValue))
                            {
                                sb.Append($"{includeValue} [Duplicated]");
                                errorAdded = true;
                            }

                            if (!includeRefs.Contains(includeValue))
                            {
                                if (errorAdded)
                                    sb.Append($" [No Remove Tag Found]");
                                else
                                    sb.Append($"{Environment.NewLine}{includeValue} [No Remove Tag Found]");

                                errorAdded = true;
                            }

                            if (!char.IsUpper(app[0]) || char.IsUpper(app[1]) || !char.IsUpper(transName[0]) || char.IsUpper(transName[1]) || transName != trnasFile)
                            {
                                if (errorAdded)
                                    sb.Append($" [Invalid Name Must be PascalCase]");
                                else
                                    sb.Append($"{Environment.NewLine}{includeValue} [Invalid Name Must be PascalCase]");

                                errorAdded = true;
                            }

                            if (optionsValue == null)
                            {
                                if (errorAdded)
                                    sb.Append($" [No Options Tag Found]");
                                else
                                    sb.Append($"{Environment.NewLine}{includeValue} [No Options Tag Found]");

                                errorAdded = true;
                            }

                            addedRef.Add(includeValue);
                        }
                    }
                }
                else
                {
                    IEnumerable<string> references = GetReferences(file);

                    bool hasRootRef = HasRootReferences(file);

                    var anyDuplicate = references.GroupBy(r => r).Where(g => g.Count() > 1).Select(a => a.Key).ToList();

                    foreach (var r in references)
                    {
                        if (!r.StartsWith("Micro") || r.StartsWith("Microsoft"))
                            continue;

                        if (!addedRef.Contains(r))
                        {
                            if (anyDuplicate.Count > 0 && anyDuplicate.Contains(r))
                            {
                                if (!duplicateFound)
                                    sb.AppendLine($"{contractCode}:");

                                duplicateFound = true;

                                sb.Append($"{r} [Duplicated]");

                                if (hasRootRef && rootReferences.Contains(r))
                                {
                                    sb.Append(" [Already in Root]");
                                }

                                if (commonReferences.Contains(r))
                                {
                                    sb.Append(" [Already in Common]");
                                }

                                sb.Append(Environment.NewLine);

                                addedRef.Add(r);
                            }
                            else
                            {
                                if (hasRootRef && rootReferences.Contains(r))
                                {
                                    if (!duplicateFound)
                                        sb.AppendLine($"{contractCode}:");

                                    duplicateFound = true;

                                    sb.Append($"{r} [Already in Root]");

                                    if (commonReferences.Contains(r))
                                    {
                                        sb.Append(" [Already in Common]");
                                    }

                                    sb.Append(Environment.NewLine);

                                    addedRef.Add(r);
                                }
                                else
                                {
                                    if (commonReferences.Contains(r))
                                    {
                                        if (!duplicateFound)
                                            sb.AppendLine($"{contractCode}:");

                                        duplicateFound = true;

                                        sb.Append($"{r} [Already in Common]");
                                        sb.Append(Environment.NewLine);

                                        addedRef.Add(r);
                                    }
                                }
                            }
                        }
                    }
                }

                if (duplicateFound)
                {
                    sb.Append(Environment.NewLine);
                    sb.Append(Environment.NewLine);
                }

                //if (!duplicateFound)
                //sb.AppendLine("No duplicate references found");
            }
        }

        //StringBuilder sb = new();

        //var folders = Directory.GetDirectories(PathInput.Text);

        //foreach (var folder in folders)
        //{
        //    var contractCode = folder[folder.LastIndexOf("\\")..].Replace("\\", "").Trim();
        //    //contractCode = contractCode.Replace("Nic.Apis.", "");

        //    var files = Directory.GetFiles(folder, "*", SearchOption.AllDirectories).Where(f => f.EndsWith(".csproj")).FirstOrDefault();

        //    if (files != null)
        //    {
        //        var anyDuplicate = GetReferences(files).GroupBy(r => r).Where(g => g.Count() > 1).Select(a => a.Key).ToList();

        //        if (anyDuplicate.Any())
        //        {
        //            sb.AppendLine($"{contractCode}:");
        //            sb.AppendJoin(Environment.NewLine, anyDuplicate);

        //            sb.Append(Environment.NewLine);
        //            sb.Append(Environment.NewLine);

        //        }
        //        else
        //        {
        //            //sb.AppendLine("No duplicate references found");
        //        }

        //    }
        //}

        UserInput.Text = string.IsNullOrWhiteSpace(sb.ToString()) ? "" : sb.ToString();
    }

    private IEnumerable<string> GetReferences(string path)
    {
        XDocument projectDefinition = XDocument.Load(path);

        IEnumerable<string> references = projectDefinition
            .Element("Project")
            .Elements("ItemGroup")
            .Elements("PackageReference")
            .Attributes("Include")
            .Select(refElm => refElm.Value);

        return references;
    }

    private bool HasRootReferences(string path)
    {
        XDocument projectDefinition = XDocument.Load(path);

        var references = projectDefinition
            .Element("Project")
            .Elements("ItemGroup")
            .Elements("ProjectReference")
            .Attributes("Include")
            .Select(refElm => refElm.Value);

        foreach (var reference in references)
        {
            if (reference.EndsWith("Nic.Apis.Root.csproj"))
                return true;
        }

        return false;
    }

    private void VbToCSharpAsync()
    {
        //string fromLanguage = LanguageNames.VisualBasic;
        //string toLanguage = LanguageNames.CSharp;

        //var codeWithOptions = new CodeWithOptions(UserInput.Text)
        //    .SetFromLanguage(fromLanguage)
        //    .SetToLanguage(toLanguage);

        //var result = CodeConverter.ConvertAsync(codeWithOptions).Result;

        //Input2.Text = result.ConvertedCode;

        //Clipboard.SetDataObject(result.ConvertedCode, true);

        SuccessMessage("VB converted to C# successfully!");

        //var languages = todo.requestedConversion.Split('2');

        ////string fromLanguage = LanguageNames.CSharp;
        ////string toLanguage = LanguageNames.VisualBasic;

        ////if (languages.Length == 2)
        ////{
        ////    fromLanguage = ParseLanguage(languages[0]);
        ////    toLanguage = ParseLanguage(languages[1]);
        ////}

        //try
        //{
        //    var codeWithOptions = new CodeWithOptions(UserInput.Text)
        //  //.WithTypeReferences(DefaultReferences.NetStandard2)
        //  .SetFromLanguage(LanguageNames.CSharp)
        //  .SetToLanguage(LanguageNames.VisualBasic);

        //    var result = CodeConverter.ConvertAsync(codeWithOptions).Result;

        //    Input2.Text = result.ConvertedCode;

        //    Clipboard.SetDataObject(result.ConvertedCode, true);

        //    SuccessMessage("VB converted to C# successfully!");
        //}
        //catch (Exception)
        //{
        //    throw;
        //}
    }

    private void MoveCommonSetting()
    {
        if (!Directory.Exists(PathInput.Text))
            throw new DevAssistantException("The given path for the solution is not existing on disk.", 7014);

        if (!File.Exists(@$"\\nicdevnfs\d\Alyahya\commonsettings.json"))
            throw new DevAssistantException("Couldn't found commonsettings.json file in the server.", 7015);

        try
        {
            File.Copy(@$"\\nicdevnfs\d\Alyahya\commonsettings.json", $"{PathInput.Text}\\commonsettings.json", true);

            SuccessMessage("commonsettings.json was moved to your sln successfully!");
        }
        catch (Exception ex)
        {
            throw new DevAssistantException($"Couldn't move commonsettings.json! Details: {ex.Message}", 7016);
        }
    }

    private void XmlToJson()
    {
        var json = JsonXmlConvertService.XmlToJSON(UserInput.Text);

        Input2.Text = json;

        Clipboard.SetDataObject(json, true);

        SuccessMessage("Xml converted to Json successfully!");
    }

    private void JsonToXml()
    {
        //Clipboard.SetDataObject(JsonToXMLFun(UserInput.Text), true);

        var xml = JsonXmlConvertService.JsonToXml(UserInput.Text);

        Input2.Text = xml;

        Clipboard.SetDataObject(xml, true);

        SuccessMessage("Json converted to Xml successfully!");
    }

    public void JsonToCsharp()
    {
        // Replace embty and null to A << if not an ex happned
        using var reader = new MemoryReader(UserInput.Text.Replace("\"\"", "\"A\"").Replace("null", "\"A\""));
        var options = new JsonToCsharpOptions
        {
            DeclareDataMember = false,
            ListType = ListType.List,
            NameSpace = null
        };

        try
        {
            // JsonToCsharpGenerator is open source from GitHub
            var classData = new JsonToCsharpGenerator(options).Create("RootClass", reader);

            StringBuilder sb = new();

            foreach (var item in classData)
            {
                sb.AppendLine();
                sb.AppendLine(item.Value.ToString());
                sb.AppendLine();
            }

            Input2.Text = sb.ToString();

            Clipboard.SetDataObject(sb.ToString(), true);

            SuccessMessage("Json converted to C# class successfully!");
        }
        catch (Exception ex)
        {
            throw new DevAssistantException($"Couldn't convert Json to C# class! Details: {ex.Message}", 7013);
        }
    }

    private void ReviewReport()
    {
        // Still not fininshed yet :)

        PathInput.Text = PathInput.Text.Trim();

        appHome.LogInfo("Generate Review Repor -- START");

        if (!PathInput.Text.Contains("Nic.Apis."))
        {
            throw new DevAssistantException($"Invald path. It is not contains \"Nic.Apis.***\"", 7200);
        }

        var code = UserInput.Text;
        var modelPath = PathInput.Text.Replace("\\BusinessLayer", "\\Models");

        if (!string.IsNullOrWhiteSpace(modelPath))
        {
            code = FileService.GetCodeByFile(modelPath);
        }

        // Check the model
        List<ClassModel> models = ModelExtractionService.GetClassesByCode(code, new GetClassesOptions { CheckSpellingAndRules = true });
        string propsName = DecodeHelperService.CopyOutputOfSpellingAndRules(models);

        //ValidateServicenfoForIG(PathInput.Text, true, out string contractName, out string controllerName, out string serviceName,
        //           out string contrllerDesc, out List<BusinessApiException> apiExceptions, out List<DevAssistantException> allExs);

        // Check BL, rules, dbo, with(nolock) and namespace and  controller name

        // Check controller

        Clipboard.SetText(propsName);

        // TODO: opent the report in Word file.

        appHome.LogInfo("Generate Review Repor -- END | Successfully copied to your clipboard. Paste it into Word file then check for Spelling");
    }

    private Event GetCurrentEvent()
    {
        if (CompareTRadBtn.Checked)
        {
            if (IsModel.Checked)
                return DevEvents.RCompareModel;
            else if (IsJson.Checked)
                return DevEvents.RCompareJson;
            else if (IsSql.Checked)
                return DevEvents.RCompareSql;
            else if (IsText.Checked)
                return DevEvents.RCompareText;
        }
        else if (ConverterBtn.Checked)
        {
            if (SqlToCodeBtn.Checked)
                return DevEvents.RSqlToCode;
            else if (CodeToSqlBtn.Checked)
                return DevEvents.RCodeToSql;
            else if (XmlToJsonBtn.Checked)
                return DevEvents.RXmlToJson;
            else if (JsonToXmlBtn.Checked)
                return DevEvents.RJsonToXml;
            else if (JsonToCsharpBtn.Checked)
                return DevEvents.RJsonToCsharp;
            //else if (VbToCSharpBtn.Checked)
            //    return DevEvents.RVbToCSharp;
            else if (DecryptTokenBtn.Checked)
                return DevEvents.RConvertByteArrayToImage;
            else if (Base2ImageBtn.Checked)
                return DevEvents.RConvertBase64ToImage;
            else if (Blob2ImageBtn.Checked)
                return DevEvents.RConvertBlobToImage;
            else if (VbToCharpBtn.Checked)
                return DevEvents.RConvertVbToCharpClass;
        }
        else
        {
            if (GenerateIGRadBtn.Checked)
            {
                if (GenApiBtn.Checked)
                {
                    if (GenerateIgsOptionRatBrn.Checked)
                        return DevEvents.RGenerateAllIGs;
                    else
                        return DevEvents.RGenerateApiIG;
                }
                else
                {
                    return DevEvents.RGenerateMicroIG;
                }
            }
            if (GenerateNdbJsonRadBtn.Checked)
                return DevEvents.RGenerateNdbJson;
            else if (GetPropsInfoBtn.Checked)
                return DevEvents.RGetPropsInfo;
            else if (GetDbLengthBtn.Checked)
                return DevEvents.RGetDbLength;
            else if (GetAllMethodsBtn.Checked)
                return DevEvents.RGetAllMethods;
            else if (ConverterBtn.Checked)
                return DevEvents.RCodeToSql;
            else if (GetAllSerivcesBtn.Checked)
                return DevEvents.RGetAllServices;
            else if (GetPackageReferencesBtn.Checked)
                return DevEvents.RGetPackageReferences;
            else if (CheckRulesRadBtn.Checked)
                return DevEvents.RCheckRules;
            else if (ReviewReportRadBtn.Checked)
                return DevEvents.RReviewReport;
            //else if (MoveCommonSettingBtn.Checked)
            //    return DevEvents.RMoveCommonsettings;
        }

        return null;
    }


    /// <summary>
    /// This method used to prepare API service to generate IG or Ndb Json only
    /// </summary>
    /// <param name="projectType"></param>
    /// <exception cref="DevAssistantException"></exception>
    private void PrepareApiService(ProjectType projectType)
    {
        bool generateIgsOptionOldValue = GenerateIgsOptionRatBrn.Checked;

        try
        {
            appHome.LogInfo($"Generate {projectType} IG -- START");

            if (projectType != ProjectType.NdbJson && string.IsNullOrWhiteSpace(WrittenByInput.Text))
            {
                throw new DevAssistantException("Please enter the author name (Written By)", 7008);
            }

            double igVersion = 0;

            if (projectType != ProjectType.NdbJson && (!double.TryParse(IGVersionInput.Text.Replace(_prefixText, ""), out igVersion) || igVersion == 0))
            {
                throw new DevAssistantException("Please enter the IG version (Document version)", 7009);
            }

            if (!WrittenByInput.Text.Equals(Consts.UserSettings.WrittenByIG, StringComparison.CurrentCultureIgnoreCase))
                Consts.UserSettings.WrittenByIG = WrittenByInput.Text;


            if (int.TryParse(PathInput.Text.Trim().Replace("!", ""), out int pullRequestId))
            {
                List<NicService> prServices = PullRequestService.PreparePullRequest(pullRequestId);

                if (prServices.Count == 0)
                {
                    throw new DevAssistantException($"No services found for PR#{pullRequestId}", 7034);
                }

                if (prServices.Count > 1)
                {
                    var dialogInfo = new DevDialogInfo
                    {
                        Title = $"Mulitple services found:",
                        Message = $"We found {prServices.Count} services. Do you want to continue and generate all servcies?",
                        Buttons = MessageBoxButtons.YesNo,
                        BoxIcon = MessageBoxIcon.Question
                    };

                    DialogResult result = DevDialog.Show(dialogInfo);

                    if (result == DialogResult.No)
                    {
                        appHome.LogError($"Stop generating IGs...");

                        return;
                    }
                }

                CheckSpellingDialog checkSpelling = new();

                IntegrationGuideService.GenerateApiIG(new()
                {
                    Services = prServices,
                    ProjectType = projectType,
                    IgVersion = igVersion,
                    AddBracket = AddBracketBox.Checked,
                    GenerateMultipleIGs = prServices.Count > 1,
                    OpenJsonAfterCreate = OpenJsonBox.Checked,
                    WrittenBy = WrittenByInput.Text.Trim(),
                }, _log, checkSpelling);

                checkSpelling.Dispose();

                return;
            }

            // because if user generate IGs of controller we will set GenerateIgsOptionRatBrn to true coding, then after generate we'll set it back

            PathInput.Text = PathInput.Text.Trim();

            Log.Logger.Information("Generate {a} IG -- START", projectType);

            if (projectType is ProjectType.RestApi && !PathInput.Text.Contains("Nic.Apis."))
            {
                throw new DevAssistantException($"Invald path. It is not contains \"Nic.Apis.***\"", 7100);
            }

            List<NicService> services = new();

            var serviceInfo = FileService.GetServiceInfo(PathInput.Text);

            // \Controllers\ServiceBusController.cs
            if (projectType is ProjectType.RestApi && PathInput.Text.EndsWith("Controller.cs") && !GenerateIgsOptionRatBrn.Checked
                || (PathInput.Text.EndsWith("Controller.cs") && PathInput.Text.Contains("Nic.Apis.Npv\\")))
            {
                GenerateIgsOptionRatBrn.Checked = true;

                //Log.Logger.Information($"GetFileInfo Called - paramValue: {path}");

                //  namespace+conCode             controllerName            serviceName
                // \Nic.Apis.SmartCity\BusinessLayer\ServiceBus\EsbCustomsGetSuspectPersonByID.cs

                var temp = PathInput.Text[PathInput.Text.LastIndexOf("Nic.Apis")..].Split("\\");

                if (!temp[1].Equals("Controllers", StringComparison.CurrentCultureIgnoreCase) || PathInput.Text.EndsWith("HomeController.cs"))
                {
                    throw new DevAssistantException($"Please ensure that the controller is correct and valid. {PathInput.Text}", 7025);
                }

                // Contract path
                string contractPath = Path.Combine(PathInput.Text[..PathInput.Text.LastIndexOf("Nic.Apis")], serviceInfo.Namespace);

                if (serviceInfo.Namespace.IsNamespaceNpv()) // Generate IGs for specific Npv contracts
                {
                    appHome.LogInfo($"Generate IGs for {serviceInfo.ContractCode}...");

                    Log.Logger.Information("Generate Npv IGs");

                    string[] controllers = Directory.GetDirectories(Path.Combine(contractPath, "Models"));

                    bool isFirstService = true;

                    List<string> npvServices = new()
                    {
                        "MicroGetFirmInfo",
                        "MicroListNPVFineRemarks",
                        "MicroListNPVImages",
                        "MicroListNPViolations",
                        "MicroLKAF400GeneralFineGroup",
                        "MicroLKAF401GeneralFineType",
                        "MicroLKAF402FineBusinessOwner",
                        "MicroLKGS200GeoArea",
                        "MicroLKIF246VehicleMake",
                        "MicroLKMV201RegistrationType",
                        "Npv1001ValidateFineRegistration",
                        "Npv1002ValidateFineCancellation",
                        "Npv1003ValidateFineResolution",
                        "Npv2001DoFineRegistration",
                        "Npv2002DoFineCancellation",
                        "Npv2003DoFineResolution"
                    };

                    foreach (var folder in controllers)
                    {
                        foreach (var file in Directory.GetFiles(folder, "*.cs"))
                        {
                            var service = GetNicService(file, serviceInfo.ContractCode);

                            if (isFirstService)
                            {
                                WebService webService = ModelExtractionService.GetAllMethods(service.Controller.Code);

                                if (webService.Funcations.Count != 16)
                                {
                                    throw new DevAssistantException("One or more service(s) missing. Npv contract must be 16 services", 7029);
                                }

                                if (!webService.Funcations.Select(f => f.FunName).ContainsAny(npvServices, out string serviceNotFound))
                                {
                                    throw new DevAssistantException($"Couldn't found {serviceNotFound} servcie in {service.Controller.FullControllerName}", 7030);
                                }

                                int startCtorIndex = service.Controller.Code.IndexOf($"public {service.Controller.FullControllerName}(ILogger");

                                string constructorCode = service.Controller.Code[startCtorIndex..];

                                int endCtorIndex = constructorCode.IndexOf('}') + 1;

                                constructorCode = service.Controller.Code.Substring(startCtorIndex, endCtorIndex);

                                if (!constructorCode.Contains($"_npvOptions = contractOptions.Value.{service.Controller.ControllerName};"))
                                {
                                    throw new DevAssistantException($"Invalid binding for _npvOptions in the constructor. Expected \"_npvOptions = contractOptions.Value.{service.Controller.ControllerName};\"", 7031);
                                }

                                if (service.Controller.Code.Contains("private readonly IOptionsSnapshot<ContractOptions> _contractOptions)"))
                                {
                                    throw new DevAssistantException($"Npv controllers must not contains or inject the ContractOptions", 7032);
                                }

                                if (!service.Controller.Code.Contains("[AuthorizeNpv]"))
                                {
                                    throw new DevAssistantException($"Npv controllers must have [AuthorizeNpv] annotation", 7033);
                                }

                                isFirstService = false;
                            }

                            services.Add(service);
                        }
                    }
                }
                else // Generate IGs for specific Controller
                {
                    appHome.LogInfo($"Generate IGs for {serviceInfo.ControllerName}Controller in {serviceInfo.ContractCode} contract...");

                    Log.Logger.Information("Generate IGs for {a}Controller in {b} contract...", serviceInfo.ControllerName, serviceInfo.ContractCode);

                    var controllerFilesPath = Path.Combine(contractPath, "Models", serviceInfo.ControllerName);

                    foreach (var file in Directory.GetFiles(controllerFilesPath, "*.cs"))
                    {
                        services.Add(GetNicService(file, serviceInfo.ContractCode));
                    }
                }
            }
            else
            {
                if (PathInput.Text.EndsWith("Nic.Apis.Npv") || PathInput.Text.Contains("Nic.Apis.Npv\\"))
                {
                    throw new DevAssistantException("Please specify the Npv controller to generate all IGs. Please note that you can’t generate single IG for Npv contract", 7026);
                }

                // Generate all contract IGs is only for API
                if (GenerateIgsOptionRatBrn.Checked && projectType is ProjectType.RestApi)
                {
                    if (PathInput.Text.EndsWith(".cs"))
                    {
                        string message = $"Do you want to continue to generate all IGs for {serviceInfo.ContractCode} contract";
                        string title = $"Generate all IGs for {serviceInfo.ContractCode} contract";

                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                        DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Question);

                        if (result == DialogResult.No)
                        {
                            throw new DevAssistantException($"Generating IG for {serviceInfo.ContractCode} contract canceled successfully!", 7027);
                        }

                        // else continue generating IGs...
                    }

                    appHome.LogInfo($"Generate All IGs for {serviceInfo.ContractCode} contract...");

                    Log.Logger.Information("Generate All IGs for {a} contract...", serviceInfo.ContractCode);

                    // Contract path
                    string contractPath = Path.Combine(PathInput.Text[..PathInput.Text.LastIndexOf("Nic.Apis")], serviceInfo.Namespace);

                    string[] controllers = Directory.GetDirectories(Path.Combine(contractPath, "Models"));

                    foreach (var folder in controllers)
                    {
                        foreach (var file in Directory.GetFiles(folder, "*.cs"))
                        {
                            services.Add(GetNicService(file));
                        }
                    }
                }
                else
                {
                    if (!PathInput.Text.EndsWith(".cs"))
                    {
                        throw new DevAssistantException("Invalid path for the service. If you want to generate all IGs for the contract then select \"Generate IGs for contract\" option.", 7028);
                    }

                    Log.Logger.Information("Generate one file");

                    services.Add(GetNicService(PathInput.Text));
                }
            }

            CheckSpellingDialog checkSpellingDialog = new();

            IntegrationGuideService.GenerateApiIG(new()
            {
                Services = services,
                ProjectType = projectType,
                IgVersion = igVersion,
                AddBracket = AddBracketBox.Checked,
                GenerateMultipleIGs = GenerateIgsOptionRatBrn.Checked,
                OpenJsonAfterCreate = OpenJsonBox.Checked,
                WrittenBy = WrittenByInput.Text.Trim(),
            }, _log, checkSpellingDialog);

            checkSpellingDialog.Dispose();

        }
        finally
        {
            appHome.LogInfo($"Generate {projectType} IG -- END");
            Log.Logger.Information("Generate {a} IG -- END", projectType);

            GenerateIgsOptionRatBrn.Checked = generateIgsOptionOldValue;
        }
    }


    private static NicService GetNicService(string servicePath, string contractCode = "")
    {
        // Get file info by calling GetFileInfo
        var serviceInfo = FileService.GetServiceInfo(servicePath);

        if (string.IsNullOrWhiteSpace(contractCode))
            contractCode = serviceInfo.ContractCode;

        // Get root path
        // Ex:
        //     Root path     \ remining path
        // C:\Project\API\Git\Nic.Apis.SmartCity\BusinessLayer\ServiceBus\EsbCustomsGetSuspectPersonByID.cs
        string rootPath = servicePath[..servicePath.IndexOf(serviceInfo.Namespace)];

        NicService service = new()
        {
            Namespace = serviceInfo.Namespace,
            ContractCode = contractCode,
            ControllerName = serviceInfo.ControllerName,
            ServiceName = serviceInfo.ServiceName,
        };

        // Get BusinessLayer File -- START
        // Check if the path is for the Model
        if (servicePath.Contains(@"\Models"))
        {
            // To get the BL code
            servicePath = servicePath.Replace("\\Models", "\\BusinessLayer");
        }

        if (!File.Exists(servicePath))
        {
            throw new DevAssistantException($"The given path is not existing on disk. Couldn't open BusinessLayer file {servicePath}", 7102);
        }

        NicServiceFile blFile = new()
        {
            ServiceName = serviceInfo.ServiceName,
            FilePath = new() { Path = servicePath, IsRemotePath = false },
            FileType = NicServiceFileType.BusinessLayer,
            Code = FileService.GetCodeByFile(servicePath)
        };

        service.BusinessLayer = blFile;
        // Get BusinessLayer File -- END

        // Get Model File -- START
        servicePath = servicePath.Replace("\\BusinessLayer", "\\Models");

        if (!File.Exists(servicePath))
        {
            throw new DevAssistantException($"The given path is not existing on disk. Couldn't open Model file {servicePath}", 7103);
        }

        NicServiceFile modelFile = new()
        {
            ServiceName = serviceInfo.ServiceName,
            FilePath = new() { Path = servicePath, IsRemotePath = false },
            FileType = NicServiceFileType.Model,
            Code = FileService.GetCodeByFile(servicePath)
        };

        service.Model = modelFile;
        // Get Model File -- END

        // Get Controller File -- START
        var contrllerPath = string.Empty;

        // All services of Npv contract will be in one controller
        if (serviceInfo.Namespace.IsNamespaceNpv())
        {
            contrllerPath = $"{rootPath}{serviceInfo.Namespace}\\Controllers\\{contractCode.Replace("Npv", "")}Controller.cs";
        }
        else // regelar services for Rest
        {
            contrllerPath = $"{rootPath}{serviceInfo.Namespace}\\Controllers\\{serviceInfo.ControllerName}Controller.cs";
        }

        if (!File.Exists(contrllerPath))
        {
            throw new DevAssistantException($"The given path is not existing on disk. Couldn't open controller file {contrllerPath}", 7104);
        }

        NicServiceFile controllerFile = new()
        {
            ServiceName = serviceInfo.ServiceName,
            FilePath = new() { Path = contrllerPath, IsRemotePath = false },
            FileType = NicServiceFileType.Controller,
            Code = FileService.GetCodeByFile(contrllerPath)
        };

        service.Controller = controllerFile;
        // Get Controller File -- END

        return service;
    }


    private void PrepareMicroService()
    {
        PathInput.Text = PathInput.Text.Trim();

        appHome.LogInfo($"Generate Micro IG -- START");
        Log.Logger.Information("Generate Micro IG -- START");

        try
        {
            if (PathInput.Text.Contains("Nic.Apis."))
            {
                throw new DevAssistantException($"Invald path. It is is contains \"Nic.Apis.***\". Please note that you're trying to generate Micro IG", 7035);
            }

            if (!double.TryParse(IGVersionInput.Text.Replace(_prefixText, ""), out double igVersion) || igVersion == 0)
            {
                throw new DevAssistantException("Please enter the IG version (Document version)", 7035);
            }

            if (!WrittenByInput.Text.Equals(Consts.UserSettings.WrittenByIG, StringComparison.CurrentCultureIgnoreCase))
                Consts.UserSettings.WrittenByIG = WrittenByInput.Text;

            CheckSpellingDialog checkSpellingDialog = new();

            IntegrationGuideService.GenerateMicroIG(new()
            {
                MicroPath = PathInput.Text,
                IgVersion = igVersion,
                WrittenBy = WrittenByInput.Text.Trim(),
            }, _log, checkSpellingDialog);

            checkSpellingDialog.Dispose();
        }
        finally
        {
            appHome.LogInfo($"Generate Micro IG -- END");
            Log.Logger.Information("Generate Micro IG -- END");
        }
    }


    private void CheckRules()
    {
        // From Git
        if (int.TryParse(PathInput.Text.Trim().Replace("!", ""), out int pullRequestId))
        {
            List<NicService> services = PullRequestService.PreparePullRequest(pullRequestId, new()
            {
                ExcludeBusinessLayer = true,
                ExcludeController = true,
                ExcludeModels = false // We only need model code
            });

            if (services.Count == 0)
            {
                throw new DevAssistantException($"No services found for PR#{pullRequestId}", 7034);
            }

            if (!ValidationService.ValidateServiceLimit(services.Count, _log))
                return;

            foreach (var service in services)
            {
                List<ClassModel> models = ModelExtractionService.GetClassesByCode(service.Model.Code, new GetClassesOptions { CheckSpellingAndRules = true, ShowErrorCode = true });

                string nameSpace = ModelExtractionService.GetNamespace(service.Model.Code);

                DecodeHelperService.CheckSpellingAndRules(new()
                {
                    Models = models,
                    Event = DevEvents.RCheckRules,
                    AlwaysShowDialog = true,
                    Namespace = nameSpace
                }, _log, null);
            }

            return;
        }
        else // Locally
        {
            var code = UserInput.Text;
            var modelPath = PathInput.Text.Replace("\\BusinessLayer", "\\Models");

            if (!string.IsNullOrWhiteSpace(modelPath))
            {
                code = FileService.GetCodeByFile(modelPath);
            }

            List<ClassModel> models = ModelExtractionService.GetClassesByCode(code, new GetClassesOptions { CheckSpellingAndRules = true, ShowErrorCode = true });

            string nameSpace = ModelExtractionService.GetNamespace(code);

            DecodeHelperService.CheckSpellingAndRules(new()
            {
                Models = models,
                Event = DevEvents.RCheckRules,
                AlwaysShowDialog = true,
                Namespace = nameSpace
            }, _log, null);

            return;
        }
    }





    private void CompareJson()
    {
        bool isMatch = CompareService.CompareJson(Input1, Input2.Text, new CompareJsonOptions { ToLowerCase = ToLowerCase.Checked, OnlyKeys = OnlyKeys.Checked });

        if (isMatch)
        {
            SuccessMessage("The two fields are match.");
        }
        else
        {
            appHome.LogError("The two fields do not match.");
        }
    }

    private void CompareText()
    {
        if (Input1.Trim().Equals(Input2.Text.Trim()))
        {
            SuccessMessage("The two Text are match.");
        }
        else
        {
            appHome.LogError("The two Text are not match.");
        }
    }

    private void CompareSql()
    {
        string sql1;
        string sql2;

        RegexOptions options = RegexOptions.None;
        Regex regex = new(@"\@\w+\b", options);

        // remocve parameters @*****
        var input1 = regex.Replace(Input1, "");
        var input2 = regex.Replace(Input2.Text, "");

        SqlConvertOptions sqlOptions = new()
        {
            RemoveComments = true,
            CommentStyle = DevCommentType.Sql,
            RemoveWhiteSpace = true,
            IsSqlToCode = true, // to enter if (options.IsSqlToCode) in CleanSql
        };

        Console.WriteLine("Sql1 START");
        sql1 = SqlConvertService.CodeToSql(input1.ToLower().Replace(" as ", " @as "), sqlOptions).Replace(" ", "").Replace("\n", "").Replace("\r", "");
        Console.WriteLine();
        Console.WriteLine("Sql2 START");
        sql2 = SqlConvertService.CodeToSql(input2.ToLower().Replace(" as ", " @as "), sqlOptions).Replace(" ", "").Replace("\n", "").Replace("\r", "");

        if (RemoveAsBox.Checked)
        {
            Console.WriteLine("Sql1:");
            sql1 = SqlConvertService.RemoveAs(sql1);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Sql2:");
            sql2 = SqlConvertService.RemoveAs(sql2);
        }

        //Console.WriteLine("Sql1:");
        //Console.WriteLine(sql1);
        //Console.WriteLine();
        //Console.WriteLine("Sql2:");
        //Console.WriteLine(sql2);

        if (sql1.Trim().Equals(sql2.Trim()))
        {
            SuccessMessage("The two SQL are match.");
        }
        else
        {
            //Input1 = sql1;
            //Input2.Text = sql2;

            Clipboard.SetText(sql1 + Environment.NewLine + Environment.NewLine + sql2);
            appHome.LogError("The two SQL are not match. The cleaned queries are copied to your clipboard successfully!");
        }
    }

    private void CompareModel()
    {
        var result = CompareService.CompareModel(Input1, Input2.Text);

        Result output = new(this, result.Models1, result.Models2);

        output.Show();
    }

    private void PrepareGitCmd()
    {
        var path = PathInput.Text;

        if (path.EndsWith(".sln"))
        {
            path = path[..path.LastIndexOf("\\")];
        }

        if (!Directory.Exists(path))
        {
            throw new DevAssistantException("The given path is not existing on disk " + path, 7006);
        }

        string[] folders = Directory.GetDirectories(path);

        var cmd = new StringBuilder();

        foreach (var folder in folders)
        {
            int index = folder.LastIndexOf("\\");
            string contractName = folder[(index + 1)..].Replace("Service.cs", "");

            if (!contractName.StartsWith("Nic.Apis."))
                continue;

            //  git rm --cached Nic.Apis.EhsanEligibility / Nic.Apis.EhsanEligibility.xml

            cmd.AppendLine($"git rm --cached {contractName}/{contractName}.xml");
        }

        Clipboard.SetText(cmd.ToString());
    }

    private void GetAllServices()
    {
        string path = PathInput.Text;
        bool oneContract = false;

        //List<string> contractsList = new()
        //{
        //    "AmnKashif",
        //    "Clearance",
        //    "Egate",
        //    "Housing",
        //    "Kashif",
        //    "Riyadah",
        //    "SITA",
        //    "SNAD",
        //    "VaccineReg",
        //    "Yakeen",
        //    "Alien",
        //    "BOG",
        //    "Border",
        //    "CAMPCA",
        //    "CIB",
        //    "CITC",
        //    "CMA",
        //    "Darp",
        //    "EhsanEligibility",
        //    "Eid",
        //    "EPassport",
        //    "FingerPrintDelete",
        //    "Gis",
        //    "GOSI",
        //    "GRP",
        //    "HealthPassport",
        //    "MAB",
        //    "Moci",
        //    "MOE",
        //    "MOF",
        //    "MOLData",
        //    "NAZAHA",
        //    "NDB",
        //    "NDMO",
        //    "NGHA",
        //    "Pension",
        //    "QIYAS",
        //    "REDF",
        //    "RoyalCourt",
        //    "SAR",
        //    "SCFHS",
        //    "SCJ",
        //    "SDAIAERP",
        //    "SFDA",
        //    "SHO",
        //    "SIDF",
        //    "SRG",
        //    "SRO",
        //    "TVTC",
        //    "Employment",
        //    "Absher",
        //};

        if (path.EndsWith(".csproj") || path.Contains("Nic.Apis."))
        {
            //path = path[..path.LastIndexOf("\\")];

            if (PathInput.Text.EndsWith(".csproj"))
                path = path[..path.LastIndexOf("\\")];

            oneContract = true;
        }

        List<FileInfoModel> services = new();

        if (!oneContract)
        {
            var folders = Directory.GetDirectories(PathInput.Text);

            foreach (var folder in folders)
            {
                var contractCode = folder[folder.LastIndexOf("\\")..].Replace("\\", "").Trim();

                if (!contractCode.Contains("Nic.Apis"))
                    continue;

                if (!Directory.Exists($"{folder}\\BusinessLayer"))
                    continue;

                contractCode = contractCode.Replace("Nic.Apis.", "");

                //if (!contractsList.Any(item => item.ToLower() == contractCode.ToLower()))
                //    continue;

                var tempServices = Services.GetMicros(folder, getFromBayan: true);

                foreach (var item in tempServices)
                {
                    item.ContractCode = contractCode;
                }

                services.AddRange(tempServices);
            }
        }
        else
        {
            services = Services.GetMicros(path, getFromBayan: true);
        }

        //services.Sort((x, y) => x.ContractCode.CompareTo(y.ContractCode));
        //services.Sort((x, y) => x.Controller.CompareTo(y.Controller));

        var output = new AllServicesResult(services);
        output.Show();
    }

    private void MatchMicroServicesWithBayan()
    {
        string path = PathInput.Text;

        if (!path.Contains("Nic.Apis."))
            throw new DevAssistantException("", 0);

        if (path.EndsWith(".csproj"))
            path = path[..path.LastIndexOf("\\")];

        List<FileInfoModel> microServices = Services.GetMicros(@"C:\Project\MicroServices\Core\Development\", getFromBayan: false);
        List<FileInfoModel> bayanServices = Services.GetMicros(path, getFromBayan: true);
        List<NdbJson> ndbServices = new();

        //NdbJson

        var folders = Directory.GetFiles(@"\\nicdevnfs\d\Faisal Aljuweir\Ndb Marketplace");

        foreach (var folder in folders)
        {
            try
            {
                using StreamReader file = File.OpenText(folder);
                NdbJson ndbJson = JsonConvert.DeserializeObject<NdbJson>(file.ReadToEnd());

                ndbServices.Add(ndbJson);
            }
            catch (Exception ex)
            {
            }
        }

        foreach (var micro in microServices)
        {
            var bayanService = bayanServices.FirstOrDefault(s => s.Name == micro.Name);

            if (bayanService != null)
            {
                micro.Controller = bayanService.Controller;
            }
            else
            {
                var ndbService = ndbServices.FirstOrDefault(s => s.onBoardingRequest.body.api_name_tech == micro.Name);

                if (ndbService != null)
                    micro.Controller = ndbService.onBoardingRequest.body.tags[0];
            }
        }

        var a = microServices.GroupBy(s => s.Controller).ToList();

        StringBuilder sb = new();

        foreach (var item in a)
        {
            sb.Clear();
            sb.AppendLine("namespace MicroApiClient.Controllers;");
            sb.AppendLine("");
            sb.AppendLine("[Route(\"api/[controller]\")]");
            sb.AppendLine("[ApiController]");
            sb.AppendLine("[Produces(MediaTypeNames.Application.Json)]");
            sb.AppendLine("[Consumes(MediaTypeNames.Application.Json)]");
            sb.AppendLine($"public class {item.Key}Controller : ControllerBase");
            sb.AppendLine("{");
            sb.AppendLine("private readonly IEnvironmentService _environmentService;");
            sb.AppendLine("");
            sb.AppendLine($"public {item.Key}Controller(IEnvironmentService environmentService) => _environmentService = environmentService;");
            sb.AppendLine("");
            sb.AppendLine("");

            foreach (var service in item.OrderBy(s => s.Name))
            {
                sb.AppendLine($"[HttpPost(\"{service.Name}\")]");
                sb.AppendLine($"public ActionResult<{service.Name}.ObjectModel.{service.Name}Outputs> {service.Name}({service.Name}.ObjectModel.{service.Name}Inputs inputs)");
                sb.AppendLine("{");
                sb.AppendLine("try");
                sb.AppendLine("{");
                sb.AppendLine($"var srv = new {service.Name}.{service.Name}Service();");
                sb.AppendLine($"var output = srv.{service.Name}(inputs, _environmentService.GetPdsConnection());");
                sb.AppendLine("");
                sb.AppendLine("return Ok(output);");
                sb.AppendLine("}");
                sb.AppendLine("catch (MicroServiceException ex)");
                sb.AppendLine("{");
                sb.AppendLine("return BadRequest(ex.ToProblemDetails());");
                sb.AppendLine("}");
                sb.AppendLine("}");

                sb.AppendLine("");
            }

            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("======================================");
            sb.AppendLine("");
            sb.AppendLine("");
        }

        var output = new AllServicesResult(microServices);
        output.Show();
    }

    private void GetPropsInfo()
    {
        string code = UserInput.Text;
        string path = PathInput.Text;

        if (!string.IsNullOrWhiteSpace(PathInput.Text))
        {
            // Check if the path is for the Model
            if (PathInput.Text.Contains(@"\BusinessLayer"))
            {
                // To get the BL code
                path = PathInput.Text.Replace("\\BusinessLayer", "\\Models");
            }

            code = FileService.GetCodeByFile(path);
        }

        var classes = ModelExtractionService.GetClassesByCode(code, new GetClassesOptions
        {
            DataTypeAsPascalCase = true,
            CheckSpellingAndRules = !BypassValidationsBtn.Checked,
            ThrowException = !BypassValidationsBtn.Checked,
            AddStarForRequiredProp = true
        });

        IGPropsResult output;

        if (!string.IsNullOrWhiteSpace(path))
            output = new(appHome, classes, path);
        else
            output = new(appHome, classes);

        output.Show();
    }

    private void GetDbLength()
    {
        string sqlStatment = SqlConvertService.GetPropsLengthFromQuery(UserInput.Text);

        Clipboard.SetDataObject(sqlStatment, true);

        SuccessMessage("Sql copied successfully. Use the generated Sql to get the Data Type Length. Go to SQL Server then right click on ODSASIS then \"New Query\" then run the query (Click Execute)");
    }

    private void CodeToSql()
    {
        string cleanedSql = SqlConvertService.CodeToSql(UserInput.Text, new()
        {
            RemoveComments = RemoveCommentBox.Checked,
            CommentStyle = DevCommentType.CSharp,
            RemoveWhiteSpace = RemoveWhiteSpaceskBox.Checked,
            IsSqlToCode = false
        });

        Input2.Text = cleanedSql;

        Clipboard.SetDataObject(cleanedSql, true);

        SuccessMessage("Code cleaned successfully. Sql copied successfully. Go to SQL Server and run the query (Click Execute)");
    }

    private void SqlToCode()
    {
        SqlConvertOptions options = new() { RemoveComments = RemoveCommentBox.Checked, CommentStyle = DevCommentType.Sql, RemoveWhiteSpace = RemoveWhiteSpaceskBox.Checked, IsSqlToCode = true };

        string cleanedSql = SqlConvertService.SqlToCode(UserInput.Text, options);

        Input2.Text = cleanedSql;

        Clipboard.SetDataObject(cleanedSql, true);

        SuccessMessage("Code cleaned successfully. StringBuilder copied successfully. Paste it into your code");
    }

    private void GetAllMethods()
    {
        var code = UserInput.Text;

        if (!string.IsNullOrWhiteSpace(PathInput.Text))
        {
            code = FileService.GetCodeByFile(PathInput.Text);
        }

        var service = ModelExtractionService.GetAllMethods(code, new GetAllMethodsOptions { GetDBType = true, ListAll = true });

        ResultForm output = new(service);

        output.Show();
    }

    //public void ErrorMessage(string message)
    //{
    //    appHome.LogError(message);
    //}

    public void SuccessMessage(string message)
    {
        appHome.LogSuccess(message);
    }

    private void HidePath()
    {
        UserInput.Enabled = true;

        PathLabel.Visible = false;
        PathInput.Visible = false;
        BrowseBtn.Visible = false;
    }

    private void ShowPath()
    {
        UserInput.Enabled = true;

        PathLabel.Visible = true;
        PathInput.Visible = true;
        BrowseBtn.Visible = true;
    }

    private void GetPropsInfoBtn_CheckedChanged(object sender, EventArgs e)
    {
        ShowPath();

        if (GetPropsInfoBtn.Checked)
        {
            Consts.UserSettings.RFeatureCriteria = 2;

            PathLabel.Text = "Enter the Model or BusinessLayer path:";

            GetPropsOpt.Visible = true;
        }
        else
        {
            PathLabel.Text = "or Enter file/contarct path:";

            GetPropsOpt.Visible = false;
        }
    }

    private void GetDbLengthBtn_CheckedChanged(object sender, EventArgs e)
    {
        HidePath();

        if (GetDbLengthBtn.Checked)
        {
            Consts.UserSettings.RFeatureCriteria = 4;
        }
    }

    private void GetAllMethodsBtn_CheckedChanged(object sender, EventArgs e)
    {
        ShowPath();

        if (GetAllMethodsBtn.Checked)
        {
            Consts.UserSettings.RFeatureCriteria = 5;
        }
    }

    private void ConverterBtnlBtn_CheckedChanged(object sender, EventArgs e)
    {
        HidePath();

        int converterSelectedOpt = Consts.UserSettings.RConverterSelectedOption;

        switch (converterSelectedOpt)
        {
            case 1 or 0:
                CodeToSqlBtn.Checked = true;
                break;

            case 2:
                SqlToCodeBtn.Checked = true;
                break;

            case 3:
                XmlToJsonBtn.Checked = true;
                break;

            case 4:
                JsonToXmlBtn.Checked = true;
                break;

            case 5:
                JsonToCsharpBtn.Checked = true;
                break;

            case 6:
                DecryptTokenBtn.Checked = true;
                break;

            case 7:
                Base2ImageBtn.Checked = true;
                break;

            case 8:
                Blob2ImageBtn.Checked = true;
                break;

            case 9:
                VbToCharpBtn.Checked = true;
                break;
        }

        if (ConverterBtn.Checked)
        {
            Consts.UserSettings.RFeatureCriteria = 7;

            GetOutput.Text = "Convert";
            _getOutputLabel = "Convert";

            Input2Label.Visible = true;
            Input2Label.Text = "Output:";

            Input2.Visible = true;
            Input2.ReadOnly = true;
            MainSplitContainer.Panel2Collapsed = false;

            ConverterOptions.Visible = true;

            if (CodeToSqlBtn.Checked || SqlToCodeBtn.Checked)
                SqlCoverterOptions.Visible = true;
            else
                SqlCoverterOptions.Visible = false;

            if (VbToCharpBtn.Checked)
                ClassConversionOpt.Visible = true;
            else
                ClassConversionOpt.Visible = false;
        }
        else
        {
            GetOutput.Text = "Get the Result";
            _getOutputLabel = "Get the Result";

            Input2Label.Visible = false;
            Input2.Text = string.Empty;
            Input2.Visible = false;
            Input2.ReadOnly = false;
            MainSplitContainer.Panel2Collapsed = true;

            ConverterOptions.Visible = false;
            SqlCoverterOptions.Visible = false;
            ClassConversionOpt.Visible = false;
        }
    }

    private void SqlToCodeBtn_CheckedChanged(object sender, EventArgs e) => HidePath();

    private void radioButton1_CheckedChanged(object sender, EventArgs e)
    {
        if (GetAllSerivcesBtn.Checked)
        {
            Consts.UserSettings.RFeatureCriteria = 6;

            // save the text before we clean it
            _oldUserInput = UserInput.Text;

            ShowPath();

            PathLabel.Text = "Enter the contarct path: (or Solution path to get all services):";

            UserInput.Text = string.Empty;
            UserInput.Enabled = false;
        }
        else
        {
            PathLabel.Text = "or Enter file/contarct path:";
            // set back the text
            UserInput.Text = _oldUserInput;
        }
    }

    private void GitCmdBtn_CheckedChanged(object sender, EventArgs e)
    {
        ShowPath();

        UserInput.Text = string.Empty;
        UserInput.Enabled = false;
    }

    private void IsJson_CheckedChanged(object sender, EventArgs e)
    {
        Consts.UserSettings.RCompareSelectedOption = 2;

        if (IsJson.Checked)
        {
            SqlOptions.Visible = true;
            OnlyKeys.Enabled = true;
            ToLowerCase.Enabled = true;

            RemoveAsBox.Enabled = false;
        }
        else
        {
            SqlOptions.Visible = false;
        }
    }

    private void IsSql_CheckedChanged(object sender, EventArgs e)
    {
        Consts.UserSettings.RCompareSelectedOption = 3;

        if (IsSql.Checked)
        {
            SqlOptions.Visible = true;
            RemoveAsBox.Enabled = true;

            OnlyKeys.Enabled = false;
            ToLowerCase.Enabled = false;
        }
        else
        {
            SqlOptions.Visible = false;
        }
    }

    private void CompareTRadBtn_Click(object sender, EventArgs e)
    {
        if (CompareTRadBtn.Checked)
        {
            GetOutput.Text = "Compare";
            _getOutputLabel = "Compare";
        }
        else
        {
            GetOutput.Text = "Get the Result";
            _getOutputLabel = "Get the Result";
        }
    }

    private void CompareTRadBtn_CheckedChanged(object sender, EventArgs e)
    {
        int compareSelectedOpt = Consts.UserSettings.RCompareSelectedOption;

        switch (compareSelectedOpt)
        {
            case 1 or 0:
                IsModel.Checked = true;
                break;

            case 2:
                IsJson.Checked = true;
                break;

            case 3:
                IsSql.Checked = true;
                break;

            case 4:
                IsText.Checked = true;
                break;
        }

        if (CompareTRadBtn.Checked)
        {
            Consts.UserSettings.RFeatureCriteria = 8;

            GetOutput.Text = "Compare";
            _getOutputLabel = "Compare";

            Input2Label.Visible = true;

            if (IsModel.Checked)
                Input2Label.Text = "Input 2 (New Service):";
            else
                Input2Label.Text = "Input 2:";

            Input2.Visible = true;
            CompareGroupBox.Visible = true;

            //UserInput.Size = new  Size(395, 413);
            MainSplitContainer.Panel2Collapsed = false;

            HidePath();

            if (IsSql.Checked || IsJson.Checked)
                SqlOptions.Visible = true;
        }
        else
        {
            GetOutput.Text = "Get the Result";
            _getOutputLabel = "Get the Result";

            Input2Label.Visible = false;
            Input2.Visible = false;

            //UserInput.Size = new  Size(838, 413);
            MainSplitContainer.Panel2Collapsed = true;

            CompareGroupBox.Visible = false;
            SqlOptions.Visible = false;
        }
    }

    public string Input1
    {
        get => UserInput.Text;
        set => UserInput.Text = value;
    }

    public string NewInput
    {
        get => Input2.Text;
        set => Input2.Text = value;
    }

    private void CheckRulesRadBtn_CheckedChanged(object sender, EventArgs e)
    {
        ShowPath();

        if (CheckRulesRadBtn.Checked)
        {
            Consts.UserSettings.RFeatureCriteria = 3;

            PathLabel.Text = "or Enter .cs file path or Pull Request number:";
        }
        else
        {
            PathLabel.Text = "or Enter file/contarct path:";
        }
    }

    private void BrowseBtn_Click(object sender, EventArgs e)
    {
        try
        {
            if ((GenerateIGRadBtn.Checked && GenerateIgsOptionRatBrn.Checked) || GetAllSerivcesBtn.Checked || GetPackageReferencesBtn.Checked)
            {
                PathInput.Text = FileService.FolderPickerDialog(
                    title: "Select the contract path:",
                    initialDirectory: Consts.UserSettings.LastPathInput);

                if (string.IsNullOrWhiteSpace(PathInput.Text))
                {
                    ActiveControl = PathInput;

                    throw new DevAssistantException("The returned path is empty or invalid.", 7007);
                }
            }
            else
            {
                var a = Consts.UserSettings.LastPathInput;
                PathInput.Text = FileService.FolderPickerDialog(
                    title: GenerateIGRadBtn.Checked ? "Select the Model or BusinessLayer path" : "Select the file path:",
                    isFilePicker: true,
                    initialDirectory: Consts.UserSettings.LastPathInput);

                if (string.IsNullOrWhiteSpace(PathInput.Text))
                {
                    ActiveControl = PathInput;

                    throw new DevAssistantException("The returned path is empty or invalid.", 7012);
                }
            }

            Consts.UserSettings.LastPathInput = PathInput.Text;
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

    private void GenerateIGRadBtn_CheckedChanged(object sender, EventArgs e)
    {
        int projTypeSelectedOpt = Consts.UserSettings.RIGProjectTypeOption;
        int igSelectedOpt = Consts.UserSettings.RIGSelectedOption;

        switch (igSelectedOpt)
        {
            case 0: // none selected
                AddBracketBox.Checked = false;
                GenerateIgsOptionRatBrn.Checked = false;
                break;

            case 1:
                AddBracketBox.Checked = true;
                GenerateIgsOptionRatBrn.Checked = false;
                break;

            case 2:
                AddBracketBox.Checked = false;
                GenerateIgsOptionRatBrn.Checked = true;
                break;

            case 3:
                AddBracketBox.Checked = true;
                GenerateIgsOptionRatBrn.Checked = true;
                break;
        }

        GenApiBtn.Checked = false;
        GenMicroBtn.Checked = false;

        switch (projTypeSelectedOpt)
        {
            case 1:
                GenApiBtn.Checked = true;
                break;

            case 2:
                GenMicroBtn.Checked = true;
                break;
        }

        if (GenerateIGRadBtn.Checked)
        {
            Consts.UserSettings.RFeatureCriteria = 1;

            GetOutput.Text = "Generate";
            _getOutputLabel = "Generate";

            GenerateIGOptions.Visible = true;

            // save the text before we clean it
            _oldUserInput = UserInput.Text;

            ShowPath();

            PathLabel.Text = "Enter the Model, BusinessLayer path, or Pull Request number (or contarct path: for creating all IGs):";

            UserInput.Text = string.Empty;
            UserInput.Enabled = false;
        }
        else
        {
            // set back the text
            GetOutput.Text = "Get the Result";
            _getOutputLabel = "Get the Result";

            GenerateIGOptions.Visible = false;
            PathLabel.Text = "or Enter file/contarct path:";

            UserInput.Text = _oldUserInput;
        }
    }

    private void IGVersionInput_TextChanged(object sender, EventArgs e)
    {
        if (IGVersionInput.Text.Contains(' '))
        {
            IGVersionInput.Text = IGVersionInput.Text.Replace(" ", "");
            IGVersionInput.SelectionStart = IGVersionInput.Text.Length;

            appHome.LogError("Space not allowed!");
        }

        if (!IGVersionInput.Text.StartsWith(_prefixText))
        {
            IGVersionInput.Text = _prefixText;
            IGVersionInput.SelectionStart = IGVersionInput.Text.Length;
        }
    }

    private void IGVersionInput_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && e.KeyChar == ' ')
        {
            e.Handled = true;
            appHome.LogError("Space not allowed");
        }
        else if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
        {
            e.Handled = true;
            appHome.LogError($"{e.KeyChar} is not a number. Please enter numeric values only");
        }

        // Only allowed one decimal point
        else if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
        //else if (e.KeyChar == '.')
        {
            e.Handled = true;
            appHome.LogError("Only allowed one decimal point");
            //appHome.LogError("Decimal point is not allowed");
        }
    }

    private void ReviewReportRadBtn_CheckedChanged(object sender, EventArgs e)
    {
        if (ReviewReportRadBtn.Checked)
        {
            GetOutput.Text = "Review";
            _getOutputLabel = "Review";

            // save the text before we clean it
            _oldUserInput = UserInput.Text;

            ShowPath();

            PathLabel.Text = "Enter the Model or BusinessLayer path:";

            UserInput.Text = string.Empty;
            UserInput.Enabled = false;
        }
        else
        {
            // set back the text
            GetOutput.Text = "Get the Result";
            _getOutputLabel = "Get the Result";

            //GenerateIGOptions.Visible = false;
            PathLabel.Text = "or Enter file/contarct path:";

            UserInput.Text = _oldUserInput;
        }
    }

    private void IsModel_CheckedChanged(object sender, EventArgs e)
    {
        Consts.UserSettings.RCompareSelectedOption = 1;

        if (IsModel.Checked)
            Input2Label.Text = "Input 2 (New Service):";
        else
            Input2Label.Text = "Input 2:";
    }

    private void MoveCommonSettingBtn_CheckedChanged(object sender, EventArgs e)
    {
        //if (MoveCommonSettingBtn.Checked)
        if (true)
        {
            GetOutput.Text = "Move";
            _getOutputLabel = "Move";

            PathLabel.Text = "Enter your solution path:";

            // save the text before we clean it
            _oldUserInput = UserInput.Text;

            ShowPath();

            UserInput.Text = string.Empty;
            UserInput.Enabled = false;
        }
        else
        {
            PathLabel.Text = "or Enter file/contarct path:";

            GetOutput.Text = "Get the Result";
            _getOutputLabel = "Get the Result";
            UserInput.Text = _oldUserInput;
        }
    }

    private void GetPackageReferencesBtn_CheckedChanged(object sender, EventArgs e)
    {
        if (GetPackageReferencesBtn.Checked)
        {
            // save the text before we clean it
            _oldUserInput = UserInput.Text;

            StringBuilder sb = new();

            sb.AppendLine("This option is used to get all duplicated, already in Root, or already in Common Package References for each project in the sln");
            sb.AppendLine();
            sb.AppendLine("[Duplicated]                   : This mean this Package is duplicated in the .csproj");
            sb.AppendLine("[Already in Root]           : This mean this Package already in Root project. So, no need to add it.");
            sb.AppendLine("[Already in Common]  : This mean this Package already in Commin project. So, no need to add it.");

            UserInput.Text = sb.ToString();

            Consts.UserSettings.RFeatureCriteria = 9;

            ShowPath();

            PathLabel.Text = "Enter the Solution path (to get all PackageReferences for each .csproj):";

            UserInput.ReadOnly = true;
        }
        else
        {
            PathLabel.Text = "or Enter file/contarct path:";
            // set back the text
            UserInput.Text = _oldUserInput;
            UserInput.ReadOnly = false;
        }
    }

    private void DecryptTokenBtn_CheckedChanged(object sender, EventArgs e)
    {
        Consts.UserSettings.RConverterSelectedOption = 6;
    }

    private void Base2ImageBtn_CheckedChanged(object sender, EventArgs e)
    {
        Consts.UserSettings.RConverterSelectedOption = 7;

        if (Base2ImageBtn.Checked)
            ImageResult.Visible = true;
        else
            ImageResult.Visible = false;
    }

    private void Blob2ImageBtn_CheckedChanged(object sender, EventArgs e)
    {
        Consts.UserSettings.RConverterSelectedOption = 8;

        if (Blob2ImageBtn.Checked)
            ImageResult.Visible = true;
        else
            ImageResult.Visible = false;
    }

    private void AddBracketBox_CheckedChanged(object sender, EventArgs e)
    {
        if (GenerateIgsOptionRatBrn.Checked)
        {
            if (AddBracketBox.Checked)
                Consts.UserSettings.RIGSelectedOption = 3; // Both selected
            else
                Consts.UserSettings.RIGSelectedOption = 2; // only GenerateIgsOption selected
        }
        else
        {
            if (AddBracketBox.Checked)
                Consts.UserSettings.RIGSelectedOption = 1; // only AddBracket selected
            else
                Consts.UserSettings.RIGSelectedOption = 0; // None selected
        }
    }

    private void GenerateIgsOptionRatBrn_CheckedChanged(object sender, EventArgs e)
    {
        if (AddBracketBox.Checked)
        {
            if (GenerateIgsOptionRatBrn.Checked)
                Consts.UserSettings.RIGSelectedOption = 3; // Both selected
            else
                Consts.UserSettings.RIGSelectedOption = 1; // only AddBracket selected
        }
        else
        {
            if (GenerateIgsOptionRatBrn.Checked)
                Consts.UserSettings.RIGSelectedOption = 2; // only GenerateIgsOption selected
            else
                Consts.UserSettings.RIGSelectedOption = 0; // None selected
        }
    }

    private void CodeToSqlBtn_CheckedChanged(object sender, EventArgs e)
    {
        Consts.UserSettings.RConverterSelectedOption = 1;

        if (CodeToSqlBtn.Checked || SqlToCodeBtn.Checked)
            SqlCoverterOptions.Visible = true;
        else
            SqlCoverterOptions.Visible = false;
    }

    private void SqlToCodeBtn_CheckedChanged_1(object sender, EventArgs e)
    {
        Consts.UserSettings.RConverterSelectedOption = 2;

        if (SqlToCodeBtn.Checked)
            SqlCoverterOptions.Visible = true;
        else
            SqlCoverterOptions.Visible = false;
    }

    private void XmlToJsonBtn_CheckedChanged(object sender, EventArgs e) => Consts.UserSettings.RConverterSelectedOption = 3;

    private void JsonToXmlBtn_CheckedChanged(object sender, EventArgs e) => Consts.UserSettings.RConverterSelectedOption = 4;

    private void JsonToCsharpBtn_CheckedChanged(object sender, EventArgs e) => Consts.UserSettings.RConverterSelectedOption = 5;

    private void IsText_CheckedChanged(object sender, EventArgs e) => Consts.UserSettings.RCompareSelectedOption = 4;

    private void GenerateNdbJsonRadBtn_CheckedChanged(object sender, EventArgs e)
    {
        if (GenerateNdbJsonRadBtn.Checked)
        {
            Consts.UserSettings.RFeatureCriteria = 10;

            bool openFile = Consts.UserSettings.RNdbOpenFileCheckOption;

            OpenJsonBox.Checked = openFile;

            GetOutput.Text = "Generate";
            _getOutputLabel = "Generate";

            GenerateJsonOptions.Visible = true;

            // save the text before we clean it
            _oldUserInput = UserInput.Text;

            ShowPath();

            PathLabel.Text = "Enter the Model or BusinessLayer path:";

            UserInput.Text = string.Empty;
            UserInput.Enabled = false;
        }
        else
        {
            // set back the text
            GetOutput.Text = "Get the Result";
            _getOutputLabel = "Get the Result";

            GenerateJsonOptions.Visible = false;

            PathLabel.Text = "or Enter file/contarct path:";

            UserInput.Text = _oldUserInput;
        }
    }

    private void OpenJsonBox_CheckedChanged(object sender, EventArgs e) => Consts.UserSettings.RNdbOpenFileCheckOption = OpenJsonBox.Checked;

    private void WrittenByInput_TextChanged(object sender, EventArgs e)
    {
        Consts.UserSettings.WrittenByIG = WrittenByInput.Text;
    }

    private void panel1_Paint(object sender, PaintEventArgs e)
    {
        Panel p = (Panel)sender;

        ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.Gray, ButtonBorderStyle.Dotted);
    }

    private void GenApiBtn_CheckedChanged(object sender, EventArgs e)
    {
        Consts.UserSettings.RIGProjectTypeOption = 1; // Api
    }

    private void GenMicroBtn_CheckedChanged(object sender, EventArgs e)
    {
        Consts.UserSettings.RIGProjectTypeOption = 2; // Micro

        if (GenMicroBtn.Checked)
        {
            AddBracketBox.Visible = false;
            GenerateIgsOptionRatBrn.Visible = false;
        }
        else
        {
            AddBracketBox.Visible = true;
            GenerateIgsOptionRatBrn.Visible = true;
        }
    }

    private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
    {
    }

    private void VbToCharpBtn_CheckedChanged(object sender, EventArgs e)
    {
        Consts.UserSettings.RConverterSelectedOption = 9;

        if (VbToCharpBtn.Checked)
            ClassConversionOpt.Visible = true;
        else
            ClassConversionOpt.Visible = false;
    }

    private void PrepareXmlBtn_CheckedChanged(object sender, EventArgs e)
    {
    }

    private void UserInput_TextChanged(object sender, EventArgs e)
    {
        if (ConverterBtn.Checked && VbToCharpBtn.Checked && DecodeHelperService.IsTextCode(UserInput.Text))
        {
            if (DecodeHelperService.IsVBCode(UserInput.Text))
            {
                if (!_isVbCodePrev)
                {
                    appHome.LogWarning("Entered code is VB, converting from VB to C# class is enabled");
                    _isVbCodePrev = true;
                }
            }
            else
            {
                if (!_isCSharpCodePrev)
                {
                    appHome.LogWarning("Entered code is C#, preparing C# class is enabled");
                    _isCSharpCodePrev = true;
                }

            }
        }
        else
        {
            _isVbCodePrev = false;
            _isCSharpCodePrev = false;
        }
    }
}