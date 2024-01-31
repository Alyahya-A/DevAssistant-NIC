namespace Dev.Assistant.App.PullRequests;

public partial class EnterIgVersionDialog : Form
{
    private readonly string _writtenBy;

    private const string _prefixText = "V";

    public double IgVersion = 0;

    public EnterIgVersionDialog(string writtenBy)
    {
        InitializeComponent();

        _writtenBy = writtenBy;

        WrittenByLabel.Text = $"The author (Written By) will be {_writtenBy}.";

        DialogResult = DialogResult.Cancel;
    }

    private void SaveBtn_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(IgVersionTxt.Text) || !double.TryParse(IgVersionTxt.Text.Replace(_prefixText, ""), out double igVersion))
        {
            TipLabel.Text = "Invalid value. IG version must be numeric";
            TipLabel.ForeColor = Color.Red;

            return;
        }

        IgVersion = igVersion;

        DialogResult = DialogResult.OK;

        Close();
    }

    private void IgVersionTxt_TextChanged(object sender, EventArgs e)
    {
        if (IgVersionTxt.Text.Contains(' '))
        {
            IgVersionTxt.Text = IgVersionTxt.Text.Replace(" ", "");
            IgVersionTxt.SelectionStart = IgVersionTxt.Text.Length;
        }

        if (!IgVersionTxt.Text.StartsWith(_prefixText))
        {
            IgVersionTxt.Text = _prefixText;
            IgVersionTxt.SelectionStart = IgVersionTxt.Text.Length;
        }
    }

    private void IgVersionTxt_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && e.KeyChar == ' ')
        {
            e.Handled = true;
            TipLabel.Text = "Space not allowed";

            return;
        }
        else if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
        {
            e.Handled = true;

            TipLabel.Text = $"{e.KeyChar} is not a number. Please enter numeric values only";

            return;
        }

        // Only allowed one decimal point
        else if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
        //else if (e.KeyChar == '.')
        {
            e.Handled = true;

            TipLabel.Text = "Only allowed one decimal point";

            return;
        }

        TipLabel.Text = string.Empty;
    }
}