using Dev.Assistant.Business.Core.Extensions;
using Dev.Assistant.Business.Core.Models;
using Dev.Assistant.Business.Core.Services;
using Dev.Assistant.Business.Core.Utilities;
using Dev.Assistant.Business.Decoder.Models;
using Dev.Assistant.Configuration;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using NetOffice.ExcelApi.Enums;
using NetOffice.ExcelApi.Tools.Contribution;
using System.Text;
using System.Text.RegularExpressions;
using Excel = NetOffice.ExcelApi;

// Error Code Start 7400

namespace Dev.Assistant.App.TasksBoard;

public partial class TasksBoardHome : UserControl
{
    private readonly AppHome _appHome;

    private readonly Regex _testDateRegex = new("[a-zA-Z0-9]+_[a-zA-Z0-9]+_V[0-9]+\\.txt", RegexOptions.IgnoreCase);
    private readonly Regex _igRegex = new("[a-zA-Z0-9]+_[a-zA-Z0-9]+_V[0-9]+\\.docx", RegexOptions.IgnoreCase);

    private string _defaultCriteriaLabel;
    private const string _prefixText = @"NICHQ\";
    private bool _isLoading = false;
    private List<int> criteriaValue = new();
    private List<CustomWorkItem> _myWorkItems = new();
    private DataGridView _report;

    public TasksBoardHome(AppHome appHome)
    {
        InitializeComponent();

        //PatCredentials credentials = new("", _pat);
        //_connection = new(new Uri(_collectionUri), credentials);

        _appHome = appHome;
        _defaultCriteriaLabel = "Enter the period when tasks state changed (Default 15 days):";

        // Get User Settings
        //UsernameInput.Text = Consts.UserSettings.NicHqUsername;
    }

    private void CheckBtn_Click(object sender, EventArgs e)
    {
        ////Event ev = GetCurrentEvent();

        //_appHome.LogEvent(ev, EventStatus.Clicked);

        //if (_isLoading)
        //    return;

        ////Clear();

        //try
        //{
        //    _appHome.LogEvent(ev, EventStatus.Succeed);
        //}
        //catch (DevAssistantException ex)
        //{
        //    _appHome.LogError(ex, ev, EventStatus.Failed);
        //}
        //catch (Exception ex)
        //{
        //    _appHome.LogError(new DevAssistantException(ex.Message, 7403));
        //    _appHome.LogEvent(ev, EventStatus.Failed, ex: ex);
        //}

        //_isLoading = false;
    }

    private void UpdateBtn_Click(object sender, EventArgs e)
    {
        try
        {
            string serviceName = "MicroGetPersonBasicInfo";

            TfvcHttpClient tfvcClient = DevOpsClient.QueryConnection.GetClient<TfvcHttpClient>();

            // /MicroServices/Core/Development
            var microFiles = tfvcClient.GetItemsAsync(DevOpsClient.MicroProjectName, DevOpsClient.GetMicroScopePath(serviceName)).Result;

            // Get C# files only
            microFiles = microFiles.Where(item => item.Path.EndsWith($"{serviceName}.cs") || item.Path.EndsWith($"{serviceName}Service.cs")).ToList();

            if (microFiles.Count != 1)
            {
                // TODO: throw ex.
            }

            var microServicePath = microFiles.Select(p => p.Path).First();

            var pathContent = tfvcClient.GetItemContentAsync(DevOpsClient.MicroProjectName, microServicePath, fileName: null,
                         includeContent: true,
                      versionDescriptor: new TfvcVersionDescriptor() { }).Result;

            using (StreamReader reader = new(pathContent, Encoding.Default, true))
            {
                string Code = reader.ReadToEnd();
            };
        }
        catch (Exception ex)
        {

            //throw;
        }

    }

    private void UpdateBtn_Click1(object sender, EventArgs e)
    {
        try
        {
 
            //var path1 = gitClient.GetTreeAsync(DevOpsClient.ProjectName, DevOpsClient.RepoName, "decd74fcf9257473237d147ba22b3e1e078e6d41", projectId: DevOpsClient.ProjectName, fileName: "MicroListCustomCards").Result;

            // "Nic.Apis.Banan/Models/Person/MicroListPersonHajjPermits.cs"
            //var path = gitClient.GetFilePathsAsync(DevOpsClient.ProjectName, DevOpsClient.RepoName).Result;

            int prId = 6064;

            var pr = DevOpsClient.GitClient.GetPullRequestAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, prId, includeWorkItemRefs: true).Result;

            var iterations = DevOpsClient.GitClient.GetPullRequestIterationsAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, prId, includeCommits: false).Result;

            var pathChanges = DevOpsClient.GitClient.GetPullRequestIterationChangesAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, prId, (int)iterations.Last().Id, compareTo: iterations.First().Id).Result.ChangeEntries.Select(change => change.Item.Path).ToList();

            //using (StreamReader reader = new(mainContracts, Encoding.Default, true))
            //{
            //    string a = reader.ReadToEnd();
            //};

            /*
             * To generate an IG we must have BL, Controller and Models
             */

            bool canGenerateIg = false;
            List<string> businessLayerPaths = pathChanges.Where(p => p.Contains("/BusinessLayer")).ToList();

            // This all services in PR that we can generate IGs
            List<NicService> prServiceList = new();

            foreach (var bl in businessLayerPaths)
            {
                var serviceInfo = FileService.GetServiceInfo(bl);

                // Chcek if pathChanges has the Model path for the service
                if (pathChanges.TryGetFirst(p => p.Contains($"{serviceInfo.Namespace}/Models/{serviceInfo.ControllerName}/{serviceInfo.ServiceName}.cs"), out var modelPath)) // "/Nic.Apis.Csmarc/Models/Person/GetPersonFullDetails.cs"
                {
                    // Now check if pathChanges has the Controller
                    if (pathChanges.TryGetFirst(p => p.Contains($"{serviceInfo.Namespace}/Controllers/{serviceInfo.ControllerName}Controller.cs"), out var controllerPath)) // "/Nic.Apis.Csmarc/Controllers/PersonController.cs",
                    {
                        canGenerateIg = true;
                        string branchName = pr.SourceRefName.Replace("refs/heads/", "");

                        // Get Model File -- START
                        NicServiceFile modelFile = new()
                        {
                            ServiceName = modelPath[modelPath.LastIndexOf("/")..].Replace(".cs", ""),
                            FilePath = new() { Path = modelPath, IsRemotePath = true, BranchName = branchName },
                            FileType = NicServiceFileType.Model
                        };

                        var pathContent = DevOpsClient.GitClient.GetItemContentAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, modelFile.FilePath.Path,
                            scopePath: null, includeContent: true, includeContentMetadata: true,
                            versionDescriptor: new GitVersionDescriptor() { Version = branchName, VersionType = GitVersionType.Branch }).Result;

                        using (StreamReader reader = new(pathContent, Encoding.Default, true))
                        {
                            modelFile.Code = reader.ReadToEnd();
                        };
                        // Get Model File -- END

                        // Get BusinessLayer File -- START
                        NicServiceFile blFile = new()
                        {
                            ServiceName = serviceInfo.ServiceName,
                            FilePath = new() { Path = bl, IsRemotePath = true },
                            FileType = NicServiceFileType.BusinessLayer
                        };

                        pathContent = DevOpsClient.GitClient.GetItemContentAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, blFile.FilePath.Path,
                          scopePath: null, includeContent: true, includeContentMetadata: true,
                          versionDescriptor: new GitVersionDescriptor() { Version = pr.SourceRefName.Replace("refs/heads/", ""), VersionType = GitVersionType.Branch }).Result;

                        using (StreamReader reader = new(pathContent, Encoding.Default, true))
                        {
                            blFile.Code = reader.ReadToEnd();
                        };
                        // Get BusinessLayer File -- END

                        // Get Controller File -- START
                        NicServiceFile controllerFile = new()
                        {
                            ServiceName = $"{serviceInfo.ControllerName}Controller",
                            FilePath = new() { Path = controllerPath, IsRemotePath = true, BranchName = branchName },
                            FileType = NicServiceFileType.Controller
                        };

                        pathContent = DevOpsClient.GitClient.GetItemContentAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, controllerFile.FilePath.Path,
                          scopePath: null, includeContent: true, includeContentMetadata: true,
                          versionDescriptor: new GitVersionDescriptor() { Version = pr.SourceRefName.Replace("refs/heads/", ""), VersionType = GitVersionType.Branch }).Result;

                        using (StreamReader reader = new(pathContent, Encoding.Default, true))
                        {
                            controllerFile.Code = reader.ReadToEnd();
                        };
                        // Get Controller File -- END

                        // TODO: get root code.
                        //if (lines[i].Contains(" Root.BusinessLayer") || lines[i].StartsWith("Root.BusinessLayer") || (lines[i].Contains(".ApiBusiness(") && rootIsImported))
                        //{
                        //if (blFile.Code.Contains(" Root.BusinessLayer") || blFile.Code.StartsWith("Root.BusinessLayer") || (blFile.Code.Contains(".ApiBusiness(") && blFile.Code.Contains("Nic.Apis.Root.BusinessLayer")))
                        //{
                        //    rootControllers.Add(service.ControllerName);

                        //    bool isSuccess = false;

                        //    foreach (var controller in rootControllers)
                        //    {
                        //        string rootPath = GetRootPath(lines, i, path, controller, out string serviceName);

                        //        if (File.Exists(rootPath))
                        //        {
                        //            apiExceptions.AddRange(GetApiExceptions(rootPath, "Root", controller, out List<DevAssistantException> exs, options));

                        //            if (options.IsReport)
                        //                devAssistantExceptions.AddRange(exs);

                        //            isSuccess = true;

                        //            break;
                        //        }
                        //        else
                        //        {
                        //            Log.Logger.Error($"Couldn't open \"Root.BusinessLayer.{service.ControllerName}.{serviceName}\" with the following path: {rootPath}");
                        //        }
                        //    }

                        //    if (!isSuccess)
                        //    {
                        //        throw new DevAssistantException($"Couldn't open Root.BusinessLayer", 1601);
                        //    }

                        //}

                        prServiceList.Add(new NicService
                        {
                            Namespace = serviceInfo.Namespace,
                            ContractCode = serviceInfo.ContractCode,
                            ControllerName = serviceInfo.ControllerName,
                            ServiceName = serviceInfo.ServiceName,
                            //Files = new() { modelFile, blFile, controllerFile },
                            BusinessLayer = blFile,
                            Controller = controllerFile,
                            Model = modelFile,
                        });
                    }
                }
            }

            //var pat3 = gitClient.GetItemContentAsync(DevOpsClient.ProjectName, DevOpsClient.RepoName, "Nic.Apis.Csmarc/BusinessLayer/Person/GetPersonFullDetails.cs", scopePath: null, includeContent: true, includeContentMetadata: true, versionDescriptor: new GitVersionDescriptor() { Version = "CR/418310", VersionType = GitVersionType.Branch }).Result;

            ////var path2 = gitClient.GetItemContentAsync(DevOpsClient.ProjectName, DevOpsClient.RepoName, "Nic.Apis.SmartCity/BusinessLayer/Person/MicroGetPersonBasicInfo.cs", scopePath: null, includeContent: true, includeContentMetadata: true, versionDescriptor: new GitVersionDescriptor() { Version = "refs/heads/CR/418310", VersionType = GitVersionType.Branch }).Result;

            ////using StreamReader reader = new(pat3, Encoding.Default, true);

            ////string a = reader.ReadToEnd();

            //FileDiffsCriteria criteria = new()
            //{
            //    BaseVersionCommit = "ed97dba310877f45962b71f241caacb6b0061760",
            //    TargetVersionCommit = "a840a759a448d861f2472dc77496e3c1b3034603",

            //    FileDiffParams = new[] { new FileDiffParams()
            //    {
            //        Path = "Nic.Apis.Banan/Models/Person/MicroListPersonHajjPermits.cs"
            //    },
            //    }
            //};

            //string json = JsonConvert.SerializeObject(pathChanges, Formatting.Indented);
            //File.WriteAllText(@"C:\Users\aayahya\Documents\pr.json", json);

            //var commit = gitClient.GetFileDiffsAsync(criteria, DevOpsClient.ProjectName, DevOpsClient.RepoName).Result;
        }
        catch (Exception ex)
        {
            _appHome.LogError(ex.ToString());
        }
    }

    private void devButton1_Click(object sender, EventArgs e)
    {
        string progId = @"C:\Project\Personal\DevAssistant\Dev.Assistant.App\Assets\Copy of Web Services Report V1 - August 2023 - Remarks.xlsx";

        // start excel and turn off msg boxes
        using Excel.Application excelApplication = new();
        excelApplication.DisplayAlerts = false;

        // create a utils instance, not need for but helpful to keep the lines of code low
        CommonUtils utils = new(excelApplication);

        //// add a new workbook
        //Excel.Workbook workBook = excelApplication.Workbooks.Add();
        //Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Worksheets[1];

        using Excel.Workbook workBook = excelApplication.Workbooks.Add(progId); ;
        using Excel.Workbook logsBook = excelApplication.Workbooks.Add(@"C:\Project\Personal\DevAssistant\Dev.Assistant.App\Assets\WsTransactions (Logs).xlsx");

        using Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Worksheets[1];
        using Excel.Worksheet logsSheet = (Excel.Worksheet)logsBook.Worksheets[1];

        List<TransactionLogInfo> logs = new();

        for (int i = 1; i < 3056; i++)
        {
            var row = logsSheet.Range($"A{i}", $"D{i}");
            var values = (row.Value as Array).Cast<string>().ToList();

            if (values[0].Contains("MethodName"))
                continue;

            TransactionLogInfo log = new()
            {
                MethodName = values.At(0),
                ContractName = values.At(1),
                CountActions = values.At(2).Split(":")[1],
                MethodDesc = values.At(3),
            };

            logs.Add(log);
        }

        var contracts = logs.GroupBy(x => x.ContractName).ToList();

        string prevContractName = string.Empty;

        List<string> methodInfos = new();
        List<string> contractNames = new();

        int allRowsAdded = 0;
        int rowsAdded = 0;

        int lastIndex = 0;// tobe deleted

        List<RowInfo> rows = new();

        var prevCell1 = string.Empty;
        var prevCell6 = string.Empty;
        var prevCell7 = string.Empty;

        List<TransactionLogInfo> serviceDescs = new();

        for (int i = 1; i < 2559 + rowsAdded; i++)
        {
            lastIndex = i;
            //if (rowsAdded > 0)
            //{
            //    i += rowsAdded;
            //    rowsAdded = 0;
            //}

            var row = workSheet.Range($"A{i}", $"H{i}");

            var values = (row.Value as Array).Cast<string>().ToList();

            if (values.At(0)?.Contains("نوع القطاع") ?? false)
                continue;

            var cell1 = values[0];
            var cell6 = values[5];
            var cell7 = values[6];

            var methodName = values[1] ?? values[2];
            var contractName = values[4];

            var serviceDesc = values[3];

            if (!string.IsNullOrWhiteSpace(serviceDesc) && !serviceDescs.Exists(i => i.MethodName.Contains(methodName) || methodName.Contains(i.MethodName)))
            {
                serviceDescs.Add(new TransactionLogInfo { MethodName = methodName, MethodDesc = serviceDesc });
            }

            if (string.IsNullOrWhiteSpace(methodName) || string.IsNullOrWhiteSpace(contractName))
                continue;

            if (i == 2550)
            {
            }

            if (prevContractName.Equals(contractName.ToLower()))
            {
                methodInfos.Add(methodName);
            }
            else
            {
                if (contracts.TryGetFirst(i => i.Key.ToLower() == prevContractName, out var contractMethods))
                {
                    foreach (var method in contractMethods)
                    {
                        if (!methodInfos.Contains(method.MethodName))
                        {
                            //int rowFrom = i;
                            //int rowTo = i;

                            //workSheet.Range($"A{rowFrom}:H{rowTo}").Insert(XlInsertShiftDirection.xlShiftDown);

                            //workSheet.Range($"A{rowFrom}:H{rowTo}").Cells[1].Value = cell1;
                            //workSheet.Range($"A{rowFrom}:H{rowTo}").Cells[2].Value = method.MethodName;
                            //workSheet.Range($"A{rowFrom}:H{rowTo}").Cells[3].Value = method.MethodName;
                            //workSheet.Range($"A{rowFrom}:H{rowTo}").Cells[4].Value = method.MethodDesc;
                            //workSheet.Range($"A{rowFrom}:H{rowTo}").Cells[5].Value = contractName;
                            //workSheet.Range($"A{rowFrom}:H{rowTo}").Cells[6].Value = cell6;
                            //workSheet.Range($"A{rowFrom}:H{rowTo}").Cells[7].Value = cell7;
                            //workSheet.Range($"A{rowFrom}:H{rowTo}").Cells[8].Value = "New - (New service)";

                            //rowsAdded++;
                            //allRowsAdded++;

                            rows.Add(new RowInfo(prevCell1.Trim(), method.MethodName.Trim(), method.MethodName.Trim(), method.MethodDesc, method.ContractName.Trim(), prevCell6.Trim(), prevCell7, "New - (New service)"));
                        }
                    }

                    //break;
                }

                prevContractName = contractName.ToLower();
                prevCell1 = cell1;
                prevCell6 = cell6;
                prevCell7 = cell7;

                methodInfos = new();
                methodInfos.Add(methodName);

                contractNames.Add(contractName.ToLower());
            }
        }

        // must be eqaul to lastIndex
        var sum = 2538 + allRowsAdded; // 2950

        var logsContracts = contracts.Select(i => i.Key.ToLower()).ToList();

        rowsAdded = 0;

        foreach (var cont in logsContracts)
        {
            if (!contractNames.Contains(cont))
            {
                if (contracts.TryGetFirst(i => i.Key.ToLower() == cont, out var contractMethods))
                {
                    foreach (var method in contractMethods)
                    {
                        //int rowFrom = lastIndex + rowsAdded;
                        //int rowTo = lastIndex + rowsAdded;

                        //workSheet.Range($"A{rowFrom}:H{rowTo}").Insert(XlInsertShiftDirection.xlShiftDown);

                        ////workSheet.Range($"A{rowFrom}:H{rowTo}").Cells[1].Value = cell1;
                        //workSheet.Range($"A{rowFrom}:H{rowTo}").Cells[2].Value = method.MethodName;
                        //workSheet.Range($"A{rowFrom}:H{rowTo}").Cells[3].Value = method.MethodName;
                        //workSheet.Range($"A{rowFrom}:H{rowTo}").Cells[4].Value = method.MethodDesc;
                        //workSheet.Range($"A{rowFrom}:H{rowTo}").Cells[5].Value = method.ContractName;
                        ////workSheet.Range($"A{rowFrom}:H{rowTo}").Cells[6].Value = cell6;
                        ////workSheet.Range($"A{rowFrom}:H{rowTo}").Cells[7].Value = cell7;
                        //workSheet.Range($"A{rowFrom}:H{rowTo}").Cells[8].Value = "New - (New contract)";

                        //rowsAdded++;
                        //allRowsAdded++;

                        rows.Add(new RowInfo("", method.MethodName.Trim(), method.MethodName.Trim(), method.MethodDesc, method.ContractName.Trim(), "", "", "New - (New contract)"));
                    }

                    //break;
                }
            }
        }

        lastIndex++;
        rowsAdded = 0; // 334

        foreach (var row in rows)
        {
            int rowFrom = lastIndex;
            int rowTo = lastIndex;

            workSheet.Range($"A{rowFrom}:H{rowTo}").Insert(XlInsertShiftDirection.xlShiftDown);

            var desc = row.Cell4.IsArabic() ? row.Cell4.Trim() : string.Empty;

            if (string.IsNullOrWhiteSpace(desc))
            {
                var serviceName = row.Cell2.ToLower().Replace($"for{row.Cell5.ToLower()}", "");

                if (row.Cell5.ToLower().EndsWith("npv"))
                {
                    var temp = row.Cell5[..^3];
                    serviceName = row.Cell2.ToLower().Replace($"for{temp.ToLower()}", "");
                }

                if (serviceDescs.TryGetFirst(i => i.MethodName.ToLower().Contains(serviceName), out var outDesc))
                {
                    desc = outDesc.MethodDesc.Trim();
                }
                else if (serviceDescs.TryGetFirst(i => serviceName.Contains(i.MethodName.ToLower()), out var outDesc1))
                {
                    desc = outDesc1.MethodDesc.Trim();
                }
                else
                {
                    desc = row.Cell4.Trim();
                }
            }

            // MicroListPersonHolyPermits
            // MicroListPersonHolyPermits
            workSheet.Range($"A{rowFrom}:H{rowTo}").Cells[1].Value = row.Cell1;
            workSheet.Range($"A{rowFrom}:H{rowTo}").Cells[2].Value = row.Cell2;
            workSheet.Range($"A{rowFrom}:H{rowTo}").Cells[3].Value = row.Cell3;
            workSheet.Range($"A{rowFrom}:H{rowTo}").Cells[4].Value = desc;
            workSheet.Range($"A{rowFrom}:H{rowTo}").Cells[5].Value = row.Cell5;
            workSheet.Range($"A{rowFrom}:H{rowTo}").Cells[6].Value = row.Cell6;
            workSheet.Range($"A{rowFrom}:H{rowTo}").Cells[7].Value = row.Cell7;
            workSheet.Range($"A{rowFrom}:H{rowTo}").Cells[8].Value = row.Cell8;

            rowsAdded++;
        }

        //var cellAddressFrom = ((NetOffice.ExcelApi.Range)workSheet.Cells[6, 5]).Address;
        //var cellAddressTo = ((NetOffice.ExcelApi.Range)workSheet.Cells[10, 10]).Address;

        //workSheet.Range(cellAddressFrom, cellAddressTo).Insert(XlInsertShiftDirection.xlShiftDown);

        // save the book
        string workbookFile = utils.File.Combine(@"C:\Project\Personal\DevAssistant\Dev.Assistant.App\Assets", "Result", DocumentFormat.Normal);
        workBook.SaveAs(workbookFile);

        // close excel and dispose reference
        excelApplication.Quit();
        excelApplication.Dispose();
    }

    private void devButton2_Click(object sender, EventArgs e)
    {
        var gitClient = DevOpsClient.QueryConnection.GetClient<GitHttpClient>();

        var item = gitClient.GetItemsAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, scopePath: null, includeContentMetadata: true, includeLinks: true,
            versionDescriptor: new GitVersionDescriptor()
            {
                Version = "main",
                VersionType = GitVersionType.Branch
            }).Result;

        // "1419b7ca550811e3b1a81f2cfe56c39ad363371b"

        // get all main contracts
        var mainContracts = gitClient.GetTreeAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, item.First().ObjectId, projectId: null, recursive: null).Result;

        List<string> contracts = mainContracts.TreeEntries.Where(x =>
        x.RelativePath.StartsWith("Nic.Apis.")
        && !Consts.ExcludedContracts.Contains(x.RelativePath))
            .Select(x => x.RelativePath)
            .ToList();
    }
}

public record TransactionLogInfo
{
    public string MethodName { get; set; }

    public string ContractName { get; set; }

    public string CountActions { get; set; }

    public string MethodDesc { get; set; }
}

public record RowInfo(string Cell1, string Cell2, string Cell3, string Cell4, string Cell5, string Cell6, string Cell7, string Cell8);