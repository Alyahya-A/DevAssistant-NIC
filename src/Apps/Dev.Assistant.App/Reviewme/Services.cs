using Dev.Assistant.Business.Core.Models;
using Dev.Assistant.Business.Core.Services;
using Dev.Assistant.Business.Decoder.Models;
using System.Text.RegularExpressions;

namespace Dev.Assistant.App.Reviewme;

public class Services
{
    private static List<FileInfoModel> _listOfMicros;
    private static FileInfoModel _microInfo;
    private static string contract;

    public static List<FileInfoModel> GetMicros(string path, bool getFromBayan = false)
    {
        contract = getFromBayan ? "Nic.Apis.Bayan" : "MicroServices";

        if (path.EndsWith(".csproj"))
        {
            path = path[..path.LastIndexOf("\\")];
        }

        if (string.IsNullOrWhiteSpace(path))
        {
            //Home.Form.AddLog($"{contract}: Invalid value for parameter(path)");
            throw new Exception("Invalid value for parameter(path) " + DateTime.Now.ToString());
        }
        else if (!Directory.Exists(path))
        {
            //Home.Form.AddLog($"{contract}: The given path is not existing on disk: " + path);
            throw new Exception("The given path is not existing on disk " + path);
        }

        _listOfMicros = new List<FileInfoModel>();

        Folder(path, getFromBayan);

        return _listOfMicros;
    }

    private static void Folder(string path, bool getFromBayan)
    {
        path = FileService.NormalizeSystemPath(path);

        string[] folders;

        if (getFromBayan)
        {
            path += "\\BusinessLayer";

            folders = Directory.GetDirectories(path);
        }
        else
        {
            //if (!Directory.Exists(path))
            //{
            //    Home.Form.AddLog($"{contract}: The given path is not existing on disk");
            //    throw new Exception("The given path is not existing on disk " + DateTime.Now.ToString());
            //}

            folders = Directory.GetDirectories(path);
        }

        if (folders.Length == 0)
        {
            //Home.Form.AddLog($"{contract}: Supplied folder is empty");
            return;
        }

        string folderName;

        foreach (var folder in folders)
        {
            int index = folder.LastIndexOf("\\");
            folderName = folder[(index + 1)..]; // may MicroName or ControllerName

            if ((!getFromBayan && !folderName.StartsWith("Micro")) || folderName.Contains('.') || folderName.Contains('$') || ValidationService.IsExcludedMicro(folderName))
                continue;

            string modelsPath;

            /*
            * in Bayan Api the folders will be controller names. Person, Business etc.
            * in MicroServices the folders will be the Micro projects. MicroGetPersonBasinInfo etc.
            *
            * so, in Bayan we must get the files (micros) in that controller (folder) first.
            * but, in micros the microName will be same as the name of Micro projects
            *
            * in RestApis each Micro has One file for model which have the Req and Res classes
            * in Micros each Micro has Two files for model: Inputs and Outputs files
            */

            if (getFromBayan)
                modelsPath = folder;
            else
                modelsPath = $@"{folder}\\ObjectModel";

            string[] files = Directory.GetFiles(modelsPath, "*.cs");

            if (files.Length == 0)
            {
                //Home.Form.AddLog($"{contract}: Folder " + folder + " doesn't have any CS files");
                continue;
            }

            if (getFromBayan)
            {
                // Here we are in a Controller. we will loop the filse(MicroServices) in this Controller

                //Home.Form.AddLog($"{contract}: Starting Controller: " + folderName);

                foreach (var microModel in files)
                { // microModel is the MicroService which is a file contains all models of this micro,
                  // ex. MicroGetPersonBasicInfo have 2+ classes (Req & Res classes)

                    index = microModel.LastIndexOf("\\");

                    _microInfo = new FileInfoModel
                    {
                        Name = microModel[(index + 1)..].Replace(".cs", ""),
                        Models = File(microModel),
                        IsMicroInBayan = true,
                        Controller = folderName
                    };

                    _listOfMicros.Add(_microInfo);
                }

                //Home.Form.AddLog($"{contract}: Ending from Controller: " + folderName);
            }
            else
            {
                // Here we are in a MicroService. we will loop the filse(Input and Output files) in this MicroService

                //Home.Form.AddLog($"{contract}: Starting Micro: " + folderName);

                _microInfo = new FileInfoModel
                {
                    Name = folderName,
                    Models = new List<ClassModel>()
                };

                foreach (var microModel in files)
                { // file may Input or Output file of MicroService which in folder: "<MicroService>\ObjectModel"
                    var classes = File(microModel);

                    _microInfo.Models.AddRange(classes);
                }

                _listOfMicros.Add(_microInfo);

                //Home.Form.AddLog($"{contract}: Ending from Micro: " + folderName);
            }
        }
    }

    private static List<ClassModel> File(string path)
    {
        path = FileService.NormalizeSystemPath(path);

        int index = path.LastIndexOf("\\");

        var fileName = path[(index + 1)..].Replace(".cs", "");

        //Home.Form.AddLog($"{contract}: Starting with file: " + fileName);

        //_homeForm.AddLog();

        StreamReader reader;
        try
        {
            reader = new StreamReader(path);
        }
        catch (Exception)
        {
            //Home.Form.AddLog($"{contract}: Error when opening " + path);
            throw new Exception("Error when opening " + path + " " + DateTime.Now.ToString());
        }

        var code = reader.ReadToEnd();

        //Home.Form.AddLog($"{contract}: Getting classes -- START");
        var classes = GetClasses(code);
        //Home.Form.AddLog($"{contract}: Getting classes -- END with {classes.Count} class(es)");

        //Home.Form.AddLog($"{contract}: End with file: " + fileName);

        reader.Dispose();

        return classes;
    }

    public static List<ClassModel> GetClasses(string code)
    {
        List<ClassModel> classes = new();

        string[] words = code.Trim().Split(Convert.ToChar(" "));

        // Getting classes
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i] == "public" && words[i + 1].Contains("class"))
            { // it is a Class
                ClassModel classModel = new();

                string className = words[i + 2].Trim();
                classModel.Name = className;

                string classHeader = words[i] + " " + words[i + 1] + " " + className;

                int startIndex = code.IndexOf(classHeader);
                string classCode = code[startIndex..];

                int openedCBrackets = 0;
                int closedCBrackets = 0;

                int openedCBracketsIndex = 0;
                int closedCBracketsIndex = 0;
                string tempCode = classCode;
                int length = 0;
                int tempLength = 0;

                // to determine the first '{' and the last '}' to get the classes for this file
                while (true)
                {
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

                    if (openedCBrackets == closedCBrackets)
                    {
                        classCode = classCode[..length];
                        break;
                    }
                }

                classModel.Body = classCode;
                classModel.Properties = new List<Property>();

                classes.Add(classModel);
            }
        }

        Property property;

        // to get the props for each class within this file
        foreach (ClassModel classM in classes)
        {
            string[] lines = classM.Body.Split(Convert.ToChar("\n"));

            for (int i = 0; i < lines.Length; i++)
            {
                // commented prop, comment or summary
                if (lines[i].Trim().StartsWith("//"))
                    continue;

                string[] wordsPerLine = lines[i].Split(Convert.ToChar(" "));

                for (int j = 0; j < wordsPerLine.Length; j++)
                {
                    if (wordsPerLine[j] == "public" && wordsPerLine[j + 1] != "class" && !wordsPerLine[j + 1].Contains($"{classM.Name}()"))
                    {
                        property = new Property
                        {
                            Name = wordsPerLine[j + 2],
                            DataType = wordsPerLine[j + 1]
                        };

                        classM.Properties.Add(property);
                    }
                } // end of wordsPerLine loop
            } // end of lines loop
        } // end of getting the props

        return classes;
    }
}