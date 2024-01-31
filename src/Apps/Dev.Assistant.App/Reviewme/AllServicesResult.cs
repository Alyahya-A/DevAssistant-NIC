using Dev.Assistant.Business.Decoder.Models;

namespace Dev.Assistant.App.Reviewme;

public partial class AllServicesResult : Form
{
    private readonly List<FileInfoModel> _services;

    public AllServicesResult(List<FileInfoModel> services)
    {
        InitializeComponent();

        _services = services;

        NumSerivces.ReadOnly = true;
        NumSerivces.BorderStyle = 0;
        NumSerivces.BackColor = this.BackColor;
        NumSerivces.TabStop = false;

        ShowResult();

        Notes.SelectionBullet = true;
        Notes.SelectedText = "\"-\" Refer to the previous object's props\n";
        Notes.SelectedText = "Don't forgot! Click here for more information";
        Notes.Hide();
    }

    private void ShowResult()
    {
        ResultGridView.Columns.Add("", "Service Name");
        ResultGridView.Columns.Add("", "Contract Code");
        ResultGridView.Columns.Add("", "Controller");

        int propsCount = 0;

        string serviceName = string.Empty;

        List<string> resAdded = new();

        foreach (var model in _services)
        {
            ResultGridView.Rows.Add(model.Name, model.ContractCode, model.Controller);

            propsCount += 1;
        }

        var fontSize = 9;

        DataGridViewCellStyle style = new();
        Font font = new("Objectivity Light", fontSize, FontStyle.Bold);
        style.Font = font;

        ResultGridView.Columns[0].DefaultCellStyle = style;

        font = new Font("Objectivity Light", fontSize);
        style.Font = font;

        font = new Font("HelveticaNeueLT Arabic 45 Light", fontSize);
        style.Font = font;
        ResultGridView.Columns[1].DefaultCellStyle = style;

        ResultGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
        NumSerivces.Text = propsCount.ToString();
    }

    private void GetOutput_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void CopyExcel_Click(object sender, EventArgs e)
    {
        PasteLabel.Text = "Copying...";
        Notes.Hide();

        string label = "Paste the output to Excel file";

        //to remove the first blank column from datagridview
        ResultGridView.RowHeadersVisible = false;
        ResultGridView.ColumnHeadersVisible = false;
        ResultGridView.SelectAll();
        DataObject dataObj = ResultGridView.GetClipboardContent();
        if (dataObj != null)
            Clipboard.SetDataObject(dataObj);

        ResultGridView.ClearSelection();
        ResultGridView.RowHeadersVisible = true;

        PasteLabel.Text = label;
    }
}