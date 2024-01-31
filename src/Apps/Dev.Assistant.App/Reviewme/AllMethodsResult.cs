using Dev.Assistant.Business.Decoder.Models;
using System.Data;

namespace Dev.Assistant.App.Reviewme;

public partial class ResultForm : Form
{
    private readonly WebService _service;

    public ResultForm(WebService service)
    {
        _service = service;
        InitializeComponent();

        NameSpace.ReadOnly = true;
        NameSpace.BorderStyle = 0;
        NameSpace.BackColor = BackColor;
        NameSpace.TabStop = false;

        ServiceName.ReadOnly = true;
        ServiceName.BorderStyle = 0;
        ServiceName.BackColor = this.BackColor;
        ServiceName.TabStop = false;

        FuncNum.ReadOnly = true;
        FuncNum.BorderStyle = 0;
        FuncNum.BackColor = BackColor;
        FuncNum.TabStop = false;

        FileType.ReadOnly = true;
        FileType.BorderStyle = 0;
        FileType.BackColor = this.BackColor;
        FileType.TabStop = false;

        ShowResult();
    }

    private void ShowResult()
    {
        NameSpace.Text = _service.Namespace;
        ServiceName.Text = _service.Name;
        FuncNum.Text = _service.Funcations.Count().ToString() + " Funcation(s)";
        FileType.Text = _service.IsVB ? "Visual Basic" : "C#";

        DataTable t = new();

        t.Columns.Add("FuncationName", typeof(string));
        t.Columns.Add("DBName", typeof(string));
        t.Columns.Add("AccessLevel", typeof(string));
        t.Columns.Add("Configuration", typeof(string));

        foreach (Function func in _service.Funcations)
        {
            t.Rows.Add(func.FunName, func.DBName, func.AccessLevel, func.Configuration);
        }

        ResultGridView.DataSource = t;
    }

    private void GetOutput_Click(object sender, EventArgs e)
    {
        Close();
    }
}