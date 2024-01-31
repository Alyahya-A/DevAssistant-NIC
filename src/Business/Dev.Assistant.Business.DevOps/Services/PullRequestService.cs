using Dev.Assistant.Business.Core.Services;
using Dev.Assistant.Business.Core.Utilities;
using Dev.Assistant.Business.Decoder.Models;
using Dev.Assistant.Business.DevOps.Models;
using Dev.Assistant.Configuration;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Serilog;
using System.Text;

namespace Dev.Assistant.Business.DevOps.Services;

public static class PullRequestService
{
    #region Public Methods

    public static List<NicService> PreparePullRequest(int prId, PreparePullRequestOptions options = null)
    {
        options ??= new PreparePullRequestOptions();

        try
        {

            var pr = DevOpsClient.GitClient.GetPullRequestAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, prId, includeWorkItemRefs: true).Result;
            var iterations = DevOpsClient.GitClient.GetPullRequestIterationsAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, prId, includeCommits: false).Result;

            List<string> pathChanges = DevOpsClient.GitClient.GetPullRequestIterationChangesAsync(DevOpsClient.ApiProjectName,
                                                                            DevOpsClient.ApiRepoName, prId,
                                                                            (int)iterations.Last().Id).Result.ChangeEntries.Select(change => change.Item.Path).ToList();


            List<string> servicePaths = GetRemoteChangePathsAsDistinct(pathChanges);

            // This all services in PR that we can generate IGs
            List<NicService> prServiceList = new();

            foreach (var servicePath in servicePaths)
            {
                var serviceInfo = FileService.GetServiceInfo(servicePath);


                string branchName = pr.Status == PullRequestStatus.Active ? pr.SourceRefName.Replace("refs/heads/", "") : "Development";

                // Get Model File -- START
                NicServiceFile modelFile = null;

                if (!options.ExcludeModels)
                {
                    modelFile = new()
                    {
                        ServiceName = serviceInfo.ServiceName,
                        FilePath = new() { Path = serviceInfo.GetModelPath(), IsRemotePath = true, BranchName = branchName },
                        FileType = NicServiceFileType.Model
                    };

                    var pathContent = DevOpsClient.GitClient.GetItemContentAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, modelFile.FilePath.Path,
                        scopePath: null, includeContent: true, includeContentMetadata: true,
                        versionDescriptor: new GitVersionDescriptor() { Version = branchName, VersionType = GitVersionType.Branch }).Result;

                    using (StreamReader reader = new(pathContent, Encoding.Default, true))
                    {
                        modelFile.Code = reader.ReadToEnd();
                    };
                }
                // Get Model File -- END


                // Get BusinessLayer File -- START
                NicServiceFile blFile = null;

                if (!options.ExcludeBusinessLayer)
                {
                    blFile = new()
                    {
                        ServiceName = serviceInfo.ServiceName,
                        FilePath = new() { Path = serviceInfo.GetBusinessLayerPath(), IsRemotePath = true, BranchName = branchName },
                        FileType = NicServiceFileType.BusinessLayer
                    };

                    var pathContent = DevOpsClient.GitClient.GetItemContentAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, blFile.FilePath.Path,
                         scopePath: null, includeContent: true, includeContentMetadata: true,
                         versionDescriptor: new GitVersionDescriptor() { Version = branchName, VersionType = GitVersionType.Branch }).Result;

                    using (StreamReader reader = new(pathContent, Encoding.Default, true))
                    {
                        blFile.Code = reader.ReadToEnd();
                    };
                }
                // Get BusinessLayer File -- END


                // Get Controller File -- START
                NicServiceFile controllerFile = null;

                if (!options.ExcludeController)
                {
                    controllerFile = new()
                    {
                        ServiceName = $"{serviceInfo.ControllerName}Controller",
                        FilePath = new() { Path = serviceInfo.GetControllerPath(), IsRemotePath = true, BranchName = branchName },
                        FileType = NicServiceFileType.Controller
                    };

                    var pathContent = DevOpsClient.GitClient.GetItemContentAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, controllerFile.FilePath.Path,
                        scopePath: null, includeContent: true, includeContentMetadata: true,
                        versionDescriptor: new GitVersionDescriptor() { Version = branchName, VersionType = GitVersionType.Branch }).Result;

                    using (StreamReader reader = new(pathContent, Encoding.Default, true))
                    {
                        controllerFile.Code = reader.ReadToEnd();
                    };
                }
                // Get Controller File -- END


                prServiceList.Add(new NicService
                {
                    Namespace = serviceInfo.Namespace,
                    ContractCode = serviceInfo.ContractCode,
                    ControllerName = serviceInfo.ControllerName,
                    ServiceName = serviceInfo.ServiceName,
                    BusinessLayer = blFile,
                    Controller = controllerFile,
                    Model = modelFile
                });
            }

            return prServiceList;

        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "An unexpected error occurred in PreparePullRequestForIG");
            throw;
        }
    }

    public static List<string> GetPullRequestChangesPaths(int prId, bool includeController = false)
    {
        var iterations = DevOpsClient.GitClient.GetPullRequestIterationsAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, prId, includeCommits: false).Result;

        List<string> pathChanges = DevOpsClient.GitClient.GetPullRequestIterationChangesAsync(DevOpsClient.ApiProjectName,
                                                                        DevOpsClient.ApiRepoName, prId,
                                                                        (int)iterations.Last().Id).Result.ChangeEntries.Select(change => change.Item.Path).ToList();

        return GetRemoteChangePathsAsDistinct(pathChanges, includeController);
    }

    #endregion

    #region Private Methods

    private static List<string> GetRemoteChangePathsAsDistinct(List<string> pathChanges, bool includeController = false)
    {
        List<string> servicePaths = new();

        // Loop any path contains BusinessLayer or Models
        foreach (var path in pathChanges.Where(p => p.Contains("/BusinessLayer") || p.Contains("/Models") || (includeController && p.EndsWith("Controller.cs"))).ToList())
        {
            var serviceInfo = FileService.GetServiceInfo(path);

            if (Consts.ExcludedContracts.Contains(serviceInfo.Namespace))
                continue;

            // Check if path is Businlayer, if yes check if we already add it to servicePaths as models path. Since we only need one path for each service and so on
            // We we did this? since some time the PR only contains one file changed (BusinessLayer, or Model only).

            if (path.Contains("/BusinessLayer"))
            {
                if (servicePaths.Exists(p => p.Contains($"{serviceInfo.Namespace}/Models/{serviceInfo.ControllerName}/{serviceInfo.ServiceName}")))
                    continue;

                servicePaths.Add(path);
            }
            else if (path.Contains("/Models"))
            {
                if (servicePaths.Exists(p => p.Contains($"{serviceInfo.Namespace}/BusinessLayer/{serviceInfo.ControllerName}/{serviceInfo.ServiceName}")))
                    continue;

                servicePaths.Add(path);
            }
            else if (path.EndsWith("Controller.cs"))
            {
                servicePaths.Add(path);
            }
        }

        return servicePaths;
    }


    #endregion
}
