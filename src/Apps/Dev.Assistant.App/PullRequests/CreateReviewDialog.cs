namespace Dev.Assistant.App.PullRequests;

public partial class CreateReviewDialog : Form
{
    private string _html;

    public CreateReviewDialog(string html)
    {
        InitializeComponent();

        DialogResult = DialogResult.Cancel;
        _html = html;

        webBrowser.DocumentText = _html;
    }

    private void TipLabel_Click(object sender, System.EventArgs e)
    {
        //MicroPathTxt.Text = @"C:\Project\MicroServices\Core\Development";
    }

    private void CancelBtn_Click(object sender, EventArgs e)
    {
        Close(DialogResult.Cancel);
    }

    private void ContinueBtn_Click(object sender, EventArgs e)
    {
        Close(DialogResult.Continue);
    }

    private void CreateBtn_Click(object sender, EventArgs e)
    {
        Close(DialogResult.OK);
    }

    private void Close(DialogResult dialogResult)
    {
        DialogResult = dialogResult;
        Close();
    }
}