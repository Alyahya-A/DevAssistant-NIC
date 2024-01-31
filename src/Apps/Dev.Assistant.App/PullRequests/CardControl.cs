using Dev.Assistant.Business.Core.Utilities;

namespace Dev.Assistant.App.PullRequests;

public partial class CardControl : UserControl
{
    public PullRequestInfo PullRequest { get; set; }

    public event EventHandler OnClicked;

    public CardControl()
    {
        InitializeComponent();
    }

    public CardControl(PullRequestInfo viewModel)
    {
        PullRequest = viewModel;
        InitializeComponent();
    }

    public void DataBind()
    {
        SuspendLayout();

        panel1.BorderStyle = BorderStyle.FixedSingle;

        TtileLabel.Text = PullRequest.Title;
        DeveloperNameLabel.Text = PullRequest.DeveloperName;

        if (DevOpsClient.QueryConnection.AuthorizedIdentity.Id.ToString().Equals(PullRequest.DeveloperId))
        {
            if (PullRequest.Reviewers.Count == 0)
            {
                CommentsNumLabel.Text = "No reviewers";
            }
            else
            {
                string assignedLabel = "Assigned to ";

                if (PullRequest.Reviewers.Count == 1)
                {
                    // Check if the reviewer is the same as the developer
                    assignedLabel += DevOpsClient.QueryConnection.AuthorizedIdentity.Id.ToString().Equals(PullRequest.Reviewers.First().Id) ? "you" : PullRequest.Reviewers.First().DisplayName;
                }
                else
                {
                    assignedLabel += PullRequest.Reviewers.Exists(r => r.Id == DevOpsClient.QueryConnection.AuthorizedIdentity.Id.ToString())
                        ? $"you and {PullRequest.Reviewers.Count - 1} other"
                        : $"{PullRequest.Reviewers.First().DisplayName} and {PullRequest.Reviewers.Count - 1} other";
                }

                CommentsNumLabel.Text = assignedLabel;
            }
        }
        else if (PullRequest.Reviewers.Exists(r => r.Id == DevOpsClient.QueryConnection.AuthorizedIdentity.Id.ToString()))
        {
            CommentsNumLabel.Text = "Assigned to you";
        }

        CreatedAtLabel.Text = PullRequest.CreatedAt.ToString("dd MMMM yyyy (hh:mm tt)");
        ResumeLayout();
    }

    public void SetToFixed3D()
    {
        panel1.BorderStyle = BorderStyle.Fixed3D;
    }

    public void SetToFixedSingle()
    {
        panel1.BorderStyle = BorderStyle.FixedSingle;
    }

    private void panel1_Click(object sender, EventArgs e)
    {
        //ViewModel.OnClick();
        if (OnClicked != null)
            OnClicked(this, e);
    }
}