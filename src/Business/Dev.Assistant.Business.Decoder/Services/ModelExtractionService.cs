using Dev.Assistant.Business.Core.Enums;
using Dev.Assistant.Business.Core.Extensions;
using Dev.Assistant.Business.Core.Models; // TODO use DevErros.Decoder
using Dev.Assistant.Business.Core.Services;
using Dev.Assistant.Business.Decoder.Models;
using Dev.Assistant.Configuration;
using Dev.Assistant.Configuration.Models;
using Serilog;
using System.Text;
using System.Text.RegularExpressions;

// Error code start with 1000

namespace Dev.Assistant.Business.Decoder.Services;

/// <summary>
/// Service class for handling various operations related to classes, methods, and models codes.
/// </summary>
public class ModelExtractionService
{
    #region Private Members

    private static readonly Regex regexEnNum = new(@"^[a-zA-Z0-9. -_?#]*$");

    #endregion Private Members

    #region Public Methods

    /// <summary>
    /// Get all methods from code for C# or VB<br/>
    /// Code must contain only one class
    /// </summary>
    /// <param name="code">File code in string</param>
    /// <param name="options">Method options</param>
    /// <returns>The class details with all methods</returns>
    /// <exception cref="DevAssistantException">Thrown when an error occurs in the DevAssistant tool</exception>
    public static WebService GetAllMethods(string code, GetAllMethodsOptions options = null)
    {
        // Logging the method call
        Log.Logger.Information("GetAllMethods Called");

        options ??= new GetAllMethodsOptions();

        try
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                // Throw an exception for invalid input
                throw new DevAssistantException("Invalid value for parameter(code)", 1000);
            }

            // Get class keyword count -- START
            //var words = Regex.Split(code, @"\W+").Select((x, i) => new { Item = x, Index = i }).ToDictionary(x => x.Index, x => x.Item);
            var words = Regex.Split(code, @"\W+");
            var classCount = 0;

            for (int i = 0; i < words.Length; i++)
            {
                if (i == 0)
                    continue;

                if ((words[i].ToLower() == "class" && (words[i - 1].ToLower() == "public" || (words[i - 1].ToLower() == "static" && words[i - 2].ToLower() == "public")))
                    || (words[i].ToLower() == "interface" && words[i - 1].ToLower() == "public"))
                    classCount++;
            }
            // Get class keyword count -- END

            if (classCount == 0)
            {
                // Throw an exception if no "class" keyword is found
                throw new DevAssistantException("Invalid code. No \"class\" keyword found!", 1001);
            }
            //else if (!options.CanHasMultiClasses && classCount > 1)
            //{
            //    // the new GetCSharpFunctions will get all methods even if there is multi calsses


            //    // Throw an exception if more than one "class" keyword is found
            //    //throw new DevAssistantException("Invalid code. It has 2 \"classes\" keywords or more!", 1002);
            //}

            var isVB = DecodeHelperService.IsVBCode(code);

            var service = new WebService
            {
                Funcations = new List<Function>(),
                IsVB = isVB,
            };

            string tempStr;
            int endIndex1;
            int endIndex2;

            /****************** Get Namespace  --  START *****************/
            if (code.IndexOf("Namespace") != -1 || code.IndexOf("namespace") != -1)
            {
                // Extract namespace from the code
                tempStr = code[(isVB ? (code.IndexOf("Namespace") + 10) : (code.IndexOf("namespace") + 10))..];
                endIndex1 = tempStr.IndexOf(" ") - 1;
                endIndex2 = tempStr.IndexOf("\n") - 1;

                string namesPace = tempStr[..(endIndex1 < endIndex2 ? endIndex1 : endIndex2)];
                service.Namespace = namesPace.Trim();
            }
            else
            {
                // Set a default value if no namespace is found
                service.Namespace = "No namespace found!";
            }
            /****************** Get Namespace  --   END  *****************/

            /****************** Get Service Name  --  START *****************/
            if (classCount == 1)
            {
                // Extract service name from the code
                tempStr = code[(isVB ? code.IndexOf("Public Class") + 13 : code.IndexOf("public class") + 13)..];
                endIndex1 = tempStr.IndexOf(" ");
                endIndex2 = tempStr.IndexOf("\n");

                string serviceName = tempStr[..(endIndex1 < endIndex2 ? endIndex1 : endIndex2)];
                service.Name = serviceName.Trim();

                //// Get the code after declration calss
                //tempStr = tempStr[(endIndex1 < endIndex2 ? endIndex1 : endIndex2)..];

                //// if there is no more class, this get the code without the class declration
                //code = tempStr[(endIndex1 < endIndex2 ? endIndex1 : endIndex2)..];

                //// Remove constructor in C#
                //code = code.Replace("public " + service.Name, "");
            }
            /****************** Get Service Name  --   END  *****************/

            /****************** Get Functions --  START *****************/


            List<Function> functions;

            if (code.Contains("public interface "))
            {
                //var clss = GetClassesByCode(code, new() { IncludProperties = false });

                functions = new List<Function>();

                string[] lines = code.Split(Convert.ToChar("\n"));
                Function function;

                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i] = lines[i].Trim();

                    if (lines[i].StartsWith("[OperationContract()") || lines[i].StartsWith("<OperationContract()"))
                    {
                        words = lines[i + 1].Trim().Split(Convert.ToChar(" "));
                        function = new();

                        // Extract function name from the interface
                        string funName = words[1][..words[1].IndexOf("(")];
                        function.FunName = funName.Trim();
                        functions.Add(function);
                    }
                }
            }
            else
            {
                functions = isVB ? GetVBFunctions(code, options) : GetCSharpFunctions(code, options);

                if (functions.Count == 0)
                    throw new DevAssistantException("No functions found", 1003);
            }

            service.Funcations = functions;
            /****************** Get Functions --   END   *****************/

            return service;
        }
        catch (DevAssistantException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new DevAssistantException(ex.Message, 1004);
        }
    }


    /// <summary>
    /// Get all ApiErrorException(s) from the BusinessLayer
    /// </summary>
    /// <param name="service">NicServiceFile representing the service information</param>
    /// <param name="devAssistantExceptionsOut">List of DevAssistantExceptions generated during processing</param>
    /// <param name="connectionStrings">List of connection strings extracted from the code</param>
    /// <param name="options">Method options</param>
    /// <returns>List of business API exceptions</returns>
    /// <exception cref="DevAssistantException">Thrown when an error occurs in the DevAssistant tool</exception>
    public static List<BusinessApiException> GetApiExceptions(NicServiceFile service, out List<DevAssistantException> devAssistantExceptionsOut, out List<string> connectionStrings, GetApiExceptionOptions options = null)
    {
        Log.Logger.Information("GetApiExceptions Called - path: {a}, service.ContractCode: {b}, service.ControllerName: {c}", service.FilePath.Path, service.ContractCode, service.ControllerName);

        if (options is null)
        {
            options = new GetApiExceptionOptions();
        }

        connectionStrings = new();

        List<DevAssistantException> devAssistantExceptions = new();

        string code = FileService.GetCodeByFile(new GetCodeByFileReq
        {
            Code = service.Code,
            IsRemotePath = service.FilePath.IsRemotePath,
            Path = service.FilePath.Path,
            BranchName = service.FilePath.BranchName
        });

        List<BusinessApiException> apiExceptions = new();
        List<BusinessApiException> apiCommonExceptions = new();

        string nameSpace;

        // Determine the namespace based on the file path
        if (service.FilePath.Path.Contains(".Root"))
        {
            if (service.FilePath.Path.Contains("BusinessLayer\\Gcc\\"))
                nameSpace = $"Nic.Apis.{service.ContractCode}.BusinessLayer.Gcc.{service.ControllerName}";
            else if (service.FilePath.Path.Contains("BusinessLayer\\Npv\\"))
                nameSpace = $"Nic.Apis.{service.ContractCode}.BusinessLayer.Npv.{service.ControllerName}";
            else
                nameSpace = $"Nic.Apis.{service.ContractCode}.BusinessLayer.{service.ControllerName}";
        }
        else
        {
            if (service.Namespace.IsNamespaceNpv())
                nameSpace = $"Nic.Apis.Npv.BusinessLayer.{service.ControllerName}";
            else
                nameSpace = $"Nic.Apis.{service.ContractCode}.BusinessLayer.{service.ControllerName}";
        }

        BusinessApiException apiException;

        try
        {
            // Check if the code is empty
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new DevAssistantException("Invalid value for parameter(code)", 1301);
            }
            // Check for the presence of "RequestValidations" method and the correct namespace
            else if (!code.Contains("private void RequestValidations(") && !code.Contains("namespace Nic.Apis.Root"))
            {
                var ex = new DevAssistantException("Couldn't find private method \"RequestValidations()\" in the BusinessLayer", 1302);

                if (options.IsReport)
                    devAssistantExceptions.Add(ex);
                else
                    throw ex;

                // TODO: check if the user doesn't called the RequestValidations method in BL.
                // if (*** "RequestValidations(**);" )
            }

            // Check namespace
            DecodeHelperService.ValidateNamespace(code, nameSpace);

            string fileName = service.ContractCode.Equals("Root") ? $"Root.BusinessLayer" : "BusinessLayer";

            string[] lines = code.Split(Convert.ToChar("\n"));
            ReadOnlySpan<char> line;
            bool rootIsImported = false;

            List<string> rootControllers = new();

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Trim();
                line = lines[i].Trim();

                // Skip empty lines and lines starting with comments
                if (line.IsWhiteSpace() || line.StartsWith("//") || line.StartsWith("*"))
                    continue;

                // Skip lines related to error code variable assignment
                if (lines[i].ToLower().RemoveWhiteSpaces().StartsWith("varerrorcode="))
                    continue;

                // Skip lines ending with a semicolon and containing no dots (likely variable assignments)
                if (lines[i].Contains(';') && !lines[i].Contains('.'))
                    continue;

                // Check lines importing root namespaces
                if (line.StartsWith("using Nic.Apis.Root.BusinessLayer"))
                {
                    // Extract the controller name from the line and add it to the list
                    rootControllers.Add(line[(line.LastIndexOf('.') + 1)..].ToString().Replace(";", "").Trim());
                }

                // Check lines Contains importing Root namespaces
                if (lines[i].Contains("Nic.Apis.Root.BusinessLayer"))
                    rootIsImported = true;

                // check lines containing ".ConnectionStrings."
                if (lines[i].Contains(".ConnectionStrings."))
                {
                    // Extract the connection key from the line
                    // Bug (solved): Replace ) for case: con = new SqlConnection(contractOptions.ConnectionStrings.PdsConnectionString);
                    var connectionKey = lines[i][(line.LastIndexOf(".ConnectionStrings.") + 19)..].Replace(",", "").Replace(")", "").Replace(";", "").Trim();

                    // Split the connection key into parts
                    var connectionKeySplit = connectionKey.Split('.');

                    if (connectionKeySplit.Length > 1)                //  0   1
                        connectionKey = connectionKeySplit[0].Trim(); // Db2.Own

                    // Check if the connection key is not a common one and add it to the list
                    if (!connectionKey.Equals("PdsConnectionString") && !connectionKey.Equals("Db2") && !connectionKey.Equals("Archive"))
                        connectionStrings.Add(connectionKey);
                }

                // Check lines containing method declarations
                if ((line.StartsWith("public") || line.StartsWith("private")) && !lines[i].Contains(" class "))
                {
                    if (line.IndexOf("(") > 0)
                    {
                        // Extract method arguments body
                        var argsBody = lines[i][(line.IndexOf("(") + 1)..].Replace(")", "");

                        if (!string.IsNullOrWhiteSpace(argsBody))
                        {
                            // Split arguments and check for camelCase naming
                            var args = argsBody.Split(',');

                            foreach (var arg in args)
                            {
                                var words = arg.Trim().Split(' ');

                                if (words.Length != 2)
                                    break;

                                if (char.IsUpper(words[1][0]))
                                {
                                    // Throw an exception for camelCase violation
                                    var ex = new DevAssistantException($"Method argument starts with Capital. {words[1]} must be camelCase. Please check \"{words[1]}\" arg at line {i + 1} in BusinessLayer", 1310);

                                    if (options.IsReport)
                                        devAssistantExceptions.Add(ex);
                                    else
                                        throw ex;
                                }
                            }
                        }
                    }
                }

                // Checl lines starting with "using" and containing "Nic.Apis." 
                if (line.StartsWith("using") && lines[i].Contains("Nic.Apis.") && !lines[i].Contains("Backend") && !lines[i].Contains("Common.Caching") && !lines[i].Contains("Nic.Apis.Root") && lines[i].EndsWith("Shared"))
                {
                    // Check if "using" contains the expected service.ControllerName
                    if (!lines[i].Contains(service.ControllerName))
                    {
                        var ex = new DevAssistantException($"Unexpected error in {fileName} at line {i + 1}. Expected \"using\" contains \"{service.ControllerName}\" but actual is \"{line}\"", 1304);

                        if (options.IsReport)
                            devAssistantExceptions.Add(ex);
                        else
                            throw ex;
                    }

                    // Check if "using" contains the expected service.ContractCode
                    if (!lines[i].Contains(service.ContractCode))
                    {
                        var ex = new DevAssistantException($"Unexpected error in {fileName} at line {i + 1}. Expected \"using\" contains \"{service.ContractCode}\" but actual is \"{line}\"", 1305);

                        if (options.IsReport)
                            devAssistantExceptions.Add(ex);
                        else
                            throw ex;
                    }
                }

                // Check lines containing ".validateidtype"
                if (lines[i].ToLower().Contains(".validateidtype"))
                {
                    // Construct the idTypes
                    string idTypes = string.Empty;

                    if (lines[i].Contains(".Citizen"))
                        idTypes += "C";

                    if (lines[i].Contains(".Alien"))
                        idTypes += "A";

                    if (lines[i].Contains(".Visitor"))
                        idTypes += "V";

                    if (lines[i].Contains(".Pligrim"))
                        idTypes += "P";

                    if (lines[i].Contains(".Establishment"))
                        idTypes += "E";

                    if (lines[i].Contains(".Unknown"))
                        idTypes += "U";

                    // Extract IDCategory and IDTypeOptions information from the line
                    var idCategory = line[line.IndexOf("IDCategoryOptions")..];
                    idCategory = idCategory[(idCategory.IndexOf(".") + 1)..];
                    idCategory = idCategory[..idCategory.IndexOf(",")];

                    // Store the line number where this information is found
                    string foundAtLine;

                    if (service.ContractCode.Equals("Root"))
                        foundAtLine = $"{i + 1} in Root";
                    else
                        foundAtLine = $"{i + 1}";

                    string idCategoryEn, idCategoryAr;

                    // Extract IDTypeOptions information from the line
                    var idTypeOptions = line[line.IndexOf("IDTypeOptions")..];
                    idTypeOptions = idTypeOptions[(idTypeOptions.IndexOf(".") + 1)..];
                    idTypeOptions = idTypeOptions[..idTypeOptions.IndexOf(",")];

                    (idCategoryAr, idCategoryEn) = GetCategoryTypeDesc(idCategory.ToString());

                    // Bug: Get error 4201 only if IDCategoryOptions supplied
                    if (!idTypeOptions.ToString().Equals("None"))
                    {
                        apiException = DecodeHelperService.ValidateIDType(idTypes, idCategoryEn, idCategoryAr);
                        apiException.FoundAtLine = foundAtLine;

                        apiCommonExceptions.Add(apiException);
                    }

                    // Construct API exceptions for common scenarios
                    apiException = new()
                    {
                        HttpStatusCode = 400,
                        ErrorCode = 4200,
                        ErrorMessageEn = "Invalid ****".Replace("****", idCategoryEn),
                        ErrorMessageAr = "رقم **** غير صحيح".Replace("****", idCategoryAr),
                        FoundAtLine = foundAtLine,
                        AllowDuplicate = true
                    };

                    apiCommonExceptions.Add(apiException);

                    apiException = new()
                    {
                        HttpStatusCode = 400,
                        ErrorCode = 4202,
                        ErrorMessageEn = "Invalid ****".Replace("****", idCategoryEn),
                        ErrorMessageAr = "رقم **** غير صحيح".Replace("****", idCategoryAr),
                        FoundAtLine = foundAtLine,
                        AllowDuplicate = true
                    };

                    apiCommonExceptions.Add(apiException);
                }

                // check lines containing ValidatePermitNumber error
                if (lines[i].ToLower().Contains("sharedvalidations.validatepermitnumber("))
                {
                    // Construct API exceptions for permit number validation scenarios
                    apiException = new()
                    {
                        HttpStatusCode = 400,
                        ErrorCode = 4150,
                        ErrorMessageEn = "Permit number is incorrect",
                        ErrorMessageAr = "رقم التصريح غير صحيح",
                        FoundAtLine = "",
                    };

                    apiExceptions.Add(apiException);

                    apiException = new()
                    {
                        HttpStatusCode = 400,
                        ErrorCode = 4151,
                        ErrorMessageEn = "No Hajj permit found",
                        ErrorMessageAr = "لم يتم العثور على تصريح الحج",
                        FoundAtLine = "",
                    };

                    apiExceptions.Add(apiException);
                }

                /*
                 * Case 1:
                 * Root.BusinessLayer.Person.GetFamilyRelationship rootService = new();
                 * Case 2:
                 * var rootService = new Root.BusinessLayer.Person.GetFamilyRelationship();
                 * Case 3:
                 *  a)
                 *  GetMoiJobDetailsRes rootServiceRes = rootService.ApiBusiness(rootServiceReq, _contractOptions);
                 *  b)
                 *  var rootServiceRes = rootService.ApiBusiness(rootServiceReq, _contractOptions);
                 *      if start with var go back to service(class) Initialize
                 */

                // check lines related to Root.BusinessLayer services
                if (lines[i].Contains(" Root.BusinessLayer") || lines[i].StartsWith("Root.BusinessLayer") || lines[i].Contains(".ApiBusiness(") && rootIsImported)
                {
                    // Add the current service.ControllerName to the rootControllers list
                    rootControllers.Add(service.ControllerName);

                    // Flag to indicate if the service was successfully processed
                    bool isSuccess = false;

                    foreach (var controller in rootControllers)
                    {
                        string serviceName = string.Empty;

                        string rootPath = GetRootPath(lines, i, service.FilePath.Path, controller, out serviceName); // local path

                        // Check if the root file exists locally || ignore if file is remote
                        if (File.Exists(rootPath) || service.FilePath.IsRemotePath)
                        {
                            // Get API exceptions from the root file and add them to the list
                            apiExceptions.AddRange(GetApiExceptions(new()
                            {
                                FilePath = new() { Path = rootPath, IsRemotePath = service.FilePath.IsRemotePath, BranchName = service.FilePath.BranchName },
                                ContractCode = "Root",
                                ControllerName = controller,
                                Namespace = $"Nic.Apis.Root.BusinessLayer.Npv.{controller}",
                                ServiceName = service.FilePath.IsRemotePath ? service.ServiceName : serviceName
                            }, out List<DevAssistantException> exs, out var connections, options));

                            if (options.IsReport)
                                devAssistantExceptions.AddRange(exs);

                            // Add the connections to the connectionStrings list
                            if (connections.Count > 0)
                                connectionStrings.AddRange(connections);

                            // Mark the process as successful
                            isSuccess = true;

                            // Exit the loop
                            break;
                        }
                        else
                        {
                            Log.Logger.Error("Couldn't open \"Root.BusinessLayer.{a}.{b}\" with the following path: {c}", service.ControllerName, serviceName, rootPath);
                        }
                    }

                    // Check if the service processing was unsuccessful
                    if (!isSuccess)
                    {
                        throw new DevAssistantException($"Couldn't open Root.BusinessLayer", 1601);
                    }
                }
                // && !lines[i].Replace("var ", "").RemoveWhiteSpaces().StartsWith("ErrorCode=")
                // If the current line contains "ErrorCode" and '='
                else if (lines[i].Contains("ErrorCode") && lines[i].Contains('='))
                {
                    // Check the range of error codes for Root and other contracts
                    // Check if the ErrorCode = Test (ErrorCode = ex.ErrorCode)

                    // Get errorCode, if failed, skip to the next line
                    if (!int.TryParse(lines[i][(lines[i].IndexOf('=') + 1)..].Replace(";", "").Replace(",", "").Trim(), out int errorCode))
                        continue;

                    // Validate the range of error codes based on the contract type
                    if (service.ContractCode == "Root" && errorCode is < 4300 or > 4399)
                    {
                        var ex = new DevAssistantException($"Error code in Root must start from 4300. Expected \"4300-4399\" but actual is \"{errorCode}\" in {fileName} at line {i + 1}.", 1307);

                        // Add the exception to the list or throw it based on the report option
                        if (options.IsReport)
                            devAssistantExceptions.Add(ex);
                        else
                            throw ex;
                    }
                    else if (service.ContractCode != "Root" && errorCode is < 4100 or > 4199)
                    {
                        var ex = new DevAssistantException($"Error code in {fileName} must start from 4100. Expected \"4100-4199\" but actual is \"{errorCode}\" in {fileName} at line {i + 1}.", 1307);

                        // Add the exception to the list or throw it based on the report option
                        if (options.IsReport)
                            devAssistantExceptions.Add(ex);
                        else
                            throw ex;
                    }

                    // Check if there is a line with ApiErrorException (e.g., ApiErrorException ex = new... or throw new ApiErrorException...)
                    int apiExObjIndex = -1;

                    // Go back 5 lines to find ApiErrorException
                    for (int j = 1; j <= 5; j++)
                    {
                        if (lines[i - j].ToLower().Contains("apierrorexception"))
                        {
                            apiExObjIndex = i - j;
                            break;
                        }
                    }

                    if (apiExObjIndex > -1)
                    {
                        // Get the errorMessage -- START
                        var errorMessage = string.Empty;

                        // Start from the next line to find the ErrorMessage
                        for (int j = 1; j < 5; j++)
                        {
                            if (lines[i + j].Contains("ErrorMessage"))
                            {
                                var tempLine = lines[i + j].Trim();
                                if (tempLine.Contains('\"'))
                                    errorMessage += tempLine;

                                if (tempLine.ToLower().Contains(','))
                                    break;

                                tempLine = lines[i + j + 1].Trim();
                                if (tempLine.Contains('\"'))
                                    errorMessage += tempLine;

                                if (tempLine.ToLower().Contains(','))
                                    break;

                                tempLine = lines[i + j + 2].Trim();
                                if (tempLine.Contains('\"'))
                                    errorMessage += tempLine;

                                break;
                            }
                        }

                        // Go back to find ErrorMessage if not found after going forward
                        if (string.IsNullOrWhiteSpace(errorMessage))
                        {
                            for (int j = 1; j < 5; j++)
                            {
                                if (lines[i - j].Contains("ErrorMessage"))
                                {
                                    var tempLine = lines[i - j].Trim();
                                    if (tempLine.Contains('\"'))
                                        errorMessage += tempLine;

                                    if (tempLine.ToLower().Contains(','))
                                        break;

                                    tempLine = lines[i - j + 1].Trim();
                                    if (tempLine.Contains('\"'))
                                        errorMessage += tempLine;

                                    if (tempLine.ToLower().Contains(','))
                                        break;

                                    tempLine = lines[i - j + 2].Trim();
                                    if (tempLine.Contains('\"'))
                                        errorMessage += tempLine;

                                    break;
                                }
                            }
                        }

                        // Go to apiExObjIndex to find errorMessage using new ApiErrorException(true ? "رقم 2" : "Error #2")...
                        if (string.IsNullOrWhiteSpace(errorMessage))
                        {
                            if (lines[apiExObjIndex].Contains("new"))
                            {
                                var tempLine = lines[apiExObjIndex].Trim();
                                if (tempLine.Contains('\"'))
                                    errorMessage += tempLine;

                                tempLine = lines[apiExObjIndex + 1].Trim();
                                if (tempLine.Contains('\"'))
                                    errorMessage += tempLine;

                                tempLine = lines[apiExObjIndex + 2].Trim();
                                if (tempLine.Contains('\"'))
                                    errorMessage += tempLine;
                            }
                        }
                        // Get the errorMessage -- END

                        // Checking for errorMessage
                        // if (string.IsNullOrWhiteSpace(errorMessage))
                        //    throw new DevAssistantException($"Unexpected error in {fileName} at line {i + 1}. Couldn't find ErrorMessage for error #{errorCode}", 1306);

                        // Get ErrorMessageAr and ErrorMessageEn -- START
                        string errorMessageAr = string.Empty;
                        string errorMessageEn = string.Empty;

                        var errors = errorMessage.Split('\"');

                        if (errors.Length == 5)
                        {
                            // TODO: use validator.
                            if (regexEnNum.IsMatch(errors[1])) // English language
                            {
                                errorMessageEn = errors[1];
                                errorMessageAr = errors[3];
                            }
                            else
                            {
                                errorMessageAr = errors[1];
                                errorMessageEn = errors[3];
                            }
                        }
                        else
                        {
                            // throw new DevAssistantException($"Unexpected error in {fileName} at line {i + 1}.", 1308);
                        }
                        // Get ErrorMessageAr and ErrorMessageEn -- END

                        string foundAtLine;

                        if (service.ContractCode.Equals("Root"))
                            foundAtLine = $"{i + 1} in Root";
                        else
                            foundAtLine = $"{i + 1}";

                        apiException = new()
                        {
                            HttpStatusCode = 400,
                            ErrorCode = errorCode,
                            ErrorMessageEn = errorMessageEn.Trim(),
                            ErrorMessageAr = errorMessageAr.Trim(),
                            FoundAtLine = foundAtLine
                        };

                        apiExceptions.Add(apiException);
                    }
                }
            }

            // Add the exceptions to the common exceptions list

            // Check if the code contains ".ValidateBirthDate"
            if (code.Contains(".ValidateBirthDate"))
            {
                apiException = new()
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4203,
                    ErrorMessageEn = "Invalid Birthdate",
                    ErrorMessageAr = "تاريخ الميلاد غير صحيح"
                };

                apiCommonExceptions.Add(apiException);
            }

            if (code.Contains(".ValidateIPAddress"))
            {
                apiException = new()
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4204,
                    ErrorMessageEn = "Client IP address is incorrect",
                    ErrorMessageAr = "عنوان بروتوكول الإنترنت للعميل غير صحيح"
                };

                apiCommonExceptions.Add(apiException);
            }

            if (code.Contains(".ValidateOperatorServiceAccess"))
            {
                apiException = new()
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4206,
                    ErrorMessageEn = "Operator is not allowed to use this service",
                    ErrorMessageAr = "المشغل ليس لديه صلاحية لاستخدام هذه الخدمة"
                };

                apiCommonExceptions.Add(apiException);
            }

            if (code.Contains(".ValidatePersonIrregularStatus"))
            {
                apiException = new()
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4207,
                    ErrorMessageEn = "Invalid ID",
                    ErrorMessageAr = "رقم الهوية غير صحيح"
                };

                apiCommonExceptions.Add(apiException);
            }

            if (code.Contains(".ValidateNpvFineOwnership"))
            {
                apiException = new()
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4208,
                    ErrorMessageEn = "Invalid fine number",
                    ErrorMessageAr = "رقم المخالفة غير صحيح"
                };

                apiCommonExceptions.Add(apiException);
            }

            if (code.Contains(".ValidateNpvFineGroupOwnership"))
            {
                apiException = new()
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4209,
                    ErrorMessageEn = "Invalid fine group code",
                    ErrorMessageAr = "رمز نوع المخالفة غير صحيح"
                };

                apiCommonExceptions.Add(apiException);
            }

            if (code.Contains(".ValidateWantedDepartment"))
            {
                apiException = new()
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4210,
                    ErrorMessageEn = "Request department is not allowed to use this service",
                    ErrorMessageAr = "جهة الطلب لا تمتلك صلاحية لاستخدام هذه الخدمة"
                };

                apiCommonExceptions.Add(apiException);
            }

            if (code.Contains(".ValidateGccNationality"))
            {
                apiException = new()
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4211,
                    ErrorMessageEn = "The entered nationality code is not a Gcc country",
                    ErrorMessageAr = "رمز الدولة المدخل ليس من دول مجلس التعاون الخليجي"
                };

                apiCommonExceptions.Add(apiException);
            }

            if (code.Contains(".ValidateVehiclePlateDetails"))
            {
                apiException = new()
                {
                    HttpStatusCode = 400,
                    ErrorCode = 7204,
                    ErrorMessageEn = "The letter isn't part of the allowed letters",
                    ErrorMessageAr = "الحرف المدخل ليس من الحروف المتاحة",
                    AllowDuplicate = true
                };

                apiCommonExceptions.Add(apiException);

                apiException = new()
                {
                    HttpStatusCode = 400,
                    ErrorCode = 7204,
                    ErrorMessageEn = "Please enter only one Vehicle plate letter",
                    ErrorMessageAr = "الرجاء إدخال حرف واحد",
                    AllowDuplicate = true
                };

                apiCommonExceptions.Add(apiException);

                apiException = new()
                {
                    HttpStatusCode = 400,
                    ErrorCode = 7205,
                    ErrorMessageEn = "Mixing plate letters language isn't allowed",
                    ErrorMessageAr = "الرجاء تحديد لغة لوحة المركبة; لا يسمح بدمج اللغات"
                };

                apiCommonExceptions.Add(apiException);
            }

            // Check if there are predefined common exceptions
            if (Consts.ApiCommonExceptions?.Count > 0)
            {
                foreach (var ex in Consts.ApiCommonExceptions)
                {
                    // Check if the code contains the placeholder of the common exception
                    if (code.Contains(ex.PlaceHolder))
                    {
                        // Add the common exception to the common exceptions list
                        apiCommonExceptions.Add(ex);
                    }
                }
            }

            // Sort the list of apiExceptions based on error codes
            // To make common error in the top of the table
            apiExceptions.Sort((e, y) => e.ErrorCode.CompareTo(y.ErrorCode));
            //apiCommonExceptions.Sort((e, y) => e.ErrorCode.CompareTo(y.ErrorCode));

            // Check if the service contract code is not "Root"
            if (!service.ContractCode.Equals("Root"))
            {
                // Create a 404 error BusinessApiException for "No data found"
                apiException = new()
                {
                    HttpStatusCode = 404,
                    ErrorCode = 4000,
                    ErrorMessageEn = "No data found",
                    ErrorMessageAr = "لا توجد بيانات"
                };

                apiExceptions.Add(apiException);
            }

            // Combine apiCommonExceptions and apiExceptions into a single list
            apiCommonExceptions.AddRange(apiExceptions);

            // Assign the list of DevAssistantExceptions to the out parameter
            devAssistantExceptionsOut = devAssistantExceptions;

            // Create a cleaned list of BusinessApiException to remove duplicates
            var cleanedList = new List<BusinessApiException>();

            // TODO: is Distinct works here with custom object! I forgot :( but anyway the cleanedList.Any is enough
            foreach (var ex in apiCommonExceptions.Distinct().ToList())
            {
                // Check if the cleaned list already contains an exception with the same error code and English error message
                if (cleanedList.Any(e => e.ErrorCode == ex.ErrorCode && e.ErrorMessageEn == ex.ErrorMessageEn))
                    continue;

                // Add the exception to the cleaned list
                cleanedList.Add(ex);
            }

            return cleanedList;
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "An unexpected error occurred in GetApiExceptions");

            throw;
        }
    }

    /// <summary>
    /// Get the body of the method or if-statement
    /// </summary>
    /// <param name="code">The code containing the method or if-statement</param>
    /// <returns>Code body</returns>
    [Obsolete("This must not be used anymore. It was used in old GetCSharpFunctions to get each method's body, but now the new GetCSharpFunctions cover it by regular expression in the right & fast way.")]
    public static string GetBody(string code)
    {
        Log.Logger.Information("GetBody Called");

        // Initialize counters for opened and closed curly brackets
        int openedCBrackets = 0;
        int closedCBrackets = 0;

        // Initialize indeies for opened and closed curly brackets
        int openedCBracketsIndex = 0;
        int closedCBracketsIndex = 0;

        // Store a copy of the original code
        string tempCode = code;

        int length = 0;
        int tempLength = 0;

        // Continue processing until a complete code block is found
        while (true)
        {
            // Find the index of the opening and closing curly brackets
            openedCBracketsIndex = tempCode.IndexOf("{");
            closedCBracketsIndex = tempCode.IndexOf("}");

            if (openedCBracketsIndex != -1 && closedCBracketsIndex != -1 && openedCBracketsIndex < closedCBracketsIndex)
            {
                openedCBrackets++;

                tempLength = tempCode.Length;
                tempCode = tempCode[(openedCBracketsIndex + 1)..];
                tempLength -= tempCode.Length;

                length += tempLength;
            }
            else if (openedCBracketsIndex != -1 && closedCBracketsIndex != -1 && closedCBracketsIndex < openedCBracketsIndex)
            {
                closedCBrackets++;

                tempLength = tempCode.Length;
                tempCode = tempCode[(closedCBracketsIndex + 1)..];
                tempLength -= tempCode.Length;

                length += tempLength;
            }
            else if (openedCBracketsIndex == -1 && closedCBracketsIndex == -1)
            {
                // Exit the loop if there are no more curly brackets
                break;
            }
            else if (openedCBracketsIndex != -1)
            {
                openedCBrackets++;

                tempLength = tempCode.Length;
                tempCode = tempCode[(openedCBracketsIndex + 1)..];
                tempLength -= tempCode.Length;

                length += tempLength;
            }
            else if (closedCBracketsIndex != -1)
            {
                closedCBrackets++;

                tempLength = tempCode.Length;
                tempCode = tempCode[(closedCBracketsIndex + 1)..];
                tempLength -= tempCode.Length;

                length += tempLength;
            }

            // Check if the count of opened and closed curly brackets is equal
            if (openedCBrackets == closedCBrackets)
            {
                // Extract the code body based on the length
                code = code[..length];
                break;
            }
        }

        return code;
    }

    /// <summary>
    /// Get the body of the method or if-statement.
    /// </summary>
    /// <param name="code">The code containing the method or if-statement.</param>
    /// <returns>Code body.</returns>
    public static string GetBodyV2(string code)
    {
        // TODO: test it again

        Log.Logger.Information("GetBody Called");

        int openedCBrackets = 0;
        int closedCBrackets = 0;

        // Use a regular expression to find opening and closing curly brackets
        Regex curlyBracketsRegex = new(@"\{|\}");
        MatchCollection matches = curlyBracketsRegex.Matches(code);

        int codeLength = 0;
        int tempCodeLength = 0;

        // Continue processing until a complete code block is found
        foreach (Match match in matches)
        {
            // Increment or decrement the count of opened and closed curly brackets
            openedCBrackets += match.Value == "{" ? 1 : 0;
            closedCBrackets += match.Value == "}" ? 1 : 0;

            // Update the length based on the substring
            tempCodeLength = match.Index + 1;
            codeLength += tempCodeLength;

            // Check if the count of opened and closed curly brackets is equal
            if (openedCBrackets == closedCBrackets)
            {
                // Extract the code body based on the length
                return code[..codeLength];
            }
        }

        throw new DevAssistantException("Unmatched curly brackets found.", 1300);
    }

    /// <summary>
    /// Get all classes with their properties by code as a string. This method extracts classes and their properties from the provided code.
    /// </summary>
    /// <param name="code">File code in string.</param>
    /// <param name="options">Method options.</param>
    /// <returns>List of classes with its properties.</returns>
    /// <exception cref="DevAssistantException">Thrown if an exception occurs during the process.</exception>
    public static List<ClassModel> GetClassesByCode(string code, GetClassesOptions options = null) => GetClasses(code, options);

    /// <summary>
    /// Get all classes with their properties by file path. This method reads the code from the specified file path and extracts classes with their properties.
    /// </summary>
    /// <param name="filePath">File path to read the code.</param>
    /// <param name="options">Method options.</param>
    /// <returns>List of classes with its properties.</returns>
    /// <exception cref="DevAssistantException">Thrown if an exception occurs during the process.</exception>
    public static List<ClassModel> GetClassesByFilePath(string filePath, GetClassesOptions options = null) => GetClasses(FileService.GetCodeByFile(filePath), options);

    /// <summary>
    /// Retrieves microservice names from a C# file.
    /// </summary>
    /// <param name="filePath">The path of the C# file.</param>
    /// <returns>List of microservice names.</returns>
    public static List<string> GetMicroNamesFromBL(NicServiceFilePath file)
    {
        Log.Logger.Information("GetMicroNamesFromBL Called - path {FilePath}", file.Path);

        string code = FileService.GetCodeByFile(new GetCodeByFileReq
        {
            Path = file.Path,
            IsRemotePath = file.IsRemotePath,
            BranchName = file.BranchName,
        });

        List<string> microservices = new();

        // Constants for better readability
        const string MicroCommonNamespace = "using MicroCommon";
        const string RootBusinessLayerNamespace = "using Nic.Apis.Root.BusinessLayer.";
        const string MicroKeyword = "Micro";

        string[] lines = code.Split(Convert.ToChar("\n"));

        bool rootIsImported = false;
        List<string> rootControllers = new();

        for (int i = 0; i < lines.Length; i++)
        {
            ReadOnlySpan<char> line = lines[i].Trim();

            // Skips comments and empty lines
            if (line.IsWhiteSpace() || line.StartsWith("//"))
                continue;

            if (line.StartsWith(MicroCommonNamespace))
                continue;

            if (line.StartsWith(RootBusinessLayerNamespace))
            {
                rootControllers.Add(line[line.LastIndexOf('.')..].ToString().Replace(".", "").Replace(";", "").Trim());
            }

            if (lines[i].Contains("Nic.Apis.Root.BusinessLayer"))
                rootIsImported = true;

            if (line.StartsWith("using") && lines[i].Contains(MicroKeyword))
            {
                // Avoiding ******.ObjectModel
                if (line.Contains('.'))
                    continue;

                int startIndex = line.IndexOf(MicroKeyword);
                int endIndex = line.LastIndexOf(';') - startIndex;

                string name = line.Slice(startIndex, endIndex).ToString();
                microservices.Add(name);
            }
            else if (lines[i].Contains(" Root.BusinessLayer") || lines[i].StartsWith("Root.BusinessLayer") || lines[i].Contains(".ApiBusiness(") && rootIsImported)
            {
                string path = FileService.NormalizeSystemPath(file.Path);

                var serviceInfo = FileService.GetServiceInfo(path);

                rootControllers.Add(serviceInfo.ControllerName);

                bool isSuccess = false;

                foreach (var controller in rootControllers)
                {
                    string rootPath = GetRootPath(lines, i, path, controller, out string serviceName);

                    if (File.Exists(rootPath) || file.IsRemotePath)
                    {
                        microservices.Add(rootPath);
                        microservices.AddRange(GetMicroNamesFromBL(new() { Path = rootPath, IsRemotePath = file.IsRemotePath, BranchName = file.BranchName }));

                        isSuccess = true;

                        break;
                    }
                    else
                    {
                        Log.Logger.Error("Couldn't open Root.BusinessLayer.{Controller}.{ServiceName} with the following path: {RootPath}", controller, serviceName, rootPath);
                    }
                }

                if (!isSuccess)
                {
                    throw new DevAssistantException($"Couldn't open Root.BusinessLayer", 1309);
                }
            }
        }

        return microservices.Distinct().ToList();
    }

    /// <summary>
    /// Extracts the namespace from the provided code.
    /// </summary>
    /// <param name="code">The code to extract the namespace from.</param>
    /// <returns>The extracted namespace.</returns>
    public static string GetNamespace(string code)
    {
        // Logging the method call
        Log.Logger.Information("GetNamespace Called - code.Length: {codeLength}", code.Length);

        string[] lines = code.Split(Convert.ToChar("\n"));
        ReadOnlySpan<char> line;

        StringBuilder namespaceBuilder = new();

        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = lines[i].Trim();
            line = lines[i].Trim();

            if (line.IsWhiteSpace() || line.StartsWith("//"))
                continue;

            if (line.StartsWith("namespace ", StringComparison.InvariantCultureIgnoreCase))
            {
                namespaceBuilder.Append(lines[i].Replace("namespace", "").Replace(".Models", "").Replace(".BusinessLayer", "").Trim());
            }

            // I forgot why :(
            if (line.StartsWith("public class ", StringComparison.InvariantCultureIgnoreCase))
            {
                namespaceBuilder.Append($".{lines[i].Replace("public class", "").Replace("{", "").Replace("Req", "").Trim()}");
                break;
            }
        }

        return namespaceBuilder.ToString();
    }

    /// <summary>
    /// Gets the summary description for a specific service in the controller.
    /// </summary>
    /// <param name="controller">The NicServiceFile representing the controller.</param>
    /// <param name="devAssistantExceptionsOut">Output parameter for exceptions detected during processing.</param>
    /// <param name="options">Method options.</param>
    /// <returns>Service summary description.</returns>
    /// <exception cref="DevAssistantException">Thrown for DevAssistant-related errors.</exception>
    public static string GetServiceDescFromController(NicServiceFile controller, out List<DevAssistantException> devAssistantExceptionsOut, GetServiceDescOptions options = null)
    {
        Log.Logger.Information("GetServiceDescFromController Called from {a}.{b}.{c}", controller.Namespace, controller.ControllerName, controller.ServiceName);

        // Initialize options if not provided
        options ??= new GetServiceDescOptions();

        List<DevAssistantException> devAssistantExceptions = new();

        try
        {
            // Validate controller code
            if (string.IsNullOrWhiteSpace(controller.Code))
            {
                throw new DevAssistantException("Invalid value for parameter(code)", 1101);
            }

            // Check namespace of the controller
            DecodeHelperService.ValidateNamespace(controller.Code, controller.Namespace);

            string desc = string.Empty;

            // Split controller code into lines
            string[] lines = controller.Code.Split(Convert.ToChar("\n"));
            string line;

            bool serviceFound = false;

            for (int i = 0; i < lines.Length; i++)
            {
                line = lines[i].Trim();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Check for the start of the controller class
                if (line.StartsWith("public class"))
                {
                    var words = line.Split(' ');

                    // Validate the controller name
                    string controllerName = words[2];
                    if (controllerName != $"{controller.ControllerName}Controller")
                    {
                        var ex = new DevAssistantException($"Controller name does not match with parameter(controllerName) in {controller.ControllerName}Controller at {i + 1}. Expected \"{controller.ControllerName}Controller\" but actual is \"{controllerName}\"", 1103);

                        if (options.IsReport)
                            devAssistantExceptions.Add(ex);
                        else
                            throw ex;
                    }

                    continue;
                }

                // Check for the declaration of ILogger with correct TCategoryName
                if (line.StartsWith("private readonly ILogger"))
                {
                    /*
                     * Some time when we create a controller we don't change the TCategoryName <T>. and if it was diff no error will be there
                     * So, we will check
                     *
                     * public class VisaController : ControllerBase
                     * ...
                     * private readonly ILogger<PersonController> _logger // wrong, must be VisaController
                     */

                    // ServiceBus
                    if (!line.Contains($"<{controller.ControllerName}Controller>"))
                    {
                        var ex = new DevAssistantException($"ILogger's TCategoryName does not match the current {controller.ControllerName}Controller at {i + 1}. Expected \"private readonly ILogger<{controller.ControllerName}Controller> _logger;\" but actual is \"{line}\"", 1104);

                        if (options.IsReport)
                            devAssistantExceptions.Add(ex);
                        else
                            throw ex;
                    }

                    continue;
                }

                // Skip private fields and controller method declaration
                if (line.StartsWith("private") || line.StartsWith($"public {controller.ControllerName}"))
                    continue;

                // Check for the service attribute
                if (line.Equals($"[HttpPost(\"{controller.ServiceName}\")]"))
                {
                    serviceFound = true;

                    // Check if the <summary> tag is present for the service
                    if (lines[i - 5].Trim().Equals("/// <summary>") && lines[i - 3].Trim().Equals("/// </summary>"))
                    {
                        desc = lines[i - 4].Trim().ToLower().Replace("returns", "retrieve").Replace("///", "").Trim().RemoveWhiteSpaces(" ");
                    }
                    else
                    {
                        var ex = new DevAssistantException($"Couldn't find the <summary> tag for {controller.ServiceName} in {controller.ControllerName}Controller at line {i - 5}", 1105);

                        if (options.IsReport)
                            devAssistantExceptions.Add(ex);
                        else
                            throw ex;
                    }

                    // Check response typeof if it ends with Res or not
                    if (lines[i + 1].Trim().Contains("Status200OK"))
                    {
                        var temp = lines[i + 1].Trim()[lines[i + 1].Trim().IndexOf("typeof")..].Replace(")]", "").Trim();

                        if (!temp.Equals($"typeof({controller.ServiceName}Res)"))
                        {
                            var ex = new DevAssistantException($"The \"typeof\" of SwaggerResponse for Status200OK is expected \"typeof({controller.ServiceName}Res)\" but actual is \"{temp}\" in {controller.ServiceName}Controller at line {i + 2}", 1107);

                            if (options.IsReport)
                                devAssistantExceptions.Add(ex);
                            else
                                throw ex;
                        }
                    }

                    // Check method name -- START
                    string[] words = null;
                    if (lines[i + 6].Trim().StartsWith("public"))
                        words = lines[i + 6].Trim().Split(' ');
                    else if (lines[i + 7].Trim().StartsWith("public"))
                        words = lines[i + 7].Trim().Split(' ');

                    if (words is not null and { Length: > 0 })
                    {
                        string methodName = string.Empty;

                        // Mostly it will be the last index, for example:
                        //   0   |                    1                           |                                 2                                |             3
                        // public ActionResult<MicroListPersonHealthInsurancesRes> MicroListPersonHealthInsurances(MicroListPersonHealthInsurancesReq personHealthInsurancesReq)
                        if (words.Length >= 3)
                        {
                            if (char.IsUpper(words[3][0]))
                            {
                                var ex = new DevAssistantException($"Method argument starts with a capital letter. {words[3].Replace(")", "")} must be camelCase. Please check \"{words[3].Replace(")", "")}\" arg at line {i + 6}-{i + 7} in {controller.ServiceName}Controller.", 1111);

                                if (options.IsReport)
                                    devAssistantExceptions.Add(ex);
                                else
                                    throw ex;
                            }
                        }

                        // To fix when doesn't have all SwaggerResponses
                        if (words.Length > 2)
                            methodName = words[2][..words[2].IndexOf("(")];

                        // Check if the method name matches the service name
                        if (!methodName.Equals(controller.ServiceName))
                        {
                            var ex = new DevAssistantException($"Method name \"{methodName}\" does not match the service name in {controller.ControllerName}Controller at line {i + 6}-{i + 7}. Expected \"{controller.ServiceName}\" but actual is \"{methodName}\".", 1106);

                            if (options.IsReport)
                                devAssistantExceptions.Add(ex);
                            else
                                throw ex;
                        }
                        else
                        {
                            // This means methodName matches serviceName. Now we will get the method to perform some validations
                            var method = GetAllMethods(controller.Code, new() { IsControllerMethod = true }).Funcations.FirstOrDefault(f => f.FunName == controller.ServiceName);

                            if (method != null && !method.Code.Contains("if (result == null)"))
                            {
                                var ex = new DevAssistantException($"Method \"{methodName}\" in {controller.ServiceName}Controller does not handle the null case. The controller must return 404 if result == null", 1109);

                                if (options.IsReport)
                                    devAssistantExceptions.Add(ex);
                                else
                                    throw ex;
                            }
                        }
                    }
                    else
                    {
                        var ex = new DevAssistantException($"Couldn't find the method header for {controller.ServiceName} service. Please make sure that all SwaggerResponses are there (such as StatusCodes.Status404NotFound), the document is formatted, and there are no empty lines between SwaggerResponse and method header", 1110);

                        if (options.IsReport)
                            devAssistantExceptions.Add(ex);
                        else
                            throw ex;
                    }
                    // Check method name -- END

                    break;
                }
            }

            // Check if the service was not found
            if (!serviceFound)
            {
                throw new DevAssistantException($"Couldn't find {controller.ServiceName} in {controller.ControllerName}Controller", 1108);
            }

            // Ensure the description ends with a period
            if (!string.IsNullOrWhiteSpace(desc) && !desc.EndsWith('.'))
                desc += ".";

            // Output detected exceptions
            devAssistantExceptionsOut = devAssistantExceptions;

            return desc;
        }
        catch (DevAssistantException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new DevAssistantException(ex.Message, 1100);
        }
    }


    /// <summary>
    /// Extracts details from a microservice code string, including its description, category, and exceptions.
    /// </summary>
    /// <param name="code">The microservice code as a string.</param>
    /// <returns>An object of MicroDetails containing the extracted information.</returns>
    public static MicroDetails GetMicroDetails(string code)
    {
        MicroDetails details = new();


        // Extract Description
        // This regex looks for a summary tag and captures multiple lines until it reaches the closing tag
        var descriptionMatch = Regex.Match(code, @"/// <summary>\s*([\s\S]+?)\s*/// </summary>");

        if (descriptionMatch.Success)
        {
            // Replace '///' and trim spaces for each line in the summary
            List<string> lines = Regex.Replace(descriptionMatch.Groups[1].Value, @"///", "")
                                         .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
                                         .Select(line => line.Trim())
                                         .Where(line => !string.IsNullOrWhiteSpace(line))
                                         .ToList();

            details.Description = string.Join(Environment.NewLine, lines).Replace("<br/>", "");
        }

        // Extract MicroExceptions
        details.MicroExceptions = GetMicroExceptions(code);

        return details;
    }

    #endregion Public Methods

    #region Private Methods


    private static List<MicroException> GetMicroExceptions(string code)
    {
        var microExceptions = new List<MicroException>();

        // Split the code into lines for multiline analysis
        //var lines = code.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

        //// Extract Exceptions
        ////var exceptionRegex = new Regex(@"throw\s+new\s+MicroServiceException\((.*?), (\d+), MicroServiceErrorType\.(.*?)\);", RegexOptions.Singleline);
        ////var exceptionRegex = new Regex(@"throw\s+new\s+MicroServiceException\s*\(\s*(.*?)\s*,\s*(\d+)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        //var exceptionRegex = new Regex(@"throw\s+new\s+MicroServiceException\(([^,]*), (\d+), MicroServiceErrorType\.(.*?)\);", RegexOptions.Singleline);

        // Get MicroServiceException -- START
        var lines = code.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

        //var exceptionRegex = new Regex(@"throw\s+new\s+MicroServiceException\(([^,]+), (\d+), MicroServiceErrorType\.(.*?)\);", RegexOptions.Singleline);
        //var exceptionRegex = new Regex(@"throw\s+new\s+MicroServiceException\s*\(([^,]*?),\s*(\d+),\s*MicroServiceErrorType\.(.*?)\);", RegexOptions.Singleline | RegexOptions.IgnoreCase);

        // Regex to identify lines that throw MicroServiceException
        //var exceptionRegex = new Regex(@"throw\s+new\s+MicroServiceException\s*\(([^;]*?)\s*,\s*(\d+)\s*,\s*MicroServiceErrorType\.Functional\s*\)\s*;", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        //var exceptionRegex = new Regex(@"throw\s+new\s+MicroServiceException\s*\(\s*(.*?),\s*(\d+),\s*MicroServiceErrorType\.Functional\s*\);", RegexOptions.Singleline);
        var exceptionRegex = new Regex(@"throw\s+new\s+MicroServiceException\s*\(\s*(.*?),\s*(\d+|\w+)\s*,\s*MicroServiceErrorType\.Functional\s*\);", RegexOptions.Singleline);

        var exceptionMatches = exceptionRegex.Matches(code);

        foreach (Match exceptionMatch in exceptionMatches)
        {
            if (exceptionMatch.Success)
            {
                (string errorMessageAr, string errorMessageEn) exceptionDetails;


                // to avoid: throw new MicroServiceException(errorMessage, 7002, MicroServiceErrorType.Functional);
                // since if it was a variable such as errorMessage we want to go back.
                // but if it was string (") like: throw new MicroServiceException(LanguageConstants.Arabic.Equals(input.lang) ? "تاريخ الميلاد بالميلادي غير صحيح......... all in one line

                string messagePart = exceptionMatch.Groups[0].Value.Replace("$", "").Trim();

                if (messagePart.Contains('"') && messagePart.EndsWith(';'))
                //if (messagePart.StartsWith("\""))
                {
                    // Direct string in throw statement
                    exceptionDetails = ExtractMessagesFromCondition(messagePart);
                }
                else
                {
                    int lineNumber = Array.FindIndex(lines, l => l.Contains(exceptionMatch.Groups[0].Value));
                    int j = lineNumber;

                    if (lineNumber > -1)
                    {
                        /* this used in case the error message was a variable.
                         * 
                         * Example:
                         * 
                         *  string errorMessage = "";
                         *  if (string.Compare(lang, "Ar", true) == 0)
                         *      errorMessage = "لا توجد معلومات فعالة للرقم المدخل";
                         *  else
                         *      errorMessage = "No active records.";
                         *
                         *  throw new MicroServiceException(errorMessage, 7002, MicroServiceErrorType.Functional);
                         * 
                         * 
                         * So, we must get the assignments (binding) full code
                         * 
                         */


                        string variableName = exceptionMatch.Groups[1].Value.Replace("$", "").Trim();

                        string fullCondition = FindVariableAssignments(lines.Take(lineNumber).ToArray(), variableName, lineNumber);


                        // Continue to get the full code. lineNumber + 10 just to avoid infinite loop
                        while (j <= lineNumber + 10 && (!lines[j].Contains(';') || lines[j].Contains("MicroServiceErrorType.Functional")))
                        {
                            fullCondition += lines[j];

                            // We get the full condition
                            if (fullCondition.Contains("MicroServiceErrorType.Functional"))
                                break;

                            j++;
                        }

                        exceptionDetails = ExtractMessagesFromCondition(fullCondition);
                    }
                    else
                    {
                        exceptionDetails = (string.Empty, string.Empty);
                    }
                }

                microExceptions.Add(new MicroException
                {
                    ErrorCode = int.Parse(exceptionMatch.Groups[2].Value),
                    ErrorMessageAr = exceptionDetails.errorMessageAr,
                    ErrorMessageEn = exceptionDetails.errorMessageEn
                });
            }
        }

        // Check if there is missing error that we couldn't extract it from the micro
        int numError = lines.Count(l => l.Contains("new MicroServiceException"));

        int remian = numError - microExceptions.Count;

        if (remian > 0)
        {
            // if there, add rows for missing error.
            for (int i = 0; i < remian; i++)
            {
                microExceptions.Add(new MicroException
                {
                    ErrorCode = 0,
                    ErrorMessageAr = "",
                    ErrorMessageEn = "**************************** The there is an MicroServiceException we couldn't extract the Error details. Please check the missing errors ****************************"
                });
            }
        }
        // Get MicroServiceException -- END


        // Get CommonException -- START
        if (code.Contains(".ValidateIrregularPerson"))
        {
            microExceptions.Add(new()
            {
                ErrorCode = 7104,
                ErrorMessageEn = "There is no Irregular status function for this database type",
                ErrorMessageAr = "لا توجد عملية للتحقق من حالة الشخص لقاعدة البيانات المدخلة",
            });
        }

        bool guardSingleIdFound = false;
        bool validateVehiclePlateLetter = false;
        bool validateVehiclePlateDetails = false;

        for (int i = 0; i < lines.Length; i++)
        {
            if (guardSingleIdFound && validateVehiclePlateLetter && validateVehiclePlateDetails)
                break;

            lines[i] = lines[i].Trim();

            // Skip empty lines and lines starting with comments
            if (string.IsNullOrWhiteSpace(lines[i]) || lines[i].StartsWith("//") || lines[i].StartsWith("*"))
                continue;

            // Check lines containing ".guardsingleid"
            if (!guardSingleIdFound && lines[i].ToLower().Contains(".guardsingleid("))
            {
                guardSingleIdFound = true;

                // Construct the idTypes
                string idTypes = string.Empty;

                if (lines[i].Contains(".Citizen"))
                    idTypes += "C";

                if (lines[i].Contains(".Alien"))
                    idTypes += "A";

                if (lines[i].Contains(".Visitor"))
                    idTypes += "V";

                if (lines[i].Contains(".Pligrim"))
                    idTypes += "P";

                if (lines[i].Contains(".Establishment"))
                    idTypes += "E";

                if (lines[i].Contains(".Unknown"))
                    idTypes += "U";


                var businessException = DecodeHelperService.ValidateIDType(idTypes, "", "", ProjectType.Micro);

                microExceptions.Add(new()
                {
                    ErrorCode = businessException.ErrorCode,
                    ErrorMessageAr = businessException.ErrorMessageAr,
                    ErrorMessageEn = businessException.ErrorMessageEn
                });

                microExceptions.Add(new()
                {
                    ErrorCode = 7100,
                    ErrorMessageAr = "رقم الهوية غير صحيح",
                    ErrorMessageEn = "Invalid ID"
                });

                continue;
            }


            if (!validateVehiclePlateLetter && lines[i].ToLower().Contains(".validatevehicleplateletter("))
            {
                validateVehiclePlateLetter = true;

                microExceptions.Add(new()
                {
                    ErrorCode = 7204,
                    ErrorMessageEn = "Please enter only one Vehicle plate letter",
                    ErrorMessageAr = "الرجاء إدخال حرف واحد",
                });

                microExceptions.Add(new()
                {
                    ErrorCode = 7204,
                    ErrorMessageEn = "The letter isn't part of the allowed letters",
                    ErrorMessageAr = "الحرف المدخل ليس من الحروف المتاحة",
                });

                continue;
            }

            if (!validateVehiclePlateDetails && lines[i].ToLower().Contains(".validatevehicleplatedetails("))
            {
                validateVehiclePlateDetails = true;

                // from ValidateVehiclePlateLetter() is called under ValidateVehiclePlateDetails()
                if (!validateVehiclePlateLetter)
                {
                    validateVehiclePlateLetter = true;

                    microExceptions.Add(new()
                    {
                        ErrorCode = 7204,
                        ErrorMessageEn = "Please enter only one Vehicle plate letter",
                        ErrorMessageAr = "الرجاء إدخال حرف واحد",
                    });

                    microExceptions.Add(new()
                    {
                        ErrorCode = 7204,
                        ErrorMessageEn = "The letter isn't part of the allowed letters",
                        ErrorMessageAr = "الحرف المدخل ليس من الحروف المتاحة",
                    });
                }


                microExceptions.Add(new()
                {
                    ErrorCode = 7205,
                    ErrorMessageEn = "Mixing plate letters language isn't allowed",
                    ErrorMessageAr = "الرجاء تحديد لغة لوحة المركبة; لا يسمح بدمج اللغات",
                });

                continue;
            }
        }


        // Check if there are predefined common exceptions
        if (Consts.ApiCommonExceptions?.Count > 0)
        {
            foreach (var ex in Consts.ApiCommonExceptions)
            {
                // Check if the code contains the placeholder of the common exception
                if (code.Contains(ex.PlaceHolder))
                {
                    // Add the common exception to the common exceptions list
                    microExceptions.Add(new()
                    {
                        ErrorCode = ex.ErrorCode,
                        ErrorMessageAr = ex.ErrorMessageAr,
                        ErrorMessageEn = ex.ErrorMessageEn
                    });
                }
            }
        }

        // Sort the list of apiExceptions based on error codes
        // To make common error in the top of the table
        microExceptions.Sort((e, y) => e.ErrorCode.CompareTo(y.ErrorCode));

        // Create a cleaned list of BusinessApiException to remove duplicates
        var cleanedList = new List<MicroException>();

        foreach (var ex in microExceptions.ToList())
        {
            // Check if the cleaned list already contains an exception with the same error code and English error message
            if (cleanedList.Any(e => e.ErrorCode != 0 && e.ErrorCode == ex.ErrorCode && e.ErrorMessageEn == ex.ErrorMessageEn))
                continue;

            // Add the exception to the cleaned list
            cleanedList.Add(ex);
        }

        // Get CommonException -- END

        return cleanedList;
    }


    private static string FindVariableAssignments(string[] lines, string variableName, int matchIndex)
    {
        /* this used in case the error message was a variable.
         * 
         * Example:
         * 
         *  string errorMessage = "";
         *  if (string.Compare(lang, "Ar", true) == 0)
         *      errorMessage = "لا توجد معلومات فعالة للرقم المدخل";
         *  else
         *      errorMessage = "No active records.";
         *
         *  throw new MicroServiceException(errorMessage, 7002, MicroServiceErrorType.Functional);
         * 
         * 
         * So, we must get the assignments (binding) full code
         * 
         */

        string fullCondition = "";
        int initLineIndex = Array.FindLastIndex(lines, line => line.Contains($"string {variableName}") || line.Contains($"var {variableName}")
        || line.Contains($"{variableName} = \"\";") || line.Contains($"{variableName} = string.Empty;") || line.Contains("if (string.Compare("));


        if (initLineIndex >= 0)
        {
            for (int i = initLineIndex; i < matchIndex; i++)
            {
                if (lines[i].Contains(variableName + " ="))
                {
                    fullCondition += lines[i];
                }
            }
        }
        return fullCondition;
    }



    /// <summary>
    /// Extracts Arabic and English error messages from a conditional error message string.
    /// </summary>
    /// <param name="condition">The string containing the conditional error message.</param>
    /// <returns>A tuple containing the Arabic and English error messages.</returns>
    private static (string, string) ExtractMessagesFromCondition(string condition)
    {
        string message1 = "";
        string message2 = "";


        // Check and extract messages based on known patterns
        if (condition.Contains("LanguageConstants.Arabic.Equals") || condition.Contains("LanguageConstants.English.Equals"))
        {
            // Pattern: LanguageConstants.Arabic.Equals(...) ? "Arabic message" : "English message"
            var regex = new Regex(@"LanguageConstants\.(Arabic|English)\.Equals\(.*?\)\s*\?\s*""(.*?)""\s*:\s*""(.*?)""");
            var match = regex.Match(condition);

            if (match.Success)
            {
                message1 = match.Groups[2].Value;
                message2 = match.Groups[3].Value;
            }
        }
        else if (condition.Contains("errorMessage ="))
        {
            // Extract all assignments to errorMessage
            var assignmentRegex = new Regex(@"errorMessage\s*=\s*""(.*?)"";", RegexOptions.Singleline);
            var matches = assignmentRegex.Matches(condition);

            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    message1 = match.Groups[1].Value;

                    if (matches.Count > 1)
                        message2 = matches[1].Groups[1].Value;

                    break;
                }
            }
        }
        else
        {
            // For simple message patterns or unhandled cases
            // Example: "Some error message", errorCode
            var regex = new Regex(@"""(.*?)""");
            var match = regex.Match(condition);

            if (match.Success)
            {
                message1 = match.Groups[1].Value;
                message2 = match.Groups[1].Value;
            }
        }

        // (Arabic, English)
        return (ValidationService.IsArabic(message1) ? message1 : message2, ValidationService.IsArabic(message1) ? message2 : message1);
    }


    /// <summary>
    /// Get catrgory description by the IDCategoryOptions
    /// Operator => Operator ID
    /// </summary>
    /// <param name="idCategory"></param>
    /// <returns>Catrgory description as (DescAr, DescEn)</returns>
    /// <exception cref="NotImplementedException"></exception>
    private static (string, string) GetCategoryTypeDesc(string idCategory)
    {
        Log.Logger.Information("GetCategoryTypeDesc Called - idCategory: {a}", idCategory);

        string idCategoryMessageEn;
        string idCategoryMessageAr;

        switch (idCategory)
        {
            case "Person":
                idCategoryMessageEn = "ID";
                idCategoryMessageAr = "الهوية";
                break;

            case "Operator":
                idCategoryMessageEn = "Operator ID";
                idCategoryMessageAr = "المشغل";
                break;

            case "Owner":
                idCategoryMessageEn = "Owner ID";
                idCategoryMessageAr = "المالك";
                break;

            case "Sponsor":
                idCategoryMessageEn = "Sponsor ID";
                idCategoryMessageAr = "الكفيل";
                break;

            case "Coowner":
                idCategoryMessageEn = "Coowner ID";
                idCategoryMessageAr = "المستخدم الفعلي";
                break;

            case "Delegate":
                idCategoryMessageEn = "Delegate ID";
                idCategoryMessageAr = "المفوض";
                break;

            case "Firm":
                idCategoryMessageEn = "Firm ID";
                idCategoryMessageAr = "المنشآة";
                break;

            default:
                throw new NotImplementedException("The ID category is not supported");
        }

        return (idCategoryMessageAr, idCategoryMessageEn);
    }

    /// <summary>
    /// Get all classes with their properties from the provided code.
    /// </summary>
    /// <param name="code">Code containing class definitions.</param>
    /// <param name="options">Method options for code analysis.</param>
    /// <returns>List of class models with their properties.</returns>
    /// <exception cref="DevAssistantException">
    /// Thrown when there is an issue with the provided code or during code analysis.
    /// </exception>
    /// <remarks>
    /// This method extracts class information, including properties, from the given code.
    /// </remarks>
    private static List<ClassModel> GetClasses(string code, GetClassesOptions options = null)
    {
        // Ensure options are initialized
        options ??= new GetClassesOptions();

        // Log method invocation
        Log.Logger.Information("GetClasses Called - {a}", options);

        try
        {
            // Validate the provided code
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new DevAssistantException("Invalid value for parameter (code)", 1400);
            }

            //// Determine the language of the code (C# or VB)
            //if (code.StartsWith("using") || code.Contains("public class"))
            //{
            //    options.IsVB = false;
            //}
            //else if (code.StartsWith("<DataContract()>") || code.Contains("Public Class"))
            //{
            //    options.IsVB = true;
            //}
            //else
            //{
            //    // Inform the user about the expected code format
            //    var errorMessage = "Make sure your Model has at least one class. Both C# and VB are acceptable :)";
            //    throw new DevAssistantException(errorMessage, 1401);
            //}

            //int csCount = 0;
            //int vbCount = 0;

            //csCount += code.Count(';');
            //csCount += code.Count("//");
            //csCount += code.Count("/*");
            //csCount += code.Count("{");
            //csCount += code.Count("}");

            //vbCount += code.Count("If");
            //vbCount += code.Count("Then");
            //vbCount += code.Count("Else");
            //vbCount += code.Count("End");
            //vbCount += code.Count("Return");

            //if (vbCount > csCount)
            //    options.IsVB = true;
            //else
            //    options.IsVB = false;

            bool isVB = DecodeHelperService.IsVBCode(code);

            // Retrieve classes based on the detected language
            List<ClassModel> classes = isVB ? GetVBClasses(code, options) : GetCSharpClasses(code, options);

            if (classes.Count == 0)
                throw new DevAssistantException("No classes found", 1201);

            if (options.IncludProperties)
            {
                Log.Logger.Information("Get all properties for classes's body");

                // Extract namespace information for contracts
                string contractCode = string.Empty;
                if (!isVB)
                {
                    int startIndex = code.IndexOf("namespace Nic.Apis.");

                    if (startIndex > -1)
                    {
                        contractCode = code[(startIndex + 19)..].Trim();
                        contractCode = contractCode[..contractCode.IndexOf('.')].ToLower();
                    }
                }

                foreach (var classInfo in classes)
                {
                    if (isVB)
                    {
                        GetVBProperties(classInfo, options);
                    }
                    else
                    {
                        GetCSharpProperties(classInfo, contractCode, options);
                    }
                }

                // usually happened when classes that has no Properties because in some cases there is classes have nested class only (without Properties) such as Sbc models in SOAP
                int removedClasses = classes.RemoveAll(cls => cls.Properties.Count == 0);

                if (removedClasses > 0)
                {
                    Log.Logger.Information($"{removedClasses} was removed. Classes that has no Properties was removed");
                }

                // Check for specific conditions if exceptions or spelling/rules checks are enabled
                if (options.ThrowException || options.CheckSpellingAndRules)
                {
                    // Check specific conditions for API classes
                    if (classes.Any(c => c.Name.EndsWith("Res", StringComparison.InvariantCultureIgnoreCase)) && // It's API
                        !classes.Any(c => c.Name.EndsWith("Outputs", StringComparison.InvariantCultureIgnoreCase))) // And it's not a Micro
                    {
                        // Check if both RequestTimestamp and ResponseTimestamp properties are present
                        if (classes.Where(c => c.Name.EndsWith("Res", StringComparison.InvariantCultureIgnoreCase))
                            .SelectMany(i => i.Properties)
                            .Count(p => p.Name.Equals("RequestTimestamp") || p.Name.Equals("ResponseTimestamp")) != 2)
                        {
                            throw new DevAssistantException($"Couldn't found RequestTimestamp or ResponseTimestamp in your model. Please note it's case-sensitive", 1441);
                        }
                    }
                }
            }

            if (options.IncludeFunctions)
            {
                Log.Logger.Information("Get all methods for classes's body");

                GetAllMethodsOptions methodsOptions = new()
                {
                    //CanHasMultiClasses = false, // Because we already get all classes. so when we call it it will be one class in cls.Body
                    ListAll = true
                };


                foreach (var cls in classes)
                {
                    cls.Functions = isVB ? GetVBFunctions(code, methodsOptions) : GetCSharpFunctions(cls.Body, methodsOptions);
                }
            }


            // Check if any classes were found
            if (classes.Count == 0)
            {
                throw new DevAssistantException("No classes found", 1402);
            }

            // Return the list of class models
            return classes;
        }
        catch (Exception)
        {
            throw;// new DevAssistantException(ex.Message, 1403);
        }
    }

    ///// <summary>
    ///// Extract C# classes and their properties from the given code.
    ///// </summary>
    ///// <param name="code">C# code containing class definitions</param>
    ///// <param name="options">Method options for code analysis</param>
    ///// <returns>List of ClassModel representing C# classes with their properties</returns>
    //private static List<ClassModel> GetCSharpClassesOld(string code, GetClassesOptions options)
    //{
    //    // Log method invocation
    //    Log.Logger.Information("GetCSharpClasses Called - {a}", options);

    //    // List to store the identified C# classes
    //    List<ClassModel> classes = new();

    //    int startIndex = code.IndexOf("namespace Nic.Apis.");

    //    // Ex. of RemoveWhiteSpaces in C:\Project\API\Git2\Nic.Apis.Sbc\BusinessLayer\ServiceBus\EsbMciCompaniesEditRequestCreate.cs:
    //    // With RemoveWhiteSpaces(" ")     : 8356  words
    //    // Woithout RemoveWhiteSpaces(" ") : 17030 words
    //    // Split code into words and lines for efficient processing
    //    string[] words = code.Replace("\r\n", "\n").Replace("\n", " ").Replace("\r", " ").RemoveWhiteSpaces(" ").Trim().Split(Convert.ToChar(" "));
    //    string[] lines = code.Split(Convert.ToChar("\n"));

    //    Log.Logger.Information("Get all classes");

    //    for (int i = 0; i < words.Length; i++)
    //    {
    //        // Check for "public class" keyword sequence indicating class declaration
    //        if (words[i] == "public" && words[i + 1].Equals("class"))
    //        {
    //            // Create a ClassModel to represent the identified class
    //            ClassModel classModel = new()
    //            {
    //                IsVB = false
    //            };

    //            // Extract class name from the code
    //            string className = words[i + 2].Trim();
    //            classModel.Name = className;

    //            // Identify the index of the class declaration line
    //            int index = Array.FindIndex(lines, l => l.EndsWith($"class {className}"));

    //            // Handle variations in class declaration formatting
    //            if (index == -1)
    //            {
    //                // Attempt to handle different formatting scenarios
    //                index = DecodeHelperService.FindClassDeclarationIndex(lines, className);
    //            }

    //            // Extract XML documentation summary for the class
    //            if (lines.At(index - 1)?.Contains("/// </summary>") ?? false)
    //                classModel.ClassDesc = lines.At(index - 2)?.Replace("///", "").Trim();
    //            else if (lines.At(index - 2)?.Contains("/// </summary>") ?? false)
    //                classModel.ClassDesc = lines.At(index - 3)?.Replace("///", "").Trim();

    //            // Extract class header and code
    //            string classHeader = lines.At(index);

    //            startIndex = code.LastIndexOf(classHeader);

    //            if (startIndex == -1)
    //            {
    //                classHeader = $"{words[i]} {words[i + 1]} {className}\r";

    //                startIndex = code.LastIndexOf(classHeader);

    //                if (startIndex == -1)
    //                {
    //                    classHeader = $"{words[i]} {words[i + 1]} {className}\n";

    //                    startIndex = code.LastIndexOf(classHeader);

    //                    if (startIndex == -1)
    //                    {
    //                        classHeader = $"{words[i]} {words[i + 1]} {className}{{";

    //                        startIndex = code.LastIndexOf(classHeader);

    //                        if (startIndex == -1)
    //                        {
    //                            classHeader = $"{words[i]} {words[i + 1]} {className} {{";

    //                            startIndex = code.LastIndexOf(classHeader);

    //                            if (startIndex == -1)
    //                            {
    //                                classHeader = $"{words[i]} {words[i + 1]} {className} {{\r";

    //                                startIndex = code.LastIndexOf(classHeader);
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //            string classCode = code[startIndex..];

    //            // Extract the body of the class
    //            classModel.Body = GetBody(classCode);
    //            classModel.Properties = new List<Property>();

    //            // Add the class model to the list
    //            classes.Add(classModel);
    //        }
    //    }

    //    Log.Logger.Information("{a} class(s) found.", classes.Count);

    //    // Return the list of extracted classes
    //    return classes;
    //}

    /// <summary>
    /// Extracts properties from a C# class and performs various checks and validations.
    /// </summary>
    /// <param name="classInfo">The class information object containing the name and body of the class.</param>
    /// <param name="contractCode">The contract code for specific business logic checks.</param>
    /// <param name="options">Options for controlling the behavior of the method.</param>
    /// <returns>A list of properties found in the class.</returns>
    private static void GetCSharpProperties(ClassModel classInfo, string contractCode, GetClassesOptions options)
    {
        // List of unexpected or non-standardized data types
        List<string> unexpectedDataTypes = new()
        {
            "int16",
            "int32",
            "int64",
            "uint16",
            "uint32",
            "uint64",
            "ulong",
            "long",
            "boolean",
        };


        // Log class processing start
        Log.Logger.Information("{a} -- START", classInfo.Name);

        // Two capital letters are allowed next to each other in one word -- START
        classInfo.Remarks = DecodeHelperService.CheckCapitalLetters(classInfo.Name, options.ThrowException, true, true);
        classInfo.LongRemarks = DecodeHelperService.CheckCapitalLetters(classInfo.Name, options.ThrowException, true, true, true);

        // Split class body into lines
        var lines = classInfo.Body.Split(Convert.ToChar("\n"));

        // Regex to match property declarations in C#.
        var propertyRegex = new Regex(@"public\s+(?<type>[^\s]+)\s+(?<name>[^\s]+)\s*\{.*?\}", RegexOptions.Singleline);

        bool isInsideNestedClass = false;
        int nestedClassDepth = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];

            // commented Prop, comment or summary will be ignored. Also to ignore "public" word in the summary
            // Skip lines starting with "//" (comments)
            if (string.IsNullOrWhiteSpace(line) || line.Trim().StartsWith("//"))
                continue;

            // Split line into words
            //string[] wordsPerLine = lines[i].RemoveWhiteSpaces(" ").Split(Convert.ToChar(" "));

            //for (int j = 0; j < wordsPerLine.Length; j++)
            //{
            //    string word1 = wordsPerLine[j].Trim();
            //    string word2 = wordsPerLine.At(j + 1)?.Trim();

            //    // Check if it's a property declaration
            //    //    it's declaration     &&       not a class      &&              not ctor             >> so, it is a prop
            //    if (word1.Equals("public") && !word2.Equals("class") && !word2.Equals($"{classInfo.Name}()"))
            //    {
            //        Console.WriteLine(wordsPerLine[j] + " " + wordsPerLine[j + 1]);

            //        // validation and processing was here before refactoring.. but now I used propertyRegex.Match
            //    }
            //}



            // Below check should skip nested classes when processing properties of the current (classInfo) class. 
            // the processing for properties is skipped until we exit the nested class block

            // Check for nested class start or end
            //       Not main class's declaration          &&
            if (!ValidateClassDeclaration(classInfo, line) && (line.Contains("class") || isInsideNestedClass))
            {
                if (!isInsideNestedClass)
                    isInsideNestedClass = true;

                if (line.Contains('{'))
                {
                    nestedClassDepth++;
                }

                if (line.Contains('}'))
                {
                    nestedClassDepth--;

                    if (nestedClassDepth == 0)
                    {
                        isInsideNestedClass = false;
                    }
                }
            }

            // Skip processing if inside a nested class
            if (isInsideNestedClass)
                continue;

            var match = propertyRegex.Match(line);

            if (match.Success)
            {
                var type = match.Groups["type"].Value.Trim();
                var name = match.Groups["name"].Value.Trim();

                // Get basic property info
                Property property = new()
                {
                    Name = name,
                    ModelName = classInfo.Name,
                    DataType = type,
                    IsNullable = type.EndsWith('?')
                };

                // Remove namespace (if present) from data type. such as "MicoA.ObjectModel.MicroAInputs"
                if (property.DataType.Contains('.'))
                {
                    property.DataType = property.DataType[property.DataType.LastIndexOf('.')..];
                }

                // Log property found
                Log.Logger.Information("Property Found. {a} {b}", property.Name, property.DataType);

                // Validate & Get XML documentation
                DecodeHelperService.ValidateAndGetXmlDoc(options, lines, property, classInfo, i);

                // Check Spelling and Rules
                if (options.CheckSpellingAndRules)
                {
                    // Data type as chars
                    char[] dataT = property.DataType.ToCharArray();

                    // Validate XML documentation for Arabic language
                    if (options.ProjectType is not ProjectType.Micro && string.IsNullOrWhiteSpace(property.DescAr))
                    {
                        DecodeHelperService.LogOrThrow(options, "No XML found for Arabic language", property, 1404);
                    }

                    // Validate XML documentation for English language
                    if (string.IsNullOrWhiteSpace(property.DescEn))
                    {
                        DecodeHelperService.LogOrThrow(options, "No XML found for English language", property, 1442);
                    }

                    // Check if data type starts with a capital letter
                    if (dataT[0] == char.ToUpper(dataT[0]) && ValidationService.IsPrimitiveDatatype(property.DataType, true, false))
                    {
                        DecodeHelperService.LogOrThrow(options, $"Data type \"{property.DataType}\" starts with a capital letter", property, 1405);
                    }

                    // Check unexpected data types
                    if (unexpectedDataTypes.Contains(property.DataType.ToLower()))
                    {
                        DecodeHelperService.LogOrThrow(options, $"Unexpected data type \"{property.DataType}\"", property, 1406);
                    }

                    // Check for short data type
                    if (property.DataType.ToString().ToLower() == "short")
                    {
                        DecodeHelperService.LogOrThrow(options, "Data type is short. Must be int", property, 1407);
                    }

                    // Check specific rules for "BirthDate"
                    /*
                     ******************************************************
                     * Check BirthDate must be before all dates validations
                     ******************************************************
                     */

                    if (classInfo.Name.EndsWith("Req"))
                    {
                        if (property.Name.RemoveRequired().Contains("BirthDate") && !property.Name.RemoveRequired().Equals("BirthDate"))
                        {
                            DecodeHelperService.LogOrThrow(options, $"Birth date property must accept both Hijri and Gregorian date in {classInfo.Name}", property, 1431);
                        }
                        else if (property.Name.RemoveRequired().Equals("BirthDate") && !property.DataType.ToLower().Equals("int"))
                        {
                            DecodeHelperService.LogOrThrow(options, "Birth date property must be integer", property, 1432);
                        }
                    }
                    // Check BirthDate -- END

                    // Check date properties in "Res" or "Req"
                    //  check all date in Res           ||                        check all except BirthDate
                    if ((classInfo.Name.EndsWith("Res") || !property.Name.RemoveRequired().Equals("BirthDate") && classInfo.Name.EndsWith("Req")) &&
                        (!property.Name.RemoveRequired().Contains("DateH") || !property.Name.RemoveRequired().Contains("DateG")) &&
                       (property.Name.RemoveRequired().EndsWith("Date") ||
                        property.Name.RemoveRequired().EndsWith("DateHijri") ||
                        property.Name.RemoveRequired().EndsWith("HDate") ||
                        property.Name.RemoveRequired().EndsWith("GDate") ||
                        property.Name.RemoveRequired().EndsWith("DateGregorian")))
                    {
                        // Check specific conditions for data types and controller name
                        if (property.DataType.ToString().ToLower() == "datetime" || property.DataType.ToString().ToLower() == "int" ||
                            options.ControllerName.Equals("ServiceBus"))
                        {
                            DecodeHelperService.LogOrThrow(options, "Date property must end with suffix **DateG or **DateH", property, 1424);
                        }
                        else
                        {
                            // Check unexpected data type
                            DecodeHelperService.LogOrThrow(options, $"Unexpected data type for Date property in {classInfo.Name}", property, 1433);
                        }
                    }

                    // Check data type for "Hijri" date
                    if (property.Name.RemoveRequired().EndsWith("DateH") && !property.DataType.ToLower().Equals("int"))
                    {
                        DecodeHelperService.LogOrThrow(options, $"Data type for Hijri date must be integer", property, 1430);
                    }

                    // Check data type for "Gregorian" date
                    if (property.Name.RemoveRequired().EndsWith("DateG") && !property.DataType.ToLower().Equals("datetime"))
                    {
                        DecodeHelperService.LogOrThrow(options, "Data type for Gregorian date must be DateTime", property, 1434);
                    }

                    // Check Ar/En property name conditions
                    if (property.Name.RemoveRequired().EndsWith("AR") || property.Name.RemoveRequired().EndsWith("EN"))
                    {
                        DecodeHelperService.LogOrThrow(options, "Property name end with AR/EN must be Ar/En", property, 1408);
                    }

                    if (property.Name.Contains('_') && !property.Name.ToLower().Contains("yyyymmdd"))
                    {
                        DecodeHelperService.LogOrThrow(options, "Property name contains \"_\". Property must be PascalCase", property, 1409);
                    }

                    if (property.Name.EndsWith("Id"))
                    {
                        DecodeHelperService.LogOrThrow(options, "Property name end with \"Id\". Must be \"ID\"", property, 1410);
                    }

                    if (property.Name.RemoveRequired().ToLower() != "description" && property.Name.RemoveRequired().ToLower().Contains("description"))
                    {
                        DecodeHelperService.LogOrThrow(options, "Description must be Desc", property, 1411);
                    }

                    if (property.Name.RemoveRequired().ToLower() == "requesttimestamp")
                    {
                        if (!property.Name.RemoveRequired().Equals("RequestTimestamp"))
                        {
                            DecodeHelperService.LogOrThrow(options, "Property name must be RequestTimestamp. Please note it's case-sensitive", property, 1412);
                        }
                    }

                    if (property.Name.RemoveRequired().ToLower() == "responsetimestamp")
                    {
                        if (!property.Name.RemoveRequired().Equals("ResponseTimestamp"))
                        {
                            DecodeHelperService.LogOrThrow(options, "Property name must be ResponseTimestamp. Please note it's case-sensitive", property, 1413);
                        }
                    }

                    if (property.Name.RemoveRequired().ToLower() == "operatorid" && classInfo.Name.EndsWith("Req"))
                    {
                        int requiredIndex = DecodeHelperService.CheckHasRequiredAnnotation(lines, i);
                        bool isGcc = Consts.GccContracts?.Any(gcc => contractCode.EndsWith(gcc.ToLower())) ?? false;

                        if (requiredIndex > -1) // Required found
                        {
                            if (isGcc) // Gcc must not have Required
                            {
                                DecodeHelperService.LogOrThrow(options, "OperatorID for GCC contracts is not Required", property, 1429);
                            }
                        }
                        else // Required not found
                        {
                            if (!isGcc) // Other contracts must have Required
                            {
                                DecodeHelperService.LogOrThrow(options, "OperatorID must be Required", property, 1423);
                            }
                        }
                    }

                    if (property.Name.RemoveRequired().ToLower() == "clientipaddress")
                    {
                        // Check specific condition for property name
                        if (!property.Name.RemoveRequired().Equals("ClientIPAddress"))
                        {
                            DecodeHelperService.LogOrThrow(options, "Property name must be ClientIPAddress. Please note it's case-sensitive", property, 1414);
                        }
                        else
                        {
                            int requiredIndex = DecodeHelperService.CheckHasRequiredAnnotation(lines, i);

                            if (contractCode.Equals("Twk", StringComparison.CurrentCultureIgnoreCase))
                            {
                                if (requiredIndex > -1)
                                {
                                    DecodeHelperService.LogOrThrow(options, "ClientIPAddress in Tawakkalna is not Required", property, 1422);
                                }
                            }
                            else if (requiredIndex == -1)
                            {
                                DecodeHelperService.LogOrThrow(options, "ClientIPAddress must be Required", property, 1428);
                            }
                        }
                    }

                    // Two capital letters are allowed next to each other in one word
                    property.Remarks += DecodeHelperService.CheckCapitalLetters(property.Name.RemoveRequired(), options.ThrowException, options.ShowErrorCode);
                    property.LongRemarks += DecodeHelperService.CheckCapitalLetters(property.Name.RemoveRequired(), options.ThrowException, options.ShowErrorCode, isLongRemarks: true);
                } // end of checkSpelling

                // Check in the last to avoid error 1405
                if (options.DataTypeAsPascalCase)
                {
                    // Convert data type to PascalCase
                    char[] dataT = property.DataType.ToCharArray();
                    dataT[0] = char.ToUpper(dataT[0]);
                    string tempDatetype = new(dataT);

                    tempDatetype = tempDatetype.Replace("?", "");

                    // Additional conversion for specific data types
                    if (tempDatetype.Equals("Int"))
                    {
                        tempDatetype = "Integer";
                    }
                    else if (tempDatetype.Equals("Bool"))
                    {
                        tempDatetype = "Boolean";
                    }

                    property.DataType = tempDatetype;

                    if (property.IsNullable)
                        property.DataType += "?";

                }

                classInfo.Properties.Add(property);

            }
            // End of the body of the class (last prop in the current class)
            // So now we can check for props since we get all props
            // Check if this is the last line in the class body and the class name ends with "Req"
            if (i == lines.Length - 1 && classInfo.Name.EndsWith("Req"))
            {
                // Check if the class has a property named "lang"
                if (classInfo.Properties.Any(p => p.Name.ToLower() == "lang"))
                {
                    // Get a list of properties excluding specific ones
                    var excludedMainProp = classInfo.Properties.Where(prop =>
                    {
                        return prop.Name.ToLower().RemoveRequired() is not "operatorid"
                            and not "clientipaddress"
                            and not "lang";
                    }).ToList();

                    // If there is only one property, it must be marked as [Required]
                    if (excludedMainProp.Count == 1)
                    {
                        int annotationIndex = -1;

                        // Create a line to search for in the code based on the excluded property
                        var lineToSearch = $"{excludedMainProp[0].DataType.ToLower().Replace("integer", "int")} {excludedMainProp[0].Name.RemoveRequired()}";

                        // Find the index of the line in the code
                        int index = Array.FindIndex(lines, l => l.ToLower().Contains(lineToSearch.ToLower()));

                        // Loop to find the annotation for the property
                        for (int k = 1; k < 6; k++)
                        {
                            // Check if we have reached the beginning of the file or the previous property
                            //            || Prev prop
                            if (index < k || lines[index - k].Trim().StartsWith("public"))
                                break;

                            // Check if the line contains "Required" to determine annotation index
                            if (lines[index - k].Contains("Required"))
                            {
                                annotationIndex = index - k;
                                break;
                            }
                        }

                        // If Required annotation is not found, handle it
                        if (annotationIndex == -1)
                        {
                            if (options.ThrowException)
                            {
                                // Throw an exception if the property is not marked as [Required]
                                throw new DevAssistantException($"The property \"{excludedMainProp[0].Name.RemoveRequired()}\" must be [Required]", 1421);
                            }
                            else
                            {
                                // Log a remark if the property is not marked as [Required]
                                classInfo.Properties
                                    .FirstOrDefault(p => p.Name == excludedMainProp[0].Name.RemoveRequired())
                                    .Remarks += $"      * The property \"{excludedMainProp[0].Name.RemoveRequired()}\" must be [Required]{Environment.NewLine}";

                                // Log detailed remark with error code
                                classInfo.Properties
                                    .FirstOrDefault(p => p.Name == excludedMainProp[0].Name.RemoveRequired())
                                    .LongRemarks += $"The property \"{excludedMainProp[0].Name.RemoveRequired()}\" must be [Required] [1421]{Environment.NewLine}";

                                // Include error code in remarks if configured to show error codes
                                if (options.ShowErrorCode)
                                {
                                    classInfo.Properties
                                        .FirstOrDefault(p => p.Name == excludedMainProp[0].Name.RemoveRequired())
                                        .Remarks += " [1421]";
                                }
                            }
                        }
                    }
                }
            }
        } // end of lines loop

        // End of processing for the current class
        Log.Logger.Information("{a} -- END with {b} prop(s)", classInfo.Name, classInfo.Properties.Count);
    }



    /// <summary>
    /// Validate id the line is matched the class declaration for supplied classInfo
    /// </summary>
    /// <param name="classInfo"></param>
    /// <param name="line"></param>
    /// <returns></returns>
    private static bool ValidateClassDeclaration(ClassModel classInfo, string line)
    {
        string expectedDeclaration = $"public class {classInfo.Name}\r";
        string actualDeclaration = $"{line.RemoveWhiteSpaces(" ").RemoveAppendedLine()}\r";

        return actualDeclaration.Equals(expectedDeclaration);
    }


    /// <summary>
    /// Extracts properties from a VB class and performs various checks and validations.
    /// </summary>
    /// <param name="classInfo">The class information object containing the name and body of the class.</param>
    /// <param name="options">Options for controlling the behavior of the method.</param>
    /// <returns>A list of properties found in the class.</returns>
    private static void GetVBProperties(ClassModel classInfo, GetClassesOptions options)
    {
        string[] lines = classInfo.Body.Split(Convert.ToChar("\n"));

        for (int i = 0; i < lines.Length; i++)
        {
            // commented Prop, comment or summary will be ignored. Also to ignore "public" word in the summary
            if (lines[i].Trim().StartsWith("//") || lines[i].Trim().StartsWith("'") || lines[i].Trim().StartsWith("*"))
                continue;

            string[] wordsPerLine = lines[i].Trim().Split(Convert.ToChar(" "));

            for (int j = 0; j < wordsPerLine.Length; j++)
            {
                //       it's declaration       &&          it's a Property          >> so, it is a prop :)
                if (wordsPerLine[j] == "Public" && wordsPerLine[j + 1] == "Property")
                {
                    Console.WriteLine(wordsPerLine[j] + " " + wordsPerLine[j + 1] + " " + wordsPerLine[j + 2]);

                    // Get basic info
                    Property property = new()
                    {
                        Name = wordsPerLine[j + 2].Replace("()", "").Trim(),
                        DataType = wordsPerLine[j + 4].Trim(),
                        ModelName = classInfo.Name
                    };

                    if (property.DataType.Contains('.'))
                    {
                        property.DataType = property.DataType[(property.DataType.IndexOf('.') + 1)..];
                    }

                    if (options.DataTypeAsPascalCase)
                    {
                        // Ex. Int >> int
                        char[] dataT = property.DataType.ToCharArray();
                        dataT[0] = char.ToUpper(dataT[0]);
                        property.DataType = new string(dataT);

                        if (property.DataType == "Int")
                        {
                            property.DataType = "Integer";
                        }
                        else if (property.DataType == "Bool")
                        {
                            property.DataType = "Boolean";
                        }
                    }

                    if (property.DataType.Contains("()"))
                    {
                        property.DataType = property.DataType.Replace("()", "") + "[]";
                    }
                    else if (property.DataType.Contains("(Of") || property.DataType.StartsWith("List("))
                    {
                        property.DataType = $"List<{wordsPerLine[j + 5].Replace(")", "")}>";
                    }

                    // TODO: only when converting to C#.
                    if (property.DataType.Equals("Date") || property.DataType.Equals("Date[]") || property.DataType.Equals("List<Date>"))
                    {
                        property.DataType = property.DataType.Replace("Date", "DateTime");
                    }

                    // Check Spelling and Rules -- START
                    if (options.CheckSpellingAndRules)
                    {
                        // data type as chars
                        char[] dataT = property.DataType.ToCharArray();

                        //if (!lines[i - 1].Contains("///") || !lines[i - 2].Contains("///") ||
                        //       !lines[i - 3].Contains("///") || !lines[i - 4].Contains("///"))
                        //{
                        //    property.Remarks += $"      * No XML documentation found.";
                        //    property.Remarks += Environment.NewLine;
                        //}

                        if (dataT[0] == char.ToLower(dataT[0]))
                        {
                            property.Remarks += $"      * Data type \"{property.DataType}\" is start with small letter.";

                            if (options.ShowErrorCode)
                                property.Remarks += " [1435]";

                            property.Remarks += Environment.NewLine;
                        }

                        if (property.DataType.ToString().ToLower() == "short")
                        {
                            property.Remarks += "      * Data type is short. Must be Integer";

                            if (options.ShowErrorCode)
                                property.Remarks += " [1436]";

                            property.Remarks += Environment.NewLine;
                        }

                        if (property.Name.EndsWith("AR") || property.Name.EndsWith("EN"))
                        {
                            property.Remarks += "      * Property name end with AR/EN must be Ar/En.";

                            if (options.ShowErrorCode)
                                property.Remarks += " [1437]";

                            property.Remarks += Environment.NewLine;
                        }

                        if (property.Name.Contains('_'))
                        {
                            property.Remarks += "      * Property name contains \"_\". Property must be PascalCase.";

                            if (options.ShowErrorCode)
                                property.Remarks += " [1438]";

                            property.Remarks += Environment.NewLine;
                        }

                        if (property.Name.ToLower() != "description" && property.Name.ToLower().Contains("description"))
                        {
                            property.Remarks += "      * Description must be Desc.";

                            if (options.ShowErrorCode)
                                property.Remarks += " [1439]";

                            property.Remarks += Environment.NewLine;
                        }

                        // Two capital letters are allowed next to each other in one word -- START

                        // if property is 2 letters such as SP, Sp or sp. no need to check
                        // the next check if enough to check if start with capital.

                        if (property.Name[0] != char.ToUpper(property.Name[0]))
                        {
                            property.Remarks += "      * Property must start with Capital. Property must be PascalCase.";

                            if (options.ShowErrorCode)
                                property.Remarks += " [1440]";

                            property.Remarks += Environment.NewLine;
                        }

                        // if it is 3 letters such as HOH must by Hoh
                        //if (property.Name.Length == 3
                        //       && property.Name[0] == char.ToUpper(property.Name[0])
                        //       && property.Name[1] == char.ToUpper(property.Name[1])
                        //       && property.Name[2] == char.ToUpper(property.Name[2]))
                        //{
                        //    property.Remarks += "      * There are more than 2 capital letter next to each other. Property must be PascalCase.";
                        //    property.Remarks += Environment.NewLine;
                        //}
                        //else if (property.Name.Length > 3)
                        //{
                        //    for (int k = 0; k < property.Name.Length; k++)
                        //    {
                        //        // to handle if the last 3 letters are Capetel such as RecipientHOH
                        //        if (k == property.Name.Length - 1) // Last index
                        //        {
                        //            if (property.Name[k] == char.ToUpper(property.Name[k])
                        //                && property.Name[k - 1] == char.ToUpper(property.Name[k - 1])
                        //                && property.Name[k - 2] == char.ToUpper(property.Name[k - 2]))
                        //            {
                        //                property.Remarks += "      * There are more than 2 capital letter next to each other. Property must be                                          PascalCase.";
                        //                property.Remarks += Environment.NewLine;

                        //                break;
                        //            }
                        //        }
                        //        else if (k > 2)
                        //        {
                        //            if (property.Name[k - 1] == char.ToUpper(property.Name[k - 1])
                        //                && property.Name[k - 2] == char.ToUpper(property.Name[k - 2])
                        //                && property.Name[k - 3] == char.ToUpper(property.Name[k - 3]))
                        //            {
                        //                if (property.Name[k].ToString() == "_" || property.Name[k] == char.ToUpper(property.Name[k]))
                        //                {
                        //                    property.Remarks += "      * There are more than 2 capital letter next to each other. Property must be                                          PascalCase.";
                        //                    property.Remarks += Environment.NewLine;

                        //                    break;
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        // Two capital letters are allowed next to each other in one word -- END

                        // Two capital letters are allowed next to each other in one word -- START
                        property.Remarks += DecodeHelperService.CheckCapitalLetters(property.Name.RemoveRequired(), options.ThrowException, options.ShowErrorCode);
                        // Two capital letters are allowed next to each other in one word -- END
                    } // end of checkSpelling
                      // Check Spelling and Rules -- END

                    classInfo.Properties.Add(property);
                }
            } // end of wordsPerLine loop
        } // end of lines loop

        Log.Logger.Information("{a} -- END with {b} prop(s)", classInfo.Name, classInfo.Properties.Count);
    }


    // Old method
    ///// <summary>
    ///// Extracts functions from C# code.
    ///// </summary>
    ///// <param name="code">The C# code as a string.</param>
    ///// <param name="options">Options for function extraction, if any.</param>
    ///// <returns>A list of functions extracted from the code.</returns>
    //private static List<Function> GetCSharpFunctions(string code, GetAllMethodsOptions options)
    //{
    //    Log.Logger.Information("GetCSharpFunctions Called - code.Length: {a}, InterfaceCode: {b}, IsControllerMethod: {c}", code.Length, options.InterfaceCode, options.IsControllerMethod);

    //    var functions = new List<Function>();

    //    string[] words = code.Split(Convert.ToChar(" "));

    //    for (int i = 0; i < words.Length; i++)
    //    {
    //        //   it's declaration    &&       not a class       &&           not ctor           &&          not a prop     --> so, it is a method
    //        if (words[i] == "public" && words[i + 1] != "class" && !words[i + 2].Contains("()") && !words[i + 4].Contains("get"))
    //        {
    //            if (options.IsControllerMethod && !words[i + 1].StartsWith("ActionResult"))
    //                continue;

    //            var function = new Function();

    //            var a = $"{words[i]}-{words[i + 1]}-{words[i + 2]}";

    //            string funName = words[i + 2][..words[i + 2].IndexOf("(")];
    //            function.FunName = funName.Trim();

    //            string funHeader = words[i] + " " + words[i + 1] + " " + funName;

    //            int startIndex = code.IndexOf(funHeader);
    //            string funCode = code[startIndex..];

    //            funCode = GetBody(funCode);

    //            function.Code = funCode;

    //            if (options.GetDBType)
    //                (function.DBName, function.Configuration) = GetDBType(funCode, funName);

    //            functions.Add(function);
    //        }
    //    }

    //    return functions;
    //}

    /// <summary>
    /// Extracts functions from a given C# code string.
    /// </summary>
    /// <param name="code">The C# code to analyze.</param>
    /// <param name="options">Additional options for function extraction.</param>
    /// <returns>A list of extracted functions.</returns>
    private static List<ClassModel> GetCSharpClasses(string code, GetClassesOptions options)
    {
        Log.Logger.Information("GetCSharpClasses Called - {a}", options);

        var classes = new List<ClassModel>();
        //var classPattern = @"\bclass\s+(\w+)";
        //string summaryPattern = @"/// <summary>\s*([\s\S]*?)\s*/// </summary>";
        //string summaryPattern = @"(/// <summary>[\s\S]*?</summary>\s+)?public class (\w+)[^{]*\{[\s\S]*?\}"; // I really hate Regex :(
        //string classPattern = @"(public|private|protected|internal)?\s+class\s+(\w+)\s*{";
        //string classPattern = @"(/// <summary>[\s\S]*?</summary>\s+)?public\s+class\s+(\w+)[^{]*\{";
        string classPattern = @"public\s+class\s+(\w+)\s*{";
        var classMatches = Regex.Matches(code, classPattern);

        foreach (Match classMatch in classMatches)
        {
            var className = classMatch.Groups[1].Value;
            var openingIndex = classMatch.Index;
            var closingIndex = FindClosingBracketIndex(code, openingIndex + classMatch.Length);

            if (closingIndex > openingIndex)
            {
                var classBody = code.Substring(openingIndex, closingIndex - openingIndex + 1);

                // Extract class summary
                //                                                                                  we must use this to avoid getting wrong summary if the class has no summary
                string summaryPattern = @"/// <summary>[\s\S]*?</summary>\s+(?=public\s+class\s+" + Regex.Escape(className) + @"\s*{)";
                var summaryMatch = Regex.Match(code, summaryPattern, RegexOptions.Singleline | RegexOptions.RightToLeft);
                string classSummary = summaryMatch.Success ? summaryMatch.Value.Replace(@"///", "").Replace("<summary>", "").Replace("</summary>", "").Trim() : string.Empty;

                var classModel = new ClassModel
                {
                    Name = className,
                    Body = classBody,
                    ClassDesc = classSummary,
                    IsVB = DecodeHelperService.IsVBCode(classBody),
                    Functions = new(),
                    Properties = new(),
                    Remarks = string.Empty,
                    LongRemarks = string.Empty,
                };

                classes.Add(classModel);
            }
        }

        if (classes.Count != classMatches.Count)
        {
            // TODO: throw.
        }

        Log.Logger.Information("{a} class(s) found.", classes.Count);

        return classes;
    }


    /// <summary>
    /// Extracts functions from a given C# code string.
    /// </summary>
    /// <param name="code">The C# code to analyze.</param>
    /// <param name="options">Additional options for function extraction.</param>
    /// <returns>A list of extracted functions.</returns>
    private static List<Function> GetCSharpFunctions(string code, GetAllMethodsOptions options)
    {
        List<Function> functions = new();

        // Regular expression to match C# function definitions
        Regex functionRegex;

        if (options.ListAll)
            //functionRegex = new(@"\b(public|private|protected|internal|static)\s+(?:[\w<>,\[\]]+\s+)?\w+\s*\([^)]*\)\s*{", RegexOptions.Singleline); // I hate regular expression
            functionRegex = new(@"(public|private|protected|internal)(\s+static)?\s+(\w[\w<>\[\],\s]*)\s+(\w+)\s*\((.*?)\)\s*{", RegexOptions.Singleline);
        else
            functionRegex = new(@"(public|protected)(\s+static)?\s+(\w[\w<>\[\],\s]*)\s+(\w+)\s*\((.*?)\)\s*{", RegexOptions.Singleline);

        // Find all matches in the provided code
        MatchCollection matches = functionRegex.Matches(code);

        foreach (Match match in matches)
        {
            if (match.Success)
            {
                int openingIndex = match.Index;
                int closingIndex = FindClosingBracketIndex(code, openingIndex + match.Length);

                if (closingIndex > openingIndex)
                {
                    string functionDefinition = match.Value;
                    string functionBody = code[openingIndex..(closingIndex + 1)].Trim();

                    Function function = new()
                    {
                        FunName = match.Groups[4].Value,
                        Definition = functionDefinition,
                        AccessLevel = match.Groups[1].Value,
                        ReturnType = match.Groups[3].Value,
                        Code = $"{functionDefinition} {functionBody}"
                    };

                    if (options.GetDBType)
                        (function.DBName, function.Configuration) = GetDBType(function.Code, function.FunName);

                    functions.Add(function);
                }
            }
        }

        return functions;

    }


    /// <summary>
    /// Finds the index of the closing bracket for a given opening bracket in the code.
    /// </summary>
    /// <param name="code">The code to search in.</param>
    /// <param name="openingIndex">The index of the opening bracket.</param>
    /// <returns>The index of the closing bracket if found, otherwise -1.</returns>
    private static int FindClosingBracketIndex(string code, int openingIndex)
    {
        // Initialize the depth counter to track nested brackets
        //int depth = 1;

        //for (int i = openingIndex + 1; i < code.Length; i++)
        //{
        //    // If an opening bracket is found, increase the depth
        //    if (code[i] == '{')
        //    {
        //        depth++;
        //    }
        //    else if (code[i] == '}') // If a closing bracket is found, decrease the depth
        //    {
        //        depth--;

        //        // If the depth reaches zero, we found the matching closing bracket
        //        if (depth == 0)
        //        {
        //            return i;
        //        }
        //    }
        //}

        // It starts at 1 assuming openingIndex is at the opening bracket '{'
        int depth = 1;

        // Flags to track whether we are currently inside a string, single line comment, or multi-line comment
        bool isInString = false;
        bool isInSingleLineComment = false;
        bool isInMultiLineComment = false;

        for (int i = openingIndex; i < code.Length; i++)
        {
            char currentChar = code[i];

            // Check for string start/end
            if (currentChar == '"' && !isInSingleLineComment && !isInMultiLineComment && (i == 0 || code[i - 1] != '\\'))
            {
                isInString = !isInString;
            }

            // Detecting the start of comments
            if (!isInString && currentChar == '/' && i + 1 < code.Length)
            {
                if (code[i + 1] == '/' && !isInMultiLineComment)
                {
                    isInSingleLineComment = true;
                }
                else if (code[i + 1] == '*' && !isInSingleLineComment)
                {
                    isInMultiLineComment = true;
                    i++; // Skip '*' as it's part of the comment start
                }
            }

            // Detecting the end of a multi-line comment
            if (!isInString && currentChar == '*' && i + 1 < code.Length && code[i + 1] == '/' && isInMultiLineComment)
            {
                isInMultiLineComment = false;
                i++; // Skip '/' as it's part of the comment end
            }

            // Detecting the end of a single-line comment
            if (currentChar == '\n' && isInSingleLineComment)
            {
                isInSingleLineComment = false;
            }

            // Processing brackets only when not in a string or comment
            if (!isInString && !isInSingleLineComment && !isInMultiLineComment)
            {
                if (currentChar == '{')
                {
                    depth++; // Increase depth for each opening bracket
                }
                else if (currentChar == '}')
                {
                    depth--; // Decrease depth for each closing bracket
                    if (depth == 0)
                    {
                        // When depth reaches 0, we found the matching closing bracket
                        return i;
                    }
                }
            }
        }

        return -1; // indicates no closing bracket found
    }

    private static (DataBaseType, string) GetDBType(string code, string funName)
    {
        Log.Logger.Information("GetDBType Called - funName: {funName}", funName);

        var dbType = DataBaseType.NotSpecified;

        string configuration = string.Empty;
        int rootservicesIndex;
        int connectionStringIndex;
        int sqlIndex;
        int executeIndex;

        code = code.ToLower();

        rootservicesIndex = code.IndexOf("return rootservices");
        sqlIndex = code.IndexOf("select ");
        executeIndex = code.IndexOf("execute(");

        if (executeIndex != -1)
        {
            configuration = code[executeIndex..];

            executeIndex = configuration.IndexOf(",");
            configuration = configuration[(executeIndex + 1)..];

            int endIndex = configuration.IndexOf(")");

            configuration = configuration[..endIndex].Replace(" ", "").Trim();
            Console.WriteLine("Cong:" + configuration);
        }

        connectionStringIndex = code.IndexOf("getconnectionstringfromcurrenthostservice");

        if (connectionStringIndex == -1)
        {
            connectionStringIndex = code.IndexOf("configurationmanager.connectionstrings");
        }

        // calling root
        if (rootservicesIndex != -1)
        {
            dbType = DataBaseType.Null;
        }
        else
        {
            if (funName.Contains("micro") || funName.Contains("pds") || configuration.Contains("commcfgnet") ||
               sqlIndex != -1 || connectionStringIndex != -1)
            {
                dbType = DataBaseType.PDS;
            }

            if (funName.Contains("update") || funName.Contains("delete") || funName.Contains("npv") ||
               funName.Contains("issue") || funName.Contains("validate") || funName.Contains("extend") ||
               funName.Contains("cancel") || funName.Contains("send") || funName.Contains("create") ||
               funName.Contains("insert") || executeIndex != -1 && !configuration.Contains("commcfgnet"))
            {
                if (dbType == DataBaseType.PDS)
                {
                    dbType = DataBaseType.Both;
                }
                else
                {
                    dbType = DataBaseType.DB2;
                }
            }
        }

        return (dbType, configuration);
    }

    private static string GetRootPath(string[] lines, int index, string path, string controllerName, out string serviceName)
    {
        Log.Logger.Information("GetRootPath Called - path: {a}, controllerName: {b}", path, controllerName);

        /*
        * Case 1:
        * Root.BusinessLayer.Person.GetFamilyRelationship rootService = new();
        * Case 2:
        * var rootService = new Root.BusinessLayer.Person.GetFamilyRelationship();
        * Case 3:
        *  a)
        *  GetMoiJobDetailsRes rootServiceRes = rootService.ApiBusiness(rootServiceReq, _contractOptions);
        *  b)
        *  var rootServiceRes = rootService.ApiBusiness(rootServiceReq, _contractOptions);
        *      if start with var go back to service(class) Initialize
        */

        serviceName = string.Empty;

        string line = lines[index].Trim();

        bool isGcc = line.Contains(".Gcc.");
        bool isNpv = line.Contains(".Npv.");

        if (line.Contains(".ApiBusiness("))
        {
            if (line.StartsWith("var"))
            {
                for (int j = 1; j < 5; j++)
                {
                    if (lines[index - j].RemoveWhiteSpaces().Contains("=new"))
                    {
                        if (lines[index - j].Trim().StartsWith("var"))
                        {
                            serviceName = lines[index - j].Trim();

                            int startIndex = serviceName.IndexOf("new ") + 3;
                            int endIndex = serviceName.LastIndexOf('(');

                            if (endIndex < 0)
                                endIndex = serviceName.LastIndexOf('{');

                            endIndex -= startIndex;

                            serviceName = serviceName.Substring(startIndex, endIndex).Trim();
                        }
                        else
                        {
                            serviceName = lines[index - j].Trim()[..lines[index - j].IndexOf(' ')].ToString().Replace("Res", "").Replace("Req", "");
                        }

                        break;
                    }
                }
            }
            else
            {
                var temp = line.Split('=');

                // rootServiceFurijatRes = rootServiceFurijat.ApiBusiness(rootServiceFurijatReq, contractOptionsBase);
                // that mean it was Initialized above. maybe above while or in the top of the function
                if (temp.Length == 2 && temp[0].Trim().Split(' ').Length == 1)
                {
                    var rootServiceName = temp[1][..temp[1].IndexOf('.')].Trim();

                    for (int j = 1; j < index; j++)
                    {
                        if (lines[index - j].Contains($"{rootServiceName} ") || lines[index - j].Contains($"{rootServiceName};"))
                        {
                            if (lines[index - j].Trim().StartsWith("var"))
                            {
                                serviceName = lines[index - j].Trim();

                                int startIndex = serviceName.IndexOf("new ") + 3;
                                int endIndex = serviceName.LastIndexOf('(');

                                if (endIndex < 0)
                                    endIndex = serviceName.LastIndexOf('{');

                                endIndex -= startIndex;

                                serviceName = serviceName.Substring(startIndex, endIndex).Trim();
                            }
                            else
                            {
                                serviceName = lines[index - j].Trim();
                                serviceName = serviceName[..serviceName.IndexOf(' ')].Replace("Res", "").Replace("Req", "");
                            }

                            break;
                        }
                    }
                }
                else
                {
                    serviceName = line[..line.IndexOf(' ')].ToString().Replace("Res", "").Replace("Req", "");
                }
            }
        }
        else
        {
            int startIndex = line.IndexOf("Root.BusinessLayer");

            int endIndex;

            //                                               ' ' << here space
            // Root.BusinessLayer.Person.GetFamilyRelationship rootService = new();
            if (line.StartsWith("Root.BusinessLayer"))
                endIndex = line.IndexOf(' ');
            else
                endIndex = line.LastIndexOf('(');

            if (endIndex < 0)
                endIndex = line.LastIndexOf('{');

            endIndex -= startIndex;

            string[] rootInfo = line.Substring(startIndex, endIndex).ToString().Split('.');

            if (isGcc || isNpv)
                serviceName = rootInfo[4];
            else
                serviceName = rootInfo[3];
        }

        string rootPath = path[..path.LastIndexOf("Nic.Apis")];

        if (isGcc)
            rootPath = Path.Combine(rootPath, "Nic.Apis.Root", "BusinessLayer", "Gcc", controllerName, serviceName + ".cs");
        else if (isNpv)
            rootPath = Path.Combine(rootPath, "Nic.Apis.Root", "BusinessLayer", "Npv", controllerName, serviceName + ".cs");
        else
            rootPath = Path.Combine(rootPath, "Nic.Apis.Root", "BusinessLayer", controllerName, serviceName + ".cs");

        return rootPath;
    }

    private static List<ClassModel> GetVBClasses(string code, GetClassesOptions options)
    {
        Log.Logger.Information("GetVBClasses Called");

        if (code.Contains(" Inherits "))
        {
            throw new DevAssistantException("Couldn't resolved the inherits", 1500);
        }

        List<ClassModel> classes = new();

        string[] words = code.Replace("\r\n", "\n").Replace("\n", " ").Replace("\t", " ").Trim().Split(Convert.ToChar(" "));

        // Get classes
        for (int i = 0; i < words.Length; i++)
        {
            if (!code.Contains("Public Class") || string.IsNullOrWhiteSpace(code))
                break;

            if (words[i].Trim() == "Public" && words[i + 1].Equals("Class"))
            {
                ClassModel classModel = new()
                {
                    Name = words[i + 2].Trim(),
                    IsVB = true
                };

                string classHeader = words[i].Trim() + " " + words[i + 1] + " " + classModel.Name;

                int startIndex = code.IndexOf(classHeader);
                string classCode = code[startIndex..];

                int endIndex = classCode.IndexOf("End Class") + 9;
                classCode = classCode[..endIndex];

                // to remove/cut current class since we dont need it any more and to avoiding if there are 2 class start with same name
                // ex: VehicleTransportOffices and VehicleTransport****. in 2nd class the first 2 words are match the VehicleTransportOffices.
                // So, it will take the body of VehicleTransportOffices instead of VehicleTransport.
                endIndex = code.IndexOf("End Class") + 9;
                code = code[endIndex..];

                classModel.Body = classCode;
                classModel.Properties = new List<Property>();

                classes.Add(classModel);
            }
        }



        return classes;
    }

    private static List<Function> GetVBFunctions(string code, GetAllMethodsOptions options)
    {
        Log.Logger.Information("GetVBFuncations Called - code.Length: {a}, InterfaceCode: {b}, IsControllerMethod: {c}", code.Length, options.IsControllerMethod);

        var functions = new List<Function>();

        string[] words = code.Split(Convert.ToChar(" "));

        for (int i = 0; i < words.Length; i++)
        {
            if ((words[i] == "Public" || words[i] == "Friend") && words[i + 1] == "Function")
            {
                var function = new Function();

                string funName = words[i + 2][..words[i + 2].IndexOf("(")];
                function.FunName = funName.Trim();

                string funHeader = words[i] + " " + words[i + 1] + " " + funName;

                int startIndex = code.IndexOf(funHeader);
                string funCode = code[startIndex..];

                int endIndex = funCode.IndexOf("End Function");
                funCode = funCode[..endIndex];

                function.Code = funCode;
                (function.DBName, function.Configuration) = GetDBType(funCode, funName);

                functions.Add(function);
            }
        }

        return functions;
    }

    #endregion Private Methods
}