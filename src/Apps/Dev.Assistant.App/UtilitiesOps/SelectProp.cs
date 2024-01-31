using Dev.Assistant.Business.Core.Models;

namespace Dev.Assistant.App.UtilitiesOps;

public partial class SelectProp : Form
{
    private List<Property> _properties;
    public Property selectedProp;

    public SelectProp(List<Property> properties)
    {
        InitializeComponent();

        _properties = properties;

        InputsComboBox.DataSource = properties;
        InputsComboBox.DisplayMember = "Name";
        InputsComboBox.ValueMember = "Name";
    }

    private void SaveBtn_Click(object sender, EventArgs e)
    {
        if (selectedProp is null)
        {
            ErrorLabel.Text = "Select an input from the list";
            return;
        }

        selectedProp = _properties.Where(prop => prop.Name == InputsComboBox.Text).FirstOrDefault();

        if (selectedProp is null)
        {
            ErrorLabel.Text = "Select a valid input";
            return;
        }

        DialogResult = DialogResult.OK;

        Close();
    }

    private void InputsComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedProp = (Property)InputsComboBox.SelectedItem;
    }
}