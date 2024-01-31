using Dev.Assistant.Business.Core.Enums;
using Dev.Assistant.Business.Core.Models;
using Dev.Assistant.Dashboard.Extensions;
using Dev.Assistant.Dashboard.LogEvents;

namespace Dev.Assistant.Dashboard;

public partial class AppHome : Form
{
    private DevApp _selectedApp;

    private LogEvents.LogEventsHome _adminDashboard;

    public AppHome()
    {
        InitializeComponent();

        IsMdiContainer = true;

        // Defind Apps -- START
        var appItems = new List<AppItem>
        {
            new AppItem { Value = 1, Text = "LogEvents", AppType = DevApp.LogEvents },
            //new AppItem { Value = 2, Text = "LogErrors", AppType = Apps.LogErrors },
        };

        AppsComboBox.DataSource = appItems;
        AppsComboBox.DisplayMember = "Text";
        AppsComboBox.ValueMember = "Value";
        // Defind Apps -- END

        OpenAdminDashboardApp();
        AppsComboBox.Text = "Dashboard";

        //if (_readerApp is null && _adminDashboard is null && _utilitiesForms is null)
        //{
        //    ShowButtons();
        //}

        toolTip1.SetToolTip(StartupAppLabel, "Select startup application for the next time the app is run");
        toolTip1.SetToolTip(AppsComboBox, "Select startup application for the next time the app is run");
        toolTip1.InitialDelay = 50;

        ActiveControl = null;
    }

    private void AddToPanel(UserControl uc)
    {
        uc.Dock = DockStyle.Fill;

        AppScreen.Controls.Add(uc);

        uc.BringToFront();
    }

    private void ReviewmeAppMenuItem_Click(object sender, EventArgs e) => OpenAdminDashboardApp();

    private void OpenAdminDashboardApp()
    {
        if (_selectedApp == DevApp.LogEvents)
        {
            return;
        }

        _selectedApp = DevApp.LogEvents;

        if (_adminDashboard is null)
        {
            _adminDashboard = new LogEventsHome(this);

            AddToPanel(_adminDashboard);
        }

        HideFormsExcept(typeof(LogEventsHome).Name);
        _adminDashboard.Show();
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedApp = (AppItem)AppsComboBox.SelectedItem;

        switch (selectedApp.Value)
        {
            case 1:
                OpenAdminDashboardApp();
                break;

            default:
                break;
        }
    }

    private void HideFormsExcept(string formName)
    {
        ActiveControl = null;

        if (formName == "LogEventsHome")
        {
        }
    }

    #region Log

    public void LogInfo(string log)
    {
        LoggerText.AppendText($"{DateTime.Now:hh:mm:ss tt}: {log}{Environment.NewLine}", Color.Gray);
    }

    public void LogSuccess(string log)
    {
        LoggerText.AppendText($"{DateTime.Now:hh:mm:ss tt}: {log}{Environment.NewLine}", Color.Green);
    }

    public void LogWarning(string log)
    {
        LoggerText.AppendText($"{DateTime.Now:hh:mm:ss tt}: {log}{Environment.NewLine}", Color.DarkGoldenrod);
    }

    public void LogError(string log, Event ev = null, EventStatus status = EventStatus.InputError)
    {
        LoggerText.AppendText($"{DateTime.Now:hh:mm:ss tt}: {log}{Environment.NewLine}", Color.Red);
    }

    public void LogError(DevAssistantException ex, Event ev = null, EventStatus status = EventStatus.InputError)
    {
        LoggerText.AppendText($"{DateTime.Now:hh:mm:ss tt}: [{ex.Code}] {ex.Message}{Environment.NewLine}", Color.Red);
    }

    public void LogNote(string log)
    {
        LoggerText.AppendText($"{DateTime.Now:hh:mm:ss tt}: {log}{Environment.NewLine}", Color.MidnightBlue);
    }

    private void AppHome_Load(object sender, EventArgs e)
    {
    }

    private void ClearLoggerBtn_Click(object sender, EventArgs e)
    {
        LoggerText.Clear();
    }

    #endregion Log

}