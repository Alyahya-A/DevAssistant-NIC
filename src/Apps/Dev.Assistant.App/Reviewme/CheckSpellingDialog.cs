namespace Dev.Assistant.App.Reviewme;

public partial class CheckSpellingDialog : Form
{
    public CheckSpellingDialog()
    {
        InitializeComponent();

        DialogResult = DialogResult.Cancel;
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
        Close(DialogResult.Retry);
    }

    private void Close(DialogResult dialogResult)
    {
        DialogResult = dialogResult;
        Close();
    }

    private void ResolvedBtn_Click(object sender, EventArgs e)
    {
    }
}