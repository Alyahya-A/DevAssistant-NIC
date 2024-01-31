using Dev.Assistant.Business.Core.Models;
using Dev.Assistant.Business.Core.Utilities;
using Dev.Assistant.Configuration;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

//using Microsoft.TeamFoundation.Client;
//using Microsoft.TeamFoundation.VersionControl.Client;

// Error Code Start 7400

namespace Dev.Assistant.App.MyWork;

public partial class MyWorkHome : UserControl
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

    public MyWorkHome(AppHome appHome)
    {
        InitializeComponent();

        //PatCredentials credentials = new("", _pat);
        //_connection = new(new Uri(_collectionUri), credentials);

        _appHome = appHome;
        _defaultCriteriaLabel = "Enter the period when tasks state changed (Default 15 days):";

        // Get User Settings
        UsernameInput.Text = Consts.UserSettings.NicHqUsername;

        if (string.IsNullOrWhiteSpace(UsernameInput.Text.Replace(@"NICHQ\", "")))
            ActiveControl = UsernameInput;
        else
            ActiveControl = CriteriaInput;

        int searchCriteria = Consts.UserSettings.MyWorkSearchCriteria;

        if (searchCriteria == 1)
        {
            ByLastActivityRadBtn.Checked = true;
            ByCRNumRadBtn.Checked = false;
            ByTitleRadBtn.Checked = false;
            ReviewedByMeRadBtn.Checked = false;
        }
        else if (searchCriteria == 2)
        {
            ByLastActivityRadBtn.Checked = false;
            ByCRNumRadBtn.Checked = true;
            ByTitleRadBtn.Checked = false;
            ReviewedByMeRadBtn.Checked = false;
        }
        else if (searchCriteria == 3)
        {
            ByLastActivityRadBtn.Checked = false;
            ByCRNumRadBtn.Checked = false;
            ByTitleRadBtn.Checked = true;
            ReviewedByMeRadBtn.Checked = false;
        }
        else if (searchCriteria == 4)
        {
            ByLastActivityRadBtn.Checked = false;
            ByCRNumRadBtn.Checked = false;
            ByTitleRadBtn.Checked = false;
            ReviewedByMeRadBtn.Checked = true;
        }

        ExcludeActiveRadBtn.Checked = Consts.UserSettings.ExcludeActive;
        GetDevNameRadBtn.Checked = Consts.UserSettings.GetDevName;
        CheckIgInAttachmentsRadBtn.Checked = Consts.UserSettings.CheckIgInAttachments;
        CopiedRadBtn.Checked = Consts.UserSettings.Copied;
        ExcludeUnderDevRadBtn.Checked = Consts.UserSettings.ExcludeUnderDev;
    }

    private void CheckBtn_Click(object sender, EventArgs e)
    {
        Event ev = GetCurrentEvent();

        _appHome.LogEvent(ev, EventStatus.Clicked);

        if (_isLoading)
            return;

        Clear();

        try
        {
            if (string.IsNullOrWhiteSpace(UsernameInput.Text) || UsernameInput.Text.Equals(_prefixText))
            {
                if (ByLastActivityRadBtn.Checked)
                    _appHome.LogInfo("Search by recent activitites for all API's CRs");

                if (ReviewedByMeRadBtn.Checked)
                {
                    string errMesg = "Please enter your username";

                    UsernameErrMeg.Text = errMesg;
                    _appHome.LogError(errMesg, ev);

                    return;
                }
            }

            if (Consts.UserSettings.NicHqUsername != UsernameInput.Text)
            {
                Consts.UserSettings.NicHqUsername = UsernameInput.Text;
            }

            if (string.IsNullOrWhiteSpace(CriteriaInput.Text) && !ByTitleRadBtn.Checked)
            {
                if (ByLastActivityRadBtn.Checked || ReviewedByMeRadBtn.Checked)
                    _appHome.LogInfo("Search by recent activitites during the last 15 days.");
                else
                {
                    string errMesg = "Please enter CR number(s)";

                    CriteriaInputErrMeg.Text = errMesg;
                    _appHome.LogError(errMesg, ev);

                    return;
                }
            }
            else if (string.IsNullOrWhiteSpace(CriteriaInput.Text) && ByTitleRadBtn.Checked)
            {
                string errMesg = "Please enter a text";

                CriteriaInputErrMeg.Text = errMesg;
                _appHome.LogError(errMesg + " to search like contract name", ev);

                return;
            }

            if (CriteriaInput.Text.Contains(','))
            {
                if (!ByCRNumRadBtn.Checked)
                {
                    _appHome.LogError("Use comma only if you want to search by CR numbers.", ev);
                    return;
                }

                if (CriteriaInput.Text.Trim().EndsWith(","))
                    CriteriaInput.Text = CriteriaInput.Text[..^1];

                var values = CriteriaInput.Text.Split(',');

                foreach (var value in values)
                {
                    if (int.TryParse(value.Trim(), out int intValue))
                    {
                        criteriaValue.Add(intValue);
                    }
                    else
                    {
                        _appHome.LogError($"{value} is not a number. Please enter numeric values only", ev);
                        return;
                    }
                }
            }
            else
            {
                if (!ByTitleRadBtn.Checked)
                {
                    if (int.TryParse(CriteriaInput.Text.Trim(), out int intValue))
                    {
                        if ((ByLastActivityRadBtn.Checked || ReviewedByMeRadBtn.Checked) && intValue > 190)
                        {
                            string errMesg = "The period must be less than 190 days";

                            CriteriaInputErrMeg.Text = errMesg;
                            _appHome.LogError(errMesg, ev);

                            return;
                        }

                        criteriaValue.Add(intValue);
                    }
                    else
                    {
                        // if ByCRNumRadBtn is empty it will not Parsed to int. but still we want to use our Default value
                        if ((ReviewedByMeRadBtn.Checked || ByLastActivityRadBtn.Checked) && string.IsNullOrWhiteSpace(CriteriaInput.Text))
                        {
                            criteriaValue.Add(15);
                        }
                        else
                        {
                            string errMesg = $"{CriteriaInput.Text} is not a number. Please enter numeric values only";

                            CriteriaInputErrMeg.Text = errMesg;
                            _appHome.LogError(errMesg, ev);
                            return;
                        }
                    }
                }
            }

            if (criteriaValue.Count == 0 && !ByTitleRadBtn.Checked)
            {
                _appHome.LogError("Something wrong!! criteriaValue count is 0", ev);
                return;
            }

            _isLoading = true;

            CheckBtn.Text = "Checking....";

            CheckCrRules();

            _appHome.LogEvent(ev, EventStatus.Succeed);
        }
        catch (DevAssistantException ex)
        {
            _appHome.LogError(ex, ev, EventStatus.Failed);
        }
        catch (Exception ex)
        {
            _appHome.LogError(new DevAssistantException(ex.Message, 7403));
            _appHome.LogEvent(ev, EventStatus.Failed, ex: ex);
        }

        CheckBtn.Text = "Check";

        _isLoading = false;
    }

    private Event GetCurrentEvent()
    {
        if (ByLastActivityRadBtn.Checked)
            return DevEvents.MWByLastActivity;
        else if (ByCRNumRadBtn.Checked)
            return DevEvents.MWByCRNum;
        else if (ByTitleRadBtn.Checked)
            return DevEvents.MWByTitle;
        else if (ReviewedByMeRadBtn.Checked)
            return DevEvents.MWReviewedByMe;

        return null;
    }

    public static async void GetProjects()
    {
        try
        {
            var personalaccesstoken = "PAT_FROM_WEBSITE";

            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(
                    Encoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", "", personalaccesstoken))));

            using HttpResponseMessage response = await client.GetAsync(
                        "https://dev.azure.com/{organization}/_apis/projects");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private void GetPRThreads()
    {
        // Get a GitHttpClient to talk to the Git endpoints
        var gitClient = DevOpsClient.QueryConnection.GetClient<GitHttpClient>();

        // Get data about a specific repository
        //var repo = gitClient.GetRepositoryAsync(DevOpsClient.ProjectName, DevOpsClient.RepoName).Result;
        //var reposa = gitClient.GetCommentsAsync(repo.Id, 2596, 1).Result;

        var aa = DevOpsClient.QueryConnection.AuthorizedIdentity.DisplayName;
        var sa = DevOpsClient.QueryConnection.AuthorizedIdentity;

        List<string> reviewerNames = new();

        //GitPullRequestSearchCriteria criteria = new GitPullRequestSearchCriteria()
        //{
        //    ReviewerId = ,
        //};

        //gitClient.GetPullRequestsAsync()
        var a = gitClient.GetPullRequestReviewersAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, 2596).Result;
        var b = gitClient.GetPullRequestReviewerAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, 2596, "37142674-ec09-4723-90fd-5a3db1c3faaf").Result;

        TeamHttpClient teamClient = DevOpsClient.QueryConnection.GetClient<TeamHttpClient>();
        //IEnumerable<IdentityRef> teamMembers = teamClient.GetTeam(DevOpsClient.ProjectName, "37142674-ec09-4723-90fd-5a3db1c3faaf").Result;
        //var team = teamClient.GetTeamMembersWithExtendedPropertiesAsync(DevOpsClient.ProjectName, "37142674-ec09-4723-90fd-5a3db1c3faaf").Result;

        // 1- Get PR's reviewer name (Display name)
        GitPullRequest pullRequest = gitClient.GetPullRequestAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, 2596, includeCommits: true, includeWorkItemRefs: true).Result;

        int reviewersCount = pullRequest?.Reviewers.Length ?? 0;

        if (reviewersCount is 1)
            reviewerNames.Add(pullRequest.Reviewers[0].DisplayName);

        //                                                        || the only reviewer in this PR is the default group Reviewers
        if (reviewersCount is 0 || (pullRequest.IsDraft ?? false) || (reviewerNames.Any() && reviewerNames[0].Equals("[API]\\PR Reviewers")))
        {
            throw new DevAssistantException($"The PR#{pullRequest.PullRequestId} - {pullRequest.Title} has not been joined by any reviewer.", 7400);
        }

        reviewerNames.AddRange(pullRequest.Reviewers.Where(r => !r.DisplayName.Equals("[API]\\PR Reviewers") && r.HasDeclined == false).Select(r => r.DisplayName));

        if (!reviewerNames.Any())
            throw new DevAssistantException($"The PR#{pullRequest.PullRequestId} - {pullRequest.Title} has not been joined by any reviewer.", 7401);

        // 2- Get all threads for the PR
        var prThreads = gitClient.GetThreadsAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, 2596).Result.Where(t => !t.IsDeleted).ToList();

        // 3- Get all thread that created by the reviewer. and the ConmentType must be Text
        var reviwerComments = prThreads.SelectMany(t => t.Comments.Where(c => !c.IsDeleted && reviewerNames.Contains(c.Author.DisplayName) && c.CommentType == CommentType.Text)).ToList();
    }

    private void CheckCrRules()
    {
        try
        {
            _appHome.LogTrace("Connecting to Azure...");

            // Create instance of WorkItemTrackingHttpClient using VssConnection
            WorkItemTrackingHttpClient witClient = DevOpsClient.QueryConnection.GetClient<WorkItemTrackingHttpClient>();

            IEnumerable<int> crIds;
            List<WorkItemReference> relatedTasks;

            /** Get CR Number(s) -- START **/

            // Get prpare the query
            Wiql wiql = PrepareWiql(ByCRNumRadBtn.Checked ? criteriaValue : null);

            _appHome.LogInfo("Executing Wiql (query) to get CR number(s), wait....");
            WorkItemQueryResult result = witClient.QueryByWiqlAsync(wiql).Result;

            // since we use "Work items and direct links" it will return the CRs and the Tasks
            // Cr1 then releated task, then Cr2 then releated task.
            // the CR's Rel value will be null. so we need items that have Rel with null to get CRs only
            crIds = result.WorkItemRelations?.Where(item => string.IsNullOrWhiteSpace(item.Rel))?.Select(item => item.Target.Id);

            // To get dev task
            if (crIds.Count() > 0 && ReviewedByMeRadBtn.Checked && GetDevNameRadBtn.Checked)
            {
                wiql = PrepareWiql(crIds);
                result = witClient.QueryByWiqlAsync(wiql).Result;
            }

            crIds = result.WorkItemRelations?.Where(item => string.IsNullOrWhiteSpace(item.Rel))?.Select(item => item.Target.Id);

            // Get the related tasks,,, from our query each task is come after each CR (CR > Task, CR > Task)
            relatedTasks = result.WorkItemRelations?.Where(item => !string.IsNullOrWhiteSpace(item.Rel))?.Select(item => item.Target).ToList();

            /** Get CR Number(s) -- END **/

            CustomWorkItem customWorkItem;

            bool testDataFound;
            bool igLinkFound;
            bool isCmTabEmpty;

            if (crIds.Any())
            {
                _appHome.LogInfo($"{crIds.Count()} work item(s) were returned from query");
                _appHome.LogInfo($"Fetching CRs details. Please wait....");

                int skip = 0;
                const int batchSize = 100;
                int counter = 0;
                IEnumerable<int> currentWorkItems;

                do
                {
                    _appHome.LogTrace($"Skip {skip} work item(s)");

                    currentWorkItems = crIds.Skip(skip).Take(batchSize);

                    if (currentWorkItems.Any())
                    {
                        _appHome.LogTrace($"Get {currentWorkItems.Count()} work item(s) details once...");

                        // 330177
                        // get details for each work item in the batch
                        List<WorkItem> workItems = witClient.GetWorkItemsAsync(currentWorkItems, expand: WorkItemExpand.All).Result;

                        foreach (WorkItem workItem in workItems)
                        {
                            testDataFound = false;
                            igLinkFound = false;
                            isCmTabEmpty = false;

                            _appHome.LogTrace("");

                            _appHome.LogTrace($"{workItem.Id} - {workItem.Fields["System.Title"]} -- START");

                            if (workItem.Fields["System.WorkItemType"].ToString() != "Change Request")
                            {
                                _appHome.LogError("This work item is not a Change Request (CR). So, continue..");
                                continue;
                            }

                            string crTitle = workItem.Fields["System.Title"].ToString();

                            string contractName = string.Empty;
                            int index = crTitle.IndexOf(":");

                            if (index != -1)
                                contractName = crTitle[..index].Trim();

                            _appHome.LogTrace($"Contract name is {contractName}");

                            if (workItem.Fields.ContainsKey("custom.ejb"))
                            {
                                if (string.IsNullOrWhiteSpace(workItem.Fields["custom.ejb"].ToString()))
                                    isCmTabEmpty = true;
                                else
                                    isCmTabEmpty = false;
                            }
                            else
                            {// no key for custom.ejb
                                isCmTabEmpty = true;
                            }

                            customWorkItem = new()
                            {
                                ID = workItem.Id ?? 0,
                                Title = workItem.Fields["System.Title"].ToString(),
                                ContractName = contractName,
                                Type = workItem.Fields["System.WorkItemType"].ToString(),
                                CrState = workItem.Fields["System.State"].ToString(),
                                IsCmTabEmpty = isCmTabEmpty
                            };

                            // Get Developer Name -- START
                            if (GetDevNameRadBtn.Checked && relatedTasks != null)
                            {
                                WorkItem devTask = witClient.GetWorkItemAsync(relatedTasks[counter].Id).Result;

                                IdentityRef assignedTo = (IdentityRef)devTask.Fields["System.AssignedTo"];
                                customWorkItem.DeveloperName = assignedTo.DisplayName;

                                customWorkItem.TaskState = (string)devTask.Fields["System.State"];

                                counter++;
                            }
                            // Get Developer Name -- END

                            if (workItem.Relations is null)
                            {
                                _appHome.LogError("Relations is null. So no attachments or links found");

                                continue;
                            }

                            foreach (WorkItemRelation itemRelation in workItem.Relations)
                            {
                                if (itemRelation.Rel == "AttachedFile")
                                {
                                    foreach (var item in itemRelation.Attributes)
                                    {
                                        if (item.Key == "name")
                                        {
                                            if (_testDateRegex.IsMatch(item.Value.ToString()))
                                            {
                                                testDataFound = true;
                                                break;
                                            }

                                            if (CheckIgInAttachmentsRadBtn.Checked)
                                            {
                                                if (_igRegex.IsMatch(item.Value.ToString()))
                                                {
                                                    igLinkFound = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }

                                if (itemRelation.Rel == "ArtifactLink" && itemRelation.Url.Contains("Changeset"))
                                {
                                    foreach (var item in itemRelation.Attributes)
                                    {
                                        if (item.Key == "comment")
                                        {
                                            var comment = Regex.Replace(item.Value.ToString(), @"\s", "").ToLower();

                                            if (comment.Contains("integration") || comment.Contains("guide"))
                                            {
                                                igLinkFound = true;
                                                break;
                                            }
                                        }
                                    }
                                }

                                if (testDataFound && igLinkFound)
                                    break;
                            }

                            customWorkItem.IsTestDataAttached = testDataFound;
                            customWorkItem.IsIGLinked = igLinkFound;

                            _myWorkItems.Add(customWorkItem);

                            _appHome.LogTrace($"{workItem.Id} - {workItem.Fields["System.Title"]} -- END");
                        }
                    }

                    skip += batchSize;
                }
                while (currentWorkItems.Count() == batchSize);

                ShowResult();
            }
            else
            {
                _appHome.LogError("No work items were returned from query.");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private Wiql PrepareWiql(IEnumerable<int> ids = null)
    {
        /*
         * I will send ids only with ReviewedByMy checked..
         * because first we will call first Query to get CRs that reviewed by the Username.
         * then we will call it again with the rerurned CRs IDs. to get Development task.
         */

        StringBuilder sb = new();

        sb.AppendLine(" SELECT [System.Id], [System.State] FROM workitemLinks ");
        sb.AppendLine(" WHERE ");

        // Condition 1
        sb.AppendLine(" ( ");
        sb.AppendLine(" [Source].[System.TeamProject] = 'API' ");
        sb.AppendLine(" AND [Source].[System.WorkItemType] = 'Change Request' ");

        // 1- if not by title get all CRs (this case has its own validation bello)
        // 2- if by ByCRNumRadBtn then don't enter if-body to get all CRs
        //if (!ByTitleRadBtn.Checked && !ByCRNumRadBtn.Checked)
        if (!ByTitleRadBtn.Checked)
        {
            sb.AppendLine(" AND ( ");
            sb.AppendLine(" [Source].[System.State] = 'Completed in DEV' ");
            sb.AppendLine(" OR [Source].[System.State] = 'Send to CM' ");
            sb.AppendLine(" OR [Source].[System.State] = 'Installed in INT' ");

            if (!ExcludeUnderDevRadBtn.Checked)
            {
                sb.AppendLine(" OR [Source].[System.State] = 'Under Development' ");
                sb.AppendLine(" OR [Source].[System.State] = 'Developer Assigned' ");
            }

            sb.AppendLine(" ) ");
        }

        if (ids is not null)
            sb.AppendLine($" AND [Source].[System.Id] IN ({string.Join(", ", ids)}) ");

        if (ByTitleRadBtn.Checked)
        {
            sb.AppendLine($" AND [Source].[System.Title] CONTAINS '{CriteriaInput.Text}' ");

            // if ture then get only completed crs
            if (ExcludeUnderDevRadBtn.Checked)
            {
                sb.AppendLine(" AND ( ");
                sb.AppendLine(" [Source].[System.State] = 'Completed in DEV' ");
                sb.AppendLine(" OR [Source].[System.State] = 'Send to CM' ");
                sb.AppendLine(" OR [Source].[System.State] = 'Installed in INT' ");
                sb.AppendLine(" ) ");
            }
        }

        sb.AppendLine(" ) ");

        // Condition 2 -- START
        sb.AppendLine(" AND ( ");

        sb.AppendLine(" [Target].[System.TeamProject] = 'API' ");

        if (!ByTitleRadBtn.Checked && ids is null)
            sb.AppendLine($" AND [Target].[Microsoft.VSTS.Common.StateChangeDate] > @startOfDay('-{criteriaValue[0]}d') ");

        if (ReviewedByMeRadBtn.Checked && ids is null)
        {
            sb.AppendLine(" AND [Target].[System.WorkItemType] = 'Task' ");
            sb.AppendLine(" AND [Target].[System.Title] CONTAINS 'Perform Code Review' ");
            sb.AppendLine($" AND[Target].[System.AssignedTo] = '{UsernameInput.Text}'");
        }
        else
        {
            sb.AppendLine(" AND [Target].[System.WorkItemType] = 'Task' ");
            sb.AppendLine(" AND [Target].[System.Title] CONTAINS 'Develop' ");

            //                               && if it's equal to _prefixText then don't add AssignedTo to get all Api crs
            if (ByLastActivityRadBtn.Checked && !UsernameInput.Text.Equals(_prefixText))
                sb.AppendLine($" AND[Target].[System.AssignedTo] = '{UsernameInput.Text}'");
        }

        if (ExcludeActiveRadBtn.Checked)
            sb.AppendLine(" AND [Target].[System.State] <> 'Active' ");

        sb.AppendLine(" AND [Target].[System.State] <> 'Proposed' ");
        sb.AppendLine(" AND [Target].[System.State] <> 'On Hold' ");

        sb.AppendLine(" ) ");
        // Condition 2 -- END

        sb.AppendLine(" ORDER BY [System.State], [System.Id]");
        sb.AppendLine(" MODE (MustContain) ");

        return new Wiql
        {
            Query = sb.ToString()
        };
    }

    private void ShowResult()
    {
        if (_myWorkItems.Count == 0)
        {
            _appHome.LogInfo("No CR(s) to show!");
            return;
        }

        if (CopiedRadBtn.Checked)
        {
            _report = new();
            _report.Columns.Add("", "CR#");
            _report.Columns.Add("", "Title");
            _report.Columns.Add("", "CR State");

            if (GetDevNameRadBtn.Checked)
            {
                _report.Columns.Add("", "Developer Name");
                _report.Columns.Add("", "Task State");
            }

            _report.Columns.Add("", "IG");
            _report.Columns.Add("", "TD");
            _report.Columns.Add("", "CM");

            _report.RowHeadersVisible = false;
        }

        StringBuilder sb = new();
        int count = 1;

        foreach (var item in _myWorkItems.OrderBy(item => item.DeveloperName))
        {
            if (CopiedRadBtn.Checked)
            {
                if (!MissingRequirementsOnlyRadBtn.Checked || (!item.IsIGLinked || !item.IsTestDataAttached || item.IsCmTabEmpty))
                {
                    if (GetDevNameRadBtn.Checked)
                    {
                        _report.Rows.Add(item.ID, item.Title, item.CrState, item.DeveloperName, item.TaskState, item.IsIGLinked ? "✔" : "❌", item.IsTestDataAttached ? "✔" : "❌", !item.IsCmTabEmpty ? "✔" : "❌");
                    }
                    else
                    {
                        _report.Rows.Add(item.ID, item.Title, item.CrState, item.IsIGLinked ? "✔" : "❌", item.IsTestDataAttached ? "✔" : "❌", !item.IsCmTabEmpty ? "✔" : "❌");
                    }
                }
            }

            sb.AppendLine($"{count}) {item.ID} - {item.Title} - {item.CrState}:");

            if (GetDevNameRadBtn.Checked)
                sb.AppendLine($"By: {item.DeveloperName} - {item.TaskState}");

            if (item.IsIGLinked && item.IsTestDataAttached && !item.IsCmTabEmpty)
            {
                sb.AppendLine("     Great job 👍, everything is fine for this CR");
            }
            else
            {
                if (CheckIgInAttachmentsRadBtn.Checked)
                    sb.Append($"   {(item.IsIGLinked ? "✔" : "❌")} - [IG] An attachment for the Integration Guide");
                else
                    sb.Append($"   {(item.IsIGLinked ? "✔" : "❌")} - [IG] A link for the Changeset with comment \"Integration Guide\"");

                if (!item.IsIGLinked)
                    sb.AppendLine(" ➡ Not found");
                else
                    sb.AppendLine(" ➡ Found");

                sb.Append($"   {(item.IsTestDataAttached ? "✔" : "❌")} - [TD] An attachment for the Test Data");

                if (!item.IsTestDataAttached)
                    sb.AppendLine(" ➡ Not found");
                else
                    sb.AppendLine(" ➡ Found");

                sb.Append($"   {(!item.IsCmTabEmpty ? "✔" : "❌")} - [CM] The CM tab");

                if (item.IsCmTabEmpty)
                    sb.AppendLine(" ➡ Doesn't contain any text");
                else
                    sb.AppendLine(" ➡ Contain a text");
            }

            sb.AppendLine();

            count++;
        }

        if (CopiedRadBtn.Checked)
        {
            _report.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            _report.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            _report.AutoResizeColumns();
            _report.SelectAll();

            DataObject dataObject = _report.GetClipboardContent();

            Clipboard.SetDataObject(dataObject);

            _appHome.LogSuccess("Checked successfully! and copied to your clipboard. Paste it into Excel file then check your CR(s)");

            _report.Dispose();
        }
        else
        {
            _appHome.LogSuccess("Checked successfully!");
        }

        ResultBox.Text = sb.ToString();
    }

    private void Clear()
    {
        CriteriaInputErrMeg.Text = string.Empty;
        UsernameErrMeg.Text = string.Empty;
        NumServicesMsg.Text = string.Empty;
        ResultBox.Text = string.Empty;

        criteriaValue.Clear();
        _myWorkItems.Clear();
    }

    private void UsernameInput_TextChanged(object sender, EventArgs e)
    {
        if (UsernameInput.Text.Contains(' '))
        {
            UsernameInput.Text = UsernameInput.Text.Replace(" ", "");
            UsernameInput.SelectionStart = UsernameInput.Text.Length;

            _appHome.LogError("Space not allowed!");
        }

        if (!UsernameInput.Text.StartsWith(_prefixText))
        {
            UsernameInput.Text = _prefixText;
            UsernameInput.SelectionStart = UsernameInput.Text.Length;
        }

        //if (UsernameInput.Text.Equals(_prefixText))
        //{
        //    GetDevNameRadBtn.Enabled = true;
        //}
        //else
        //{
        //    GetDevNameRadBtn.Enabled = false;
        //}
        //else if (UsernnameInput.Text.Equals($"{prefixText}Twk", StringComparison.InvariantCultureIgnoreCase))
        //{
        //    UsernnameInput.Text = $"{prefixText}EstishrafBeResponsible";
        //    UsernnameInput.SelectionStart = UsernnameInput.Text.Length;

        //    appHome.LogError("The production username for Twk contract is ws_EstishrafBeResponsible");
        //}
    }

    private void ByLastActivityRadBtn_CheckedChanged(object sender, EventArgs e)
    {
        if (ByLastActivityRadBtn.Checked)
        {
            EnterContractNameLabel.Text = "Enter your username (if empty, it will serach for all API's CRs):";
            EnterCriteriaLabel.Text = _defaultCriteriaLabel;

            UsernameInput.Visible = true;
            EnterContractNameLabel.Visible = true;

            GetDevNameRadBtn.Enabled = true;
            ExcludeUnderDevRadBtn.Enabled = true;

            Consts.UserSettings.MyWorkSearchCriteria = 1;
        }
    }

    private void ByTitleRadBtn_CheckedChanged(object sender, EventArgs e)
    {
        ClearErrMesg();

        if (ByTitleRadBtn.Checked)
        {
            EnterCriteriaLabel.Text = "Enter title (ex: Absher or Rest):";

            UsernameInput.Visible = false;
            EnterContractNameLabel.Visible = false;

            GetDevNameRadBtn.Enabled = true;
            ExcludeUnderDevRadBtn.Enabled = true;

            ExcludeUnderDevRadBtn.Text = "Get completed CRs only";

            Consts.UserSettings.MyWorkSearchCriteria = 3;
        }
        else
        {
            ExcludeUnderDevRadBtn.Text = "Exclude \"Under Development\" CRs";
        }
    }

    private void ByCRNumRadBtn_CheckedChanged(object sender, EventArgs e)
    {
        ClearErrMesg();

        if (ByCRNumRadBtn.Checked)
        {
            EnterCriteriaLabel.Text = "Enter CR number (for more than 1 CR separate CRs with comma):";

            UsernameInput.Visible = false;
            EnterContractNameLabel.Visible = false;

            Consts.UserSettings.MyWorkSearchCriteria = 2;
        }
    }

    private void ReviewedByMeRadBtn_CheckedChanged(object sender, EventArgs e)
    {
        ClearErrMesg();

        if (ReviewedByMeRadBtn.Checked)
        {
            EnterContractNameLabel.Text = "Enter your username:";
            EnterCriteriaLabel.Text = _defaultCriteriaLabel;

            UsernameInput.Visible = true;
            EnterContractNameLabel.Visible = true;

            GetDevNameRadBtn.Enabled = true;

            ExcludeUnderDevRadBtn.Enabled = true;

            Consts.UserSettings.MyWorkSearchCriteria = 4;
        }
    }

    private void GetDevNameRadBtn_CheckedChanged(object sender, EventArgs e)
    {
        if (GetDevNameRadBtn.Checked)
        {
            Consts.UserSettings.GetDevName = true;
        }
        else
        {
            Consts.UserSettings.GetDevName = false;
        }
    }

    private void CheckIgInAttachmentsRadBtn_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckIgInAttachmentsRadBtn.Checked)
        {
            Consts.UserSettings.CheckIgInAttachments = true;
        }
        else
        {
            Consts.UserSettings.CheckIgInAttachments = false;
        }
    }

    private void CopiedRadBtn_CheckedChanged(object sender, EventArgs e)
    {
        if (CopiedRadBtn.Checked)
        {
            MissingRequirementsOnlyRadBtn.Visible = true;

            NoteLabel.Text = "The output also will be Copied to your Clipboard";
            Consts.UserSettings.Copied = true;
        }
        else
        {
            MissingRequirementsOnlyRadBtn.Visible = false;

            NoteLabel.Text = string.Empty;
            Consts.UserSettings.Copied = false;
        }
    }

    private void ExcludeUnderDevRadBtn_CheckedChanged(object sender, EventArgs e)
    {
        if (ExcludeUnderDevRadBtn.Checked)
        {
            _appHome.LogInfo($"If [{ExcludeUnderDevRadBtn.Text}] option checked, it will return only \"Completed in DEV\", \"Send to CM\" and \"Installed in INT\"");

            ExcludeActiveRadBtn.Checked = true;
            ExcludeActiveRadBtn.Enabled = false;

            Consts.UserSettings.ExcludeUnderDev = true;
        }
        else
        {
            ExcludeActiveRadBtn.Enabled = true;

            Consts.UserSettings.ExcludeUnderDev = false;
        }
    }

    private void ExcludeActiveRadBtn_CheckedChanged(object sender, EventArgs e)
    {
        if (ExcludeActiveRadBtn.Checked)
        {
            Consts.UserSettings.ExcludeActive = true;
        }
        else
        {
            Consts.UserSettings.ExcludeActive = false;
        }
    }

    private void ClearErrMesg()
    {
        UsernameErrMeg.Text = string.Empty;
        CriteriaInputErrMeg.Text = string.Empty;
    }
}