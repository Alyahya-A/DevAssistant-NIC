using Dev.Assistant.App.Reviewme;
using Dev.Assistant.App.Services;
using Dev.Assistant.Business.Core.Enums;
using Dev.Assistant.Business.Core.Models;
using Dev.Assistant.Business.Core.Services;
using Dev.Assistant.Business.Core.Utilities;
using Dev.Assistant.Business.Decoder.Models;
using Dev.Assistant.Business.Decoder.Services;
using Dev.Assistant.Business.DevOps.Services;
using Dev.Assistant.Business.Generator.Services;
using Dev.Assistant.Configuration;
using Markdig;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using Serilog;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;

// Error Code Start 7400

namespace Dev.Assistant.App.PullRequests;

public partial class PullRequestsHome : UserControl
{
    private readonly AppHome _appHome;
    private readonly PdsTableService _listTables;
    private readonly string workItemBaseUrl = $"{Consts.TfsUrl}/c34cd60c-7c56-49f0-a4c3-4671ba0d2012/_apis/wit/workItems/";
    private readonly PdsTableProcessingService _tableProcessingService;
    private readonly LogArgs _log;
    private readonly string loadingLabel = "Loading...";
    private readonly Regex _testDateRegex = new("[a-zA-Z0-9]+_[a-zA-Z0-9]+_V[0-9]+\\.txt", RegexOptions.IgnoreCase);
    private readonly Regex _igRegex = new("[a-zA-Z0-9]+_[a-zA-Z0-9]+_V[0-9]+\\.docx", RegexOptions.IgnoreCase);

    private List<PullRequestInfo> _pullRequests;
    private PullRequestInfo _selectedPullRequest;

    private int _preSelectedIndex = -1;
    private bool _isLoading;
    private bool _isCreateLoading;
    private bool _firstPaint;



    public PullRequestsHome(AppHome appHome)
    {
        InitializeComponent();

        _appHome = appHome;

        _firstPaint = true;

        _isLoading = true;
        _isCreateLoading = false;
        GenerateIGBtn.Visible = false;
        CheckRulesRadBtn.Visible = false;
        GetPdsTablesBtn.Visible = false;

        _log = new()
        {
            LogInfo = appHome.LogInfo,
            LogEvent = appHome.LogEvent,
            LogWarning = appHome.LogWarning,
            LogSuccess = appHome.LogSuccess,
            LogError = appHome.LogError,
            LogTrace = appHome.LogTrace
        };

        _listTables = new PdsTableService(_log);

        _tableProcessingService = new PdsTableProcessingService();
    }

    private void PrReviewHome_Load(object sender, EventArgs e)
    {
        if (!_firstPaint) return;

        _firstPaint = false;

        LoadPullRequestsToUI(true);
    }

    private void LoadPullRequestsToUI(bool firstRun = false)
    {
        _appHome.LogInfo("Getting all active Pull Requests that created by you or assigned (joined as reviewer) to you....");

        Clear();

        _isLoading = true;

        // TODO: get all PRs.
        _pullRequests = GetPullRequests();

        if (firstRun)
        {
            // must be first. before assign the data
            cardsPanel1.OnClicked += CardsPanel1_OnClicked;

            ObservableCollection<PullRequestInfo> cards = new();

            foreach (var pr in _pullRequests)
            {
                cards.Add(pr);
            }

            CardsViewModel cardsView = new()
            {
                Cards = cards
            };

            cardsPanel1.ViewModel = cardsView;
        }
        else
        {
            cardsPanel1.ViewModel.Cards.Clear();

            foreach (var pr in _pullRequests)
            {
                cardsPanel1.ViewModel.Cards.Add(pr);
            }
        }

        cardsPanel1.DataBind();

        if (_pullRequests.Count > 0)
        {
            if (_pullRequests.Count == 1)
                _appHome.LogInfo("1 PR found");
            else
                _appHome.LogInfo($"{_pullRequests.Count} PRs found");
        }
        else
        {
            _appHome.LogInfo("No pull requests found");
        }

        _isLoading = false;
    }

    private void Clear()
    {
        PrTitleLabel.Text = string.Empty;
        PrInfoLabel.Text = string.Empty;
        TodoCheckListBox.Items.Clear();
        CrInfoTxt.Text = string.Empty;

        _selectedPullRequest = null;
        _preSelectedIndex = -1;
        _pullRequests?.Clear();

        _isCreateLoading = false;
        GenerateIGBtn.Visible = false;
        CheckRulesRadBtn.Visible = false;
        GetPdsTablesBtn.Visible = false;

        //cardsPanel1.ViewModel?.Cards?.Clear();
        //cardsPanel1.ViewModel = null;
        //cardsPanel1.Controls.Clear();
    }

    private void CardsPanel1_OnClicked(object sender, EventArgs e)
    {
        try
        {
            var card = (CardControl)sender;

            if (_preSelectedIndex == card.TabIndex)
                return;

            PrTitleLabel.Text = "Loading..";

            if (_preSelectedIndex > -1)
            {
                var preCard = (CardControl)cardsPanel1.Controls[_preSelectedIndex];
                preCard.SetToFixedSingle();
            }

            _preSelectedIndex = card.TabIndex;


            GenerateIGBtn.Visible = true;
            CheckRulesRadBtn.Visible = true;
            GetPdsTablesBtn.Visible = true;


            //card.BackColor = Color.Red;
            card.SetToFixed3D();

            _selectedPullRequest = card.PullRequest;

            PrInfoLabel.Text = $"!{_selectedPullRequest.Id} - {_selectedPullRequest.DeveloperName} {_selectedPullRequest.SourceRefName} into {_selectedPullRequest.TargetRefName}";

            TodoCheckListBox.Items.Clear();

            // It will be null for first time 
            if (_selectedPullRequest.ReviewerComments is null)
                GetReviewerComments(_selectedPullRequest);

            foreach (var todo in _selectedPullRequest.ReviewerComments)
            {
                TodoCheckListBox.Items.Add(todo, true);
            }

            // It will be null for first time 
            if (string.IsNullOrWhiteSpace(_selectedPullRequest.Remarks))
            {
                if (int.TryParse(_selectedPullRequest.SourceRefName.Replace("CR/", ""), out int crNumber))
                {
                    _selectedPullRequest.Remarks = GetPullRequestRemarks(crNumber);
                }
                else
                {
                    _selectedPullRequest.Remarks = string.Empty;

                    _appHome.LogError($"Coudn't get the CR number form the branch name {_selectedPullRequest.SourceRefName}");
                }
            }

            CrInfoTxt.Text = _selectedPullRequest.Remarks;

            return;
        }
        finally
        {
            PrTitleLabel.Text = _selectedPullRequest.Title;
        }

    }

    private string GetPullRequestRemarks(int crNumber)
    {
        WorkItem workItem = DevOpsClient.TrackingClient.GetWorkItemAsync(DevOpsClient.ApiRepoName, crNumber, expand: WorkItemExpand.All).Result;

        bool testDataFound = false;
        bool igLinkFound = false;
        bool isCmTabEmpty = false;

        _appHome.LogTrace($"{workItem.Id} - {workItem.Fields["System.Title"]} -- START");

        if (workItem.Fields["System.WorkItemType"].ToString() != "Change Request")
        {
            _appHome.LogError("This work item is not a Change Request (CR)!");
            return string.Empty;
        }

        string crTitle = workItem.Fields["System.Title"].ToString();
        string crState = workItem.Fields["System.State"].ToString();

        if (workItem.Fields.ContainsKey("custom.ejb"))
        {
            if (string.IsNullOrWhiteSpace(workItem.Fields["custom.ejb"].ToString()))
                isCmTabEmpty = true;
            else
                isCmTabEmpty = false;
        }
        else // no key for custom.ejb
        {
            isCmTabEmpty = true;
        }

        if (workItem.Relations is null)
        {
            _appHome.LogError($"No relations found for CR#{crNumber}. So no attachments or links found");

            return string.Empty;
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

        StringBuilder sb = new();

        sb.AppendLine($"CR#{crNumber} - {crTitle} - {crState}:");



        if (igLinkFound && testDataFound && !isCmTabEmpty)
        {
            sb.AppendLine("     Great job 👍, everything is fine for this CR");
        }
        else
        {

            sb.Append($"   {(igLinkFound ? "✔" : "❌")} - [IG] A link for the Changeset with comment \"Integration Guide\"");

            if (!igLinkFound)
                sb.AppendLine(" ➡ Not found");
            else
                sb.AppendLine(" ➡ Found");

            sb.Append($"   {(testDataFound ? "✔" : "❌")} - [TD] An attachment for the Test Data");

            if (!testDataFound)
                sb.AppendLine(" ➡ Not found");
            else
                sb.AppendLine(" ➡ Found");

            sb.Append($"   {(!isCmTabEmpty ? "✔" : "❌")} - [CM] The CM tab");

            if (isCmTabEmpty)
                sb.AppendLine(" ➡ Doesn't contain any text");
            else
                sb.AppendLine(" ➡ Contain a text");
        }

        sb.AppendLine();

        return sb.ToString();
    }

    private void CheckBtn_Click(object sender, EventArgs e)
    {
        // TODO: create new event.
        Event ev = DevEvents.Testing;

        _appHome.LogEvent(ev, EventStatus.Clicked);

        if (_isLoading)
            return;

        try
        {
            _isLoading = true;

            //CheckCrRules();

            GetPullRequests();

            _appHome.LogEvent(ev, EventStatus.Succeed);
        }
        catch (DevAssistantException ex)
        {
            _appHome.LogError(ex);
        }
        catch (Exception ex)
        {
            _appHome.LogError(new DevAssistantException(ex.Message, 7404));
            _appHome.LogEvent(ev, EventStatus.Failed, ex: ex);
        }

        _isLoading = false;
    }

    private List<PullRequestInfo> GetPullRequests()
    {
        List<PullRequestInfo> pullRequests = new();

        Event ev = DevEvents.PrGetPullRequests;

        _appHome.LogEvent(ev, EventStatus.Clicked);

        try
        {
            WelcomLabel.Text = $"Welcome {DevOpsClient.QueryConnection.AuthorizedIdentity.DisplayName},";

            var activePullRequests = DevOpsClient.GitClient.GetPullRequestsAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, new()
            {
                ReviewerId = DevOpsClient.QueryConnection.AuthorizedIdentity.Id,
                Status = PullRequestStatus.Active //| PullRequestStatus.Completed
            }).Result;

            activePullRequests.AddRange(DevOpsClient.GitClient.GetPullRequestsAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, new()
            {
                CreatorId = DevOpsClient.QueryConnection.AuthorizedIdentity.Id,
                Status = PullRequestStatus.Active //| PullRequestStatus.Completed
            }).Result);

            // Use DistinctBy to avoid duplicate PRs in case the reviewer was the same as the developer 
            pullRequests = activePullRequests.DistinctBy(pr => pr.PullRequestId).Select(p => new PullRequestInfo
            {
                Id = p.PullRequestId,
                Title = p.Title,
                Status = p.Status,
                CreatedAt = p.CreationDate,

                SourceRefName = p.SourceRefName.Replace("refs/heads/", ""),
                TargetRefName = p.TargetRefName.Replace("refs/heads/", ""),

                DeveloperId = p.CreatedBy.Id,
                DeveloperName = p.CreatedBy.DisplayName,
                DeveloperUniqueName = p.CreatedBy.UniqueName,

                //                      Since in API PRs, the first record is always the 'PR Reviewers' group.
                Reviewers = p.Reviewers.Where(r => !r.DisplayName.Contains("PR Reviewers")).OrderByDescending(r => r.Vote).Select(r => new ReviewerInfo
                {
                    Id = r.Id,
                    DisplayName = r.DisplayName,
                    UniqueName = r.UniqueName
                }).ToList()

            }).ToList();


            _appHome.LogEvent(ev, EventStatus.Succeed, $"{pullRequests.Count} PR(s) found.");
        }
        catch (Exception ex)
        {
            _appHome.LogError(new DevAssistantException(ex.Message, 7402));
            _appHome.LogEvent(ev, EventStatus.Failed, ex: ex);
        }

        return pullRequests;
    }

    private static void GetReviewerComments(PullRequestInfo pr)
    {
        // Get all threads for the PR
        var prThreads = DevOpsClient.GitClient.GetThreadsAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, pr.Id).Result.Where(t => !t.IsDeleted).ToList();
        //var workItemRefs = gitClient.GetPullRequestWorkItemRefsAsync(DevOpsClient.ProjectName, DevOpsClient.RepoName, pr.Id).Result;

        // Get all thread that created by the reviewer. and the ConmentType must be Text
        var reviwerComments = prThreads?.SelectMany(t => t.Comments).ToList();

        pr.TotalComments = reviwerComments.Count;

        if (reviwerComments.Count > 0)
        {
            reviwerComments = reviwerComments.Where(c => !c.IsDeleted && c.CommentType == CommentType.Text && !c.Content.StartsWith("Submitted conflict")).ToList();
            pr.ReviewerComments = reviwerComments.Select(c => c.Content.Replace("\n", "\r\n")).ToList();
        }
        else
        {
            pr.ReviewerComments = new();
        }
    }

    private void GetPRThreads()
    {
        Event ev = DevEvents.Testing;
        _appHome.LogEvent(ev, EventStatus.Clicked);

        try
        {
            List<string> reviewerNames = new();

            // 1- Get PR's reviewer name (Display name)
            GitPullRequest pullRequest = DevOpsClient.GitClient.GetPullRequestAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, 2596, includeCommits: true, includeWorkItemRefs: true).Result;

            int reviewersCount = pullRequest?.Reviewers.Length ?? 0;

            if (reviewersCount is 1)
                reviewerNames.Add(pullRequest.Reviewers[0].DisplayName);

            //                                                        || the only reviewer in this PR is the default group Reviewers
            if (reviewersCount is 0 || (pullRequest.IsDraft ?? false) || (reviewerNames.Any() && reviewerNames[0].Equals("[API]\\PR Reviewers")))
            {
                throw new DevAssistantException($"The PR#{pullRequest.PullRequestId} - {pullRequest.Title} has not been joined by any reviewer.", 7407);
            }

            reviewerNames.AddRange(pullRequest.Reviewers.Where(r => !r.DisplayName.Equals("[API]\\PR Reviewers") && r.HasDeclined == false).Select(r => r.DisplayName));

            if (!reviewerNames.Any())
                throw new DevAssistantException($"The PR#{pullRequest.PullRequestId} - {pullRequest.Title} has not been joined by any reviewer.", 7408);

            // 2- Get all threads for the PR
            var prThreads = DevOpsClient.GitClient.GetThreadsAsync(DevOpsClient.ApiProjectName, DevOpsClient.ApiRepoName, 2596).Result.Where(t => !t.IsDeleted).ToList();

            // 3- Get all thread that created by the reviewer. and the ConmentType must be Text
            var reviwerComments = prThreads.SelectMany(t => t.Comments.Where(c => !c.IsDeleted && reviewerNames.Contains(c.Author.DisplayName) && c.CommentType == CommentType.Text)).ToList();
        }
        catch (DevAssistantException ex)
        {
            _appHome.LogError(ex);
        }
        catch (Exception ex)
        {
            _appHome.LogError(new DevAssistantException(ex.Message, 7405));
            _appHome.LogEvent(ev, EventStatus.Failed, ex: ex);
        }
    }

    private void GenerateIGBtn_Click(object sender, EventArgs e)
    {

        Event ev = DevEvents.PrGenerateIGFromTfs;
        _appHome.LogEvent(ev, EventStatus.Clicked);

        // Get orginal label
        string tempLabel = GenerateIGBtn.Text;

        // change it to "Loading..."
        GenerateIGBtn.Text = loadingLabel;

        try
        {
            double igVersion = 0;

            using (var dialog = new EnterIgVersionDialog(_selectedPullRequest.DeveloperName))
            {
                var result = dialog.ShowDialog();


                if (result == DialogResult.OK)
                {
                    igVersion = dialog.IgVersion;
                }
                else
                {
                    return;
                }
            }

            // 8951
            List<NicService> services = PullRequestService.PreparePullRequest(_selectedPullRequest.Id);

            if (services.Count == 0)
            {
                throw new DevAssistantException($"No services found for PR#{_selectedPullRequest.Id}", 7406);
            }

            if (services.Count > 1)
            {
                var dialogInfo = new DevDialogInfo
                {
                    Title = $"Mulitple services found:",
                    Message = $"We found {services.Count} services. Do you want to continue and generate all servcies?",
                    Buttons = MessageBoxButtons.YesNo,
                    BoxIcon = MessageBoxIcon.Question
                };

                DialogResult result = DevDialog.Show(dialogInfo);

                if (result == DialogResult.No)
                {
                    _appHome.LogError($"Stop generating IGs...");

                    return;
                }
            }

            CheckSpellingDialog checkSpelling = new();

            IntegrationGuideService.GenerateApiIG(new()
            {
                Services = services,
                ProjectType = ProjectType.RestApi,
                IgVersion = igVersion,
                AddBracket = false,
                GenerateMultipleIGs = services.Count > 1,
                WrittenBy = _selectedPullRequest.DeveloperName,
            },
            new()
            {
                LogInfo = _appHome.LogInfo,
                LogEvent = _appHome.LogEvent,
                LogWarning = _appHome.LogWarning,
                LogSuccess = _appHome.LogSuccess,
                LogError = _appHome.LogError,
            },
            checkSpelling);

            checkSpelling.Dispose();

            _appHome.LogEvent(ev, EventStatus.Succeed);
        }
        catch (DevAssistantException ex)
        {
            _appHome.LogError(ex, ev, EventStatus.Failed);
        }
        catch (Exception ex)
        {
            _appHome.LogError(new DevAssistantException(ex.Message, 7409));
            _appHome.LogEvent(ev, EventStatus.Failed, ex: ex);
        }
        finally
        {
            GenerateIGBtn.Text = tempLabel;
        }

    }

    private void splitContainer1_SizeChanged(object sender, EventArgs e)
    {
        //_appHome.LogInfo(Size.ToString());
    }

    private void UpdatePRsBtin_Click(object sender, EventArgs e)
    {
        UpdatePRsBtin.Text = "Loading..";

        if (_preSelectedIndex > -1)
        {
            var preCard = (CardControl)cardsPanel1.Controls[_preSelectedIndex];
            preCard.SetToFixedSingle();
        }

        LoadPullRequestsToUI();

        UpdatePRsBtin.Text = "Refresh";
    }

    private void cardsPanel1_Paint(object sender, PaintEventArgs e)
    {
        //if (!_firstPaint) return;

        //_firstPaint = false;

        //Thread.Sleep(5000);
        //LoadPullRequests(true);
    }

    private void PreviewBtn_Click(object sender, EventArgs e)
    {
        if (_isCreateLoading)
            return;

        //WorkItemTrackingHttpClient c = DevOpsClient.QueryConnection.GetClient<WorkItemTrackingHttpClient>();

        //var workItems = c.GetWorkItemAsync(290498, expand: WorkItemExpand.All).Result;

        //string json = JsonConvert.SerializeObject(workItems, Formatting.Indented);
        //File.WriteAllText(@"C:\Users\aayahya\Documents\review1.json", json);

        //return;

        _isCreateLoading = true;

        // Get orginal label
        string tempLabel = GenerateIGBtn.Text;

        // change it to "Loading..."
        GenerateIGBtn.Text = "Creating...";

        try
        {
            Event ev = DevEvents.PrCreateTaskReview;

            _appHome.LogEvent(ev, EventStatus.Clicked);

            if (_selectedPullRequest == null)
            {
                _appHome.LogError("No pull request selected", ev, EventStatus.Failed);

                return;
            }

            var crNum = _selectedPullRequest.Title[(_selectedPullRequest.Title.IndexOf('#') + 1)..];

            var isNum = crNum.All(char.IsDigit);

            if (!isNum)
            {
                _appHome.LogError("Couldn't get the CR number form PR's title", ev, EventStatus.Failed);

                return;
            }

            string htmlText = string.Empty;

            foreach (var item in TodoCheckListBox.CheckedItems)
            {
                htmlText += Markdown.ToHtml($"- {item.ToString().Trim()}");
                htmlText += Markdown.ToHtml("\n");
            }

            htmlText += @$"<p><a href=""{Consts.TfsUrl}/c34cd60c-7c56-49f0-a4c3-4671ba0d2012/_git/04bf0ff8-5c45-4f09-a18e-bd3fb1fa86be/pullrequest/{_selectedPullRequest.Id}?_a=overview"">For more details please go to PR#{_selectedPullRequest.Id}: {_selectedPullRequest.Title}</a>&nbsp;<br></p>";

            using var dialog = new CreateReviewDialog(htmlText);
            var result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                JsonPatchDocument patchDocument = new();

                patchDocument.Add(
                    new JsonPatchOperation()
                    {
                        Operation = Operation.Add,
                        Path = "/fields/System.Title",
                        //Value = $"Test-Review"
                        Value = $"Review: CR#{crNum}"
                    }
                );

                patchDocument.Add(
                    new JsonPatchOperation()
                    {
                        Operation = Operation.Add,
                        Path = "/fields/Microsoft.VSTS.CMMI.Purpose",
                        Value = htmlText
                    }
                );

                patchDocument.Add(
                   new JsonPatchOperation()
                   {
                       Operation = Operation.Add,
                       Path = "/fields/Microsoft.VSTS.CMMI.Comments",
                       Value = $"DA v{Consts.AppVersion}"
                   }
                );

                patchDocument.Add(
                    new JsonPatchOperation()
                    {
                        Operation = Operation.Add,
                        Path = "/fields/System.AssignedTo",
                        Value = _selectedPullRequest.DeveloperUniqueName
                    }
                );

                // Link all workitems to new Review task from workitems that linked with PR
                foreach (var itemRef in _selectedPullRequest.WorkItemRefs)
                {
                    patchDocument.Add(
                        new JsonPatchOperation()
                        {
                            Operation = Operation.Add,
                            Path = "/relations/-",
                            Value = new
                            {
                                rel = "System.LinkTypes.Related",
                                url = $"{workItemBaseUrl}{itemRef}"
                            }
                        }
                    );
                }

                // Link to PullRequest to new Review task
                patchDocument.Add(
                    new JsonPatchOperation()
                    {
                        Operation = Operation.Add,
                        Path = "/relations/-",
                        Value = new
                        {
                            rel = "ArtifactLink",
                            url = _selectedPullRequest.ArtifactId,
                            attributes = new
                            {
                                comment = _selectedPullRequest.Title,
                                name = "Pull Request"
                            }
                        }
                    }
                );

                // Get a client
                WorkItemTrackingHttpClient witClient = DevOpsClient.QueryConnection.GetClient<WorkItemTrackingHttpClient>();

                //WorkItem newWorkItem = witClient.UpdateWorkItemAsync(patchDocument, 398500).Result;

                // Create the new work item
                WorkItem newWorkItem = witClient.CreateWorkItemAsync(patchDocument, DevOpsClient.ApiProjectName, "Review").Result;

                _appHome.LogSuccess($"Reveiw task was created successfully! Task #{newWorkItem.Id} {newWorkItem.Fields["System.Title"]}");
                _appHome.LogEvent(ev, EventStatus.Succeed, $"Reveiw task was created successfully! Task ID #{newWorkItem.Id}");
            }
            else
            {
                _appHome.LogError("Create review task was canceled successfully!", ev, EventStatus.Failed);
            }
        }
        finally
        {
            _isCreateLoading = false;
            GenerateIGBtn.Text = tempLabel;
        }
    }

    private void PrContentContainer_Panel2_SizeChanged(object sender, EventArgs e)
    {
        var panel = (SplitterPanel)sender;

        if (panel.Size.Width < 250)
        {
            panel.Padding = new Padding(0, 17, 0, 17);
        }
        if (panel.Size.Width >= 250)
        {
            panel.Padding = new Padding(100, 17, 100, 17);
        }
        if (panel.Size.Width > 500)
        {
            panel.Padding = new Padding(150, 17, 150, 17);
        }
        if (panel.Size.Width > 600)
        {
            panel.Padding = new Padding(175, 17, 175, 17);
        }
        if (panel.Size.Width > 700)
        {
            panel.Padding = new Padding(200, 17, 200, 17);
        }
        if (panel.Size.Width > 800)
        {
            panel.Padding = new Padding(225, 17, 225, 17);
        }
        if (panel.Size.Width > 900)
        {
            panel.Padding = new Padding(250, 17, 250, 17);
        }
        if (panel.Size.Width > 1100)
        {
            panel.Padding = new Padding(550, 30, 550, 30);
        }
    }

    private void CheckRulesRadBtn_Click(object sender, EventArgs e)
    {
        Event ev = DevEvents.PrCheckRules;
        _appHome.LogEvent(ev, EventStatus.Clicked);

        // Get orginal label
        string tempLabel = CheckRulesRadBtn.Text;

        // change it to "Loading..."
        CheckRulesRadBtn.Text = loadingLabel;

        try
        {
            List<string> servicePaths = PullRequestService.GetPullRequestChangesPaths(_selectedPullRequest.Id);

            _appHome.LogInfo($"{servicePaths.Count} model(s) found.");

            LogArgs logArgs = new()
            {
                LogInfo = _appHome.LogInfo,
                LogEvent = _appHome.LogEvent,
                LogWarning = _appHome.LogWarning,
                LogSuccess = _appHome.LogSuccess,
                LogError = _appHome.LogError,
            };


            if (!ValidationService.ValidateServiceLimit(servicePaths.Count, logArgs))
                return;

            foreach (var path in servicePaths)
            {
                string modelPath = path.Replace("/BusinessLayer", "/Models");

                var serviceInfo = FileService.GetServiceInfo(modelPath);

                string code = FileService.GetCodeByFile(new GetCodeByFileReq
                {
                    Code = null,
                    IsRemotePath = true,
                    Path = modelPath,
                    BranchName = _selectedPullRequest.SourceRefName
                });

                List<ClassModel> models = ModelExtractionService.GetClassesByCode(code, new GetClassesOptions
                {
                    CheckSpellingAndRules = true,
                    ShowErrorCode = true,
                    ControllerName = serviceInfo.ControllerName
                });


                DecodeHelperService.CheckSpellingAndRules(new CheckSpellingAndRulesArgs()
                {
                    Models = models,
                    Event = DevEvents.RCheckRules,
                    AlwaysShowDialog = true,
                    Namespace = serviceInfo.Namespace
                }, logArgs, null);
            }

            _appHome.LogEvent(ev, EventStatus.Succeed);
        }
        catch (DevAssistantException ex)
        {
            _appHome.LogError(ex, ev, EventStatus.Failed);
        }
        catch (Exception ex)
        {
            _appHome.LogError(new DevAssistantException(ex.Message, 7410));
            _appHome.LogEvent(ev, EventStatus.Failed, ex: ex);
        }
        finally
        {
            CheckRulesRadBtn.Text = tempLabel;
        }
    }

    private void GetPdsTablesBtn_Click(object sender, EventArgs e)
    {
        Event ev = DevEvents.PrGetPdsTable;
        _appHome.LogEvent(ev, EventStatus.Clicked);

        // Get orginal label
        string tempLabel = GetPdsTablesBtn.Text;

        // change it to "Loading..."
        GetPdsTablesBtn.Text = loadingLabel;

        try
        {
            List<string> servicePaths = PullRequestService.GetPullRequestChangesPaths(_selectedPullRequest.Id).ToList();

            if (servicePaths.Count == 0)
                throw new DevAssistantException($"No services found for PR#{_selectedPullRequest.Id}", 7411);

            //if (servicePaths.Count > 1)
            //    throw new DevAssistantException($"Unable to proceed bacause there are {servicePaths.Count} services found, exceeding the limit. One service allowed", 7412);

            ServiceInfo serviceInfo = FileService.GetServiceInfo(servicePaths.First());

            bool askUserToContinue = true;

            if (servicePaths.Count > 1)
            {
                foreach (var path in servicePaths)
                {
                    var tempServiceInfo = FileService.GetServiceInfo(path);


                    if (!tempServiceInfo.ContractCode.Equals(serviceInfo.ContractCode))
                    {
                        //throw new DevAssistantException($"Unable to proceed bacause there are {servicePaths.Count} services found in different contracts!", 7412);

                        var dialogWarning = new DevDialogInfo
                        {
                            Title = $"Mulitple contracts found:",
                            Message = $"Unable to determine the username! There are {servicePaths.Count} services found in different contracts\n\nDo you want to continue and get all PDS tables for all servcies for ws_{serviceInfo.ContractCode}?",
                            Buttons = MessageBoxButtons.YesNo,
                            BoxIcon = MessageBoxIcon.Warning
                        };

                        DialogResult warningResult = DevDialog.Show(dialogWarning);

                        if (warningResult == DialogResult.No)
                        {
                            _appHome.LogError($"Stop getting PDS tables...");

                            return;
                        }
                        else
                        {
                            askUserToContinue = false;

                            break;
                        }
                    }
                }

                if (askUserToContinue)
                {
                    var dialogInfo = new DevDialogInfo
                    {
                        Title = $"Mulitple services found:",
                        Message = $"We found {servicePaths.Count} services. Do you want to continue and get all PDS tables for all servcies for ws_{serviceInfo.ContractCode}?",
                        Buttons = MessageBoxButtons.YesNo,
                        BoxIcon = MessageBoxIcon.Question
                    };

                    DialogResult result = DevDialog.Show(dialogInfo);

                    if (result == DialogResult.No)
                    {
                        _appHome.LogError($"Stop getting PDS tables...");

                        return;
                    }
                }
            }

            Log.Logger.Information($"Get PDS tables from PR [!{_selectedPullRequest.Id}]");

            List<string> servicesAndPaths = new();
            servicesAndPaths.AddRange(servicePaths);

            foreach (var msName in servicePaths)
            {
                string path = msName.Trim();

                if (path.Contains(@"\Models\"))
                {
                    path = path.Replace(@"\Models\", @"\BusinessLayer\");
                }

                if (path.Contains("\\BusinessLayer") || path.Contains("/BusinessLayer"))
                {
                    //appHome.LogInfo($"The following path is a BusinessLayer file {tempName}");
                    //appHome.LogInfo("Getting Micros names from the file....");

                    var tempMicros = ModelExtractionService.GetMicroNamesFromBL(new() { Path = path, IsRemotePath = true, BranchName = _selectedPullRequest.SourceRefName });

                    if (tempMicros.Count > 0)
                    {
                        _appHome.LogInfo($"{tempMicros.Count} micro(s) or root service(s) found in {path} file");

                        servicesAndPaths.AddRange(tempMicros);
                    }
                    //else
                    //{
                    //    _appHome.LogInfo("No micros found");
                    //}
                }
            }

            var serviceTables = new PdsTableResult
            {
                Name = "MultiServices",
                Tables = new List<string>()
            };

            foreach (var msName in servicesAndPaths)
            {
                string tempName = msName.Trim();

                if (tempName.Contains(@"\Models\"))
                {
                    tempName = tempName.Replace(@"\Models\", @"\BusinessLayer\");
                }

                var service = _listTables.ListTablesService(new() { Path = tempName, IsRemotePath = true, BranchName = _selectedPullRequest.SourceRefName }, new() { IsRemote = true });

                if (string.IsNullOrWhiteSpace(service.Name))
                    continue;

                serviceTables.Tables.Add($"{service.Name} -- START"); // will not copied to Clipboard.

                serviceTables.Tables.AddRange(service.Tables);

                serviceTables.Tables.Add($"{service.Name} -- END"); // will not copied            }

            }

            if (serviceTables.Tables != null && serviceTables.Tables.Count > 0)
            {

                TableProcessingArgs args = new()
                {
                    ServiceInfo = serviceTables,
                    IncludeDisplayIrregular = true,
                    IsMicroProject = false,
                    AddCommentAuth = false,
                    DbName = serviceInfo.ContractCode.Equals("Absher") ? "OLSDB" : "ODSASIS",
                    ContractName = serviceInfo.ContractCode,
                    TableService = _listTables
                };

                _tableProcessingService.ProcessTableNamesResult(args, _log);
            }

            _appHome.LogEvent(ev, EventStatus.Succeed);

        }
        catch (DevAssistantException ex)
        {
            _appHome.LogError(ex, ev, EventStatus.Failed);
        }
        catch (Exception ex)
        {
            _appHome.LogError(new DevAssistantException(ex.Message, 7413));
            _appHome.LogEvent(ev, EventStatus.Failed, ex: ex);
        }
        finally
        {
            GetPdsTablesBtn.Text = tempLabel;
        }
    }
}