using Dev.Assistant.Business.Core.Models;
using Dev.Assistant.Business.Core.Utilities;
using Dev.Assistant.Configuration;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.WindowsAPICodePack.Dialogs;
using Serilog;
using System.Text;
using System.Text.RegularExpressions;

namespace Dev.Assistant.Business.Core.Services;

/// <summary>
/// Service for handling file-related operations.
/// </summary>
public static class FileService
{
    #region Public Methods

    /// <summary>
    /// Gets code from all .cs files in the specified folder.
    /// </summary>
    /// <param name="folderPath">The path to the folder containing .cs files.</param>
    /// <returns>A list of code from the files.</returns>
    /// <exception cref="DevAssistantException">Thrown if there is an error during processing.</exception>
    public static List<string> GetCodeByFolder(string folderPath)
    {
        Log.Logger.Information("GetCodeByFolder Called. paramValue: {a}", folderPath);

        try
        {
            var result = new List<string>();

            // Normalize and validate the folder path
            folderPath = NormalizeSystemPath(folderPath);

            // Validate fol;der path
            if (string.IsNullOrWhiteSpace(folderPath))
            {
                throw DevErrors.DevErrors.Core.E5000InvalidPath;
            }
            else if (!Directory.Exists(folderPath))
            {
                throw DevErrors.DevErrors.Core.E5001PathNotExist;
            }

            // Get all subdirectories in the specified folder
            string[] folders = Directory.GetDirectories(folderPath);

            // Validate that there are non-empty folders
            if (folders.Length == 0)
            {
                throw DevErrors.DevErrors.Core.E5002EmptyFolderPath;
            }

            // Process each folder
            foreach (var folder in folders)
            {
                // Get all .cs files in the current folder
                string[] files = Directory.GetFiles(folder, "*.cs");

                // Skip if no .cs files are found
                if (files.Length == 0)
                {
                    continue;
                }

                // Process each .cs file
                foreach (var file in files)
                {
                    result.Add(GetCodeByFile(file));
                }
            }

            return result;
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "An unexpected error occurred in GetCodeByFolder");
            throw;
        }
    }

    /// <summary>
    /// Gets service information from the specified path.
    /// </summary>
    /// <param name="path">The path to the service file.</param>
    /// <returns>The service information extracted from the path.</returns>
    public static ServiceInfo GetServiceInfo(string path)
    {
        Log.Logger.Information("GetFileInfo Called - paramValue: {a}", path);

        ServiceInfo serviceInfo = new();

        // Normalize the file path
        path = NormalizeSystemPath(path);

        // Check if the path contains "Nic.Apis" and extract service information
        if (string.IsNullOrWhiteSpace(path) || path.LastIndexOf("Nic.Apis") < 0)
            return serviceInfo;

        // Extracting information from the path
        //  namespace+conCode\             \controllerName\        serviceName
        // \Nic.Apis.SmartCity\BusinessLayer\ServiceBus\EsbCustomsGetSuspectPersonByID.cs
        var serviceInfoArray = path[path.LastIndexOf("Nic.Apis")..].Split("\\");

        if (serviceInfoArray.Length == 0)
            return serviceInfo;

        serviceInfo.Namespace = serviceInfoArray[0];
        serviceInfo.ContractCode = serviceInfoArray[0].Replace("Nic", "").Replace("Apis", "").Replace(".", "");

        if (serviceInfoArray.Length > 2)
            serviceInfo.ControllerName = serviceInfoArray[2];

        if (serviceInfoArray.Length > 3)
            serviceInfo.ServiceName = serviceInfoArray[3].Replace(".cs", "");

        return serviceInfo;
    }

    /// <summary>
    /// Gets micro information from the specified path.
    /// </summary>
    /// <param name="path">The path to the service file.</param>
    /// <returns>The micro information extracted from the path.</returns>
    public static MicroInfo GetMicroInfo(string path)
    {
        Log.Logger.Information("GetMicroInfo Called - paramValue: {a}", path);

        MicroInfo serviceInfo = new();

        // Normalize the file path
        path = NormalizeSystemPath(path);

        if (string.IsNullOrWhiteSpace(path) || path.LastIndexOf(".cs") < 0)
            return serviceInfo;

        // Extracting information from the path
        var serviceInfoArray = path[path.LastIndexOf(DevOpsClient.MicroProjectName)..].Split("\\");

        if (serviceInfoArray.Length == 0)
            return serviceInfo;

        if (serviceInfoArray.Length > 3)
            serviceInfo.MicroRootName = serviceInfoArray[3];

        if (serviceInfoArray.Length > 4)
            serviceInfo.MicroServiceName = serviceInfoArray[4].Replace(".cs", "");

        return serviceInfo;
    }

    /// <summary>
    /// Gets the code from the specified <see cref="NicServiceFile"/>.
    /// </summary>
    /// <param name="file">The <see cref="NicServiceFile"/> containing information about the service file.</param>
    /// <returns>The code from the service file.</returns>
    public static string GetCodeByFile(GetCodeByFileReq file)
    {
        // TODO: test t again! :)
        Log.Logger.Information("GetCodeByFile-PrFile Called - paramValue: {a} - {b}", file.Path, file.IsRemotePath);

        // If the file is a remote path, return the code; otherwise, get code from the file path
        if (file.IsRemotePath)
        {
            return !string.IsNullOrWhiteSpace(file.Code) ? file.Code : GetCodeFromGit(file);
        }
        else
        {
            return GetCodeByFile(file.Path);
        }
    }


    public static string GetCodeFromGit(GetCodeByFileReq file)
    {
        try
        {
            using var pathContent = DevOpsClient.GitClient.GetItemContentAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, file.Path,
                    scopePath: null, includeContent: true, includeContentMetadata: true,
                    versionDescriptor: new GitVersionDescriptor() { Version = file.BranchName, VersionType = GitVersionType.Branch }).Result;

            using StreamReader reader = new(pathContent, Encoding.Default, true);

            return reader.ReadToEnd();
        }
        catch (AggregateException ex)
        {
            // it's occurred sometime when the branch is deleted, so we will try again from Development branch

            Log.Logger.Error(ex, "An unexpected error occurred in GetCodeFromGit [AggregateException]");

            try
            {
                using var pathContent = DevOpsClient.GitClient.GetItemContentAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, file.Path,
                   scopePath: null, includeContent: true, includeContentMetadata: true,
                   versionDescriptor: new GitVersionDescriptor() { Version = "Development", VersionType = GitVersionType.Branch }).Result;

                using StreamReader reader = new(pathContent, Encoding.Default, true);

                return reader.ReadToEnd();
            }
            catch (Exception innerEx)
            {
                Log.Logger.Error(innerEx, "An unexpected error occurred in GetCodeFromGit [AggregateException inner]");

                throw;
            }
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "An unexpected error occurred in GetCodeFromGit");

            throw;
        }

    }

    public static string GetCodeFromTfs(GetCodeByFileReq file)
    {
        Stream pathContent = null;

        try
        {
            string microServicePath = file.Path;

            // Check if file.Path is path or micro name
            if (!microServicePath.Replace("\\", "/").Contains('/') || !microServicePath.Contains(".cs"))
            {
                microServicePath = DevOpsClient.GetMicroServicePath(microServicePath);
            }

            try
            {
                pathContent = DevOpsClient.TfsClient.GetItemContentAsync(DevOpsClient.MicroProjectName, microServicePath, fileName: null, includeContent: true).Result;
            }
            catch (AggregateException ex)
            {
                Log.Logger.Error(ex, "An unexpected error occurred in GetCodeFromTfs");
            }

            /* Sometime the micro service file name is diff from micro service folder name:
             * 
             * For example:
             * 
             * MicroListInvestmentLicenses: (end with 's')
             *      - ObjectModel/
             *      - MicroListInvestmentLicenses.csproj
             *      - MicroListInvestmentLicenseService.cs
             *                                  No 's' here
             *                                  
             * So, if it was null we will get all files under micro service folder, then get the .cs file which must be the service
             * 
             */


            if (pathContent == null)
            {
                microServicePath = file.Path;

                // If it is path, get micro root folder name
                if (file.Path.Replace("\\", "/").Contains('/') || file.Path.Contains(".cs"))
                {
                    var microInfo = GetMicroInfo(file.Path);
                    microServicePath = microInfo.MicroRootName;
                }


                // Get all files under micro 
                var microFiles = DevOpsClient.TfsClient.GetItemsAsync(DevOpsClient.MicroProjectName, DevOpsClient.GetMicroScopePath(microServicePath)).Result;

                // Get C# files only
                microFiles = microFiles.Where(item => item.Path.EndsWith(".cs")).ToList();

                if (microFiles.Count == 0)
                {
                    throw new DevAssistantException($"No files found under {DevOpsClient.GetMicroFullPath(microServicePath)}. Please check micro name!", 5003);
                }

                // if there is more than 1, then try to get the service "Service.cs"
                if (microFiles.Count > 1)
                {
                    microFiles = microFiles.Where(item => item.Path.EndsWith("Service.cs")).ToList();

                    if (microFiles.Count != 1)
                    {
                        throw new DevAssistantException($"No matched files found under {DevOpsClient.GetMicroFullPath(microServicePath)}. Trying to get micro service file path, please check the name of micro service", 5004);
                    }
                }

                microServicePath = microFiles.Select(p => p.Path).First();

                pathContent = DevOpsClient.TfsClient.GetItemContentAsync(DevOpsClient.MicroProjectName, microServicePath, fileName: null, includeContent: true).Result;
            }

            using StreamReader reader = new(pathContent, Encoding.Default, true);

            return reader.ReadToEnd();
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "An unexpected error occurred in GetCodeFromTfs");

            throw;
        }
        finally
        {
            pathContent?.Dispose();
        }
    }

    /// <summary>
    /// Gets code from the specified file path.
    /// </summary>
    /// <param name="path">The path to the file.</param>
    /// <returns>The code from the file.</returns>
    public static string GetCodeByFile(string path)
    {
        Log.Logger.Information("GetCodeByFile Called - paramValue: {a}", path);

        try
        {
            // Normalize file path
            path = NormalizeSystemPath(path);

            // Validates the provided file path.
            if (string.IsNullOrWhiteSpace(path))
            {
                throw DevErrors.DevErrors.Core.E5000InvalidPath;
            }
            else if (!File.Exists(path))
            {
                throw DevErrors.DevErrors.Core.E5001PathNotExist;
            }

            // Read the contents of the file and return the code
            using StreamReader reader = new(path);
            return reader.ReadToEnd();
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "An unexpected error occurred in GetCodeByFile");
            throw;
        }
    }


    /// <summary>
    /// Normalizes the folder/file path by removing redundant characters, replacing multiple backslashes with a single backslash and ensuring proper formatting.
    /// </summary>
    /// <param name="path">The folder/file path to normalize.</param>
    /// <returns>The normalized folder/file path.</returns>
    public static string NormalizeSystemPath(string path)
    {
        // Standardizing folder path
        path = path.Replace("/", "\\").Trim();
        path = path.Replace(@"\", "\\").Trim();

        path = new Regex("\\{2,}", RegexOptions.IgnoreCase)
            .Replace(path, "\\") // Replace multiple backslashes with a single one
            .Trim(); // Remove leading and trailing whitespaces

        return path;
    }

    public static string GetPathDialog(string title = "", string filter = "", string fileName = "", string initialDirectory = "")
    {
        Log.Logger.Information("GetPathDialog Called");

        string path = string.Empty;

        if (!string.IsNullOrWhiteSpace(initialDirectory) && initialDirectory.Contains('.'))
        {
            initialDirectory = initialDirectory[..initialDirectory.LastIndexOf("\\")];
        }

        using SaveFileDialog saveFileDialog1 = new()
        {
            Filter = filter,
            //CreatePrompt = true,
            RestoreDirectory = true,
            Title = title,
            FileName = fileName,
            InitialDirectory = initialDirectory
        };

        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        {
            path = saveFileDialog1.FileName;
        }

        return path;
    }

    public static string FolderPickerDialog(string title = "", string initialDirectory = "", bool isFilePicker = false)
    {
        Log.Logger.Information("FolderPickerDialog Called");

        string path = string.Empty;

        if (!string.IsNullOrWhiteSpace(initialDirectory) && initialDirectory.Contains('.'))
        {
            initialDirectory = initialDirectory[..initialDirectory.LastIndexOf("\\")];
        }

        if (CommonFileDialog.IsPlatformSupported)
        {
            using CommonOpenFileDialog dialog = new()
            {
                IsFolderPicker = !isFilePicker,
                AddToMostRecentlyUsedList = true,
                Title = title,
                InitialDirectory = initialDirectory,
                Multiselect = false,
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                path = dialog.FileName;
            }
        }
        else
        {
            using FolderBrowserDialog dialog = new()
            {
                //RootFolder = Environment.SpecialFolder.Desktop
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                path = dialog.SelectedPath;
            }
        }

        return path;
    }

    #endregion Public Methods

    //#region Private Methods

    //#endregion Private Methods
}