using Dev.Assistant.Business.Core.DevErrors;
using Dev.Assistant.Business.Core.Enums;
using Dev.Assistant.Business.Core.Extensions;
using Dev.Assistant.Business.Core.Models;
using Dev.Assistant.Business.Core.Services;
using Dev.Assistant.Business.Decoder.Models;
using Dev.Assistant.Configuration.Models;
using NetOffice.WordApi;
using Serilog;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NF = NetOffice.WordApi;

// Error code start with 1000

namespace Dev.Assistant.Business.Decoder.Services;

/// <summary>
/// Service class for handling various operations related to classes, methods, and models codes.
/// </summary>
public class DecodeHelperService
{
    #region public Members

    private static readonly Regex regexAr = new(@"\p{IsArabic}");
    private static readonly Regex regexEn;
    private static readonly Regex regexEnNum = new(@"^[a-zA-Z0-9. -_?#]*$");
    private static readonly Regex regexEnOnly = new(@"[a-zA-Z]");

    static DecodeHelperService()
    {
        // Define a regex pattern for a collection of characters allowed in English text
        StringBuilder patternCollection = new(@"^[a-zA-Z0-9\-_\?\.'\(\):;\{\}%<>,`&!/\\،|=’#");
        patternCollection.Append("'\"");
        patternCollection.Append("]*$");

        // Initialize regexEn with the defined pattern
        regexEn = new(patternCollection.ToString());
    }

    #endregion public Members

    #region Public Methods



    /// <summary>
    ///
    /// </summary>
    /// <param name="models"></param>
    /// <param name="ev"></param>
    /// <param name="alwaysShowDialog"></param>
    /// <param name="nameSpace">Only used for logging</param>
    /// <returns>Boolean to check if we can continue the process</returns><returns></returns>
    /// <exception cref="DevAssistantException"></exception>
    public static DialogResult CheckSpellingAndRules(CheckSpellingAndRulesArgs args, LogArgs log, Form checkSpellingDialog, ProjectType projectType = ProjectType.RestApi)
    {
        var temp = checkSpellingDialog;

        DateTime processStartTime = DateTime.Now;

        // Check the model
        //string propsName = ModelExtractionService.CopyOutputOfSpllingAndRules(models);

        string propsName = string.Empty;
        string longRemarks = string.Empty;

        // Split by capital letter
        //var r = new Regex(@"
        //                (?<=[A-Z])(?=[A-Z][a-z]) |
        //                (?<=[^A-Z])(?=[A-Z])  |
        //                (?<=[^A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

        int remarksNum = 0;
        string errorCodes = "";

        bool error1442Found = false;
        bool addError1442Remarks = false;

        foreach (var model in args.Models)
        {
            if (checkSpellingDialog is null)
                checkSpellingDialog = temp;

            propsName += $"{model.Name.SplitByCapitalLetter()} Model: ";
            propsName += Environment.NewLine;

            if (projectType is not ProjectType.Micro)
            {
                if (string.IsNullOrWhiteSpace(model.ClassDesc))
                {
                    remarksNum++;

                    errorCodes += $"7400;";

                    //propsName += $"      {remarksNum}- No XML documentation found for this class{Environment.NewLine}";
                    propsName += $"      * No XML documentation found for this class{Environment.NewLine}";

                    longRemarks += $"No XML documentation found for {model.Name} class [7400]{Environment.NewLine}";
                }
            }

            if (!string.IsNullOrWhiteSpace(model.Remarks))
            {
                foreach (var item in model.Remarks.Split(Environment.NewLine))
                {
                    try
                    {
                        if (item.Length > 5)
                        {
                            //var errorCode = item.Substring(item.Length - 5).Replace("]", "").Trim();
                            var errorCode = item[^5..].Replace("]", "").Trim();

                            errorCodes += $"{errorCode};";
                        }
                    }
                    catch (Exception)
                    {
                        errorCodes += "NONE;";
                    }
                }

                remarksNum++;

                propsName += $"{model.Remarks}{Environment.NewLine}";
                longRemarks += model.LongRemarks;
            }

            // To remove the end line.. since we will add line in bellow loop
            longRemarks = longRemarks.Trim();

            foreach (Property property in model.Properties)
            {
                propsName += $"   {property.Name.SplitByCapitalLetter()}";

                if (!string.IsNullOrWhiteSpace(property.Remarks))
                {
                    // Now if remarks contains 1442 "No XML found for English language" error, will not add it to "Remarks (rules) Summary" section,
                    // becase MicroServices project is old, and a lots of services doesn't have XML. So we will diplay it in "Remarks Details" section

                    var remarks = property.LongRemarks.Trim().Split(Environment.NewLine);

                    foreach (var remark in remarks)
                    {
                        error1442Found = false;

                        try
                        {
                            string errorCode = string.Empty;

                            if (remark.Length > 5)
                            {
                                errorCode = remark[^5..].Replace("]", "").Trim();

                                errorCodes += $"{errorCode};";

                                if (errorCode.Contains("1442"))
                                {
                                    error1442Found = true;
                                    addError1442Remarks = true; // This will be set only once when ever there is 1442 error. 
                                }
                            }

                            if (projectType is not ProjectType.Micro || (projectType is ProjectType.Micro && !error1442Found))
                            {
                                longRemarks += $"{Environment.NewLine}{remark}";
                                remarksNum++;
                            }
                        }
                        catch (Exception)
                        {
                            errorCodes += "NONE;";
                        }
                    }

                    propsName += $"{Environment.NewLine}{property.Remarks}{Environment.NewLine}";
                }
                else
                {
                    propsName += Environment.NewLine;
                }
            }

            propsName += Environment.NewLine;
        }

        if (projectType is ProjectType.Micro && addError1442Remarks)
        {
            longRemarks += $"{Environment.NewLine}There are many properties doesn't have XML summary for English language. Please check bellow \"Remarks Details\" section [1442]";
            remarksNum++;
        }

        int errorCount = 0;

        NF.Application app = null;
        _Document doc = null;
        ProofreadingErrors errors = null;

        try
        {
            if (propsName.Length > 0)
            {
                //app.Visible = false;
                app = new();

                app.DisplayAlerts = NF.Enums.WdAlertLevel.wdAlertsNone;

                doc = app.Documents.Add();

                object oCollapseEnd = NF.Enums.WdCollapseDirection.wdCollapseEnd;
                NF.Range objRange = doc.Content;
                objRange.Collapse(oCollapseEnd);

                if (remarksNum > 0)
                {
                    if (string.IsNullOrWhiteSpace(longRemarks))
                    {
                        throw new DevAssistantException("Error has occurred while checking for rules", 7020);
                    }

                    objRange.Text = "Remarks (rules) Summary:" + Environment.NewLine;
                    objRange.Bold = 1;
                    //objRange.HighlightColorIndex = NF.Enums.WdColorIndex.wdRed;
                    objRange.Font.Color = NF.Enums.WdColor.wdColorRed;
                    objRange.Font.Size = 16;
                    objRange.Paragraphs.Alignment = NF.Enums.WdParagraphAlignment.wdAlignParagraphLeft;
                    objRange.Collapse(oCollapseEnd);

                    objRange.Text = longRemarks + Environment.NewLine;
                    objRange.Bold = 0;
                    objRange.ListFormat.ApplyNumberDefault(NF.Enums.WdDefaultListBehavior.wdWord10ListBehavior);
                    objRange.Paragraphs.Alignment = NF.Enums.WdParagraphAlignment.wdAlignParagraphLeft;
                    objRange.Collapse(oCollapseEnd);
                    objRange.InsertParagraph();

                    objRange.Text = Environment.NewLine + Environment.NewLine;
                    objRange.Collapse(oCollapseEnd);
                    objRange.InsertParagraph();
                }

                string paragraphTitle;

                if (remarksNum > 0)
                    paragraphTitle = "Remarks Details:" + Environment.NewLine;
                else
                    paragraphTitle = "Misspelling Details:" + Environment.NewLine;

                objRange.Text = paragraphTitle;
                objRange.Bold = 1;
                //objRange.HighlightColorIndex = NF.Enums.WdColorIndex.wdRed;
                objRange.Font.Color = NF.Enums.WdColor.wdColorBlue;
                objRange.Font.Size = 16;
                objRange.Paragraphs.Alignment = NF.Enums.WdParagraphAlignment.wdAlignParagraphLeft;
                objRange.Collapse(oCollapseEnd);

                // to avoid open word for this misspelling for Npc contracts
                if (args.Namespace.Equals("FromGenerateApiMethod"))
                    propsName = propsName.Replace("Non Financial", "Non-Financial");

                objRange.Text = propsName;
                objRange.Bold = 0;
                objRange.Paragraphs.Alignment = NF.Enums.WdParagraphAlignment.wdAlignParagraphLeft;
                objRange.Collapse(oCollapseEnd);
                objRange.InsertParagraph();

                errors = doc.SpellingErrors;

                errorCount = errors.Count;

                // Log remarks num || Log server name if method called by CheckRules method ONLY.... if end with Npv ignore since it's come from Generate method
                if (remarksNum > 0 || (!string.IsNullOrWhiteSpace(args.Namespace) && !args.Namespace.Equals("FromGenerateApiMethod")))
                    log.LogEvent(args.Event,
                        EventStatus.Called,
                        $"{errorCount} misspelling found, {remarksNum} remark(s) found{(string.IsNullOrWhiteSpace(args.Namespace) ? "" : $" in {args.Namespace.Replace("Nic.Apis.", "")}")}:{errorCodes}");

                // to open word with "Check Spelling and Rules" option
                if (args.AlwaysShowDialog)
                {
                    if (remarksNum == 0 && errorCount == 0)
                    {
                        log.LogSuccess("Rules and misspelling checked successfully! Please check the opened Word file");
                    }
                    else
                    {
                        string mesg = "Rules and misspelling checked successfully!";

                        if (errorCount > 0)
                            mesg += $" {errorCount} misspelling found";

                        if (remarksNum > 0)
                        {
                            if (errorCount > 0)
                                mesg += $" and";

                            mesg += $" {remarksNum} remark(s) found";
                        }

                        mesg += $". Please check the opened Word file";

                        log.LogError(mesg);
                    }

                    app.Visible = true;
                }
                else if (errorCount > 0 || remarksNum > 0) // Only open when there is misspelling or remarks.
                {
                    app.Visible = true;

                    string msg = "";

                    if (remarksNum > 0)
                        msg += $"{remarksNum} remark(s) need your attention. ";

                    if (errorCount > 0)
                        msg += $"Misspelling found ({errorCount} misspelling). ";

                    log.LogError(msg);

                    doc.CheckSpelling(Missing.Value, false, true);
                }
                else
                {
                    log.LogSuccess("Rules and misspelling checked successfully! No remarks & misspelling found");
                }
            }
        }
        catch (Exception ex)
        {
            throw new DevAssistantException(ex.Message, 7017);
        }
        finally
        {
            if (doc != null)
            {
                doc.Dispose();
            }

            app?.Dispose();
            errors?.Dispose();

            doc = null;
            app = null;
            errors = null;

            //if (alwaysShowDialog)
            KillProcess("winword", processStartTime);
        }

        /*
         * Show dialog to ask user to resolved remarks and continue
  
         */

        if ((errorCount > 0 || remarksNum > 0) && !args.AlwaysShowDialog)
        {
            var dialog = checkSpellingDialog;

            var result = dialog.ShowDialog();

            //doc = null;
            //app = null;

            return result;
        }

        return DialogResult.OK;
    }


    /// <summary>
    /// Copy the output of checked spelling and rules to Clipboard
    /// </summary>
    /// <param name="models">Models that coma from GetClassesByFilePath() or GetClassesByCode()</param>
    /// <exception cref="DevAssistantException"></exception>
    public static string CopyOutputOfSpellingAndRules(List<ClassModel> models)
    {
        Log.Logger.Information("CopyOutputOfSpellingAndRules Called");

        // Output string to store formatted information about class names, descriptions, remarks, and property names
        var formattedOutput = new StringBuilder();

        try
        {
            foreach (var model in models)
            {
                // Add class name
                formattedOutput.AppendLine($"{model.Name.SplitByCapitalLetter()} Model:");

                // Check for XML documentation for the class
                if (string.IsNullOrWhiteSpace(model.ClassDesc))
                    formattedOutput.AppendLine("      * No XML documentation found for this class");

                // Check for remarks and add them to the output
                if (!string.IsNullOrWhiteSpace(model.Remarks))
                    formattedOutput.AppendLine($"{model.Remarks}{Environment.NewLine}");

                foreach (Property property in model.Properties)
                {
                    // Add property name to the output
                    formattedOutput.AppendLine($"   {property.Name.SplitByCapitalLetter()}");

                    // Check for remarks on the property and add them to the output
                    if (!string.IsNullOrWhiteSpace(property.Remarks))
                        formattedOutput.AppendLine($"{property.Remarks}{Environment.NewLine}");
                    else
                        formattedOutput.AppendLine();
                }

                formattedOutput.AppendLine();
            }

            // Additional notes and rules about property naming conventions
            formattedOutput.AppendLine();
            formattedOutput.AppendLine();
            formattedOutput.AppendLine("Please note... The Property name must be PascalCase. if there are more than 1 capital letter next to each other, follow the following:");
            formattedOutput.AppendLine("  - Only Two capital letters are allowed next to each other in one word. Examples:");
            formattedOutput.AppendLine("        * P_O_BOX or PO_BOX must be > POBox.");
            formattedOutput.AppendLine("        * GIS_ LONGITUDE or GISLongitude must be > GisLongitude.");
            formattedOutput.AppendLine("          PO is allowed to be capital letters but GIS must be Gis because it is more than 2 letters.");
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "An unexpected error occurred in CopyOutputOfSpellingAndRules");

            throw;
        }

        return formattedOutput.ToString();
    }


    /// <summary>
    /// Extracts the contract name from the provided API path.
    /// Ex: C:/Project/API/Git2/Nic.Apis.Csmarc/Business..... returns Csmarc
    /// </summary>
    /// <param name="path">The API path to extract the contract name from.</param>
    /// <returns>The extracted contract name.</returns>
    public static string GetContractNameFromPath(string path) // why I developed this method? and why I didn't use this method? i forgot :(
    {
        Log.Logger.Information("GetContractNameFromPath Called - path: {path}", path);

        // Check if the path contains "Nic.Apis."
        if (!path.Contains("Nic.Apis.", StringComparison.InvariantCultureIgnoreCase))
            return string.Empty;

        // Extract the contract name from the remaining path
        var pathRemainder = path[path.LastIndexOf("Nic.Apis")..].Replace("/", "\\").Split("\\");

        // Return the extracted contract name
        return pathRemainder[0].Replace("Nic.Apis.", "");
    }

    /// <summary>
    /// Prepare Request and Response DTO to generate IG (For Req and Res table in IG)<br/>
    /// if there is no Req, Inputs, Res or Outpts all class will be added to res.
    /// </summary>
    /// <returns>2 lists, 1- Request Dtos (Left), 2- Response Dtos (Right)</returns>
    public static (List<Property> req, List<Property> res) PrepareReqAndResDto(string serviceName, List<ClassModel> classes, bool addBracketBox, ProjectType projectType)
    {
        // Log information about the method call
        Log.Logger.Information("PrepareReqAndResDto Called - serviceName: {ServiceName}, classes: {ClassesCount}, addBracketBox: {AddBracketBox}, projectType: {ProjectType}", serviceName, classes.Count, addBracketBox, projectType);

        string dataType = string.Empty;

        // Lists to track added models to avoid duplicates
        List<string> reqAdded = new();
        List<string> resAdded = new();

        // Result lists for request and response DTOs
        List<Property> requestDto = new();
        List<Property> responseDto = new();

        Property propertyDto;
        bool isGenIg = false;

        // Check if serviceName is provided and determine if IG generation is required
        if (!string.IsNullOrWhiteSpace(serviceName))
        {
            // TODO: also check for micro (Inputs & Outputs).
            if (classes.Any(c => c.Name.Equals($"{serviceName}Req", StringComparison.InvariantCultureIgnoreCase) || c.Name.Equals($"{serviceName}Inputs", StringComparison.InvariantCultureIgnoreCase)) ||
                classes.Any(c => c.Name.Equals($"{serviceName}Res", StringComparison.InvariantCultureIgnoreCase) || c.Name.Equals($"{serviceName}Outputs", StringComparison.InvariantCultureIgnoreCase)))
            {
                isGenIg = true;
            }
        }

        foreach (ClassModel model in classes)
        {
            // If serviceName is not provided, infer it from the first model
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                serviceName = model.Name[0..^3];

                // Because we didn't send the servicename, so we must check if there is Req in models,
                // if yes, we will assume that also we will found Res.
                // So, the behavior will be the same as GenIG.
                // If there is no Req or Res, all dtos will be added to responseDto, and requestDto will be always empty

                if (classes.Any(c => c.Name.Equals($"{serviceName}Req", StringComparison.InvariantCultureIgnoreCase) || c.Name.Equals($"{serviceName}Inputs", StringComparison.InvariantCultureIgnoreCase)) ||
                    classes.Any(c => c.Name.Equals($"{serviceName}Res", StringComparison.InvariantCultureIgnoreCase) || c.Name.Equals($"{serviceName}Outputs", StringComparison.InvariantCultureIgnoreCase)))
                {
                    isGenIg = true;
                }
            }

            // to be sure in bellow if-statement that model is the response for this service.
            // because in some services there are multi Res models
            //serviceName = model.Name[0..^3];

            bool isReq;

            // Determine if the current model is for request or response
            if (isGenIg)
            {
                if (IsReqModel(serviceName, projectType, model))
                {
                    isReq = true;
                }
                else if (IsResModel(serviceName, projectType, model))
                {
                    isReq = false;
                }
                else
                {
                    continue;
                }
            }
            else
            {
                isReq = false; // if not GenIG then all models will be added to Res. there are may duplicates models and props
            }

            // Skip models that are already added to request or response
            if (reqAdded.Contains(model.Name))
                continue;

            if (resAdded.Contains(model.Name))
                continue;

            foreach (Property property in model.Properties)
            {
                dataType = property.DataType;

                // Add parentheses to the data type if required
                if (addBracketBox && !property.DataType.Equals("DateTime"))
                    dataType += "()";

                // Create a new Property object for the DTO
                propertyDto = new Property
                {
                    Name = property.Name,
                    DescAr = property.DescAr,
                    DataType = dataType,
                    DescEn = property.DescEn,
                    IsRequired = property.IsRequired,
                    DataTypes = property.DataType,
                    IsDataTypeMatch = property.IsDataTypeMatch,
                    IsNameMatch = property.IsNameMatch,
                    LongRemarks = property.LongRemarks,
                    Remarks = property.Remarks,
                    SimilarName = property.SimilarName,
                    ModelName = !string.IsNullOrWhiteSpace(property.ModelName) ? property.ModelName : model.Name
                };

                // Add the property to the appropriate DTO list (request or response)
                if (isReq)
                {
                    requestDto.Add(propertyDto);
                }
                else // if (IsResModel(serviceName, projectType, model))
                {
                    responseDto.Add(propertyDto);
                }

                string objectName = property.DateTypeWithoutListOrArray();

                // Recursively process nested models
                if (HasNestedModel(classes, property, objectName) && ((isReq && !reqAdded.Contains(objectName)) || (!isReq && !resAdded.Contains(objectName))))
                {
                    // Default 2:
                    // spaceCount used to add space before the dash "-".. To organize props
                    //   - Prop1 int
                    //   - Prop2 SomeObj
                    //     - SomeObjProp1
                    //     - SomeObjProp2
                    //   - Prop3 int

                    int spaceCount = 2; // first nested model

                    if (isReq)
                        PreparePropertiesDto(objectName, classes, isReq ? requestDto : responseDto, isReq ? reqAdded : resAdded, addBracketBox, spaceCount);
                    else
                        PreparePropertiesDto(objectName, classes, responseDto, resAdded, addBracketBox, spaceCount);
                }
            }
        }

        return (requestDto, responseDto);
    }


    /// <summary>
    /// Prepare properties DTO to generate IG (For Req and Res table in IG)
    /// </summary>
    /// <param name="objectName">Name of the model for which properties are being prepared</param>
    /// <param name="classes">List of ClassModel instances representing available classes</param>
    /// <param name="dtos">List of Property instances representing the DTO properties</param>
    /// <param name="dtosAdded">List of already added DTOs to avoid duplicates</param>
    /// <param name="addBracketBox">Flag indicating whether to add brackets to data types (excluding DateTime)</param>
    /// <param name="spaceCount">Number of spaces for indentation</param>
    public static void PreparePropertiesDto(string objectName, List<ClassModel> classes, List<Property> dtos, List<string> dtosAdded, bool addBracketBox, int spaceCount)
    {
        Log.Logger.Information("PreparePropertiesDto Called - modelName: {a}, classes: {b}, dtos: {c}", objectName, classes.Count, dtos.Count);

        dtosAdded.Add(objectName);

        // Find the ClassModel instance for the given model name
        ClassModel classModel = FindClassModel(objectName, classes);

        foreach (Property supProperty in classModel.Properties)
        {
            // Create formatted property
            string prop = new string(' ', spaceCount) + $"- {supProperty.Name}";

            string dataType = supProperty.DataType;

            // Append "()"" to data type if addBracketBox is true and the data type is not DateTime
            if (addBracketBox && !supProperty.DataType.Equals("DateTime"))
                dataType += "()";

            // Create a new Property instance for the DTO
            Property propertyResDto = new()
            {
                Name = prop,
                DescAr = supProperty.DescAr,
                DataType = dataType,
                DescEn = supProperty.DescEn,
                IsRequired = supProperty.IsRequired,
                DataTypes = supProperty.DataType,
                IsDataTypeMatch = supProperty.IsDataTypeMatch,
                IsNameMatch = supProperty.IsNameMatch,
                LongRemarks = supProperty.LongRemarks,
                Remarks = supProperty.Remarks,
                SimilarName = supProperty.SimilarName,
                ModelName = !string.IsNullOrWhiteSpace(supProperty.ModelName) ? supProperty.ModelName : classModel.Name
            };

            // Add the new Property to the lis
            dtos.Add(propertyResDto);

            string subModelName = supProperty.DateTypeWithoutListOrArray();

            // Check for complex data types and recursively prepare their properties
            if (HasNestedModel(classes, supProperty, subModelName) && !dtosAdded.Contains(supProperty.DateTypeWithoutListOrArray()))
            {
                // Recursive call to prepare properties for the sub-model
                // By this, we solve the bug in v3.1.1 to get all props and all subProp (objects & lists) no limit for sub level :)
                // (See release notes for v3.1.1 & v3.3.3)
                PreparePropertiesDto(subModelName, classes, dtos, dtosAdded, addBracketBox, spaceCount + 3);
            }
        }
    }



    private static bool HasNestedModel(List<ClassModel> classes, Property property, string objectName)
    {
        return !ValidationService.IsPrimitiveDatatype(objectName) // Check if the model name is a basic data type to avoid further recursion
                && (property.DataType.Contains("List") || property.DataType.EndsWith("[]") || property.DataType.EndsWith("Res") || classes.Any(c => c.Name == property.DataType));
    }



    /// <summary>
    /// Check if the provided code contains the expected namespace.
    /// </summary>
    /// <param name="code">The code to check.</param>
    /// <param name="expectedNameSpace">The expected namespace.</param>
    /// <exception cref="DevAssistantException">Thrown when the namespace is incorrect.</exception>
    public static void ValidateNamespace(string code, string expectedNameSpace)
    {
        Log.Logger.Information("Checking Namespace. Expected: {expectedNamespace}", expectedNameSpace);

        // Trim the expected namespace for consistency
        string trimmedExpectedNamespace = expectedNameSpace.Replace(";", "").Trim();

        string[] lines = code.Split(Convert.ToChar("\n"));

        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = lines[i].Trim();

            if (string.IsNullOrWhiteSpace(lines[i]) || lines[i].StartsWith("//"))
                continue;

            if (lines[i].StartsWith("namespace "))
            {
                if (lines[i].Replace(";", "").Equals($"namespace {trimmedExpectedNamespace}"))
                {
                    // Log when the namespace is matched
                    Log.Logger.Information("Namespace is matched. Code \"{line}\" at line {lineNumber}.", lines[i], i + 1);
                    break;
                }
                else
                {
                    throw new DevAssistantException($"Incorrect namespace found at line {i + 1}. Expected \"{trimmedExpectedNamespace}\" but actual is \"{lines[i].Replace(";", "").Trim()}\"", 1600);
                }
            }
        }
    }


    public static string CheckCapitalLetters(string objName, bool throwException, bool showErrorCode, bool isClass = false, bool isLongRemarks = false)
    {
        Log.Logger.Information("CheckCapitalLetters Called - objName: {a}, throwException: {b}, showErrorCode: {c}, isClass: {d}, isLongRemarks: {e}", objName, throwException, showErrorCode, isClass, isLongRemarks);

        string objType = isClass ? "Class name" : "Property";
        var objNameForChecking = objName.Replace("*", "").Replace("YYYYMMDD", "").Trim();

        // if property is 2 letters such as SP, Sp or sp. no need to check
        // the next check if enough to check if start with capital.

        string remarks = string.Empty;
        string longRemarks = string.Empty;

        bool condition = throwException;

        // in some cases is not applicable.
        // such as when MicroLKAC210Religion. LKAC is allowed since it's a micro
        bool notApplicable = isClass && (objNameForChecking.StartsWith("Micro") || objNameForChecking.StartsWith("BDa"));// || (!isClass && !objName.ToLower().Contains("yyyymmdd"));

        if (notApplicable)
            return remarks;

        if (objNameForChecking[0] != char.ToUpper(objNameForChecking[0]))
        {
            if (!notApplicable)
            {
                if (condition)
                {
                    //throw new DevAssistantException($"Property must start with Capital. {objType} must be PascalCase. Please check \"{objName}\" {objType}", 1415);
                    throw DevErrors.Decoder.E1415PropertyStartCapital(objName, objType);
                }
                else
                {
                    //remarks += $"      * Property must start with a capital letter. {objType} must be PascalCase.";
                    remarks += DevErrors.Decoder.E1415PropertyStartCapital(objName, objType).ToRemarks();

                    //longRemarks += $"Property must start with Capital. {objType} must be PascalCase. Please check \"{objName}\" {objType} [1415]{Environment.NewLine}";
                    longRemarks += DevErrors.Decoder.E1415PropertyStartCapital(objName, objType).ToLongRemarks();

                    if (showErrorCode)
                        remarks += $" [{DevErrors.Decoder.E1415PropertyStartCapital(objName, objType).Code}]";

                    remarks += Environment.NewLine;
                }
            }
        }

        // if it is 3 letters such as HOH must by Hoh
        if (objNameForChecking.Length == 3
               && char.IsUpper(objNameForChecking[0])
               && char.IsUpper(objNameForChecking[1])
               && char.IsUpper(objNameForChecking[2]))
        {
            if (!notApplicable)
            {
                if (condition)
                {
                    throw DevErrors.Decoder.E1416MoreThanTwoCapitals(objName, objType);
                }
                else
                {
                    remarks += DevErrors.Decoder.E1416MoreThanTwoCapitals(objName, objType).ToRemarks();

                    longRemarks += DevErrors.Decoder.E1416MoreThanTwoCapitals(objName, objType).ToLongRemarks();

                    if (showErrorCode)
                        remarks += $" [{DevErrors.Decoder.E1416MoreThanTwoCapitals(objName, objType).Code}]";

                    remarks += Environment.NewLine;
                }
            }
        }
        else if (objNameForChecking.Length > 3)
        {
            for (int k = 0; k < objNameForChecking.Length; k++)
            {
                // to handle if the last 3 letters are Capetel such as RecipientHOH
                if (k == objNameForChecking.Length - 1) // Last index
                {
                    if (char.IsUpper(objNameForChecking[k])
                        && char.IsUpper(objNameForChecking[k - 1])
                        && char.IsUpper(objNameForChecking[k - 2]))
                    {
                        if (!notApplicable)
                        {
                            if (condition)
                            {
                                throw DevErrors.Decoder.E1417MoreThanTwoCapitals(objName, objType);
                            }
                            else
                            {
                                remarks += DevErrors.Decoder.E1417MoreThanTwoCapitals(objName, objType).ToRemarks();

                                longRemarks += DevErrors.Decoder.E1417MoreThanTwoCapitals(objName, objType).ToLongRemarks();

                                if (showErrorCode)
                                    remarks += $" [{DevErrors.Decoder.E1417MoreThanTwoCapitals(objName, objType).Code}]";

                                remarks += Environment.NewLine;
                            }
                        }

                        break;
                    }
                }
                else if (k > 2)
                {
                    if (char.IsUpper(objNameForChecking[k - 1])
                        && char.IsUpper(objNameForChecking[k - 2])
                        && char.IsUpper(objNameForChecking[k - 3]))
                    {
                        if (objNameForChecking[k].ToString() == "_" || objNameForChecking[k] == char.ToUpper(objNameForChecking[k]))
                        {
                            if (!notApplicable)
                            {
                                if (condition)
                                {
                                    throw DevErrors.Decoder.E1418MoreThanTwoCapitals(objName, objType);
                                }
                                else
                                {
                                    remarks += DevErrors.Decoder.E1418MoreThanTwoCapitals(objName, objType).ToRemarks();

                                    longRemarks += DevErrors.Decoder.E1418MoreThanTwoCapitals(objName, objType).ToLongRemarks();

                                    if (showErrorCode)
                                        remarks += $" [{DevErrors.Decoder.E1418MoreThanTwoCapitals(objName, objType).Code}]";

                                    remarks += Environment.NewLine;
                                }
                            }

                            break;
                        }
                    }
                }
            }
        }

        return isLongRemarks ? longRemarks : remarks;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="lines"></param>
    /// <param name="i"></param>
    /// <returns>-1 if the prop has no Required annotation</returns>
    public static int CheckHasRequiredAnnotation(string[] lines, int i)
    {
        Log.Logger.Information("CheckHasRequiredAnnotation Called - i: {a}", i);

        int requiredIndex = -1;

        for (int k = 1; k < 6; k++)
        {
            //        || Prev prop
            if (i < k || lines[i - k].Trim().StartsWith("public"))
                break;

            if (lines[i - k].Contains("[Required]"))
            {
                requiredIndex = i - k;
                break;
            }
        }

        return requiredIndex;
    }

    /// <summary>
    /// Handles variations in class declaration formatting to identify the index of the class declaration line.
    /// </summary>
    /// <param name="lines">Array of lines in the code</param>
    /// <param name="className">Name of the class to find in the lines</param>
    /// <returns>Index of the class declaration line or -1 if not found</returns>
    public static int FindClassDeclarationIndex(string[] lines, string className)
    {
        int index = -1;

        // Attempt to find class declaration with variations
        if ((index = Array.FindIndex(lines, l => l.EndsWith($"class {className}\r"))) == -1)
        {
            if ((index = Array.FindIndex(lines, l => l.EndsWith($"class {className}\n"))) == -1)
            {
                if ((index = Array.FindIndex(lines, l => l.EndsWith($"class {className}{{"))) == -1)
                {
                    if ((index = Array.FindIndex(lines, l => l.EndsWith($"class {className} {{"))) == -1)
                    {
                        if ((index = Array.FindIndex(lines, l => l.EndsWith($"class {className} {{\r"))) == -1)
                        {

                        }
                    }
                }
            }
        }

        return index;
    }

    /// <summary>
    /// Find the ClassModel instance with the specified model name in the list of classes
    /// </summary>
    /// <param name="modelName">Name of the model to find</param>
    /// <param name="classes">List of ClassModel instances</param>
    /// <returns>Found ClassModel instance or null if not found</returns>
    public static ClassModel FindClassModel(string modelName, List<ClassModel> classes)
    {
        // Find the exact match for the model name
        ClassModel classModel = classes.FirstOrDefault(c => c.Name.Equals(modelName));

        // If an exact match is not found, attempt a case-insensitive match
        return classModel ?? classes.FirstOrDefault(c => c.Name.Equals(modelName, StringComparison.CurrentCultureIgnoreCase));
    }

    /// <summary>
    /// Get catrgory description by the IDCategoryOptions
    /// Operator => Operator ID
    /// </summary>
    /// <param name="idCategory"></param>
    /// <returns>Catrgory description as (DescAr, DescEn)</returns>
    /// <exception cref="NotImplementedException"></exception>
    public static (string, string) GetCategoryTypeDesc(string idCategory)
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

    ///// <summary>
    ///// Check if the given data type is a basic data type (int, string, double, decimal, DateTime)
    ///// </summary>
    ///// <param name="dataType">Data type to check</param>
    ///// <returns>True if the data type is a basic data type, otherwise false</returns>
    //public static bool IsBasicDataType(string dataType) => ValidationService.IsPrimitiveDatatype(dataType.ToLower());
    ////      dataType.ToLower() switch
    ////{
    ////    "int" or "string" or "double" or "decimal" or "datetime" => true,
    ////    _ => false
    ////};

    public static bool IsReqModel(string serviceName, ProjectType projectType, ClassModel model)
    {
        return (projectType is ProjectType.RestApi or ProjectType.NdbJson && model.Name.EndsWith("Req") && model.Name[0..^3] == serviceName) ||
            (projectType is ProjectType.Micro && model.Name.EndsWith("Inputs") && model.Name[0..^6] == serviceName);
    }

    public static bool IsResModel(string serviceName, ProjectType projectType, ClassModel model)
    {
        return (projectType is ProjectType.RestApi or ProjectType.NdbJson && model.Name.EndsWith("Res") && model.Name[0..^3] == serviceName) ||
            (projectType is ProjectType.Micro && model.Name.EndsWith("Outputs") && model.Name[0..^7] == serviceName);
    }

    /// <summary>
    /// Logs or throws a validation exception based on the specified parameters.
    /// </summary>
    /// <param name="options">The options for DevAssistant.</param>
    /// <param name="remarks">The remarks or error message to be logged or thrown.</param>
    /// <param name="property">The <see cref="Property"/> instance for which the error occurred.</param>
    /// <param name="errorCode">The error code associated with the exception.</param>
    public static void LogOrThrow(GetClassesOptions options, string remarks, Property property, int errorCode)
    {
        if (options.ThrowException) // Mostly used in IG generator
        {
            throw new DevAssistantException(remarks, errorCode);
        }
        else // Mostly used in reports or CheckSpellingAndRules, since we need to get all validation then display it in Word file (We don't want to throw/stop the process)
        {
            // Log error message in remarks
            property.Remarks += $"      * {remarks}.";

            // Log detailed error message in long remarks
            property.LongRemarks += $"{remarks}. Please check \"{property.Name.RemoveRequired()}\" property in {property.ModelName} [{errorCode}]{Environment.NewLine}";

            // Include error code in remarks if configured to show error codes
            if (options.ShowErrorCode)
            {
                property.Remarks += $" [{errorCode}]";
            }

            property.Remarks += Environment.NewLine;
        }
    }



    public static void ValidateAndGetXmlDoc(GetClassesOptions options, string[] lines, Property property, ClassModel classInfo, int index)
    {
        Log.Logger.Information("ValidateAndGetXmlDoc Called - {a}", options);

        // Validate & Get XML documentation -- START

        int summaryStart = -1;
        int summaryEnd = -1;

        for (int k = 1; k < 10; k++)
        {
            //        || Prev prop
            if (index < k || lines[index - k].Trim().StartsWith("public"))
                break;

            if (lines[index - k].Contains("[Required]"))
            {
                property.IsRequired = true;

                if (options.AddStarForRequiredProp)
                    property.Name += " *";
            }

            if (lines[index - k].RemoveWhiteSpaces().StartsWith("///<summary>"))
            {
                summaryStart = index - k;
            }

            if (lines[index - k].RemoveWhiteSpaces().StartsWith("///</summary>"))
            {
                //xmlIndex = i - k;
                summaryEnd = index - k;
            }

            if (summaryStart > -1 && summaryEnd > -1)
                break;
        }

        if (options.CheckSpellingAndRules && options.ThrowException && (summaryStart == -1 || summaryEnd == -1 || summaryStart > summaryEnd))
        {
            throw new DevAssistantException($"No XML documentation found. Please check \"{property.Name.RemoveRequired()}\" property in {classInfo.Name}", 1126);
        }

        int lastEnIndex = -1;

        for (int n = summaryStart + 1; n < summaryEnd; n++)
        {
            string desc = lines[n].Trim();

            if (!string.IsNullOrWhiteSpace(desc.Replace("///", "").Trim()) && !regexAr.IsMatch(desc.RemoveWhiteSpaces().RemoveAppendedLine()))
            {
                if (lastEnIndex == -1)
                {
                    lastEnIndex = n;
                    continue;
                }

                // now we check if the prev line not match the lastEnIndex this mean there are diff lines of en

                // the prev line is En. so continue to ckech check
                if (lastEnIndex == n - 1)
                {
                    lastEnIndex = n;
                    continue;
                }

                /*
                 * now the prev line not match the lastEnIndex
                 *
                 * Not accecptable:
                 * En
                 * Ar
                 * En
                 *
                 * must be:
                 * En
                 * En
                 * Ar, and so on
                 */

                if (options.ThrowException)
                {
                    if (!options.CheckSpellingAndRules)
                        break;

                    throw new DevAssistantException($"The languages order incorrect. Make sure that the 1st lines are in English only. Please check \"{property.Name.RemoveRequired()}\" property in {classInfo.Name}", 1426);
                }
                else
                {
                    property.Remarks += $"      * The languages order incorrect. Make sure that the 1st lines are in English only.";

                    property.LongRemarks += $"The languages order incorrect. Make sure that the 1st lines are in English only. Please check \"{property.Name.RemoveRequired()}\" property in {classInfo.Name} [1426]{Environment.NewLine}";

                    if (options.ShowErrorCode)
                        property.Remarks += " [1426]";

                    property.Remarks += Environment.NewLine;
                    break;
                }
            }
            else
            {
                // first line in the summary must be EN
                if (n == summaryStart + 1)
                {
                    if (options.ThrowException)
                    {
                        if (!options.CheckSpellingAndRules)
                            break;

                        throw new DevAssistantException($"The 1st XML line must be in English only. Please check \"{property.Name.RemoveRequired()}\" property in {classInfo.Name}", 1427);
                    }
                    else
                    {
                        property.Remarks += $"      * The 1st XML line must be in English only.";

                        property.LongRemarks += $"The 1st XML line must be in English only. Please check \"{property.Name.RemoveRequired()}\" property in {classInfo.Name} [1427]{Environment.NewLine}";

                        if (options.ShowErrorCode)
                            property.Remarks += " [1427]";

                        property.Remarks += Environment.NewLine;
                        break;
                    }
                }
            }
        }

        string descAr = string.Empty, descEn = string.Empty;

        for (int n = summaryStart + 1; n < summaryEnd; n++)
        {
            string desc = lines[n].Trim();

            if (options.ProjectType is not ProjectType.Micro && n == lastEnIndex)
            {
                if (!desc.EndsWith("<br/>"))
                {
                    if (options.ThrowException)
                    {
                        if (!options.CheckSpellingAndRules)
                            break;

                        throw new DevAssistantException($"The English XML documentation must end with \"<br/>\" (New line). Please check \"{property.Name.RemoveRequired()}\" property in {classInfo.Name}", 1419);
                    }
                    else if (options.CheckSpellingAndRules)
                    {
                        property.Remarks += $"      * The English XML documentation must end with \"<br/>\" (New line).";

                        property.LongRemarks += $"The English XML documentation must end with \"<br/>\" (New line). Please check \"{property.Name.RemoveRequired()}\" property in {classInfo.Name} [1419]{Environment.NewLine}";

                        if (options.ShowErrorCode)
                            property.Remarks += " [1419]";

                        property.Remarks += Environment.NewLine;
                    }
                }
            }

            if (!regexAr.IsMatch(desc.RemoveWhiteSpaces().RemoveAppendedLine()))
            {
                descEn += desc.Replace("///", "").Replace("<br/>", "").Replace("\\n", "").Replace("\\r", "").Trim();

                // Only add newline if there is more En lines comming
                if (n != lastEnIndex)
                    descEn += Environment.NewLine;
            }
            else
            {
                descAr += desc.Replace("///", "").Replace("<br/>", "").Replace("\\n", "").Replace("\\r", "").Trim();

                // Only add newline if there is more Ar lines comming
                if (n != summaryEnd - 1)
                    descAr += Environment.NewLine;
            }
        }

        property.DescAr = descAr;
        property.DescEn = descEn;
        // Validate & Get XML documentation -- END
    }

    public static BusinessApiException ValidateIDType(string idTypes, string idCategoryEn, string idCategoryAr, ProjectType projectType = ProjectType.RestApi)
    {
        Log.Logger.Information("ValidateIDType Called - idTypes: {a}, idCategory: {b}, foundAtLine: {c}", idTypes);

        //Region 6
        var allOptions = "CAVPEU";
        var citizenAlienVisitorPilgrimAndEstablishment = "CAVPE";
        var citizenAlienVisitorPligrimAndUnknown = "CAVPU";

        //Region 4
        var citizenAlienVisitorAndPligrim = "CAVP";
        var citizenAlienVisitorAndEstablishment = "CAVE";
        var alienVisitorPilgrimAndUnknown = "AVPU";
        var citizenAlienVisitorAndUnknown = "CAVU";

        //Region 3
        var citizenAlienAndVisitor = "CAV";
        var citizenAlienAndEstablishment = "CAE";
        var alienVisitorAndPilgrim = "AVP";

        //Region 2
        var citizenAndAlien = "CA";
        var alienAndVisitor = "AV";
        var visitorAndPilgrim = "VP";
        var citizenAndEstablishment = "CE";
        var citizenAndVisitor = "CV";

        //Region 1
        var citizensOnly = "C";
        var aliensOnly = "A";
        var vistorsOnly = "V";
        var pligrimsOnly = "P";
        var establishmentOnly = "E";
        var unknownOnly = "U";

        #region "6"

        //validate all supported types (Citizen, Alien, Visitor, Pilgrim, Establishment and Unknown)
        if (idTypes == allOptions)
        {
            if (projectType is ProjectType.RestApi)
            {
                return new BusinessApiException
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4201,
                    ErrorMessageEn = "**** must be Citizen, Alien, Visitor, Pilgrim, Firm, or Unknown identity only".Replace("****", idCategoryEn),
                    ErrorMessageAr = "رقم **** يجب أن يكون مواطن أو مقيم أو زائر أو حاج أو منشأة أو مجهولي الهوية فقط".Replace("****", idCategoryAr),
                    AllowDuplicate = true
                };
            }
            else
            {
                return new BusinessApiException
                {
                    ErrorCode = 7101,
                    ErrorMessageEn = "ID must be Citizen, Alien, Visitor, Pilgrim, Establishment, or Unknown identity only",
                    ErrorMessageAr = "رقم الهوية يجب أن يكون مواطن أو مقيم أو زائر أو حاج أو منشأة أو مجهولي الهوية فقط",
                    AllowDuplicate = true
                };
            }

        }

        #endregion "6"

        #region "5"

        //validate all supported types (Citizen, Alien, Visitor, Pilgrim and Establishment)
        else if (idTypes == citizenAlienVisitorPilgrimAndEstablishment)
        {
            if (projectType is ProjectType.RestApi)
            {
                return new BusinessApiException
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4201,
                    ErrorMessageEn = "**** must be Citizen, Alien, Visitor, Pilgrim, or Firm only".Replace("****", idCategoryEn),
                    ErrorMessageAr = "رقم **** يجب أن يكون مواطن أو مقيم أو زائر أو حاج أو منشأة فقط".Replace("****", idCategoryAr),
                    AllowDuplicate = true
                };
            }
            else
            {
                return new BusinessApiException
                {
                    ErrorCode = 7101,
                    ErrorMessageEn = "Person ID must be Citizen, Alien, Visitor, Pilgrim, or Establishment only",
                    ErrorMessageAr = "رقم الهوية يجب أن يكون مواطن أو مقيم أو زائر أو حاج أو منشأة فقط",
                    AllowDuplicate = true
                };
            }
        }

        //validate all supported types (Citizen, Alien, Visitor, Pilgrim, and Unknown)
        else if (idTypes == citizenAlienVisitorPligrimAndUnknown)
        {
            if (projectType is ProjectType.RestApi)
            {
                return new BusinessApiException
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4201,
                    ErrorMessageEn = "**** must be Citizen, Alien, Visitor, Pilgrim, or Unknown identity only".Replace("****", idCategoryEn),
                    ErrorMessageAr = "رقم **** يجب أن يكون مواطن أو مقيم أو زائر أو حاج أو مجهولي الهوية فقط".Replace("****", idCategoryAr),
                    AllowDuplicate = true
                };
            }
            else
            {
                return new BusinessApiException
                {
                    ErrorCode = 7101,
                    ErrorMessageEn = "Person ID must be Citizen, Alien, Visitor, Pilgrim, or Unknown identity only",
                    ErrorMessageAr = "رقم الهوية يجب أن يكون مواطن أو مقيم أو زائر أو حاج أو مجهولي الهوية فقط",
                    AllowDuplicate = true
                };
            }
        }

        #endregion "5"

        #region "4"

        //validate Citizen, Alien, Visitor And Pligrim
        else if (idTypes == citizenAlienVisitorAndPligrim)
        {
            if (projectType is ProjectType.RestApi)
            {
                return new BusinessApiException
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4201,
                    ErrorMessageEn = "**** must be Citizen, Alien, Visitor, or Pilgrim only".Replace("****", idCategoryEn),
                    ErrorMessageAr = "رقم **** يجب أن يكون مواطن أو مقيم أو زائر أو حاج فقط".Replace("****", idCategoryAr),
                    AllowDuplicate = true
                };
            }
            else
            {
                return new BusinessApiException
                {
                    ErrorCode = 7101,
                    ErrorMessageEn = "Person ID must be Citizen, Alien, Visitor, or Pilgrim only",
                    ErrorMessageAr = "رقم الهوية يجب أن يكون مواطن أو مقيم أو زائر أو حاج فقط",
                    AllowDuplicate = true
                };
            }
        }
        //validate Citizen, Alien, Visitor And Establishment
        else if (idTypes == citizenAlienVisitorAndEstablishment)
        {
            if (projectType is ProjectType.RestApi)
            {
                return new BusinessApiException
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4201,
                    ErrorMessageEn = "**** must be Citizen, Alien, Visitor, or Establishment only".Replace("****", idCategoryEn),
                    ErrorMessageAr = "رقم **** يجب أن يكون مواطن أو مقيم أو زائر أو منشأة فقط".Replace("****", idCategoryAr),
                    AllowDuplicate = true
                };
            }
            else
            {
                // There is no validation in Micro for Citizen, Alien, Visitor And Establishment! Added in case!
                return new BusinessApiException
                {
                    ErrorCode = 7101,
                    ErrorMessageEn = "Person must be Citizen, Alien, Visitor, or Establishment only",
                    ErrorMessageAr = "رقم الهوية يجب أن يكون مواطن أو مقيم أو زائر أو منشأة فقط",
                    AllowDuplicate = true
                };
            }
        }
        //validate Alien, Visitor, Pilgrim and Unknown
        else if (idTypes == alienVisitorPilgrimAndUnknown)
        {
            if (projectType is ProjectType.RestApi)
            {
                return new BusinessApiException
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4201,
                    ErrorMessageEn = "**** must be Alien, Visitor, Pilgrim or Unknown identity only".Replace("****", idCategoryEn),
                    ErrorMessageAr = "رقم **** يجب أن يكون مقيم أو زائر أو حاج أو مجهول الهوية فقط".Replace("****", idCategoryAr),
                    AllowDuplicate = true
                };
            }
            else
            {
                // There is no validation in Micro for Alien, Visitor, Pilgrim and Unknown! Added in case!
                return new BusinessApiException
                {
                    ErrorCode = 7101,
                    ErrorMessageEn = "Person must be Alien, Visitor, Pilgrim or Unknown identity only",
                    ErrorMessageAr = "رقم الهوية يجب أن يكون مقيم أو زائر أو حاج أو مجهول الهوية فقط",
                    AllowDuplicate = true
                };
            }
        }
        else if (idTypes == citizenAlienVisitorAndUnknown)
        {
            if (projectType is ProjectType.RestApi)
            {
                return new BusinessApiException
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4201,
                    ErrorMessageEn = "**** must be Citizen, Alien, Visitor or Unknown identity only".Replace("****", idCategoryEn),
                    ErrorMessageAr = "رقم **** يجب أن يكون مواطن أو مقيم أو زائر أو مجهول الهوية فقط".Replace("****", idCategoryAr),
                    AllowDuplicate = true
                };
            }
            else
            {
                // There is no validation in Micro for Citizen, Alien, Visitor, and Unknown! Added in case!
                return new BusinessApiException
                {
                    ErrorCode = 7101,
                    ErrorMessageEn = "Person must be Citizen, Alien, Visitor or Unknown identity only",
                    ErrorMessageAr = "رقم الهوية يجب أن يكون مواطن أو مقيم أو زائر أو مجهول الهوية فقط",
                    AllowDuplicate = true
                };
            }
        }

        #endregion "4"

        #region "3"

        //validation Citizen, Alien And Visitor
        else if (idTypes == citizenAlienAndVisitor)
        {
            if (projectType is ProjectType.RestApi)
            {
                return new BusinessApiException
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4201,
                    ErrorMessageEn = "**** must be Citizen, Alien and Visitor only".Replace("****", idCategoryEn),
                    ErrorMessageAr = "رقم **** يجب أن يكون مواطن أو مقيم أو زائر فقط".Replace("****", idCategoryAr),
                    AllowDuplicate = true
                };
            }
            else
            {
                return new BusinessApiException
                {
                    ErrorCode = 7101,
                    ErrorMessageEn = "Person ID must be Citizen, Alien and Visitor only",
                    ErrorMessageAr = "رقم الهوية يجب أن يكون مواطن أو مقيم أو زائر فقط",
                    AllowDuplicate = true
                };
            }
        }

        //validation Citizen, Alien And Establishment
        else if (idTypes == citizenAlienAndEstablishment)
        {
            if (projectType is ProjectType.RestApi)
            {
                return new BusinessApiException
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4201,
                    ErrorMessageEn = "**** must be Citizen, Alien or Firm only".Replace("****", idCategoryEn),
                    ErrorMessageAr = "رقم **** يجب أن يكون مواطن أو مقيم أو منشأة فقط".Replace("****", idCategoryAr),
                    AllowDuplicate = true
                };
            }
            else
            {
                return new BusinessApiException
                {
                    ErrorCode = 7101,
                    ErrorMessageEn = "ID must be Citizen, Alien or Establishment only",
                    ErrorMessageAr = "رقم الهوية يجب أن يكون مواطن أو مقيم أو منشأة فقط",
                    AllowDuplicate = true
                };
            }
        }

        //validation Alien, Visitor And Pilgrim
        else if (idTypes == alienVisitorAndPilgrim)
        {
            if (projectType is ProjectType.RestApi)
            {
                return new BusinessApiException
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4201,
                    ErrorMessageEn = "**** must be Alien, Visitor or Pilgrim only".Replace("****", idCategoryEn),
                    ErrorMessageAr = "رقم **** يجب أن يكون مقيم أو زائر أو حاج فقط".Replace("****", idCategoryAr),
                    AllowDuplicate = true
                };
            }
            else
            {
                // There is no validation in Micro for this case!
                return new BusinessApiException
                {
                    ErrorCode = 7101,
                    ErrorMessageEn = "ID must be Alien, Visitor or Pilgrim only".Replace("****", idCategoryEn),
                    ErrorMessageAr = "رقم **** يجب أن يكون مقيم أو زائر أو حاج فقط".Replace("****", idCategoryAr),
                    AllowDuplicate = true
                };
            }
        }

        #endregion "3"

        #region "2"

        //validate citizen and aliens only
        else if (idTypes == citizenAndAlien)
        {
            if (projectType is ProjectType.RestApi)
            {
                return new BusinessApiException
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4201,
                    ErrorMessageEn = "**** must be Citizen or Alien only".Replace("****", idCategoryEn),
                    ErrorMessageAr = "رقم **** يجب أن يكون مواطن أو مقيم فقط".Replace("****", idCategoryAr),
                    AllowDuplicate = true
                };
            }
            else
            {
                return new BusinessApiException
                {
                    ErrorCode = 7101,
                    ErrorMessageEn = "Person ID must be Citizen or Alien only",
                    ErrorMessageAr = "رقم الهوية يجب أن يكون مواطن أو مقيم فقط",
                    AllowDuplicate = true
                };
            }
        }

        //validate alien and Visitor only
        else if (idTypes == alienAndVisitor)
        {
            if (projectType is ProjectType.RestApi)
            {
                return new BusinessApiException
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4201,
                    ErrorMessageEn = "**** must be Alien or Visitor only".Replace("****", idCategoryEn),
                    ErrorMessageAr = "رقم **** يجب أن يكون مقيم أو زائر فقط".Replace("****", idCategoryAr),
                    AllowDuplicate = true
                };
            }
            else
            {
                return new BusinessApiException
                {
                    ErrorCode = 7101,
                    ErrorMessageEn = "Person ID must be Alien or Visitor only",
                    ErrorMessageAr = "رقم الهوية يجب أن يكون مقيم أو زائر فقط",
                    AllowDuplicate = true
                };
            }
        }

        //Validate Visitor and Pilgrim only
        else if (idTypes == visitorAndPilgrim)
        {
            if (projectType is ProjectType.RestApi)
            {
                return new BusinessApiException
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4201,
                    ErrorMessageEn = "**** must be Visitor or Pilgrim only".Replace("****", idCategoryEn),
                    ErrorMessageAr = "رقم **** يجب أن يكون زائر أو حاج فقط".Replace("****", idCategoryAr),
                    AllowDuplicate = true
                };
            }
            else
            {
                return new BusinessApiException
                {
                    ErrorCode = 7101,
                    ErrorMessageEn = "Person ID must be Visitor or Pilgrim only",
                    ErrorMessageAr = "رقم الهوية يجب أن يكون زائر أو حاج فقط",
                    AllowDuplicate = true
                };
            }
        }

        //Validate Citizen and Establishment only
        else if (idTypes == citizenAndEstablishment)
        {
            if (projectType is ProjectType.RestApi)
            {
                return new BusinessApiException
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4201,
                    ErrorMessageEn = "**** must be Citizen or Firm only".Replace("****", idCategoryEn),
                    ErrorMessageAr = "رقم **** يجب أن يكون مواطن أو منشأة فقط".Replace("****", idCategoryAr),
                    AllowDuplicate = true
                };
            }
            else
            {
                // There is no validation in Micro for this case!
                return new BusinessApiException
                {
                    ErrorCode = 7101,
                    ErrorMessageEn = "ID must be Citizen or Firm only",
                    ErrorMessageAr = "رقم الهوية يجب أن يكون مواطن أو منشأة فقط",
                    AllowDuplicate = true
                };
            }
        }

        //validate citizen and Visitor only
        else if (idTypes == citizenAndVisitor)
        {
            if (projectType is ProjectType.RestApi)
            {
                return new BusinessApiException
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4201,
                    ErrorMessageEn = "**** must be Citizen or Visitor only".Replace("****", idCategoryEn),
                    ErrorMessageAr = "رقم **** يجب أن يكون مواطن أو زائر فقط".Replace("****", idCategoryAr),
                    AllowDuplicate = true
                };
            }
            else
            {
                // There is no validation in Micro for this case!
                return new BusinessApiException
                {
                    ErrorCode = 7101,
                    ErrorMessageEn = "Person ID must be Citizen or Visitor only",
                    ErrorMessageAr = "رقم الهوية يجب أن يكون مواطن أو زائر فقط",
                    AllowDuplicate = true
                };
            }
        }

        #endregion "2"

        #region "1"

        // validate citizen only
        else if (idTypes == citizensOnly)
        {
            if (projectType is ProjectType.RestApi)
            {
                return new BusinessApiException
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4201,
                    ErrorMessageEn = "**** must be Citizen only".Replace("****", idCategoryEn),
                    ErrorMessageAr = "رقم **** يجب أن يكون مواطن فقط".Replace("****", idCategoryAr),
                    AllowDuplicate = true
                };
            }
            else
            {
                return new BusinessApiException
                {
                    ErrorCode = 7101,
                    ErrorMessageEn = "Person ID must be Citizen only",
                    ErrorMessageAr = "رقم الهوية يجب أن يكون مواطن فقط",
                    AllowDuplicate = true
                };
            }
        }

        //validate alien only
        else if (idTypes == aliensOnly)
        {
            if (projectType is ProjectType.RestApi)
            {
                return new BusinessApiException
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4201,
                    ErrorMessageEn = "**** must be Alien only".Replace("****", idCategoryEn),
                    ErrorMessageAr = "رقم **** يجب أن يكون مقيم فقط".Replace("****", idCategoryAr),
                    AllowDuplicate = true
                };
            }
            else
            {
                return new BusinessApiException
                {
                    ErrorCode = 7101,
                    ErrorMessageEn = "Person ID must be Alien only",
                    ErrorMessageAr = "رقم الهوية يجب أن يكون مقيم فقط",
                    AllowDuplicate = true
                };
            }
        }

        //vistors only
        else if (idTypes == vistorsOnly)
        {
            if (projectType is ProjectType.RestApi)
            {
                return new BusinessApiException
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4201,
                    ErrorMessageEn = "**** must be Visitor only".Replace("****", idCategoryEn),
                    ErrorMessageAr = "رقم **** يجب أن يكون زائر فقط".Replace("****", idCategoryAr),
                    AllowDuplicate = true
                };
            }
            else
            {
                return new BusinessApiException
                {
                    ErrorCode = 7101,
                    ErrorMessageEn = "Person ID must be Visitor only",
                    ErrorMessageAr = "رقم الهوية يجب أن يكون زائر فقط",
                    AllowDuplicate = true
                };
            }
        }

        //pligrim only
        else if (idTypes == pligrimsOnly)
        {
            if (projectType is ProjectType.RestApi)
            {
                return new BusinessApiException
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4201,
                    ErrorMessageEn = "**** must be Pilgrim only".Replace("****", idCategoryEn),
                    ErrorMessageAr = "رقم **** يجب أن يكون حاج فقط".Replace("****", idCategoryAr),
                    AllowDuplicate = true
                };
            }
            else
            {
                return new BusinessApiException
                {
                    ErrorCode = 7101,
                    ErrorMessageEn = "Person ID must be Pilgrim only",
                    ErrorMessageAr = "رقم الهوية يجب أن يكون حاج فقط",
                    AllowDuplicate = true
                };
            }
        }

        //establishment only
        else if (idTypes == establishmentOnly)
        {
            if (projectType is ProjectType.RestApi)
            {
                return new BusinessApiException
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4201,
                    ErrorMessageEn = "**** must be Firm only".Replace("****", idCategoryEn),
                    ErrorMessageAr = "رقم **** يجب أن يكون منشأة فقط".Replace("****", idCategoryAr),
                    AllowDuplicate = true
                };
            }
            else
            {
                return new BusinessApiException
                {
                    ErrorCode = 7101,
                    ErrorMessageEn = "Person ID must be Establishment only",
                    ErrorMessageAr = "رقم الهوية يجب أن يكون يكون منشأة فقط",
                    AllowDuplicate = true
                };
            }
        }

        //unknown only
        else if (idTypes == unknownOnly)
        {
            if (projectType is ProjectType.RestApi)
            {
                return new BusinessApiException
                {
                    HttpStatusCode = 400,
                    ErrorCode = 4201,
                    ErrorMessageEn = "**** must be Unknown identity only".Replace("****", idCategoryEn),
                    ErrorMessageAr = "رقم **** يجب أن يكون مجهول الهوية فقط".Replace("****", idCategoryAr),
                    AllowDuplicate = true
                };
            }
            else
            {
                return new BusinessApiException
                {
                    ErrorCode = 7101,
                    ErrorMessageEn = "Person ID must be Unknown identity only",
                    ErrorMessageAr = "رقم الهوية يجب أن يكون مجهول الهوية فقط",
                    AllowDuplicate = true
                };
            }
        }

        #endregion "1"

        else
        {
            throw new NotImplementedException("The ID type is not supported");
        }
    }


    /// <summary>
    /// Determines if the provided text is written in VB or C#.
    /// </summary>
    /// <param name="text">The text snippet to check.</param>
    /// <returns>True if the code is likely C# or VB, or False if unable to determine.</returns>
    public static bool IsTextCode(string text) => IsCSharpCode(text) || IsVBCode(text);

    /// <summary>
    /// Determines if the provided code snippet is written in C#.
    /// </summary>
    /// <param name="code">The code snippet to check.</param>
    /// <returns>True if the code is likely C#, False if it's likely VB, or False if unable to determine.</returns>
    public static bool IsCSharpCode(string code)
    {
        // Common C#-specific keywords
        string[] codeKeywords =
        {
            "{",
            " public ",
            " namespace ",
            "}",
            " private ",
            " protected ",
            " readonly ",
            " int ",
            " foreach( ",
            " while( ",
        };

        // Check if any keywords are present
        foreach (var keyword in codeKeywords)
        {
            if (code.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                return true; // Code is likely C3
            }
        }

        // Unable to determine
        return false;
    }

    /// <summary>
    /// Determines if the provided code snippet is written in VB.
    /// </summary>
    /// <param name="code">The code snippet to check.</param>
    /// <returns>True if the code is likely VB, False if it's likely C#, or False if unable to determine.</returns>
    public static bool IsVBCode(string code)
    {
        // Common VB-specific keywords
        string[] vbKeywords = { "<DataContract(", "DataMember(", "End Function", "End Class", "End Namespace", "End Sub", "ByVal " };

        // Check if any VB-specific keywords are present
        foreach (var keyword in vbKeywords)
        {
            if (code.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                return true; // Code is likely VB
            }
        }


        // Unable to determine
        return false;
    }


    #endregion 


    #region Private Method

    private static bool KillProcess(string name, DateTime processStartTime)
    {
        //here we're going to get a list of all running processes on
        //the computer

        foreach (Process clsProcess in Process.GetProcesses())
        {
            if (Environment.ProcessId == clsProcess.Id)
                continue;

            if (clsProcess.ProcessName.ToLower().Contains(name))
            {
                // {8/2/2023 9:24:02 PM}
                // {8/2/2023 9:24:02 PM}
                //if (clsProcess.StartTime >= processStartTime && (clsProcess.MainWindowTitle.StartsWith("Document")))
                if (clsProcess.StartTime >= processStartTime && processStartTime.AddMilliseconds(2000) >= clsProcess.StartTime && string.IsNullOrWhiteSpace(clsProcess.MainWindowTitle))
                {
                    Log.Information("Word Process (winword) Killed. \"{a}\". Process StartTime: {b}", clsProcess.MainWindowTitle, clsProcess.StartTime);

                    clsProcess.Kill();
                    //return true;
                }
            }
        }

        //otherwise we return a false
        return false;
    }

    #endregion
}