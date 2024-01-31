using AuthServer;
using Dev.Assistant.Business.Core.DevErrors;
using Dev.Assistant.Business.Core.Enums;
using Dev.Assistant.Business.Core.Extensions;
using Dev.Assistant.Business.Core.Models;
using Dev.Assistant.Business.Core.Services;
using Dev.Assistant.Business.Core.Utilities;
using Dev.Assistant.Business.Decoder.Models;
using Dev.Assistant.Business.Decoder.Services;
using Dev.Assistant.Business.Generator.Models;
using Dev.Assistant.Configuration;
using Dev.Assistant.Configuration.Models;
using Serilog;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Xceed.Document.NET;
using Xceed.Words.NET;
using Json = Newtonsoft.Json;
using NF = NetOffice.WordApi;

namespace Dev.Assistant.Business.Generator.Services;

// TODO: use the new StringReplaceTextOptions.

/// <summary>
/// Service for handling IG operations.
/// </summary>
public class IntegrationGuideService
{
    #region Private Members

    private static UserReturnModel _contractUser = null;

    #endregion Private Members

    #region Public Methods


    /// <summary>
    /// This to generate IG for Api or JSON for NDB only
    /// </summary>
    /// <param name="projectType"></param>
    /// <param name="services"></param>
    /// <param name="idVersion"></param>
    /// <exception cref="DevAssistantException"></exception>
    public static void GenerateApiIG(GenerateIGArgs args, LogArgs log, Form checkSpellingDialog)
    {
        string saveFilePath = string.Empty;
        int numOfGenerated = 0;

        try
        {
            foreach (var service in args.Services)
            {
                log.LogEvent(DevEvents.RGenerateApiIG, EventStatus.Called, $"Start with service: {service.ContractCode}.{service.ControllerName}.{service.ServiceName}");

                Log.Logger.Information("Start with service: {a}.{b}.{c}", service.ContractCode, service.ControllerName, service.ServiceName);

                if (args.Services.Count > 1)
                    log.LogInfo($"[{numOfGenerated + 1} of {args.Services.Count}] Starting with: {service.ServiceName}");

                // PrpareServicen for API IG
                ValidateServicenfoForIG(service, false, out var contrllerDesc, out var apiExceptions, out _);

                //Log.Logger.Information($"SerivceInfo: {contractName}.{controllerName}.{serviceName}");

                // Get Model code
                string code = FileService.GetCodeByFile(new GetCodeByFileReq
                {
                    Code = service.Model.Code,
                    IsRemotePath = service.Model.FilePath.IsRemotePath,
                    Path = service.Model.FilePath.Path,
                    BranchName = service.Model.FilePath.BranchName
                });

                string expectedNameSpace;

                if (service.Namespace.IsNamespaceNpv())
                {
                    expectedNameSpace = $"Nic.Apis.Npv.Models.{service.ControllerName}";
                }
                else
                {
                    expectedNameSpace = $"Nic.Apis.{service.ContractCode}.Models.{service.ControllerName}";
                }

                DecodeHelperService.ValidateNamespace(code, expectedNameSpace);

                List<ClassModel> classes = ModelExtractionService.GetClassesByCode(code, new()
                {
                    CheckSpellingAndRules = true,
                    ShowErrorCode = true,
                    DataTypeAsPascalCase = true,
                    ThrowException = false,
                    ControllerName = service.ControllerName,
                    AddStarForRequiredProp = false // Here we don't need to add * to prop name. In GenerateIG in FileService we will use IsRequired
                });

                if (!classes.Any(c => c.Name.Equals($"{service.ServiceName}Req", StringComparison.InvariantCultureIgnoreCase)))
                {
                    throw new DevAssistantException($"Couldn't found request DTO with the following name \"{service.ServiceName}Req\"", 3002);
                }

                if (!classes.Any(c => c.Name.Equals($"{service.ServiceName}Res", StringComparison.InvariantCultureIgnoreCase)))
                {
                    throw new DevAssistantException($"Couldn't found response DTO with the following name \"{service.ServiceName}Res\"", 3003);
                }

                // Check for Spelling and Rules
                if (args.ProjectType is ProjectType.RestApi)
                {

                    DialogResult dialogResult = DecodeHelperService.CheckSpellingAndRules(new()
                    {
                        Models = classes,
                        Event = DevEvents.RGenerateApiIG,
                        AlwaysShowDialog = false,
                        Namespace = "FromGenerateApiMethod",
                    }, log, checkSpellingDialog);

                    if (dialogResult == DialogResult.Retry)
                    {
                        // Read the code again
                        service.Model.Code = FileService.GetCodeByFile(new GetCodeByFileReq
                        {
                            Code = "", // To read it again
                            IsRemotePath = service.Model.FilePath.IsRemotePath,
                            Path = service.Model.FilePath.Path,
                            BranchName = service.Model.FilePath.BranchName,
                        });

                        // Get Model File -- END

                        // Read classes again in case remarks was resolved
                        classes = ModelExtractionService.GetClassesByCode(service.Model.Code, new()
                        {
                            CheckSpellingAndRules = true,
                            ShowErrorCode = true,
                            DataTypeAsPascalCase = true,
                            ThrowException = false,
                            ControllerName = service.ControllerName,
                            AddStarForRequiredProp = false
                        });
                    }
                    else if (dialogResult == DialogResult.Cancel)
                    {
                        log.LogWarning("Successfully canceled. Stop the process of creating the IG(s)");

                        break;
                    }
                }

                (List<Property> requestDto, List<Property> responseDto) = DecodeHelperService.PrepareReqAndResDto(service.ServiceName, classes, args.AddBracket, args.ProjectType);

                // C:\Project\API\Development\Nic.Apis.Border\BusinessLayer\Person\CheckTravelerBiometricStatus.cs
                // C:\Project\API\ApiDev\Nic.Apis.Bog\Controllers\PersonController.cs

                if (requestDto.Count is 0 || responseDto.Count is 0)
                {
                    throw new DevAssistantException($"The request table or response table is empty! Couldn't generate the table. [Req: {requestDto.Count}, Res: {responseDto.Count}]", 3004);
                }

                //log.LogTrace("Calling AuthServer to get contract Name and NameTr -- START");

                string username = $"Nic_{service.ContractCode}";

                if (args.ProjectType is ProjectType.RestApi && (_contractUser is null || (!_contractUser.UserName?.Equals(username, StringComparison.CurrentCultureIgnoreCase) ?? true)))
                {
                    log.LogInfo("Calling AuthServer. Please wait....");

                    try
                    {
                        log.LogEvent(DevEvents.CGetUserInfoAuthServer, EventStatus.Clicked);

                        _contractUser = AuthService.GetAccountInfo(username);

                        log.LogEvent(DevEvents.CGetUserInfoAuthServer, EventStatus.Succeed);

                        log.LogSuccess($"The user found. \"{_contractUser.NameTr}\"");
                    }
                    catch (Exception ex)
                    {
                        log.LogError("An exception has occurred!");
                        log.LogEvent(DevEvents.CGetUserInfoAuthServer, EventStatus.Failed, ex: ex);

                        if (ex.InnerException is ApiException<string> apiException && apiException.Result is not null)
                        {
                            log.LogError(apiException.Result + $". Nic_{service.ContractCode}");

                            _contractUser = new()
                            {
                                Name = "Contract Name in Arabic",
                                NameTr = "Contract Name in English"
                            };
                        }
                        else
                        {
                            throw new DevAssistantException(ex.Message, 3005);
                        }
                    }
                }

                IGInfo igInfo = new()
                {
                    UserInfo = _contractUser,
                    ServiceInfo = new()
                    {
                        ControllerName = service.ControllerName,
                        ServiceDescEn = contrllerDesc,
                        ServiceName = service.ServiceName
                    },

                    WrittenBy = args.WrittenBy,
                    IGVersion = args.IgVersion,
                    RequestDto = requestDto,
                    ResponseDto = responseDto,

                    ContractCode = service.ContractCode,
                    ApiErrors = apiExceptions,
                };

                string fileName;

                if (args.ProjectType is ProjectType.NdbJson)
                    fileName = igInfo.ServiceInfo.ServiceName;
                else
                    fileName = $"{igInfo.ContractCode}_{igInfo.ServiceInfo.ServiceName}_V{igInfo.IGVersion}";

                string currentPath;

                if (args.GenerateMultipleIGs && args.ProjectType == ProjectType.RestApi)
                {
                    if (string.IsNullOrEmpty(saveFilePath))
                    {
                        saveFilePath = FileService.FolderPickerDialog(title: "Where to save your IGs files:",
                             initialDirectory: Consts.UserSettings.IGPathFolder);

                        if (string.IsNullOrWhiteSpace(saveFilePath))
                        {
                            throw new DevAssistantException("The returned path is empty or invalid.", 3006);
                        }

                        Consts.UserSettings.IGPathFolder = saveFilePath;
                    }

                    currentPath = Path.Combine(saveFilePath, fileName);
                }
                else
                {
                    string title;
                    string filter;

                    if (args.ProjectType == ProjectType.NdbJson)
                    {
                        title = $"Where to save Ndb json {fileName}";
                        filter = "Json files (*.json)|*.json|All files (*.*)|*.*";
                    }
                    else
                    {
                        title = $"Where to save your IG {fileName}";
                        filter = "Word files (*.docx)|*.docx|All files (*.*)|*.*";
                    }

                    currentPath = FileService.GetPathDialog(title, filter, fileName, initialDirectory: Consts.UserSettings.IGPathFolder);

                    if (string.IsNullOrWhiteSpace(currentPath))
                    {
                        throw new DevAssistantException("The returned path is empty or invalid.", 3007);
                    }

                    Consts.UserSettings.IGPathFolder = currentPath;
                }

                bool openFile = args.Services.Count == 1;

                if (args.ProjectType == ProjectType.RestApi)
                {
                    GenerateApiIGFile(igInfo, currentPath, openFile);
                }
                else if (args.ProjectType == ProjectType.NdbJson)
                {
                    GenerateNdbJsonFile(igInfo, currentPath);

                    if (args.OpenJsonAfterCreate)
                        Process.Start(new ProcessStartInfo(currentPath) { UseShellExecute = true });
                }

                numOfGenerated++;
            }

            if (numOfGenerated > 0 && args.Services.Count > 1)
            {
                if (args.Services.Count == numOfGenerated)
                {
                    log.LogSuccess($"All IGs generated successfully! Open folder...");
                    Log.Logger.Information("All IGs generated successfully! Open folder...");
                }
                else
                {
                    log.LogWarning($"{numOfGenerated} of {args.Services.Count} IGs generated successfully! Open folder...");
                    Log.Logger.Information("{a} of {b} IGs generated successfully! Open folder...", numOfGenerated, args.Services.Count);
                }

                // Open IG file
                ProcessStartInfo process = new()
                {
                    Arguments = saveFilePath,
                    FileName = "explorer.exe",
                };

                using var p = Process.Start(process);
                p.Dispose();
            }
        }
        catch (DevAssistantException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "An unexpected error occurred in GenerateApiIG");

            log.LogError("Stop the process of creating the IG(s)");

            if (numOfGenerated > 0 && args.Services.Count > 1 && !string.IsNullOrWhiteSpace(saveFilePath))
            {
                if (args.Services.Count == numOfGenerated)
                {
                    log.LogInfo($"All IGs generated successfully! Open folder...");
                    Log.Logger.Information("All IGs generated successfully! Open folder...");
                }
                else
                {
                    log.LogWarning($"{numOfGenerated} of {args.Services.Count} IGs generated successfully! Open folder...");
                    Log.Logger.Information("{a} of {b} IGs generated successfully! Open folder...", numOfGenerated, args.Services.Count);
                }

                // Open IG file
                ProcessStartInfo process = new()
                {
                    Arguments = saveFilePath,
                    FileName = "explorer.exe",
                };

                using var p = Process.Start(process);
                p.Dispose();
            }

            throw;
        }
    }



    public static void GenerateMicroIG(GenerateIGArgs args, LogArgs log, Form checkSpellingDialog)
    {

        string saveFilePath = string.Empty;

        try
        {
            /*
             * For Example, if we have the following paths (All possible path to micro):
             *   - C:\Project\MicroServices\Core\Development\MicroListCriminalEvents\ObjectModel\MicroListCriminalEventsInputs.cs
             *   - C:\Project\MicroServices\Core\Development\MicroListCriminalEvents\ObjectModel\MicroListCriminalEventsOutputs.cs
             *   - C:\Project\MicroServices\Core\Development\MicroListCriminalEvents\MicroListCriminalEventsService.cs
             *   - C:\Project\MicroServices\Core\Development\MicroListCriminalEvents
             *   - C:\Project\MicroServices\Core\Development\MicroListCriminalEvents\MicroListCriminalEvents.csproj
             *  
             *  
             * The paths will be:
             *  microRootPath    = C:\Project\MicroServices\Core\Development\MicroListCriminalEvents
             *  microServicePath = C:\Project\MicroServices\Core\Development\MicroListCriminalEvents\MicroListCriminalEventsService.cs
             * 
             */

            string microRootPath = args.MicroPath.Replace("/", "\\").TrimEnd('\\');
            string microServicePath = args.MicroPath.Replace("/", "\\").TrimEnd('\\');

            Log.Logger.Information("Micro root path before: {a}", microRootPath);

            if (string.IsNullOrWhiteSpace(microRootPath) || !microRootPath.Contains('\\'))
                throw DevErrors.Generator.E3001InvalidPath;

            string microName = string.Empty;

            // Get Micro Root Folder Name & Micro Name -- START
            if (microRootPath.EndsWith(".cs") || microRootPath.EndsWith(".csproj") || microRootPath.EndsWith(".vspscc"))
            {
                // This MicroPath                                          . In case file is ObjectModel
                microRootPath = microRootPath[0..microRootPath.LastIndexOf('\\')].Replace("\\ObjectModel", "");

                // by Now the path must be C:\Project\MicroServices\Core\Development\MicroListCriminalEvents
                microName = microRootPath[(microRootPath.LastIndexOf('\\') + 1)..];
            }
            else
            {
                // In this case we assum the path is Micro root folder Path. C:\Project\MicroServices\Core\Development\MicroListCriminalEvents

                microName = microRootPath[(microRootPath.LastIndexOf('\\') + 1)..];
            }
            // Get Micro Root Folder Name & Micro Name -- END

            // Validate micro name
            if (ValidationService.IsExcludedMicro(microName))
            {
                throw new DevAssistantException($"Invalid Micro Name. Please check {microName}", 3008);
            }

            // Get & Preapre Micro Service File Path -- START
            // if it is not the service path, we get the service file under root folder of the micro
            if (!microServicePath.EndsWith(".cs") || microServicePath.Contains("\\ObjectModel"))
            {
                //microServicePath = $"{microRootPath}/{microName}Service.cs";

                string[] microFiles = Directory.GetFiles(microRootPath, "*.cs");

                if (microFiles.Length == 0)
                {
                    throw new DevAssistantException($"No files found under {microRootPath}. Please check micro path!", 3009);
                }

                // if there is more than 1, then try to get the service "Service.cs"
                if (microFiles.Length > 1)
                {
                    microFiles = microFiles.Where(path => path.EndsWith("Service.cs")).ToArray();

                    if (microFiles.Length != 1)
                    {
                        throw new DevAssistantException($"No matched files found under {microRootPath}. Trying to get micro service file path, please check the name of the micro service", 3010);
                    }
                }

                microServicePath = microFiles.First();

            }

            if (!File.Exists(microServicePath))
            {
                throw new DevAssistantException($"The given path is not existing on disk. Couldn't open micro service file {microServicePath}", 3010);
            }
            // Get & Preapre Micro Service File Path -- END

            // Get & Preapre ObjectModel File Paths -- START
            string inputPath = $"{microRootPath}\\ObjectModel\\{microName}Inputs.cs";
            string outputPath = $"{microRootPath}\\ObjectModel\\{microName}Outputs.cs";

            if (!File.Exists(inputPath))
                throw new DevAssistantException($"Couldn't found Micro's input object. The given path is not existing on disk {inputPath}", 3011);

            if (!File.Exists(outputPath))
                throw new DevAssistantException($"Couldn't found Micro's output object. The given path is not existing on disk {outputPath}", 3012);
            // Get & Preapre ObjectModel File Paths -- END

            Log.Logger.Information("Micro root path after: {a}", microRootPath);
            Log.Logger.Information("Micro service path after: {a}", microServicePath);

            string contractName = "Micro";

            // For CheckSpellingAndRules
            List<ClassModel> models = new();

            // For Dtos
            List<ClassModel> classes = new();

            Log.Logger.Information("SerivceInfo: {a}.{b}.{c}", contractName, microName);

            // Inputs -- START
            string inputCode = FileService.GetCodeByFile(inputPath);

            DecodeHelperService.ValidateNamespace(inputCode, $"{microName}.ObjectModel");

            models = ModelExtractionService.GetClassesByCode(inputCode, new() { ProjectType = ProjectType.Micro, CheckSpellingAndRules = true, ShowErrorCode = true });

            classes = ModelExtractionService.GetClassesByCode(inputCode, new()
            {
                ProjectType = ProjectType.Micro,
                CheckSpellingAndRules = true,
                ShowErrorCode = true,
                DataTypeAsPascalCase = true,
                ThrowException = false,
                //ControllerName = categoryName,
                AddStarForRequiredProp = false // Here we don't need to add * to prop name. In GenerateIG in FileService we will use IsRequired
            });

            // Inputs -- END

            // Outputs -- START
            string outputCode = FileService.GetCodeByFile(outputPath);

            DecodeHelperService.ValidateNamespace(outputCode, $"{microName}.ObjectModel");

            // Get output class(es)
            models.AddRange(ModelExtractionService.GetClassesByCode(outputCode, new() { ProjectType = ProjectType.Micro, CheckSpellingAndRules = true, ShowErrorCode = true }));

            classes.AddRange(ModelExtractionService.GetClassesByCode(outputCode, new()
            {
                ProjectType = ProjectType.Micro,
                CheckSpellingAndRules = true,
                ShowErrorCode = true,
                DataTypeAsPascalCase = true,
                ThrowException = false,
                //ControllerName = categoryName,
                AddStarForRequiredProp = false
            }));
            // Outputs -- END

            // CheckSpellingAndRules -- START
            log.LogInfo($"Checking spelling & rules...");

            string nameSpace = $"{contractName}.{microName}";

            DialogResult dialogResult = DecodeHelperService.CheckSpellingAndRules(new()
            {
                Models = classes,
                Event = DevEvents.RGenerateApiIG,
                AlwaysShowDialog = false,
                Namespace = "FromGenerateMicroMethod",
            }, log, checkSpellingDialog, ProjectType.Micro);


            if (dialogResult == DialogResult.Retry)
            {
                // Read the code again
                inputCode = FileService.GetCodeByFile(inputPath);
                outputCode = FileService.GetCodeByFile(outputPath);

                // Get Model File -- END

                // Read classes again in case remarks was resolved
                classes = ModelExtractionService.GetClassesByCode(inputCode, new()
                {
                    ProjectType = ProjectType.Micro,
                    CheckSpellingAndRules = true,
                    ShowErrorCode = true,
                    DataTypeAsPascalCase = true,
                    ThrowException = false,
                    //ControllerName = categoryName,
                    AddStarForRequiredProp = false
                });

                classes.AddRange(ModelExtractionService.GetClassesByCode(outputCode, new()
                {
                    ProjectType = ProjectType.Micro,
                    CheckSpellingAndRules = true,
                    ShowErrorCode = true,
                    DataTypeAsPascalCase = true,
                    ThrowException = false,
                    //ControllerName = categoryName,
                    AddStarForRequiredProp = false
                }));
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                log.LogWarning("Successfully canceled. Stop the process of creating the IG(s)");

                return;
            }
            // CheckSpellingAndRules -- END

            (List<Property> requestDto, List<Property> responseDto) = DecodeHelperService.PrepareReqAndResDto(microName, classes, false, ProjectType.Micro);

            // C:\Project\API\Development\Nic.Apis.Border\BusinessLayer\Person\CheckTravelerBiometricStatus.cs
            // C:\Project\API\ApiDev\Nic.Apis.Bog\Controllers\PersonController.cs

            if (requestDto.Count is 0 || responseDto.Count is 0)
            {
                throw new DevAssistantException($"The request table or response table is empty! Couldn't generate the table. [Req: {requestDto.Count}, Res: {responseDto.Count}]", 3013);
            }

            log.LogTrace("Calling AuthServer to get contract Name and NameTr -- START");

            string username = $"Nic_{contractName}";

            // Micro description and exceptions
            var microDetails = ModelExtractionService.GetMicroDetails(FileService.GetCodeByFile(microServicePath));

            IGInfo igInfo = new()
            {
                UserInfo = null,
                ServiceInfo = new()
                {
                    //ControllerName = categoryName,
                    ServiceDescEn = microDetails.Description,
                    ServiceName = microName
                },

                WrittenBy = args.WrittenBy,
                IGVersion = args.IgVersion,
                RequestDto = requestDto,
                ResponseDto = responseDto,

                ContractCode = contractName,
                MicrosErrors = microDetails.MicroExceptions
            };

            string fileName = $"{igInfo.ContractCode}_{igInfo.ServiceInfo.ServiceName}_V{igInfo.IGVersion}";

            string title = $"Where to save your IG {fileName}";
            string filter = "Word files (*.docx)|*.docx|All files (*.*)|*.*";

            string currentPath = FileService.GetPathDialog(title, filter, fileName, initialDirectory: Consts.UserSettings.IGPathFolder);

            if (string.IsNullOrWhiteSpace(currentPath))
            {
                throw new DevAssistantException("The returned path is empty or invalid.", 3014);
            }

            Consts.UserSettings.IGPathFolder = currentPath;


            GenerateMicroIGFile(igInfo, currentPath, true);

        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "An unexpected error occurred in GenerateMicroIG");

            log.LogError("Stop the process of creating the IG(s)");

            throw;
        }

    }
    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Generates an API Integration Guide (IG) file based on the provided IG information.
    /// </summary>
    /// <param name="igInfo">The IG information.</param>
    /// <param name="outputPath">The output path for the generated IG file.</param>
    /// <param name="openFile">Flag indicating whether to open the generated file after creation.</param>
    /// <exception cref="DevAssistantException">Thrown if an error occurs during the generation process.</exception>
    private static void GenerateApiIGFile(IGInfo igInfo, string outputPath, bool openFile)
    {
        // Validate the output path
        if (string.IsNullOrWhiteSpace(outputPath))
            throw DevErrors.Generator.E3001InvalidPath;

        try
        {
            Log.Information("GenerateIGFile Called -- START");

            // Generate a file name based on contract code, service name, and IG version
            string fileName = $"{igInfo.ContractCode}_{igInfo.ServiceInfo.ServiceName}_V{igInfo.IGVersion}";

            // Load a document
            if (!File.Exists(Consts.ApiIGTemplateFileLocalPath))
                File.Copy(Consts.ApiIGTemplateFileServerPath, Consts.ApiIGTemplateFileLocalPath);

            using (var document = DocX.Load(Consts.ApiIGTemplateFileLocalPath))
            {
                Log.Information("Document is loaded");

                // Handle document protection
                if (document.IsProtected)
                    document.RemoveProtection();

                // Replace placeholders in the document with actal values
                ReplacePlaceholders(document, igInfo);

                // Add information to tables in the document
                AddInformationToTables(document, igInfo);

                Log.Information("Selected path " + outputPath);
                Log.Information("Document Saving");

                // Save the document
                document.SaveAs(outputPath);

                Log.Information("Document Saved");
            }

            // Update Table of Contents (ToC)
            UpdateTableOfContents(outputPath);

            if (openFile)
            {
                Log.Information("Start process to open Word file to user");

                // Open IG file
                ProcessStartInfo process = new(outputPath)
                {
                    UseShellExecute = true,
                };

                using var p = Process.Start(process);
                p?.Dispose();
            }
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "An unexpected error occurred in GenerateApiIGFile");
            throw;
        }
        finally
        {
            Log.Information("GenerateIGFile Called --  END");
        }
    }

    private static void GenerateNdbJsonFile(IGInfo igInfo, string outputPath)
    {
        Log.Logger.Information("GenerateNdbJsonFile Called");

        if (string.IsNullOrWhiteSpace(outputPath))
            throw DevErrors.Generator.E3001InvalidPath;

        try
        {
            Log.Information("GenerateNdbJsonFile -- START");

            igInfo.ResponseDto.RemoveAll(prop
                => prop.Name.Equals("RequestTimestamp", StringComparison.CurrentCultureIgnoreCase)
                || prop.Name.Equals("ResponseTimestamp", StringComparison.CurrentCultureIgnoreCase));

            NdbJson ndbJson = new()
            {
                onBoardingRequest = new()
                {
                    header = new(),
                    body = new()
                    {
                        api_name_tech = igInfo.ServiceInfo.ServiceName,
                        api_name = igInfo.ServiceInfo.ServiceName.SplitByCapitalLetter(),
                        api_name_ar = "",
                        short_description = igInfo.ServiceInfo.ServiceDescEn.FirstCharToUpper().TrimEnd('.'),
                        long_description = igInfo.ServiceInfo.ServiceDescEn.FirstCharToUpper().TrimEnd('.'),
                        short_description_ar = "",
                        long_description_ar = "",
                        tags = new() { igInfo.ServiceInfo.ControllerName },
                        tags_ar = new()
                        {
                            igInfo.ServiceInfo.ControllerName switch
                            {
                                "Person" => "الأشخاص",
                                "Business" => "المنشآت",
                                "Vehicle" => "المركبات",
                                "Visa" => "التأشيرات",
                                "Violation" => "المخالفات",
                                "User" => "المستخدمين",
                                "Lookup" => "الترميز",
                                "General" => "عام",
                                _ => igInfo.ServiceInfo.ControllerName,
                            }
                        },
                        operations = new() { igInfo.ServiceInfo.ServiceName },
                        business_fields_input = igInfo.RequestDto.Select(i => new Business_Fields_Input
                        {
                            key_en = i.Name.RemoveRequired().RemoveDash(),
                            value_en = i.DescEn,
                            key_ar = i.Name.ShortDescAr() ?? i.DescAr,
                            value_ar = i.DescAr
                        }).ToList(),
                        business_fields_output = igInfo.ResponseDto.Select(o => new Business_Fields_Output
                        {
                            key_en = o.Name.RemoveDash(),
                            value_en = o.DescEn,
                            key_ar = o.Name.ShortDescAr() ?? o.DescAr,
                            value_ar = o.DescAr
                        }).ToList(),
                    }
                }
            };

            string json = Json.JsonConvert.SerializeObject(ndbJson, Json.Formatting.Indented);
            File.WriteAllText(outputPath, json);

            Log.Information("GenerateNdbJsonFile END");
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "An unexpected error occurred in GenerateNdbJsonFile");
            throw;
        }
    }


    /// <summary>
    /// Replaces placeholders in the document with corresponding values from IG information.
    /// </summary>
    /// <param name="document">The document to replace placeholders in.</param>
    /// <param name="igInfo">The IG information.</param>
    private static void ReplacePlaceholders(DocX document, IGInfo igInfo)
    {
        document.ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "%service_name%",
            NewValue = igInfo.ServiceInfo.ServiceName
        });

        document.ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "%service_desc%",
            NewValue = igInfo.ServiceInfo.ServiceDescEn
        });

        document.ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "%contract_code%",
            NewValue = igInfo.ContractCode
        });

        document.ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "%contract_nameEn%",
            NewValue = igInfo.UserInfo.NameTr
        });

        document.ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "%contract_nameAr%",
            NewValue = igInfo.UserInfo.Name
        });

        document.ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "%ig_version%",
            NewValue = igInfo.IGVersion.ToString()
        });
    }

    /// <summary>
    /// Adds information to specific tables in the Word document using the provided IG information.
    /// </summary>
    /// <param name="document">The Word document.</param>
    /// <param name="igInfo">The IG information.</param>
    private static void AddInformationToTables(DocX document, IGInfo igInfo)
    {
        Log.Information("AddInformationToTables Called -- START");

        try
        {
            // Document_Version table
            AddRowsToTable(document, "Document_Version", new()
            {
                new WordRow(new()
                {
                    new("%ig_version%", igInfo.IGVersion.ToString()),
                    new("%date%", DateTime.Now.ToString("dd/MM/yyyy")),
                    new("%written_by%", igInfo.WrittenBy)
                })
            });

            // Api_Url table
            AddRowsToTable(document, "Api_Url", new()
            {
                new WordRow(new()
                {
                    new("%name%", "API – Development URL"),
                    new("%value%", GetApiUrl(igInfo, "hxxps://gwsdev.nic")),
                }),
                new WordRow(new()
                {
                    new("%name%", "API – Testing URL"),
                    new("%value%", GetApiUrl(igInfo, "hxxps://gwstst.nic")),
                }),
                new WordRow(new()
                {
                    new("%name%", "API – Production URL"),
                    new("%value%", GetApiUrl(igInfo, "hxxps://gws.moi")),
                }),
                new WordRow(new()
                {
                    new("%name%", "HTTP Method"),
                    new("%value%", "POST"),
                })
            });

            // RequestDto table
            AddDtoTable(document, "RequestDto", igInfo.RequestDto);

            // ResponseDto table
            AddDtoTable(document, "ResponseDto", igInfo.ResponseDto);

            // Api_Error table
            AddRowsToTable(document, "Api_Error", igInfo.ApiErrors.Select(er => new WordRow(new()
            {
                new("%http_status_code%", er.HttpStatusCode.ToString()),
                new("%error_code%", er.ErrorCode.ToString()),
                new("%error_messageEn%", er.ErrorMessageEn),
                new("%error_messageAr%", er.ErrorMessageAr)
            })).ToList(), rowPatternIndex: 2);

            // Api_Swagger table
            AddRowsToTable(document, "Api_Swagger", new()
            {
                new WordRow(new()
                {
                    new("%name%", "Swagger – Development URL"),
                    new("%value%", GetSwaggerUrl(igInfo, "hxxps://gwsdev.nic")),
                }),
                new WordRow(new()
                {
                    new("%name%", "Swagger – Testing URL"),
                    new("%value%", GetSwaggerUrl(igInfo, "hxxps://gwstst.nic")),
                }),
                new WordRow(new()
                {
                    new("%name%", "Swagger – Production URL"),
                    new("%value%", GetSwaggerUrl(igInfo, "hxxps://gws.moi")),
                })
            });
        }
        catch (Exception ex)
        {
            string errorMessage = "An error occurred while adding items to table";

            Log.Logger.Error(ex, errorMessage);
            throw;
        }
        finally
        {
            Log.Information("AddInformationToTables -- END");
        }
    }

    /// <summary>
    /// Gets the API URL based on the provided IG information and base URL.
    /// </summary>
    /// <param name="igInfo">The IG information.</param>
    /// <param name="baseUrl">The base URL.</param>
    /// <returns>The constructed API URL.</returns>
    private static string GetApiUrl(IGInfo igInfo, string baseUrl)
    {
        string url = igInfo.ContractCode.EndsWith("Npv")
            ? $"{baseUrl}/NIC/Npv/api/{igInfo.ContractCode.Replace("Npv", "")}/{igInfo.ServiceInfo.ServiceName}"
            : $"{baseUrl}/NIC/{igInfo.ContractCode}/api/{igInfo.ServiceInfo.ControllerName}/{igInfo.ServiceInfo.ServiceName}";

        // I used hxxps to avoiding the annoying blue hyperlink :)
        return url.Replace("hxxps", "https");
    }

    /// <summary>
    /// Gets the full Swagger URL based on the provided base URL and IG information.
    /// </summary>
    /// <param name="igInfo">Information for generating the IG file.</param>
    /// <param name="baseUrl">The base URL for Swagger.</param>
    /// <returns>The full Swagger URL.</returns>
    private static string GetSwaggerUrl(IGInfo igInfo, string baseUrl)
    {
        string url = igInfo.ContractCode.EndsWith("Npv")
                      ? $"{baseUrl}/NIC/Npv"
                      : $"{baseUrl}/NIC/{igInfo.ContractCode}";

        return url.Replace("hxxps", "https");
    }

    /// <summary>
    /// Adds rows to a table in the Word document using the specified placeholder and replacement values.
    /// </summary>
    /// <param name="document">The Word document.</param>
    /// <param name="tableCaption">The caption of the table in the document.</param>
    /// <param name="rows">The rows to add to the table.</param>
    private static void AddRowsToTable(DocX document, string tableCaption, List<WordRow> rows, int rowPatternIndex = 1)
    {
        // This method was created to fix many bugs. Now, to add a row to a table, we will mostly use this ONE method
        // to avoid duplicated code and prevent bugs :(

        ArgumentNullException.ThrowIfNull(document);
        ArgumentNullException.ThrowIfNull(tableCaption);
        ArgumentNullException.ThrowIfNull(rows);

        Table table = document.Tables.FirstOrDefault(t => t.TableCaption == tableCaption);

        if (table is null or { RowCount: 0 })
        {
            // Handle if the table with the specified caption is not found
            Log.Logger.Error("Error, couldn't find table with caption {tableCaption} in the current document.", tableCaption);
            return;
        }

        // Before we start, we should set BreakAcrossPages to false. to apply it on table headers and on row pattern
        foreach (var row in table.Rows)
        {
            row.BreakAcrossPages = false;
        }

        // Get the row pattern of the second row (first row is the header).
        Row rowPattern = table.Rows[rowPatternIndex];

        foreach (var row in rows)
        {
            // Add a new row to the table copy from rowPattern
            Row newRow = table.InsertRow(rowPattern, table.RowCount - 1);

            // Now we must replace %replacement% in all row's cells 
            foreach (var item in row.Cells)
            {
                StringReplaceTextOptions stringReplace = new()
                {
                    SearchValue = item.Placeholder,
                    NewValue = item.Value,
                };

                // Replace the placeholders in the newly inserted row
                newRow.ReplaceText(stringReplace);
            }
        }

        // Remove the pattern row.
        rowPattern.Remove();
    }


    /// <summary>
    /// Add rows to a DTO table in the document.
    /// </summary>
    /// <param name="document">The Word document.</param>
    /// <param name="tableCaption">The caption of the table in the document.</param>
    /// <param name="dtoProperties"></param>
    private static void AddDtoTable(DocX document, string tableCaption, List<Property> dtoProperties)
    {
        Table table = document.Tables.FirstOrDefault(t => t.TableCaption == tableCaption);

        if (table is null or { RowCount: 0 })
        {
            // Handle if the table with the specified caption is not found
            Log.Logger.Error("Error, couldn't find table with caption {tableCaption} in the current document.", tableCaption);
            return;
        }

        // Before we start, we should set BreakAcrossPages to false. to apply it on table headers and on row pattern
        foreach (var row in table.Rows)
        {
            row.BreakAcrossPages = false;
        }

        // Get the row pattern of the second row (first row is the header).
        Row rowPattern = table.Rows[1];

        // Add a new row for each DTO property.
        foreach (var prop in dtoProperties)
        {
            AddItemToDtoTable(table, rowPattern, prop);
        }

        // Remove the pattern row.
        rowPattern.Remove();
    }


    /// <summary>
    /// Updates the Table of Contents (ToC) in the Word document.
    /// </summary>
    /// <param name="outputPath">The path of the Word document.</param>
    private static void UpdateTableOfContents(string outputPath)
    {
        Log.Information("Update ToC -- START");
        if (File.Exists(outputPath))
        {

            NF.Application app = null;
            NF.Document doc = null;

            try
            {
                // initiate Word instance
                Log.Information("Initiate Word instance start");
                app = new();

                // open document
                Log.Information("Open document start");
                doc = app.Documents.Add(outputPath);

                Log.Information("Document is opened and start updating the ToC");

                // update ToC
                doc.TablesOfContents[1].Update();
                doc.TablesOfContents[1].Range.Font.Name = "Arial";

                doc.SaveAs2(outputPath);

                object missing = System.Reflection.Missing.Value;

                doc.Close(false, missing, missing);
                app.Quit();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "An unexpected error occurred in UpdateTableOfContents");
                throw;
            }
            finally
            {
                if (app != null && !app.IsDisposed)
                    app.Dispose();

                if (doc != null && !doc.IsDisposed)
                    doc.Dispose();

                doc = null;
                app = null;
            }

        }
        Log.Information("Update ToC -- END");
    }

    /// <summary>
    /// Adds a row to the RequestDto or ResponseDto table in the Word document based on the provided Property information.
    /// </summary>
    /// <param name="table">The table in the Word document.</param>
    /// <param name="rowPattern">The pattern row to be used as a template for the new row.</param>
    /// <param name="prop">The Property information representing a DTO property.</param>
    private static void AddItemToDtoTable(Table table, Row rowPattern, Property prop)
    {
        // Insert a copy of the rowPattern at the last index in the table.
        var newRow = table.InsertRow(rowPattern, table.RowCount - 1, true);

        var paraAr = prop.DescAr;
        var dataType = prop.DataType;

        CustomizeArabicProperties(prop, ref paraAr, ref dataType);

        // Replace the placeholders in the newly inserted row
        newRow.ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "%prop_name%",
            NewValue = prop.Name,
        });

        newRow.ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "%prop_nameAr%",
            NewValue = paraAr,
        });

        newRow.ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "%data_type%",
            NewValue = dataType,
        });

        newRow.ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "%prop_descEn%",
            NewValue = prop.DescEn,
        });

        newRow.ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "%prop_descAr%",
            NewValue = prop.DescAr,
        });

        // Add a red asterisk (*) for required properties
        if (prop.Name.Contains('*') || prop.IsRequired)
        {
            AddRedAsterisk(newRow);
        }
    }

    /// <summary>
    /// Customizes specific properties based on their names.
    /// </summary>
    /// <param name="prop">The Property information representing a DTO property.</param>
    /// <param name="paraAr">The Arabic description of the property.</param>
    /// <param name="dataType">The data type of the property.</param>
    private static void CustomizeArabicProperties(Property prop, ref string paraAr, ref string dataType)
    {
        // Customize specific properties based on their names
        if (prop.Name.RemoveWhiteSpaces().Replace("*", "").Replace("-", "").Trim().Equals("PersonID", StringComparison.InvariantCultureIgnoreCase))
        {
            paraAr = "رقم هوية الشخص";
            dataType = prop.DataType.Replace("()", "(10)");
        }
        else if (prop.Name.RemoveWhiteSpaces().Replace("*", "").Replace("-", "").Trim().Equals("OwnerID", StringComparison.InvariantCultureIgnoreCase))
        {
            paraAr = "رقم هوية المالك";
        }
        else if (prop.Name.RemoveWhiteSpaces().Replace("*", "").Replace("-", "").Trim().Equals("ID", StringComparison.InvariantCultureIgnoreCase))
        {
            paraAr = "رقم الهوية";
            dataType = prop.DataType.Replace("()", "(10)");
        }
        else if (prop.Name.RemoveWhiteSpaces().Replace("*", "").Replace("-", "").Trim().Equals("OperatorID", StringComparison.InvariantCultureIgnoreCase))
        {
            paraAr = "رقم هوية المشغل";
            dataType = prop.DataType.Replace("()", "(10)");
        }
        else if (prop.Name.RemoveWhiteSpaces().Replace("*", "").Replace("-", "").Trim().Equals("BirthDate", StringComparison.InvariantCultureIgnoreCase))
        {
            paraAr = "تاريخ الميلاد";
            dataType = prop.DataType.Replace("()", "(8)");
        }
        else if (prop.Name.RemoveWhiteSpaces().Replace("*", "").Replace("-", "").Trim().Equals("Lang", StringComparison.InvariantCultureIgnoreCase))
        {
            paraAr = "اللغة";
            dataType = prop.DataType.Replace("()", "(2)");
        }

        if (prop.Name.RemoveWhiteSpaces().Replace("*", "").Replace("-", "").Trim().Equals("ClientIPAddress", StringComparison.InvariantCultureIgnoreCase))
        {
            dataType = prop.DataType.Replace("()", "(19)");
        }

        // List<WantedHitRecordRes>() >> List of WantedHitRecordRes
        if (prop.DataType.Contains('<'))
        {
            dataType = prop.DataType.Replace("<", " of ").Replace(">", "").Replace("()", "");
        }
    }

    /// <summary>
    /// Adds a red asterisk (*) for required properties.
    /// </summary>
    /// <param name="newItem">The newly inserted row in the table.</param>
    /// <param name="prop">The Property information representing a DTO property.</param>
    private static void AddRedAsterisk(Row newItem)
    {
        // Replace the asterisk (*) with an empty string and then add a bold red asterisk
        newItem.Cells[0].Paragraphs[0].ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = "*",
            NewValue = "",
        });

        //newItem.Cells[0].Paragraphs[0].ToString().Trim();

        // TODO: test it.
        // Remove spaces, it don't know if this really work! I just added anyway :)
        newItem.Cells[0].Paragraphs[0].ReplaceText(new StringReplaceTextOptions
        {
            SearchValue = " ",
            NewValue = "",
        });

        // Sett the asterisk to red
        newItem.Cells[0].Paragraphs[0].Append(" *", new Formatting
        {
            Bold = true,
            FontFamily = new Xceed.Document.NET.Font("Arial (Body CS)"),
            Size = 11,
            FontColor = Color.Red
        });
    }


    private static void ValidateServicenfoForIG(NicService service, bool isReport, out string contrllerDesc,
    out List<BusinessApiException> businessLayerExceptions, out List<DevAssistantException> devAssistantExceptionsOut)
    {
        List<DevAssistantException> devAssistantExceptions = new();

        // Validation -- START

        // Validation -- END

        // BusinessLayer Code
        businessLayerExceptions = ModelExtractionService.GetApiExceptions(service.BusinessLayer, out List<DevAssistantException> exsFromBL, out var connectionStrings, new() { IsReport = isReport });

        if (connectionStrings.Count > 0)
        {
            var dialogInfo = new DevDialogInfo
            {
                Title = $"Warning: {connectionStrings.Count} ConnectionString(s) Found:",
                Message = $"Please do not forget to add the values (DbConfig) of the ConnectionStrings in the CM tab.\n\nConnectionStrings:\n  - {string.Join("\n  - ", connectionStrings)}",
                Buttons = MessageBoxButtons.OK,
                BoxIcon = MessageBoxIcon.Warning
            };

            DevDialog.Show(dialogInfo);
        }

        //                                                 avoid commo errors  ||    Get BusinessLayer errors
        //var hasDupes = businessLayerExceptions.Where(x => x.ErrorCode > 4299 || x.ErrorCode < 4200).GroupBy(x => x.ErrorCode).Where(x => x.Skip(1).Any()).ToList();
        var hasDupes = businessLayerExceptions.Where(x => !x.AllowDuplicate).GroupBy(x => x.ErrorCode).Where(x => x.Skip(1).Any()).ToList();

        if (isReport)
            devAssistantExceptions.AddRange(exsFromBL);

        if (hasDupes.Any())
        {
            var codes = hasDupes.Select(x => x.Key);
            var lines = hasDupes.SelectMany(x => x.Select(x => x.FoundAtLine)).ToList();

            var ex = new DevAssistantException($"There are errors with the same ErrorCode. Please check error codes [{string.Join(", ", codes)}] in BusinessLayer at lines [{string.Join(", ", lines)}]", 3015);

            if (isReport)
                devAssistantExceptions.Add(ex);
            else
                throw ex;
        }

        // Contract.BusinLayer Exceptions -- START                                            ShardValidationsn start with 4150. See MoHajj
        var apiExceptionsOver4100 = businessLayerExceptions.Where(x => x.ErrorCode is >= 4100 and <= 4149).Select(x => x.ErrorCode);

        if (apiExceptionsOver4100.Any())
        {
            if (!Enumerable.Range(4100, apiExceptionsOver4100.Count()).SequenceEqual(apiExceptionsOver4100))
            {
                var ex = new DevAssistantException($"Error codes in BusinessLayer are not consecutive [{string.Join(", ", apiExceptionsOver4100)}]", 3016);

                if (isReport)
                    devAssistantExceptions.Add(ex);
                else
                    throw ex;
            }
        }
        // Contract.BusinLayer Exceptions -- END

        // Root.BusinLayer Exceptions -- START
        var apiExceptionsOver4300 = businessLayerExceptions.Where(x => x.ErrorCode is >= 4300 and <= 4399).Select(x => x.ErrorCode);

        if (apiExceptionsOver4300.Any())
        {
            if (!Enumerable.Range(4300, apiExceptionsOver4300.Count()).SequenceEqual(apiExceptionsOver4300))
            {
                var ex = new DevAssistantException($"Error codes in Root.BusinessLayer are not consecutive [{string.Join(", ", apiExceptionsOver4300)}]", 3017);

                if (isReport)
                    devAssistantExceptions.Add(ex);
                else
                    throw ex;
            }
        }
        // Root.BusinLayer Exceptions -- END

        var contrllerCode = FileService.GetCodeByFile(new GetCodeByFileReq
        {
            Code = service.Controller.Code,
            IsRemotePath = service.Controller.FilePath.IsRemotePath,
            Path = service.Controller.FilePath.Path,
            BranchName = service.Controller.FilePath.BranchName
        });

        //  $"{service.Namespace}.Controllers"
        contrllerDesc = ModelExtractionService.GetServiceDescFromController(service.Controller, out List<DevAssistantException> exsFromController, new() { IsReport = isReport });

        if (isReport)
            devAssistantExceptions.AddRange(exsFromBL);

        devAssistantExceptionsOut = devAssistantExceptions;
    }


    private static void GenerateMicroIGFile(IGInfo igInfo, string outputPath, bool openFile)
    {
        // Validate the output path
        if (string.IsNullOrWhiteSpace(outputPath))
            throw DevErrors.Generator.E3001InvalidPath;

        try
        {
            Log.Information("GenerateMicroIGFile Called -- START");

            // Generate a file name based on contract code, service name, and IG version
            string fileName = $"{igInfo.ContractCode}_{igInfo.ServiceInfo.ServiceName}_V{igInfo.IGVersion}";

            // Load a document
            if (!File.Exists(Consts.MicroIGTemplateFileLocalPath))
                File.Copy(Consts.MicroIGTemplateFileServerPath, Consts.MicroIGTemplateFileLocalPath);

            using (var document = DocX.Load(Consts.MicroIGTemplateFileLocalPath))
            {
                Log.Information("Document is loaded");

                // Handle document protection
                if (document.IsProtected)
                    document.RemoveProtection();

                Log.Information("Replace placeholders -- START");

                // Replace placeholders in the document with actal values
                document.ReplaceText(new StringReplaceTextOptions
                {
                    SearchValue = "%service_name%",
                    NewValue = igInfo.ServiceInfo.ServiceName
                });


                document.ReplaceText(new StringReplaceTextOptions
                {
                    SearchValue = "%service_desc%",
                    NewValue = !string.IsNullOrWhiteSpace(igInfo.ServiceInfo.ServiceDescEn) ? igInfo.ServiceInfo.ServiceDescEn : "*****************************"
                });

                document.ReplaceText(new StringReplaceTextOptions
                {
                    SearchValue = "%contract_code%",
                    NewValue = igInfo.ContractCode
                });

                document.ReplaceText(new StringReplaceTextOptions
                {
                    SearchValue = "%ig_version%",
                    NewValue = igInfo.IGVersion.ToString()
                });

                Log.Information("Replace placeholders Called -- END");

                Log.Information("AddInformationToTables Called -- START");

                // Add information to tables in the document -- START
                // Document_Version table
                AddRowsToTable(document, "Document_Version", new()
                {
                    new WordRow(new()
                    {
                        new("%ig_version%", igInfo.IGVersion.ToString()),
                        new("%date%", DateTime.Now.ToString("dd/MM/yyyy")),
                        new("%written_by%", igInfo.WrittenBy)
                    })
                });

                // RequestDto table
                AddRowsToTable(document, "RequestDto", igInfo.RequestDto.Select(dto => new WordRow(new()
                {
                    new("%prop_name%", dto.Name),
                    new("%data_type%", dto.DataType.Replace("<", " of ").Replace(">", "").Replace("()", "")),
                    new("%prop_descEn%", dto.DescEn)
                })).ToList());

                // ResponseDto table
                AddRowsToTable(document, "ResponseDto", igInfo.ResponseDto.Select(dto => new WordRow(new()
                {
                    new("%prop_name%", dto.Name),
                    new("%data_type%", dto.DataType.Replace("<", " of ").Replace(">", "").Replace("()", "")),
                    new("%prop_descEn%", dto.DescEn)
                })).ToList());

                // Micro_Error table
                AddRowsToTable(document, "Micro_Error", igInfo.MicrosErrors.Select(er => new WordRow(new()
                {
                    new("%error_code%", er.ErrorCode.ToString()),
                    new("%error_messageEn%", er.ErrorMessageEn),
                    new("%error_messageAr%", er.ErrorMessageAr)
                })).ToList());
                // Add information to tables in the document -- END

                Log.Information("AddInformationToTables Called -- END");

                Log.Information("Selected path " + outputPath);
                Log.Information("Document Saving");

                // Save the document
                document.SaveAs(outputPath);

                Log.Information("Document Saved");
            }

            // Update Table of Contents (ToC)
            UpdateTableOfContents(outputPath);

            if (openFile)
            {
                Log.Information("Start process to open Word file to user");

                // Open IG file
                ProcessStartInfo process = new(outputPath)
                {
                    UseShellExecute = true,
                };

                using var p = Process.Start(process);
                p?.Dispose();
            }
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "An unexpected error occurred in GenerateMicroIGFile");
            throw;
        }
        finally
        {
            Log.Information("GenerateMicroIGFile Called --  END");
        }

    }

    #endregion Private Methods
}