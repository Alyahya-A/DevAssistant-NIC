using Dev.Assistant.Business.Core.Models;
using Dev.Assistant.Business.Core.Services;
using Dev.Assistant.Business.Decoder.Models;
using Dev.Assistant.Business.Decoder.Services;
using Dev.Assistant.Configuration;
using Serilog;
using System.Text.RegularExpressions;

// Error code start with 7200

namespace Dev.Assistant.App.UtilitiesForms;

public class ListPDSTablesService
{
    private PdsTableOfService _serviceInfo;
    private readonly UtilitiesFormsHome _form; // to access Log in Home Form

    public ListPDSTablesService(UtilitiesFormsHome home)
    {
        _form = home;
    }

    public PdsTableOfService ListTablesService(string path, ListTablesServiceOptions options = null)
    {
        //_form.appHome.LogInfo($"Start with {path}");

        if (options is null)
        {
            options = new ListTablesServiceOptions();
        }

        _serviceInfo = new PdsTableOfService
        {
            Name = null,
            Tables = new List<string>()
        };

        path = path.Replace("\"", "");

        //User want to list tables for a specific file.
        if (path.EndsWith(".cs") || path.EndsWith(".vb"))
        {
            File(path, options);
        }
        else if (!ListTablesForSelectedMicro(path, options)) //User want to list tables for a specific micro.
        {
            throw new DevAssistantException("Invalid value for parameter(path)", 7201);
        }

        return _serviceInfo;
    }

    private bool ListTablesForSelectedMicro(string microName, ListTablesServiceOptions options)
    {
        //string microPath = Directory.GetCurrentDirectory();

        string path;

        if (options.IsTesting)
            path = @"C:\Project\MicroServices\Core\Development";
        else
            path = options.MicroPath ?? Consts.UserSettings.MicroServicePath;

        path += "\\" + microName.Trim() + "\\" + microName.Trim() + "Service.cs";

        Log.Logger.Information("ListTablesForSelectedMicro path: {a}", path);

        //// to go back to the current path of the app
        //Directory.SetCurrentDirectory(microPath);

        return File(path, options);
    }

    private bool File(string path, ListTablesServiceOptions options)
    {
        _serviceInfo.Tables.Clear();

        path = FileService.NormalizeSystemPath(path);

        int index = path.LastIndexOf("\\");
        string fileName = path[(index + 1)..].Replace("Service.cs", "");
        string modelPath = path[..index] + $"\\ObjectModel\\{fileName}Inputs.cs";

        _form.appHome.LogTrace(options.OnlyIdTypes ? $"Get ID types for {fileName} -- START" : $"Get PDS table for {fileName} -- START");


        try
        {
            string code = string.Empty;

            // Read the contents of the file and return the code
            using (StreamReader reader = new(path))
            {
                code = reader.ReadToEnd();
            };

            if (path.Contains("Nic.Apis."))
            {
                var serviceInfo = FileService.GetServiceInfo(path);

                _serviceInfo.Name = $"{serviceInfo.ContractCode}: {serviceInfo.ServiceName}";
            }
            else
            {
                _serviceInfo.Name = fileName.Replace(".cs", "");
            }
            if (options.OnlyIdTypes)
            {
                Log.Logger.Information("GetIDTypes...");


                GetIDTypes(options, fileName, modelPath, code);

                Log.Logger.Information("ID type(s) found: {a}", string.Join(", ", _serviceInfo.Tables));
            }
            else
            {
                Log.Logger.Information("GetTableNames...");

                GetTableNames(path, code, options);

                Log.Logger.Information("PDS tables found: {a}", _serviceInfo.Tables.Count);
            }

            _form.appHome.LogTrace(options.OnlyIdTypes ? $"Get ID types for {_serviceInfo.Name} -- END" : $"Get PDS table for {_serviceInfo.Name} -- END");

            return true;

        }
        catch (Exception)
        {
            _form.appHome.LogTrace(options.OnlyIdTypes ? $"Get ID types for {fileName} -- END" : $"Get PDS table for {fileName} -- END");
            throw new DevAssistantException($"Error when opening \"{path}\". Please check your Micro path! Your micro path is: \"{Consts.UserSettings.MicroServicePath}\"", 7203);
        }


    }

    private void GetTableNames(string path, string code, ListTablesServiceOptions options)
    {
        bool displayNextLine = false;
        bool isUsingAt = false; // at == @
        bool isUsingAtNotCleaned = false; // at == @
        string prevLine = "";
        string prevLineNotCleaned = "";
        string idTypes = "";

        string lineNotCleaned;

        // Indicate if there is no dbo. or with(nolock) keywords
        bool dboNotFound = false;

        var classes = ModelExtractionService.GetClassesByCode(code, new() { IncludeFunctions = true, IncludProperties = false });

        foreach (var clazz in classes)
        {
            foreach (var func in clazz.Functions)
            {
                // tying to avoid DB2
                if (func.Code.Contains("DB2ConnectionStringBuilder") || func.Code.Contains("Provider=DB2OLEDB;Network Address"))
                    continue;

                var lines = func.Code.Split("\n");

                foreach (var l in lines)
                {
                    string line = l.Trim();


                    if (string.IsNullOrWhiteSpace(line))
                        continue;


                    if (line.StartsWith("using") || line.StartsWith("#") || line.Equals("{") || line.Equals("}") || line.StartsWith("THROW NEW"))
                        continue;

                    // For api. Ex ErrorMessage = MicroCommon.LanguageConstants.Arabic.Equals(req.Lang) ? **** : "Page should start from 1"
                    // it's contains FROM word, so the table will be 1 :(
                    if (line.StartsWith("ErrorMessage =") || line.StartsWith(":"))
                        continue;

                    if (line.StartsWith("namespace ") && _serviceInfo.Name.ToLower() == line.Replace("namespace ", "").ToLower())
                    {
                        _serviceInfo.Name = line.Replace("namespace ", "");
                        continue;
                    }

                    if (line.StartsWith("//") || line.StartsWith("*") || line.StartsWith("'"))
                        continue;

                    lineNotCleaned = line;

                    if (line.EndsWith(";"))
                        line = line.Replace(";", "").Trim();

                    // avoiding [dbo].[SC203_TERMNAL_LOC]
                    line = line.Replace("[", "");
                    line = line.Replace("]", "");

                    line = line.ToUpper().Replace("DBO.", "");
                    line = new Regex(" {2,}", RegexOptions.IgnoreCase).Replace(line, " ").Trim();
                    line = new Regex("\t", RegexOptions.IgnoreCase).Replace(line, " ").Trim();

                    lineNotCleaned = lineNotCleaned.Replace("[", "");
                    lineNotCleaned = lineNotCleaned.Replace("]", "");

                    lineNotCleaned = new Regex(" {2,}", RegexOptions.IgnoreCase).Replace(lineNotCleaned, " ").Trim();
                    lineNotCleaned = new Regex("\t", RegexOptions.IgnoreCase).Replace(lineNotCleaned, " ").Trim();

                    //          Micro common           ||         RestApi common
                    if (line.Contains("GUARDSINGLEID") || line.Contains("VALIDATEIDTYPE"))
                    {
                        if (line.Contains(".NONE"))
                        {
                            idTypes = "CAVP";
                        }
                        else
                        {
                            if (line.Contains(".CITIZEN"))
                                idTypes += "C";

                            if (line.Contains(".ALIEN"))
                                idTypes += "A";

                            if (line.Contains(".VISITOR"))
                                idTypes += "V";

                            if (line.Contains(".PLIGRIM"))
                                idTypes += "P";
                        }

                        AddTablesOfCheckingIrregularStatus(idTypes);
                    }

                    if (line.Contains(".VALIDATEVEHICLEPLATEDETAILS"))
                    {
                        _serviceInfo.Tables.Add("MV201_NEW_REG_TYPE");
                    }

                    //                                 Micro common                                      ||                   RestApi common
                    if (line.Contains("VALIDATEIRREGULARPERSON") || line.Contains("HASIRREAGULARSTATUS") || line.Contains("VALIDATEPERSONIRREGULARSTATUS"))
                    {
                        AddTablesOfCheckingIrregularStatus("CAVP");
                    }

                    if (line.Contains("VALIDATEBIRTHDATE"))
                    {
                        _serviceInfo.Tables.Add("IFR700_PERSON");
                        _serviceInfo.Tables.Add("IFR701_C_PERSON_XT");
                    }

                    if (line.Contains("VALIDATEOPERATORSERVICEACCESS"))
                    {
                        ListPDSTablesService _listTables = new(_form);

                        _serviceInfo.Tables.AddRange(_listTables.ListTablesService("MicroListPersonServiceAccessRecords", options).Tables);
                    }

                    // No sql
                    if (!isUsingAt && !line.Contains('"') && !line.Contains('\''))
                        continue;

                    if (line.Contains("@\""))
                    {
                    }

                    int f1 = line.IndexOf("\"");
                    int f2 = line.LastIndexOf("\"");

                    int f3 = lineNotCleaned.IndexOf("\"");
                    int f4 = lineNotCleaned.LastIndexOf("\"");

                    if ((f1 > -1) && (f2 > -1))
                    {
                        // that mean only one " in the line. Maybe was using @" Ex:
                        // string a = @" Select *
                        //               From Table";

                        // to enter only for first " in the query. and avoid if it was the last one (to handle this bellow)
                        if (f1 == f2 && !isUsingAt)
                        {
                            line = line[(f1 + 1)..].Trim();
                            isUsingAt = true;
                        }
                        else if (!isUsingAt) // for last line. because isUsingAt is true and we want to handle it bellow
                        {
                            line = line.Substring(f1 + 1, f2 - f1 - 1).Trim();
                            isUsingAt = false;
                        }
                    }

                    if ((f3 > -1) && (f4 > -1))
                    {
                        if (f3 == f4 && !isUsingAtNotCleaned)
                        {
                            lineNotCleaned = lineNotCleaned[(f3 + 1)..].Trim();
                            isUsingAtNotCleaned = true;
                        }
                        else if (!isUsingAtNotCleaned)
                        {
                            lineNotCleaned = lineNotCleaned.Substring(f3 + 1, f4 - f3 - 1).Trim();
                            isUsingAtNotCleaned = false;
                        }
                    }

                    if (path.EndsWith(".vb") && line.Contains('\''))
                    {
                        int singleQutIndex = line.LastIndexOf('\'');
                        int doubleQutIndex = line.LastIndexOf('\"');

                        while ((singleQutIndex > doubleQutIndex) && (singleQutIndex != -1) && (doubleQutIndex != -1))
                        {
                            line = line[..(doubleQutIndex - 1)].Trim();

                            singleQutIndex = line.LastIndexOf('\'');
                            doubleQutIndex = line.LastIndexOf('\"');
                        }
                    }
                    else
                    {
                        if (line.Contains("//"))
                        {
                            int singleQutIndex = line.LastIndexOf("//");
                            int doubleQutIndex = line.LastIndexOf('\"');

                            while ((singleQutIndex > doubleQutIndex) && (singleQutIndex != -1) && (doubleQutIndex != -1))
                            {
                                line = line[..(doubleQutIndex - 1)].Trim();

                                singleQutIndex = line.LastIndexOf("//");
                                doubleQutIndex = line.LastIndexOf('\"');
                            }
                        }
                    }

                    if (line.Contains("Table5"))
                    {
                    }

                    if (displayNextLine || isUsingAt)
                    {
                        if (isUsingAt)
                        {
                            f1 = line.IndexOf("\"");
                            f2 = line.LastIndexOf("\"");

                            // if > -1 & f1 == f2 this mean this line is the last

                            //                         && the last "
                            if ((f1 > -1) && (f2 > -1) && f1 == f2)
                            {
                                line = line[..^1].Trim();
                                lineNotCleaned = lineNotCleaned[..^1].Trim();

                                isUsingAt = false;
                                isUsingAtNotCleaned = false;
                            }
                            else
                            {
                                prevLine = prevLine + " " + line;
                                prevLineNotCleaned = prevLineNotCleaned + " " + lineNotCleaned;
                                continue;
                            }
                        }

                        if (line.EndsWith("JOIN") || line.Contains("FROM"))
                        {
                            if (!line.Contains("NOLOCK") && line.Contains("FROM") && line.Contains("ROW_NUMBER()"))
                            {
                                continue;
                            }

                            displayNextLine = true;
                            prevLine = prevLine + " " + line;
                            prevLineNotCleaned = prevLineNotCleaned + " " + lineNotCleaned;
                            continue;
                        }

                        line = prevLine + " " + line;
                        lineNotCleaned = prevLineNotCleaned + " " + lineNotCleaned;

                        dboNotFound = GetPdsTables(line, lineNotCleaned, dboNotFound);

                        displayNextLine = false;
                    }
                    else if (line.Contains("JOIN") || line.Contains("FROM") || line.Contains("NOLOCK"))
                    {
                        if (!line.Contains("NOLOCK") && line.Contains("FROM") && line.Contains("ROW_NUMBER()"))
                        {
                            continue;
                        }

                        if (line.EndsWith("JOIN") || line.EndsWith("FROM"))
                        {
                            displayNextLine = true;
                            prevLine = line;
                            prevLineNotCleaned = lineNotCleaned;
                            continue;
                        }

                        dboNotFound = GetPdsTables(line, lineNotCleaned, dboNotFound);
                    }
                }//while
            }

        }

        _form.appHome.LogInfo($"Get tables from {_serviceInfo.Name}. Number of PDS Tables found {_serviceInfo.Tables.Count}");

        if (dboNotFound)
        {
            //throw new DevAssistantException("The process has been stopped. Please resolve the aboe errors", 7200);
        }

        if (_serviceInfo.Tables.Count == 0)
        {
            _form.appHome.LogError($"Get tables from {_serviceInfo.Name}. No PDS Tables found for \"{_serviceInfo.Name}\"");
        }
        else
        {
            _serviceInfo.Tables.Sort();
        }
    }

    private bool GetPdsTables(string line, string lineNotCleaned, bool dboNotFound)
    {
        string[] words = line.Split(' ');
        string[] wordsNotCleaned = lineNotCleaned.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Equals("FROM") || words[i].Equals("JOIN"))
            {
                // For nested query. Ex .... From ( Select **** )
                if (words[i + 1] is "(" or "(SELECT")
                    continue;

                //condition 1: to not add double qutation (") into list. it can be caused be micros that access db2
                //condition 2: to not add db2 tables such as DB2ADMIN.ENROLLMENT

                //    condition 1,                    condition 2
                if (!words[i + 1].Equals("\"") && !words[i + 1].Contains('.'))
                {
                    if (!wordsNotCleaned[i + 1].ToUpper().StartsWith("DBO."))
                    {
                        dboNotFound = true;

                        _form.appHome.LogError($"Table {words[i + 1]} doesn't start with dbo.");
                    }

                    int desiredIndex = i + 2;

                    if (words.Length > desiredIndex && words[desiredIndex].ToUpper().Contains("WITH"))
                    {
                        /* Ex:
                         *     i        i + 1            i + 2
                         *    From PSR001_SAUDI_PASPR with(nolock)
                         */
                    }
                    else if (words.Length > desiredIndex + 1 && words[desiredIndex + 1].ToUpper().StartsWith("WITH"))
                    {
                        /* Ex:
                         *     i        i + 1         i + 2     i + 3
                         *    From PSR001_SAUDI_PASPR PSR001 with(nolock)
                         */
                    }
                    //else if (wordsNotCleaned.Length > 0 && wordsNotCleaned[0].ToUpper().StartsWith("WITH"))
                    //{
                    //    // if with(nolock) in next line
                    //    // not implemented yet
                    //}
                    else
                    {
                        dboNotFound = true;

                        _form.appHome.LogError($"Table {words[i + 1]} doesn't have \"with(nolock)\".");
                    }

                    // such as ZONEDETAILS
                    if (Consts.ExcludedTables.Contains(words[i + 1].ToUpper()))
                        continue;

                    // check if table already in list
                    if (_serviceInfo.Tables.Contains(words[i + 1]))
                        continue;

                    _serviceInfo.Tables.Add(words[i + 1]);
                }
            }
        }

        return dboNotFound;
    }

    private void GetIDTypes(ListTablesServiceOptions options, string fileName, string modelPath, string code)
    {
        string line;

        // Get all props of micro inputs
        var microInputs = ModelExtractionService.GetClassesByFilePath(modelPath, new GetClassesOptions { CheckSpellingAndRules = false })?
            .Where(model => model.Name.ToLower() == $"{fileName}Inputs".ToLower()).First().Properties
            .Where(prop =>
            {
                return prop.Name.ToLower() is not "displayirregular"
                       and not "pagenumber"
                       and not "pagerows"
                       and not "lang"
                       and not "listall";
            })
            .ToList();

        string propNameLog;
        string propName;

        if (options.IsTesting)
        {
            propNameLog = options.PropName; // for logging no need for ToLower().
            propName = options.PropName.ToLower();
        }
        else
        {
            // show Dialog to ask user to select one prop
            propNameLog = _form.AskUserToSelectProp(microInputs)?.Name; // for logging no need for ToLower().
            propName = propNameLog?.ToLower();
        }

        if (string.IsNullOrWhiteSpace(propName))
        {
            throw new Exception("There is no property selected. Please try again and select one");
        }

        var microMethods = ModelExtractionService.GetAllMethods(code);

        // Micro web service.. the public method which has all the validations
        var microWS = microMethods.Funcations.Where(fuc => fuc.FunName.ToLower() == fileName.Trim().ToLower()).FirstOrDefault()?.Code.ToLower();

        if (microWS is null)
        {
            throw new Exception($"There is no public method match {fileName}. Please enter a valid Micro name");
        }

        microWS = microWS.Replace("\r\n", "\n").Replace("\r", "\n");

        RegexOptions regOptions = RegexOptions.None;
        Regex regex = new("[ ]{2,}", regOptions);

        string[] lines = microWS.Split(Convert.ToChar("\n"));

        var idTypesFounded = new HashSet<string>();

        _form.appHome.LogInfo("Looping the MicroService -- START");

        for (int i = 0; i < lines.Length; i++)
        {
            line = lines[i].Trim();

            if (string.IsNullOrWhiteSpace(line))
                continue;

            if (line.StartsWith("//") || line.StartsWith("*") || line.StartsWith("'"))
                continue;

            if (line.Equals("{") || line.Equals("}") || line.Equals("else") || line.Contains("throw"))
                continue;

            if (line.Contains(" = ") || line.Contains("validateirregularperson"))
                continue;

            if (line.Contains(propName) && (line.Contains("if") || line.Contains("||") || line.Contains("&&")))
            {
                _form.appHome.LogTrace("");
                _form.appHome.LogTrace($"8000: I found validation contains {propNameLog} input");
                // remove sapces
                line = regex.Replace(line, " ").Replace(" ", "").Replace(".tostring()", "");

                //                     || line.Contains("<") -->>????
                if (line.Contains('>'))
                {
                    _form.appHome.LogTrace("8001: I found validation contains '>'");

                    if (!line.Contains($"{propName}>"))
                    {
                        _form.appHome.LogTrace($"8002: but it's not for {propNameLog}");
                        continue;
                    }
                    else
                    {
                        _form.appHome.LogTrace($"8003: yeah, it is '{propNameLog} >'");

                        // Get the remaining lines
                        var tempCode = lines.Skip(i).ToArray();

                        // Get the first body
                        _form.appHome.LogTrace($"8004: get the body for this validation (if-statement)");
                        var ifBody = ModelExtractionService.GetBody(string.Join("\n", tempCode));

                        if (ifBody.Contains("guardsingleid"))
                        {
                            _form.appHome.LogTrace($"8005: the body contains 'GuardSingleID()' validation");

                            //guardsingleid(
                            var startIndex = ifBody.IndexOf("guardsingleid(") + 14;
                            ifBody = ifBody[startIndex..];

                            //guardsingleid(****,
                            var commaIndex = ifBody.IndexOf(",");

                            var tempPropName = ifBody[..commaIndex];

                            // inputs.personid
                            if (tempPropName.Contains('.'))
                            {
                                // personid
                                tempPropName = tempPropName[(tempPropName.IndexOf(".") + 1)..];
                            }

                            // to be sure that we are in right validation -- START
                            var semiIndex = ifBody.IndexOf(";");
                            var validationLine = ifBody[..semiIndex];

                            if (tempPropName != propName)
                            {
                                _form.appHome.LogTrace($"8007: sad the validation is for {tempPropName} :(... the GuardSingleID param is not {propNameLog}");
                                continue;
                            }
                            // to be sure that we are in right validation -- END

                            var formattedLine = GetFormattedLine(propNameLog, propName, validationLine);

                            _form.appHome.LogTrace($"8007: yeah.. the validation is for {propNameLog}");
                            _form.appHome.LogTrace($"8008: validationLine: GuardSingleID({formattedLine};");

                            if (validationLine.Contains(".citizen"))
                                idTypesFounded.Add("Citizen");

                            if (validationLine.Contains(".alien"))
                                idTypesFounded.Add("Alien");

                            if (validationLine.Contains(".visitor"))
                                idTypesFounded.Add("Visitor");

                            if (validationLine.Contains(".pligrim"))
                                idTypesFounded.Add("Pligrim");

                            if (validationLine.Contains(".establishment"))
                                idTypesFounded.Add("Establishment");

                            if (validationLine.Contains(".unknown"))
                                idTypesFounded.Add("Unknown");
                        }
                        else if (ifBody.Contains(".startswith("))
                        {
                            _form.appHome.LogWarning("9001: I'm HERE, not implemented yet :(");
                        }
                        else
                        {
                            _form.appHome.LogTrace($"8009: the body doesn't contain 'GuardSingleID()' validation");
                        }
                    }
                }
                else if (line.Contains($"{propName}.startswith("))
                {
                    _form.appHome.LogTrace($"8016: yeaaaah, I found validation for '{propNameLog}.StartsWith(\"*\")'");

                    if (line.Contains($"{propName}.startswith(\"1\")"))
                        idTypesFounded.Add("Citizen");

                    if (line.Contains($"{propName}.startswith(\"2\")"))
                        idTypesFounded.Add("Alien");

                    if (line.Contains($"{propName}.startswith(\"3\")"))
                        idTypesFounded.Add("Visitor");

                    if (line.Contains($"{propName}.startswith(\"4\")"))
                        idTypesFounded.Add("Visitor");

                    if (line.Contains($"{propName}.startswith(\"5\")"))
                        idTypesFounded.Add("Visitor");

                    if (line.Contains($"{propName}.startswith(\"6\")"))
                        idTypesFounded.Add("Pligrim");

                    if (line.Contains($"{propName}.startswith(\"7\")"))
                        idTypesFounded.Add("Establishment");

                    if (line.Contains($"{propName}.startswith(\"9\")"))
                        idTypesFounded.Add("Unknown");
                }
            }
        }

        if (idTypesFounded.Count == 0)
        {
            _form.appHome.LogTrace($"8010: no types and validation found for the {propNameLog}");

            microWS = regex.Replace(microWS, " ").Replace(" ", "").Replace(".tostring()", "");

            if (microWS.Contains("guardsingleid"))
            {
                _form.appHome.LogTrace($"8011: the WS contains 'GuardSingleID()' validation without checking for the input");

                //guardsingleid(
                var startIndex = microWS.IndexOf("guardsingleid(") + 14;
                microWS = microWS[startIndex..];

                //guardsingleid(****,
                var commaIndex = microWS.IndexOf(",");

                var tempPropName = microWS[..commaIndex];

                // inputs.personid
                if (tempPropName.Contains('.'))
                {
                    // personid
                    tempPropName = tempPropName[(tempPropName.IndexOf(".") + 1)..];
                }

                // to be sure that we are in right validation -- START
                var semiIndex = microWS.IndexOf(";");
                var validationLine = microWS[..semiIndex];

                if (tempPropName != propName)
                {
                    _form.appHome.LogTrace($"8012: sad the validation is for {tempPropName} :(... the GuardSingleID param is not {propNameLog}");
                }
                else
                {
                    // to be sure that we are in right validation -- END

                    var formattedLine = GetFormattedLine(propNameLog, propName, validationLine);

                    _form.appHome.LogTrace($"8013: yeah.. the validation is for {propNameLog}");
                    _form.appHome.LogTrace($"8014: validationLine: GuardSingleID({formattedLine};");

                    if (validationLine.Contains(".citizen"))
                        idTypesFounded.Add("Citizen");

                    if (validationLine.Contains(".alien"))
                        idTypesFounded.Add("Alien");

                    if (validationLine.Contains(".visitor"))
                        idTypesFounded.Add("Visitor");

                    if (validationLine.Contains(".pligrim"))
                        idTypesFounded.Add("Pligrim");

                    if (validationLine.Contains(".establishment"))
                        idTypesFounded.Add("Establishment");

                    if (validationLine.Contains(".unknown"))
                        idTypesFounded.Add("Unknown");
                }
            }
            else
            {
                _form.appHome.LogTrace($"8015: the WS doesn't contain 'GuardSingleID()' validation");
            }
        }

        _form.appHome.LogTrace("Looping the MicroService -- END");

        if (idTypesFounded.Count > 0)
        {
            _form.appHome.LogSuccess($"ID type(s) found for {propNameLog}: \n- {string.Join("\n- ", idTypesFounded)}");

            _serviceInfo.Tables = idTypesFounded.ToList();
        }
        else
        {
            _form.appHome.LogError($"No ID types found for {propNameLog}");
        }
    }

    private static string GetFormattedLine(string propNameLog, string propName, string validationLine)
    {
        return validationLine.Replace(propName, propNameLog).Replace(",", ", ").Replace("idtypeoptions", "IDTypeOptions").Replace("|", " | ").Replace("citizen", "Citizen").Replace("alien", "Alien").Replace("visitor", "Visitor").Replace("pligrim", "Pligrim").Replace("establishment", "Establishment").Replace("unknown", "Unknown");
    }

    private void AddTablesOfCheckingIrregularStatus(string idTypes)
    {
        if (!string.IsNullOrWhiteSpace(idTypes))
        {
            foreach (var a in idTypes)
            {
                switch (a)
                {
                    case 'C':
                        if (!_serviceInfo.Tables.Contains("IFR700_PERSON"))
                            _serviceInfo.Tables.Add("IFR700_PERSON");

                        if (!_serviceInfo.Tables.Contains("IFR701_C_PERSON_XT"))
                            _serviceInfo.Tables.Add("IFR701_C_PERSON_XT");

                        if (!_serviceInfo.Tables.Contains("SRR220_LK_SEC_LOCA"))
                            _serviceInfo.Tables.Add("SRR220_LK_SEC_LOCA");

                        break;

                    case 'A':

                        if (!_serviceInfo.Tables.Contains("IFR702_A_PERSON_XT"))
                            _serviceInfo.Tables.Add("IFR702_A_PERSON_XT");

                        break;

                    case 'V':

                        if (!_serviceInfo.Tables.Contains("IFR703_V_PERSON_XT"))
                            _serviceInfo.Tables.Add("IFR703_V_PERSON_XT");

                        break;

                    case 'P':

                        if (!_serviceInfo.Tables.Contains("IFR706_P_PERSON_XT"))
                            _serviceInfo.Tables.Add("IFR706_P_PERSON_XT");

                        break;
                }
            }
        }
    }
}