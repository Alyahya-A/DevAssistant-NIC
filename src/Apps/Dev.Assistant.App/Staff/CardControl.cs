using Dev.Assistant.Business.Core.Models;

namespace Dev.Assistant.App.Staff;

public partial class CardControl : UserControl
{
    public EmployeeInfo EmployeePhoneExt { get; set; }

    public event EventHandler OnClicked;

    public CardControl()
    {
        InitializeComponent();
    }

    public CardControl(EmployeeInfo viewModel)
    {
        EmployeePhoneExt = viewModel;
        InitializeComponent();
    }

    public void DataBind()
    {
        SuspendLayout();

        panel1.BorderStyle = BorderStyle.FixedSingle;

        EmployeeNameLabel.Text = EmployeePhoneExt.FullName;
        EmployeeDeptNameLabel.Text = EmployeePhoneExt.CurrentDeptName;
        EmployeeExtLabel.Text = EmployeePhoneExt.PhoneExt;
        EmployeeEmailLabel.Text = EmployeePhoneExt.Email;

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