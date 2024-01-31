using Dev.Assistant.Business.Core.Models;
using Dev.Assistant.Business.Core.Utilities;
using System.Text;

//using Microsoft.TeamFoundation.Client;
//using Microsoft.TeamFoundation.VersionControl.Client;

// Error Code Start 7400

namespace Dev.Assistant.Dashboard.LogEvents;

public partial class LogEventsHome : UserControl
{
    private readonly AppHome _appHome;
    private List<DevEVent> _logs;
    private List<DevEVent> _errors;

    private FilterOptions _fileOptions;

    private int _logType;
    private DateTime _selectDate;

    private DateTime _currentDate = DateTime.Now;
    //private string _currentMonth = $"{DateTime.Now.AddDays(-3):MM}";

    private bool _appInitialized;

    public LogEventsHome(AppHome appHome)
    {
        _appInitialized = false;

        InitializeComponent();

        _fileOptions = new();

        var appItems = new List<FilterItem>
        {
            new FilterItem { Value = 1, Text = "DateTimestamp" },
            new FilterItem { Value = 6, Text = "Today" },
            new FilterItem { Value = 3, Text = "EventId"  },
            new FilterItem { Value = 4, Text = "EventStatus" },
            new FilterItem { Value = 5, Text = "EventName"  },
        };

        var logsTypes = new List<FilterItem>
        {
            new FilterItem { Value = 1, Text = "Logs"},
            new FilterItem { Value = 2, Text = "Errors" },
            //new FilterItem { Value = 3, Text = "Statistics" },
        };

        LogsTypeBox.DataSource = logsTypes;
        LogsTypeBox.DisplayMember = "Text";
        LogsTypeBox.ValueMember = "Value";

        _appHome = appHome;

        var events = DevEvents.Events.Select(e
            => new FilterItem { Value = e.Id, Text = $"{e.Id}- {e.Name}" }).ToList();
        EventsBox.DataSource = events;
        EventsBox.DisplayMember = "Text";
        EventsBox.ValueMember = "Value";

        var status = new List<FilterItem>();
        int count = 1;
        foreach (var e in Enum.GetValues(typeof(EventStatus)))
        {
            status.Add(new FilterItem { Value = count, Text = e.ToString() });
            count++;
        }
        EventStatusBox.DataSource = status;
        EventStatusBox.DisplayMember = "Text";
        EventStatusBox.ValueMember = "Value";

        var orderBy = new List<FilterItem>
        {
            new FilterItem { Value = 1, Text = "Date"},
            new FilterItem { Value = 2, Text = "Event ID" },
            new FilterItem { Value = 3, Text = "Event Status" }
        };

        OrderByBox.DataSource = orderBy;
        OrderByBox.DisplayMember = "Text";
        OrderByBox.ValueMember = "Value";

        //

        var sources = new List<FilterItem>
        {
            new FilterItem { Value = 1, Text = "Current Month"},
            new FilterItem { Value = 2, Text = "All Data" },
        };

        DataBox.DataSource = sources;
        DataBox.DisplayMember = "Text";
        DataBox.ValueMember = "Value";

        ExcludeText.Text = "1004;";

        _appInitialized = true;
        ReloadLogs();
    }

    private void ShowAllEvent()
    {
        List<DevEVent> events = _logType switch
        {
            2 => _errors,
            _ => _logs,
        };

        if (events is null)
            return;

        StringBuilder sb = new();

        List<DevEVent> allEvents = _fileOptions.OrderBy switch
        {
            2 => events.OrderBy(e => e.EventId).ToList(),
            3 => events.OrderBy(e => e.EventStatus).ToList(),
            _ => events.OrderBy(e => e.DateTimestamp).ToList(), // default And 1
        };

        if (_fileOptions.ExcludeEvents is not null and { Count: > 0 })
        {
            allEvents = allEvents.Where(e => !_fileOptions.ExcludeEvents.Contains(e.EventId)).ToList();
        }

        if (_fileOptions.Date != DateTime.MinValue)
        {
            allEvents = allEvents.Where(e => e.DateTimestamp.Date.Equals(_fileOptions.Date.Date)).ToList();
        }

        //allEvents = events.Where(e => e.DateTimestamp.Date == DateTime.Now.Date).ToList();

        // 1 - All events
        if (_fileOptions.EventId > 1)
        {
            allEvents = allEvents.Where(e => e.EventId.Contains(_fileOptions.EventId.ToString())).ToList();
        }

        if (_fileOptions.EventStatus != EventStatus.All)
        {
            allEvents = allEvents.Where(e => e.EventStatus == _fileOptions.EventStatus.ToString()).ToList();
        }

        sb.AppendLine($"Events #{allEvents.Count()}");
        sb.AppendLine($"Events today #{allEvents.Where(e => e.DateTimestamp.Date == DateTime.Now.Date).Count()}");
        sb.AppendLine($"Events by types #{allEvents.Select(e => e.EventId).Distinct().Count()}");

        if (_logType == 1)
        {
            var clicked = allEvents.Where(e => e.EventStatus.Equals("Clicked")).Count();
            var succeed = allEvents.Where(e => e.EventStatus.Equals("Succeed")).Count();
            var failed = allEvents.Where(e => e.EventStatus.Equals("Failed")).Count() + allEvents.Where(e => e.EventStatus.Equals("InputError")).Count();
            var called = allEvents.Where(e => e.EventStatus.Equals("Called")).Count();

            // To avoid "Attempted to divide by zero." error
            if (clicked > 0)
            {
                sb.AppendLine($"Clicked #{clicked}");
                sb.AppendLine($"Succeed #{succeed} ({succeed * 100 / clicked}%)");
                sb.AppendLine($"Failed (InputError included) #{failed} ({failed * 100 / clicked}%)");
                sb.AppendLine($"Called #{called}");
            }

            // Top 5 events:
            var groupedEvents = events.GroupBy(e => e.EventId).Select(g => g.ToList()).ToList().OrderByDescending(c => c.Count).Take(10).Select(i => $"{i.FirstOrDefault()?.EventId}-{i.FirstOrDefault()?.EventName}: {i.Count}").ToList();

            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("Top Events:");

            foreach (var item in groupedEvents)
            {
                sb.AppendLine(item);
            }

            sb.AppendLine();
        }

        if (allEvents.Any())
            sb.AppendLine($"Last event {allEvents.Select(e => e.DateTimestamp).Max():dd MMMM hh:mm:ss tt}");

        sb.AppendLine("");
        sb.AppendLine("");

        if (_logType == 1)
        {
            foreach (var ev in allEvents)
            {
                // dd MMMM HH:mm:ss (hh:mm tt)
                sb.Append($"{ev.DateTimestamp:dd-MM-yyyy HH:mm:ss (hh:mm tt)}: {ev.EventId} [{ev.EventStatus}] - {ev.EventName}");

                if (!string.IsNullOrWhiteSpace(ev.Message))
                    sb.Append($" | {ev.Message}");

                sb.AppendLine("");
            }
        }
        else
        {
            foreach (var ev in allEvents)
            {
                sb.AppendLine($"{ev.DateTimestamp:dd MMMM HH:mm:ss (hh:mm tt)}: {ev.EventId} {ev.EventName} | {ev.Message} | {ev.Exception}");

                sb.AppendLine("");
            }
        }

        AllEventBox.Text = sb.ToString();
    }

    private void FiltersBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        //var selectedApp = (FilterItem)FiltersBox.SelectedItem;

        //var logType = (FilterItem)LogsTypeBox.SelectedItem;

        //if (logType.Value == 1) // Logs
        //    ShowAllEvent(_logs, selectedApp.FilterBy);
        //else
        //    ShowAllEvent(_errors, selectedApp.FilterBy);
    }

    private void LogsTypeBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        var logType = (FilterItem)LogsTypeBox.SelectedItem;

        _logType = logType.Value;

        ReloadLogs();
    }

    private void ReloadLogs()
    {
        try
        {
            if (!_appInitialized)
                return;

            // 1- Current month
            // else- All months
            bool allData = ((FilterItem)DataBox.SelectedItem).Value != 1;

            if (_logType == 1)  // Logs
            {
                List<string> paths = new();

                if (!allData)
                {
                    paths.Add(@$"\\172.29.1.27\d\Deployment\DevAssistant\AppLogs\Logs{_currentDate.Year}{_currentDate.Month.ToString().PadLeft(2, '0')}.log");
                }
                else
                {
                    paths.Add(@$"\\172.29.1.27\d\Deployment\DevAssistant\AppLogs\Logs{_currentDate.Year}{_currentDate.Month.ToString().PadLeft(2, '0')}.log");


                    var fs = Directory.GetFiles(@$"C:\Users\aayahya\Documents\AppLogs\", "*.log").Where(f => f.Contains("\\Logs")).ToList();

                    paths.AddRange(Directory.GetFiles(@$"C:\Users\aayahya\Documents\AppLogs\", "*.log").Where(f => f.Contains("\\Logs")));
                }

                _logs = new();

                foreach (var path in paths)
                {
                    using FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using BufferedStream bs = new(fs);
                    using StreamReader sr = new(bs);

                    DevEVent devEvent;
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        var pc = line.Split('|');

                        try
                        {
                            if (pc.Length == 5)
                            {
                                devEvent = new()
                                {
                                    DateTimestamp = DateTime.Parse(pc[0]),
                                    EventId = pc[2].Trim(),
                                    EventStatus = pc[3].Trim(),
                                    EventName = DevEvents.Events.FirstOrDefault(e => e.Id == int.Parse(pc[2].Trim()))?.Name ?? "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
                                    Message = pc[4].Trim()
                                };

                                //if (devEvent.EventId == "1001" && devEvent.EventStatus == EventStatus.Succeed.ToString())
                                //{
                                //}
                                //if (pc[5].Trim() == "rOrSGbdfjo39/7Oxrs8xAolRyYaZAcuZpPlIbfHoxh4N3ONQ4CKpt9LP5NQ9phy9")
                                //{
                                //}

                                // since 7002 Message has ; such as "Staff Count: 12;اليح"
                                if (devEvent.Message.Contains(';'))
                                {
                                    var errorCodes = devEvent.Message[(devEvent.Message.IndexOf(":") + 1)..].Split(";").Distinct();

                                    string message = string.Empty;

                                    foreach (var errorCode in errorCodes)
                                    {
                                        if (string.IsNullOrWhiteSpace(errorCode))
                                            continue;

                                        message += errorCode switch
                                        {
                                            "1426" => "The languages order incorrect. Make sure that the 1st lines are in English only",
                                            "1427" => "The 1st XML line must be in English only",
                                            "1419" => "The English XML documentation must end with \"<br/>\" (New line).",
                                            "1404" => "No XML found for Arabic or/and English. Make sure that the 1st lines are in English only",
                                            "1405" => "Data type is start with capital letter",
                                            "1406" => "Unexpected data type.",
                                            "1407" => "Data type is short. Must be int",
                                            "1424" => "Date property must end with suffix **DateG or **DateH",
                                            "1433" => "Unexpected data type for Date property",
                                            "1431" => "Birth date property must accept both Hijri and Gregorian date",
                                            "1432" => "Birth date property must be integer",
                                            "1430" => "Data type for Hijri date must be integer",
                                            "1434" => "Data type for Gregorian date must be DateTime",
                                            "1408" => "Property name end with AR/EN must be Ar/En",
                                            "1409" => "Property name contains \"_\". Property must be PascalCase",
                                            "1410" => "Property name contains \"Id\". Must be \"ID\"",
                                            "1411" => "Description must be Desc",
                                            "1412" => "Property name must be RequestTimestamp",
                                            "1413" => "Property name must be ResponseTimestamp",
                                            "1429" => "OperatorID for GCC contracts is not Required",
                                            "1423" => "OperatorID must be Required",
                                            "1414" => "Property name must be ClientIPAddress",
                                            "1422" => "ClientIPAddress in Tawakkalna is not Required",
                                            "1428" => "ClientIPAddress must be Required",
                                            "1421" => "The property must be [Required]",
                                            "1415" => "Property must start with Capital",
                                            "1416" => "There are more than 2 capital letter next to each other",
                                            "1417" => "There are more than 2 capital letter next to each other",
                                            "1418" => "There are more than 2 capital letter next to each other",
                                            "1435" => "Data type is start with small letter",
                                            "1436" => "Data type is short. Must be Integer",
                                            "1437" => "Property name end with AR/EN must be Ar/En",
                                            "1438" => "Property name contains \"_\". Property must be PascalCase",
                                            "1439" => "Description must be Desc",
                                            "1440" => "Property must start with Capital. Property must be PascalCase",
                                            "7400" => "No XML documentation found for this class",
                                            _ => errorCode
                                        };

                                        message += Environment.NewLine;
                                    }

                                    devEvent.Message += $"{Environment.NewLine}{message}".TrimEnd('\n');
                                }

                                _logs.Add(devEvent);
                            }
                            else
                            {
                            }
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }
                    }
                }
            }

            if (_logType == 2)
            {
                List<string> paths = new();
                _errors = new();

                if (!allData)
                {
                    paths.Add(@$"\\172.29.1.27\d\Deployment\DevAssistant\AppLogs\Errors{_currentDate.Year}{_currentDate.Month.ToString().PadLeft(2, '0')}.log");
                }
                else
                {
                    paths.Add(@$"\\172.29.1.27\d\Deployment\DevAssistant\AppLogs\Errors{_currentDate.Year}{_currentDate.Month.ToString().PadLeft(2, '0')}.log");
                    paths.AddRange(Directory.GetFiles(@$"C:\Users\aayahya\Documents\AppLogs\", "*.log").Where(f => f.Contains("\\Errors")));
                }

                foreach (var path in paths)
                {
                    using FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using BufferedStream bs = new(fs);
                    using StreamReader sr = new(bs);

                    //StringBuilder sb = new();
                    DevEVent devEvent;

                    string line;
                    int index = 0;

                    while ((line = sr.ReadLine()) != null)
                    {
                        var pc = line.Split('|');

                        if (pc.Length == 4)
                        {
                            devEvent = new()
                            {
                                DateTimestamp = DateTime.Parse(pc[0]),
                                EventId = pc[1].Trim(),
                                EventName = DevEvents.Events.FirstOrDefault(e => e.Id == int.Parse(pc[1].Trim()))?.Name ?? "XXXXXXXXXXXXXX",
                                Message = pc[2].Trim(),
                                Exception = pc[3].Trim()
                            };

                            _errors.Add(devEvent);
                        }
                        else
                        {
                            _errors[index - 1].Exception += pc[0];
                            continue;
                        }
                        //else
                        //{
                        //}

                        index++;
                    }
                }
            }


            ShowAllEvent();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void DatePicker_ValueChanged(object sender, EventArgs e)
    {
        _selectDate = DatePicker.Value.Date;

        //if (_selectDate.Year != DateTime.Now.Year && _selectDate.Month != DateTime.Now.Month)
        //{
        //    ReloadLogs();
        //}

        _fileOptions.Date = _selectDate > DateTime.Now ? DateTime.MinValue : _selectDate;

        ShowAllEvent();
    }

    private void EventsBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedApp = (FilterItem)EventsBox.SelectedItem;

        _fileOptions.EventId = selectedApp.Value;

        ShowAllEvent();
    }

    private void EventStatusBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedApp = (FilterItem)EventStatusBox.SelectedItem;

        if (Enum.TryParse(selectedApp.Text, out EventStatus status))
            _fileOptions.EventStatus = status;

        ShowAllEvent();
    }

    private void OrderByBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedOpt = (FilterItem)OrderByBox.SelectedItem;

        _fileOptions.OrderBy = selectedOpt.Value;

        ShowAllEvent();
    }

    private void DataBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        ReloadLogs();

        //ShowAllEvent();
    }

    private void label7_Click(object sender, EventArgs e)
    {
    }

    private void ExcludeText_TextChanged(object sender, EventArgs e)
    {
        _fileOptions.ExcludeEvents = ExcludeText.Text.TrimEnd(';').TrimStart(';').Split(';').ToList();

        ShowAllEvent();
    }
}

public class FilterItem
{
    public int Value { get; set; }

    public string Text { get; set; }
}

public record FilterOptions
{
    /// <summary>
    /// 1- Date
    /// 2- Event ID
    /// 3- Event Status
    /// </summary>
    public int OrderBy { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public int EventId { get; set; }

    public EventStatus EventStatus { get; set; } = EventStatus.All;

    public List<string> ExcludeEvents { get; set; }
}