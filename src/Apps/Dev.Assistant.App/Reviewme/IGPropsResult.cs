using Dev.Assistant.Business.Core.Enums;
using Dev.Assistant.Business.Core.Extensions;
using Dev.Assistant.Business.Core.Models;
using Dev.Assistant.Business.Decoder.Models;
using Dev.Assistant.Business.Decoder.Services;
using System.Text;

// Error code start with 7100

namespace Dev.Assistant.App.Reviewme;

public partial class IGPropsResult : Form
{
    private readonly AppHome _appHome;

    private readonly List<ClassModel> _classes;
    private string _servicePath;

    private readonly DataGridView _reqGridView;
    private readonly DataGridView _resGridView;

    private List<Property> _requestDto;
    private List<Property> _responseDto;

    public IGPropsResult(AppHome appHome, List<ClassModel> classes, string servicePath = "")
    {
        InitializeComponent();

        _classes = classes;
        _servicePath = servicePath;
        _appHome = appHome;

        _reqGridView = new DataGridView();
        _resGridView = new DataGridView();

        _requestDto = new();
        _responseDto = new();

        NumClasses.ReadOnly = true;
        NumClasses.BorderStyle = 0;
        NumClasses.BackColor = this.BackColor;
        NumClasses.TabStop = false;

        NumProps.ReadOnly = true;
        NumProps.BorderStyle = 0;
        NumProps.BackColor = this.BackColor;
        NumProps.TabStop = false;

        ShowResult();
    }

    private void ShowResult()
    {
        // To show it in Result page
        ResultGridView.Columns.Add("", "Parameter (EN)");
        ResultGridView.Columns.Add("", "Parameter (AR)");
        ResultGridView.Columns.Add("", "Data Type");
        //ResultGridView.Columns.Add("", "Description (EN/AR)");
        ResultGridView.Columns.Add("", "Description (EN)");
        ResultGridView.Columns.Add("", "Description (AR)");

        // For private using (ex. copying)
        _reqGridView.Columns.Add("", "Parameter (EN)");
        _reqGridView.Columns.Add("", "Parameter (AR)");
        _reqGridView.Columns.Add("", "Data Type");
        _reqGridView.Columns.Add("", "Description (EN)");
        _reqGridView.Columns.Add("", "Description (AR)");

        _resGridView.Columns.Add("", "Parameter (EN)");
        _resGridView.Columns.Add("", "Parameter (AR)");
        _resGridView.Columns.Add("", "Data Type");
        _resGridView.Columns.Add("", "Description (EN)");
        _resGridView.Columns.Add("", "Description (AR)");

        int propsCount = 0;

        string serviceName = string.Empty;

        List<string> resAdded = new();

        string prop = string.Empty;
        string dataType = string.Empty;

        (List<Property> _requestDto, List<Property> _responseDto) = DecodeHelperService.PrepareReqAndResDto("", _classes, false, ProjectType.RestApi);

        if (_requestDto.Count == 0 && _responseDto.Count == 0)
        {
            CopyResWord.Enabled = false;
            CopyReqWord.Enabled = false;

            PasteLabel.Text = "No props found!";
        }

        if (_requestDto.Count == 0)
        {
            CopyResWord.Text = "Copy Table";
            CopyResWord.Location = new Point(15, 370);
            CopyResWord.Size = new Size(260, 55);
        }

        string headerModelName = string.Empty;

        foreach (var property in _requestDto)
        {
            _reqGridView.Rows.Add(property.Name,
                property.DescAr ?? string.Empty,
                property.DataType,
                string.IsNullOrWhiteSpace(property.DescEn) ? property.Name.Replace("-", "").SplitByCapitalLetter() : property.DescEn,
                property.DescAr);
        }

        foreach (var property in _requestDto.OrderBy(dto => dto.ModelName))
        {
            if (!headerModelName.Equals(property.ModelName))
            {
                ResultGridView.Rows.Add(property.ModelName + " ModelNotFormatted");
                headerModelName = property.ModelName;
            }

            ResultGridView.Rows.Add(property.Name,
                property.DescAr ?? string.Empty,
                property.DataType,
                string.IsNullOrWhiteSpace(property.DescEn) ? property.Name.Replace("-", "").SplitByCapitalLetter() : property.DescEn,
                property.DescAr);
        }

        foreach (var property in _responseDto)
        {
            _resGridView.Rows.Add(property.Name,
                property.DescAr ?? string.Empty,
                property.DataType,
                string.IsNullOrWhiteSpace(property.DescEn) ? property.Name.Replace("-", "").SplitByCapitalLetter() : property.DescEn,
                property.DescAr);
        }

        foreach (var property in _responseDto.OrderBy(dto => dto.ModelName))
        {
            if (!headerModelName.Equals(property.ModelName))
            {
                ResultGridView.Rows.Add(property.ModelName + " ModelNotFormatted");
                headerModelName = property.ModelName;
            }

            ResultGridView.Rows.Add(property.Name,
                property.DescAr ?? string.Empty,
                property.DataType, string.IsNullOrWhiteSpace(property.DescEn) ? property.Name.Replace("-", "").SplitByCapitalLetter() : property.DescEn,
                property.DescAr);
        }

        propsCount = _requestDto.Count + _responseDto.Count;

        var fontSize = 9;

        DataGridViewCellStyle style = new();
        Font font = new("Objectivity Light", fontSize, FontStyle.Bold);
        style.Font = font;

        ResultGridView.Columns[0].DefaultCellStyle = style;
        ResultGridView.Columns[3].DefaultCellStyle = style;

        font = new Font("Objectivity Light", fontSize);
        style.Font = font;
        ResultGridView.Columns[2].DefaultCellStyle = style;

        font = new Font("HelveticaNeueLT Arabic 45 Light", fontSize);
        style.Font = font;
        ResultGridView.Columns[1].DefaultCellStyle = style;
        ResultGridView.Columns[4].DefaultCellStyle = style;

        ResultGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
        NumClasses.Text = _classes.Count.ToString();
        NumProps.Text = propsCount.ToString();
    }

    private ClassModel GetModel(string modelName)
    {
        foreach (ClassModel model in _classes)
        {
            if (model.Name == modelName)
            {
                return model;
            }
        }
        return null;
    }

    private void GetOutput_Click(object sender, EventArgs e)
    {
        Close();
    }

    public void CopyHtmlToClipBoard(string html)
    {
        Encoding enc = Encoding.UTF8;

        string begin = "Version:0.9\r\nStartHTML:{0:000000}\r\nEndHTML:{1:000000}\r\nStartFragment:{2:000000}\r\nEndFragment:{3:000000}\r\n";

        var htmlBegin = new StringBuilder();

        htmlBegin.AppendLine("<html>");
        htmlBegin.AppendLine("<head>");
        htmlBegin.AppendLine("<meta http-equiv=\"Content-Type\"");
        htmlBegin.AppendLine("content=\"text/html; charset=" + enc.WebName + "\">");
        htmlBegin.AppendLine("<title>HTML clipboard</title>");
        htmlBegin.AppendLine("<style>");

        //EntireTableCheckBox.Checked
        if (true)
        {
            htmlBegin.AppendLine("pre { margin-top: 3.0pt; margin-bottom: 3.0pt; }");

            htmlBegin.AppendLine("table {");
            //htmlBegin.AppendLine("table-layout: fixed;");
            //htmlBegin.AppendLine("width: 119.3%;");
            //htmlBegin.AppendLine("margin-left: -45.25pt;");
            //htmlBegin.AppendLine("margin-right: 45.25pt;");

            htmlBegin.AppendLine("width: 7.75in;");
            htmlBegin.AppendLine("margin-left: -0.63in;");
            htmlBegin.AppendLine("margin-right: 0.63in;");
            htmlBegin.AppendLine("border-collapse: collapse;");
            //htmlBegin.AppendLine("border:none;");
            htmlBegin.AppendLine("font-size: 11pt;");
            htmlBegin.AppendLine("}");

            htmlBegin.AppendLine("table td {");
            //htmlBegin.AppendLine("width: 25%;");
            //htmlBegin.AppendLine("overflow-wrap: break-word;");
            //htmlBegin.AppendLine("word-break: break-all;");
            htmlBegin.AppendLine("page-break-inside: avoid;");
            htmlBegin.AppendLine("border: solid #C9C9C9 0.5pt;");
            htmlBegin.AppendLine("margin-top: 3.0pt;");
            htmlBegin.AppendLine("margin-bottom: 3.0pt;");
            htmlBegin.AppendLine("padding: 0in 5.4pt 0in 5.4pt;");
            htmlBegin.AppendLine("vertical-align: middle;");
            htmlBegin.AppendLine("}");

            htmlBegin.AppendLine("span { font-size: 11pt; }");
            htmlBegin.AppendLine("pre { font-size: 11pt; }");

            htmlBegin.AppendLine("td:nth-child(even) { background-color: #EDEDED }");
        }
        else
        {
            htmlBegin.AppendLine("pre { margin-top: 3.0pt; margin-bottom: 3.0pt; }");

            htmlBegin.AppendLine("table {");
            htmlBegin.AppendLine("font-size: 11pt;");
            htmlBegin.AppendLine("}");

            htmlBegin.AppendLine("td {");
            htmlBegin.AppendLine("word-wrap: break-word;");
            htmlBegin.AppendLine("page-break-inside: avoid;");
            htmlBegin.AppendLine("margin-top: 3.0pt;");
            htmlBegin.AppendLine("margin-bottom: 3.0pt;");
            htmlBegin.AppendLine("padding: 0in 5.4pt 0in 5.4pt;");
            htmlBegin.AppendLine("vertical-align: middle;");
            htmlBegin.AppendLine("}");

            htmlBegin.AppendLine("span { font-size: 11pt; }");
            htmlBegin.AppendLine("pre { font-size: 11pt; }");
        }
        htmlBegin.AppendLine("</style>");
        htmlBegin.AppendLine("</head>");
        htmlBegin.AppendLine("<body>");
        htmlBegin.AppendLine("<!--StartFragment-->");

        string htmlEnd = "<!--EndFragment-->\r\n</body>\r\n</html>\r\n";

        string beginSample = string.Format(begin, 0, 0, 0, 0);

        int countBegin = enc.GetByteCount(beginSample);
        int countHtmlBegin = enc.GetByteCount(htmlBegin.ToString());
        int countHtml = enc.GetByteCount(html);
        int countHtmlEnd = enc.GetByteCount(htmlEnd);

        string htmlTotal = string.Format(
          begin
          , countBegin
          , countBegin + countHtmlBegin + countHtml + countHtmlEnd
          , countBegin + countHtmlBegin
          , countBegin + countHtmlBegin + countHtml
          ) + htmlBegin + html + htmlEnd;

        DataObject obj = new();
        obj.SetData(DataFormats.Html, new MemoryStream(enc.GetBytes(htmlTotal)));

        Clipboard.SetDataObject(obj, true);
    }

    private void CopyToClipboardWithFormating(DataGridView dataGridView, string fontWeightAr, string fontWeightEn, bool isExcel = false)
    {
        var DataGridView1Counts = dataGridView.Rows.Count;

        var fontAr = isExcel ? "Arial" : "Arial";
        var fontEn = isExcel ? "Arial" : "Arial";

        var html = new StringBuilder();

        html.Append("<table>");

        if (DataGridView1Counts > 0)
        {
            if (dataGridView == ResultGridView)
            {
                // Sets Column Headers
                html.Append("<tr>");
                html.Append("<th>Parameter (EN)</th>");
                html.Append("<th>Parameter (AR)</th>");
                html.Append("<th>Data Type</th>");
                html.Append("<th>Description (EN/AR)</th>");
            }

            for (int i = 0; i <= dataGridView.RowCount - 1; i++)
            {
                Console.WriteLine($"=========== Row Start {i} ===========");

                html.Append("<tr>");

                for (int j = 0; j <= dataGridView.ColumnCount - 2; j++)
                {
                    Console.WriteLine($"Column: {i}");

                    // Description Column
                    if (j == 3)
                    {
                        DataGridViewCell descEn = dataGridView[j, i];
                        DataGridViewCell descAr = dataGridView[j + 1, i];

                        html.Append("<td>");

                        if (isExcel)
                        {
                            html.AppendFormat("<span style=\"font-weight:{1}; text-align: left; font-family:'{2}'\">{0} </span> ",
                                              descEn.Value, fontWeightEn, fontEn);

                            html.AppendFormat("<span style=\"font-weight:{1}; text-align: right; font-family:'{2}'\">{0}</span> ",
                                               descAr.Value, fontWeightAr, fontAr);
                        }
                        else
                        {
                            html.AppendFormat("<pre style=\"font-weight:{1}; text-align: left; font-family:'{2}'\">{0}\n</pre> ",
                                               descEn.Value, fontWeightEn, fontEn);

                            html.AppendFormat("<pre style=\"font-weight:{1}; direction: rtl; text-align: right; font-family:'{2}'\">{0}</pre> ", descAr.Value, fontWeightAr, fontEn);
                        }

                        html.Append("</td>");
                    }
                    else
                    {
                        DataGridViewCell cell = dataGridView[j, i];

                        if (cell.Value is null)
                        {
                            continue;
                        }

                        // New Model Starts -- only in excel
                        if (cell.Value != null && cell.Value.ToString().Contains(" ModelNotFormatted"))
                        {
                            html.AppendFormat("<td colspan=4 style=\"font-weight:bold; font-family:'{2}'\"><font color={1}>{0}</font></td>",
                                               cell.Value.ToString().Replace("ModelNotFormatted", "Model"), "#808080", fontEn);

                            continue;
                        }
                        else
                        {
                            // Parameter (AR) Column
                            if (j == 1)
                            {
                                html.AppendFormat("<td style=\"direction: rtl; text-align: center; font-family:'{1}'\">{0}</td>",
                                                   cell.Value, fontAr);
                            }
                            // Data Type Column
                            else if (j == 2)
                            {
                                string dataType = cell.Value?.ToString() ?? string.Empty;

                                if (!string.IsNullOrWhiteSpace(dataType))
                                {
                                    if (dataType.Contains('<'))
                                    {
                                        dataType = dataType.Replace("<", " of ");
                                        dataType = dataType.Replace(">", "");
                                    }
                                    else // Regular types. Ex: int, double...
                                    {
                                        var enParam = dataGridView?[j - 2, i]?.Value?.ToString() ?? string.Empty;

                                        if (AddBracketBox.Checked && !dataType.Equals("DateTime") && (!enParam.Equals("RequestTimestamp") || !enParam.Equals("ResponseTimestamp")))
                                        {
                                            dataType += "()";
                                        }
                                    }
                                }

                                html.AppendFormat("<td style=\"text-align: center; font-family:'{1}'\">{0}</td>",
                                                   dataType, fontEn);
                            }
                            // Parameter (EN) Column
                            else
                            {
                                if (cell.Value is null)
                                {
                                    continue;
                                }

                                if (cell.Value.ToString().Trim().EndsWith("*")) // Set font color for "*" to Red
                                {
                                    html.AppendFormat("<td style=\"font-weight:{1}; text-align: left; font-family:'{0}'\">", fontEn, fontWeightEn);

                                    html.AppendFormat("<span style=\"vertical-align: middle\">{0}</span>", cell.Value.ToString().Replace("*", ""));
                                    html.AppendFormat("<span><font color={0}> *</font></span>", "#FF0000"); // Red

                                    html.Append("</td>");
                                }
                                else
                                {
                                    html.AppendFormat("<td style=\"font-weight:{2}; text-align: left;; font-family:'{1}'\">{0}</td>",
                                                       cell.Value, fontEn, fontWeightEn);
                                }
                            }
                        }
                    }
                }

                html.Append("</tr>");
            }
        }

        html.Append("</table>");
        html.AppendLine("");
        html.AppendLine("");

        CopyHtmlToClipBoard(html.ToString());
    }

    private void CopyToClipboardWithFormating2(DataGridView dataGridView, string fontWeightAr, string fontWeightEn, bool isExcel = false)
    {
        var DataGridView1Counts = dataGridView.Rows.Count;

        var fontAr = isExcel ? "Arial" : "Arial";
        var fontEn = isExcel ? "Arial" : "Arial";

        var html = new StringBuilder();

        html.Append("<table>");

        if (DataGridView1Counts > 0)
        {
            if (dataGridView == ResultGridView)
            {
                // Sets Column Headers
                html.Append("<tr>");
                html.Append("<th>Parameter (EN)</th>");
                html.Append("<th>Parameter (AR)</th>");
                html.Append("<th>Data Type</th>");
                html.Append("<th>Description (EN/AR)</th>");
            }

            var headers = new List<Item> {
                new Item {Title =  "Parameter (En)", Width = "0pt"},
                new Item {Title =  "Parameter (Ar)", Width = "0pt"},
                new Item {Title =  "Data Type", Width = "0pt"},
                new Item {Title =  "Description (Ar/En)", Width = "0pt"},
            };

            html.Append("<tr>");

            foreach (var header in headers)
            {
                if (header.Title.Equals("Parameter (En)"))
                    html.Append($"<td style=\"width: 2in; overflow: hidden; white-space: nowrap; height:0.2in; border:solid #A5A5A5 1.0pt; background:#A5A5A5; \">");
                else if (header.Title.Equals("Data Type"))
                    html.Append($"<td style=\"word-break: break-all; height:0.2in; overflow: hidden; white-space: nowrap; border:solid #A5A5A5 1.0pt; background:#A5A5A5; \">");
                else
                    html.Append($"<td style=\"height:0.2in; overflow: hidden; white-space: nowrap; border:solid #A5A5A5 1.0pt; background:#A5A5A5; \">");
                //html.Append($"<td style=\"overflow: hidden; white-space: nowrap; height:0.2in; border:solid #A5A5A5 1.0pt; background:#A5A5A5; \">");

                html.Append("<p style=\"margin-top:3.0pt; margin-right: 0in; margin-bottom:3.0pt; font-size:11pt; text-align:center \">");
                html.Append($"<strong><span style='font-family:\"Arial\",sans-serif; color:white;'>{header.Title}</span></strong>");
                html.Append("</p>");

                html.Append("</td>");
            }

            html.Append("</tr>");

            for (int i = 0; i <= dataGridView.RowCount - 1; i++)
            {
                Console.WriteLine($"=========== Row Start {i} ===========");

                if (dataGridView?[0, i]?.Value is null)
                {
                    Console.WriteLine($"=========== Row {i} is Null ===========");
                    continue;
                }

                html.Append("<tr>");

                for (int j = 0; j <= dataGridView.ColumnCount - 2; j++)
                {
                    Console.WriteLine($"Column: {i}");

                    // Description Column
                    if (j == 3)
                    {
                        DataGridViewCell descEn = dataGridView[j, i];
                        DataGridViewCell descAr = dataGridView[j + 1, i];

                        html.Append("<td>");

                        if (isExcel)
                        {
                            html.AppendFormat("<span style=\"font-weight:{1}; text-align: left; font-family:'{2}'\">{0} </span> ",
                                              descEn.Value, fontWeightEn, fontEn);

                            html.AppendFormat("<span style=\"font-weight:{1}; text-align: right; font-family:'{2}'\">{0}</span> ",
                                               descAr.Value, fontWeightAr, fontAr);
                        }
                        else
                        {
                            html.AppendFormat("<pre style=\"font-weight:{1}; text-align: left; font-family:'{2}'\">{0}\n</pre> ",
                                               descEn.Value, fontWeightEn, fontEn);

                            html.AppendFormat("<pre style=\"font-weight:{1}; direction: rtl; text-align: right; font-family:'{2}'\">{0}</pre> ", descAr.Value, fontWeightAr, fontEn);
                        }

                        html.Append("</td>");
                    }
                    else
                    {
                        DataGridViewCell cell = dataGridView[j, i];

                        if (cell.Value is null)
                        {
                            continue;
                        }

                        Console.WriteLine($"Cell: {cell?.Value}");

                        // New Model Starts -- only in excel
                        if (cell.Value != null && cell.Value.ToString().Contains(" ModelNotFormatted"))
                        {
                            html.AppendFormat("<td colspan=4 style=\"font-weight:bold; font-family:'{2}'\"><font color={1}>{0}</font></td>",
                                               cell.Value.ToString().Replace("ModelNotFormatted", "Model"), "#808080", fontEn);

                            continue;
                        }
                        else
                        {
                            // Parameter (AR) Column
                            if (j == 1)
                            {
                                html.AppendFormat("<td style=\"direction: rtl; text-align: center; font-family:'{1}'\">{0}</td>",
                                                   cell.Value, fontAr);
                            }
                            // Data Type Column
                            else if (j == 2)
                            {
                                string dataType = cell.Value?.ToString() ?? string.Empty;

                                if (!string.IsNullOrWhiteSpace(dataType))
                                {
                                    if (dataType.Contains('<'))
                                    {
                                        dataType = dataType.Replace("<", " of ");
                                        dataType = dataType.Replace(">", "");
                                    }
                                    else // Regular types. Ex: int, double...
                                    {
                                        var enParam = dataGridView?[j - 2, i]?.Value?.ToString() ?? string.Empty;

                                        if (AddBracketBox.Checked && !enParam.ToLower().Equals("requesttimestamp") && !enParam.ToLower().Equals("responsetimestamp") && !dataType.Equals("DateTime"))
                                        {
                                            dataType += "()";
                                        }
                                    }
                                }

                                html.AppendFormat("<td style=\"word-break: break-all; background:#EDEDED; text-align: center; font-family:'{1}'\">{0}</td>", dataType, fontEn);
                            }
                            // Parameter (EN) Column
                            else
                            {
                                if (cell.Value is null)
                                {
                                    continue;
                                }

                                if (cell.Value.ToString().Trim().EndsWith("*")) // Set font color for "*" to Red
                                {
                                    html.AppendFormat("<td style=\"background:#EDEDED; overflow: hidden; white-space: nowrap; font-weight:{1}; text-align: left; font-family:'{0}'\">", fontEn, fontWeightEn);

                                    html.AppendFormat("<span>{0}</span>", cell.Value.ToString().Replace("*", ""));
                                    html.AppendFormat("<span><font color={0}> *</font></span>", "#FF0000"); // Red

                                    html.Append("</td>");
                                }
                                else
                                {
                                    html.Append("<td style=\"background:#EDEDED; overflow: hidden; white-space: nowrap; text-align: left;\">");
                                    html.AppendFormat("<pre style=\"font-weight:{2}; text-align: left; font-family:'{1}'\">{0}</pre></td>",
                                                       cell.Value, fontEn, fontWeightEn);
                                }
                            }
                        }
                    }
                }

                html.Append("</tr>");
            }
        }

        html.Append("</table>");

        CopyHtmlToClipBoard(html.ToString());
    }

    private void CopyExcel_Click(object sender, EventArgs e)
    {
        PasteLabel.Text = "Copying...";

        string label = "Paste the output to Excel file";

        CopyToClipboardWithFormating(ResultGridView, fontWeightAr: "bold", fontWeightEn: "normal", isExcel: true);

        PasteLabel.Text = label;
    }

    private void CopyWord_Click(object sender, EventArgs e)
    {
        PasteLabel.Text = "Copying...";
        string label;

        if (_reqGridView.Rows.Count == 0)
        {
        }

        //EntireTableCheckBox.Checked
        if (true)
        {
            label = "If your IG file has old table, remove the entire table and Paste the NEW table.";

            CopyToClipboardWithFormating2(_reqGridView, fontWeightAr: "normal", fontWeightEn: "bold");
        }
        else
        {
            label = "Insert new Row after the Request table heder, " +
            "then in the first Cell paste the output. In \"Paste Options\" select \"Merge Table\" or click Ctrl then M";

            CopyToClipboardWithFormating(_reqGridView, fontWeightAr: "normal", fontWeightEn: "bold");
        }

        PasteLabel.Text = label;
    }

    private void CopyResWord_Click(object sender, EventArgs e)
    {
        PasteLabel.Text = "Copying...";

        string label;

        //EntireTableCheckBox.Checked
        if (true)
        {
            label = "If your IG file has old table, remove the entire table and Paste the NEW table.";

            CopyToClipboardWithFormating2(_resGridView, fontWeightAr: "normal", fontWeightEn: "bold");
        }
        else
        {
            label = "Insert new Row after the Response table heder, " +
                    "then in the first Cell paste the output. In \"Paste Options\" select \"Merge Table\" or click Ctrl then M";

            CopyToClipboardWithFormating(_resGridView, fontWeightAr: "normal", fontWeightEn: "bold");
        }
        PasteLabel.Text = label;
    }

    protected class Item
    {
        public string Title { get; set; }

        public string Width { get; set; }
    }
}