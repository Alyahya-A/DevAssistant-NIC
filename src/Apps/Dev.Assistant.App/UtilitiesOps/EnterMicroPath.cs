using Dev.Assistant.Business.Core.Services;

namespace Dev.Assistant.App.UtilitiesOps;

public partial class EnterMicroPath : Form
{
    public string microPath;

    public EnterMicroPath()
    {
        InitializeComponent();

        DialogResult = DialogResult.Cancel;
    }

    private void SaveBtn_Click(object sender, System.EventArgs e)
    {
        microPath = MicroPathTxt.Text;

        if (string.IsNullOrWhiteSpace(microPath))
        {
            TipLabel.Text = "Invalid value. Micro path is required";
            TipLabel.ForeColor = Color.Red;

            return;
        }
        else if (!Directory.Exists(microPath))
        {
            TipLabel.Text = "The given path is not existing on disk: " + microPath;
            TipLabel.ForeColor = Color.Red;

            return;
        }

        DialogResult = DialogResult.OK;

        Close();
    }

    private void TipLabel_Click(object sender, System.EventArgs e)
    {
        MicroPathTxt.Text = @"C:\Project\MicroServices\Core\Development";
    }

    private void BrowseBtn_Click(object sender, EventArgs e)
    {
        microPath = FileService.FolderPickerDialog(title: "Select the MicroServices path (Root folder):");

        if (string.IsNullOrWhiteSpace(microPath))
        {
            TipLabel.Text = "Invalid value. Micro path is required";
            TipLabel.ForeColor = Color.Red;

            return;
        }

        TipLabel.ForeColor = Color.Black;

        DialogResult = DialogResult.OK;

        Close();
    }
}