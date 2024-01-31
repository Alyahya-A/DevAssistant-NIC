using Dev.Assistant.Business.Core.Extensions;
using Dev.Assistant.Business.Core.Models;
using Dev.Assistant.Business.Core.Utilities;
using Dev.Assistant.Configuration;
using Newtonsoft.Json;
using Serilog;

// Error Code Start 7400

namespace Dev.Assistant.App.Staff;

public partial class StaffHome : UserControl
{
    private readonly AppHome _appHome;

    private List<EmployeeInfo> _emplyInfo;

    private bool _isLoading;
    private bool _IsInit;
    private bool _listAll;
    private bool _checkIfArabicAgain;

    public StaffHome(AppHome appHome)
    {
        _IsInit = true;

        InitializeComponent();

        _appHome = appHome;

        _isLoading = true;
        _checkIfArabicAgain = true;

        // TODO: get from user settings.
        //_listAll =

        _listAll = Consts.UserSettings.PListAllEmp;

        //    0  ;   1  ;   2  ;     3    ;    4    ;      5      ;    6
        // "ByExt;ByName;ByDept;ByUsername;BySection;ByManagerName;ByPosition"
        if (!string.IsNullOrWhiteSpace(Consts.UserSettings.StaffSearchBy))
        {
            if (Consts.UserSettings.StaffSearchBy.Contains(';'))
            {
                var searchBy = Consts.UserSettings.StaffSearchBy.Split(';');

                if (searchBy.Length == 7)
                {
                    if (searchBy[0].Equals("1"))
                        ByExtBox.Checked = true;
                    else
                        ByExtBox.Checked = false;

                    if (searchBy[1].Equals("1"))
                        ByNameBox.Checked = true;
                    else
                        ByNameBox.Checked = false;

                    if (searchBy[2].Equals("1"))
                        ByDeptBox.Checked = true;
                    else
                        ByDeptBox.Checked = false;

                    if (searchBy[3].Equals("1"))
                        ByUsernameBox.Checked = true;
                    else
                        ByUsernameBox.Checked = false;

                    if (searchBy[4].Equals("1"))
                        BySectionBox.Checked = true;
                    else
                        BySectionBox.Checked = false;

                    if (searchBy[5].Equals("1"))
                        ByManagerNameBox.Checked = true;
                    else
                        ByManagerNameBox.Checked = false;

                    if (searchBy[6].Equals("1"))
                        ByPositionBox.Checked = true;
                    else
                        ByPositionBox.Checked = false;
                }
            }
        }

        ListAllOption();

        ListAllTip.SetToolTip(ListAllBtn, "For better performance, please disable this option and display only the top 50 (default). If you display all records, this makes rendering so slow while you searching due the amount of data");
        ListAllTip.InitialDelay = 50;
        ListAllTip.AutoPopDelay = 10000;
    }

    private void PhoneExtsHome_Load(object sender, EventArgs e)
    {
        if (!_IsInit) return;

        _IsInit = false;

        if (!File.Exists(Consts.StaffInfoFileLocalPath))
        {
            Log.Logger.Information("[StaffHome]: Copying the StaffInfoFile {a}", Consts.StaffInfoFileLocalPath);
            _appHome.LogInfo("Copying Staff info file...");

            if (File.Exists(Consts.StaffInfoFileServerPath))
                File.Copy(Consts.StaffInfoFileServerPath, Consts.StaffInfoFileLocalPath);
            else
            {
                Log.Logger.Information("[StaffHome]: Couldn't found staff info file in the server.. {a}", Consts.StaffInfoFileServerPath);

                _appHome.LogError("Couldn't load staff info file...");

                return;
            }
        }

        _emplyInfo = new();

        using (StreamReader file = File.OpenText(Consts.StaffInfoFileLocalPath))
        {
            _emplyInfo = JsonConvert.DeserializeObject<List<EmployeeInfo>>(file.ReadToEnd());
        }

        //List<EmployeeInfoOld> temp = new();

        //using (StreamReader file = File.OpenText(@"C:\Users\aayahya\Documents\PhoneExt\GetEmployeePhoneExt.json"))
        //{
        //    temp = JsonConvert.DeserializeObject<List<EmployeeInfoOld>>(file.ReadToEnd());
        //}

        //int norFound = 0;

        //foreach (var emp in emplyInfo)
        //{
        //    string newName = GetNewName(emp.FullName);

        //    var temEmp = temp.FirstOrDefault(old => old.EmployeeEmail?.Trim().Equals(emp.Email?.Trim(), StringComparison.InvariantCultureIgnoreCase) ?? false);

        //    if (temEmp == null)
        //        temEmp = temp.FirstOrDefault(old => old.EmployeePhone?.Trim().Equals(emp.PhoneExt?.Trim(), StringComparison.InvariantCultureIgnoreCase) ?? false);

        //    if (temEmp == null)
        //        temEmp = temp.FirstOrDefault(old => GetNewName(old.EmployeeFullName)?.Trim().Equals(newName?.Trim(), StringComparison.InvariantCultureIgnoreCase) ?? false);

        //    if (temEmp != null)
        //    {
        //        if (string.IsNullOrWhiteSpace(emp.CurrentDeptName))
        //            emp.CurrentDeptName = temEmp.EmployeeCurrentDeptName?.Trim() ?? "";

        //        if (string.IsNullOrWhiteSpace(emp.PhoneExt))
        //            emp.PhoneExt = temEmp.EmployeePhone?.Trim() ?? "";

        //        if (string.IsNullOrWhiteSpace(emp.Email))
        //            emp.Email = temEmp.EmployeeEmail.Trim() ?? "";
        //    }
        //    else
        //    {
        //        norFound++;
        //    }

        //}

        //string json = JsonConvert.SerializeObject(emplyInfo, Formatting.Indented);

        //File.WriteAllText(@"C:\Users\aayahya\Documents\PhoneExt\EmpInfoUpdated1.json", json);

        // Display first 100
        DisplayEmplPhoneInfo();
    }

    private static string GetNewName(string name)
    {
        return name?.Trim()
                            .Replace("بن", "")
                            .Replace("ابن", "")
                            .Replace("بنت", "")
                            .Replace("أ", "ا")
                            .Replace("عبد ال", "عبدال")
                            .Replace("آ", "ا")
                            .Replace("ابرهيم", "ابراهيم")
                            .Replace("يحي ", "يحيى ")
                            .Replace("ة", "ه")
                            .Replace("-", "")
                            .Replace("احمد محمدعلي مؤمن جان", "احمد محمد علي جان")
                            .RemoveWhiteSpaces(" ");
    }

    private void DisplayEmplPhoneInfo()
    {
        if (_emplyInfo == null)
            return;

        int displayCount = _listAll ? 0 : 50;

        //_appHome.LogInfo("Getting all active Pull Requests that assigned (joined) to you....");

        Clear();

        _isLoading = true;

        List<EmployeeInfo> employeeInfos = _emplyInfo;

        if (!string.IsNullOrWhiteSpace(SearchTextBox.Text))
        {
            foreach (var word in SearchTextBox.Text.RemoveWhiteSpaces(" ").Split(" "))
            {
                employeeInfos = employeeInfos.Where(emp =>
                {
                    if (ByExtBox.Checked)
                        if (!string.IsNullOrWhiteSpace(emp.PhoneExt))
                            if (emp.PhoneExt.Contains(word))
                                return true;

                    if (ByNameBox.Checked)
                        if (!string.IsNullOrWhiteSpace(emp.FullName))
                            if (emp.FullName.Contains(word))
                                return true;

                    if (ByDeptBox.Checked)
                        if (!string.IsNullOrWhiteSpace(emp.CurrentDeptName))
                            if (emp.CurrentDeptName.Contains(word))
                                return true;

                    if (ByUsernameBox.Checked)
                    {
                        if (!string.IsNullOrWhiteSpace(emp.Username))
                            if (emp.Username.Contains(word))
                                return true;

                        if (!string.IsNullOrWhiteSpace(emp.Email))
                            if (emp.Email.Contains(word))
                                return true;
                    }

                    if (BySectionBox.Checked)
                        if (!string.IsNullOrWhiteSpace(emp.OrgSectionName))
                            if (emp.OrgSectionName.Contains(word))
                                return true;

                    if (ByManagerNameBox.Checked)
                        if (!string.IsNullOrWhiteSpace(emp.ManagerName))
                            if (emp.ManagerName.Contains(word))
                                return true;

                    if (ByPositionBox.Checked)
                        if (!string.IsNullOrWhiteSpace(emp.PositionName))
                            if (emp.PositionName.Contains(word))
                                return true;

                    return false;
                }).ToList();
            }
        }

        StaffCountLabel.Text = $"Staff Count: {employeeInfos.Count}";

        if (employeeInfos.Count > 0)
        {
            if (!string.IsNullOrWhiteSpace(SearchTextBox.Text))
                _appHome.LogEvent(DevEvents.SSearching, EventStatus.Succeed);

            ExtsGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            ExtsGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            ExtsGridView.Columns.Add("", "#");
            ExtsGridView.Columns.Add("", "Full Name");
            ExtsGridView.Columns.Add("", "Ext");
            ExtsGridView.Columns.Add("", "Email");
            ExtsGridView.Columns.Add("", "Position Name");
            ExtsGridView.Columns.Add("", "Section Name");
            ExtsGridView.Columns.Add("", "Department Name");
            ExtsGridView.Columns.Add("", "Manager Name");

            int count = 0;
            if (displayCount > 0)
            {
                foreach (var model in employeeInfos.Take(displayCount))
                {
                    ExtsGridView.Rows.Add(count + 1, model.FullName, model.PhoneExt, model.Email, model.PositionName, model.OrgSectionName, model.CurrentDeptName, model.ManagerName);

                    count++;
                }
            }
            else
            {
                foreach (var model in employeeInfos)
                {
                    ExtsGridView.Rows.Add(count + 1, model.FullName, model.PhoneExt, model.Email, model.PositionName, model.OrgSectionName, model.CurrentDeptName, model.ManagerName);

                    count++;
                }
            }

            var fontSize = 10;

            DataGridViewCellStyle style = new()
            {
                Font = new("Calibri", fontSize),
                Alignment = DataGridViewContentAlignment.MiddleRight,
            };

            ExtsGridView.Columns[0].DefaultCellStyle = style;//fullname;

            ExtsGridView.Columns[1].DefaultCellStyle = new() // phone
            {
                Font = new("Calibri", fontSize),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            ExtsGridView.Columns[2].DefaultCellStyle = new() // emall
            {
                Font = new("Calibri", fontSize),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
            };

            ExtsGridView.Columns[3].DefaultCellStyle = style;
            ExtsGridView.Columns[4].DefaultCellStyle = style;
            ExtsGridView.Columns[5].DefaultCellStyle = style;
            ExtsGridView.Columns[6].DefaultCellStyle = style;
        }
        else
        {
            _appHome.LogEvent(DevEvents.SSearching, EventStatus.Failed);
        }

        _isLoading = false;
    }

    private void Clear()
    {
        ExtsGridView.Columns.Clear();
        ExtsGridView.Rows.Clear();

        //EmpCards.ViewModel?.Cards?.Clear();
        //EmpCards.ViewModel = null;
        //EmpCards.Controls.Clear();
    }

    private void splitContainer1_SizeChanged(object sender, EventArgs e)
    {
        //_appHome.LogInfo(Size.ToString());
    }

    private void EmpCards_Paint(object sender, PaintEventArgs e)
    {
        //if (!_firstPaint) return;

        //_firstPaint = false;

        //Thread.Sleep(5000);
        //LoadPullRequests(true);
    }

    private void PrContentContainer_Panel2_SizeChanged(object sender, EventArgs e)
    {
        var panel = (SplitterPanel)sender;

        if (panel.Size.Width < 250)
        {
            panel.Padding = new Padding(0, 17, 0, 17);
        }
        if (panel.Size.Width >= 250)
        {
            panel.Padding = new Padding(100, 17, 100, 17);
        }
        if (panel.Size.Width > 500)
        {
            panel.Padding = new Padding(150, 17, 150, 17);
        }
        if (panel.Size.Width > 600)
        {
            panel.Padding = new Padding(175, 17, 175, 17);
        }
        if (panel.Size.Width > 700)
        {
            panel.Padding = new Padding(200, 17, 200, 17);
        }
        if (panel.Size.Width > 800)
        {
            panel.Padding = new Padding(225, 17, 225, 17);
        }
        if (panel.Size.Width > 900)
        {
            panel.Padding = new Padding(250, 17, 250, 17);
        }
        if (panel.Size.Width > 1100)
        {
            panel.Padding = new Padding(550, 30, 550, 30);
        }
    }

    private void ListAllBtn_Click(object sender, EventArgs e)
    {
        _listAll = !_listAll;

        ListAllOption();

        DisplayEmplPhoneInfo();

        Consts.UserSettings.PListAllEmp = _listAll;
    }

    private void ListAllOption()
    {
        if (_listAll) // then disable
        {
            ListAllBtn.BorderColor = Color.DarkCyan;
            ListAllBtn.BackgroundColor = Color.DarkCyan;
            ListAllBtn.TextColor = Color.White;

            //ListAllBtn.Text = "Display TOP 100 records to UI";
        }
        else
        {
            ListAllBtn.BorderColor = SystemColors.ControlDarkDark;
            ListAllBtn.BackgroundColor = SystemColors.Control;
            ListAllBtn.TextColor = SystemColors.ControlDarkDark;

            //ListAllBtn.Text = "Display all records to UI";
        }
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
        //_fileOptions.Username = SearchTextBox.Text;

        if (string.IsNullOrWhiteSpace(SearchTextBox.Text))
        {
            _checkIfArabicAgain = true;
            SearchTextBox.Text = string.Empty;
        }

        if (_checkIfArabicAgain && !string.IsNullOrWhiteSpace(SearchTextBox.Text))
        {
            if (SearchTextBox.Text.IsArabic())
                SearchTextBox.RightToLeft = RightToLeft.Yes;
            else
                SearchTextBox.RightToLeft = RightToLeft.No;

            _checkIfArabicAgain = false;
        }

        //_appHome.LogEvent(DevEvents.SSearching, EventStatus.Clicked);

        DisplayEmplPhoneInfo();
    }

    private void UpdateStaffSearchBy()
    {
        if (_IsInit)
            return;

        // "ByExt;ByName;ByDept;ByUsername;BySection;ByManagerName;ByPosition"
        string searchBy = string.Empty;

        if (ByExtBox.Checked)
            searchBy += "1;";
        else
            searchBy += "2;";

        if (ByNameBox.Checked)
            searchBy += "1;";
        else
            searchBy += "2;";

        if (ByDeptBox.Checked)
            searchBy += "1;";
        else
            searchBy += "2;";

        if (ByUsernameBox.Checked)
            searchBy += "1;";
        else
            searchBy += "2;";

        if (BySectionBox.Checked)
            searchBy += "1;";
        else
            searchBy += "2;";

        if (ByManagerNameBox.Checked)
            searchBy += "1;";
        else
            searchBy += "2;";

        if (ByPositionBox.Checked)
            searchBy += "1";
        else
            searchBy += "2";

        DisplayEmplPhoneInfo();

        Consts.UserSettings.StaffSearchBy = searchBy;
    }

    private void ByUsernameBox_CheckedChanged(object sender, EventArgs e) => UpdateStaffSearchBy();

    private void ByNameBox_CheckedChanged(object sender, EventArgs e) => UpdateStaffSearchBy();

    private void ByExtBox_CheckedChanged(object sender, EventArgs e) => UpdateStaffSearchBy();

    private void ByPositionBox_CheckedChanged(object sender, EventArgs e) => UpdateStaffSearchBy();

    private void BySectionBox_CheckedChanged(object sender, EventArgs e) => UpdateStaffSearchBy();

    private void ByDeptBox_CheckedChanged(object sender, EventArgs e) => UpdateStaffSearchBy();

    private void ByManagerNameBox_CheckedChanged(object sender, EventArgs e) => UpdateStaffSearchBy();
}