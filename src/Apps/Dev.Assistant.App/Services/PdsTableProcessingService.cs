using Dev.Assistant.Business.Core.Models;
using System.Text;

namespace Dev.Assistant.App.Services;

public class PdsTableProcessingService
{
    /// <summary>
    /// Processes PDS table results and performs actions such as updating UI components.
    /// </summary>
    /// <param name="serviceInfo">The PDS table result information.</param>
    /// <param name="includeDisplayIrregular">Indicates if display irregular tables should be included.</param>
    /// <param name="includeBirthDate">Indicates if birth date validation tables should be included.</param>
    /// <param name="includeServiceAccess">Indicates if service access records tables should be included.</param>
    /// <param name="addCommentAuth">Indicates if comments should be added for AuthServer.</param>
    /// <param name="resultGridView">The DataGridView to display results.</param>
    /// <param name="numServicesMsgLabel">The Label to display the number of services and tables.</param>
    public void ProcessTableNamesResult(TableProcessingArgs args, LogArgs log, Func<StringBuilder> getAuthServerComment = null)
    {
        // Add Option if there is -- START
        log.LogTrace("Check Options -- START");

        if (args.IncludeDisplayIrregular && !args.IsMicroProject)
        {
            log.LogInfo("Include DisplayIrregular tables (for ID && OperatorID) is checked! Adding DisplayIrregular Tables");

            args.ServiceInfo.Tables.Add("Include DisplayIrregular tables -- START"); // will not copied to Clipboard.

            //if (!args.ServiceInfo.Tables.Contains("IFR700_PERSON"))
            args.ServiceInfo.Tables.Add("IFR700_PERSON");

            //if (!args.ServiceInfo.Tables.Contains("IFR701_C_PERSON_XT"))
            args.ServiceInfo.Tables.Add("IFR701_C_PERSON_XT");

            //if (!args.ServiceInfo.Tables.Contains("SRR220_LK_SEC_LOCA"))
            args.ServiceInfo.Tables.Add("SRR220_LK_SEC_LOCA");

            //if (!args.ServiceInfo.Tables.Contains("IFR702_A_PERSON_XT"))
            args.ServiceInfo.Tables.Add("IFR702_A_PERSON_XT");

            //if (!args.ServiceInfo.Tables.Contains("IFR703_V_PERSON_XT"))
            args.ServiceInfo.Tables.Add("IFR703_V_PERSON_XT");

            //if (!args.ServiceInfo.Tables.Contains("IFR706_P_PERSON_XT"))
            args.ServiceInfo.Tables.Add("IFR706_P_PERSON_XT");

            args.ServiceInfo.Tables.Add("Include DisplayIrregular tables -- END"); // will not copied to Clipboard.
        }

        // IncludeBirthDate will be passed only from UtilitiesOps app
        if (args.IncludeBirthDate && !args.IsMicroProject)
        {
            log.LogInfo("Include BirthDate validation tables  is checked! Adding BirthDate Tables");

            args.ServiceInfo.Tables.Add("Include BirthDate validation tables -- START"); // will not copied to Clipboard.

            //if (!args.ServiceInfo.Tables.Contains("IFR700_PERSON"))
            args.ServiceInfo.Tables.Add("IFR700_PERSON");

            //if (!args.ServiceInfo.Tables.Contains("IFR701_C_PERSON_XT"))
            args.ServiceInfo.Tables.Add("IFR701_C_PERSON_XT");

            args.ServiceInfo.Tables.Add("Include BirthDate validation tables -- END"); // will not copied to Clipboard.
        }

        // IncludeServiceAccess will be passed only from UtilitiesOps app
        if (args.IncludeServiceAccess && !args.IsMicroProject)
        {
            log.LogInfo("Include MicroListPersonServiceAccessRecords tables is checked! Adding ServiceAccessRecords Tables");

            args.ServiceInfo.Tables.Add("Include MicroListPersonServiceAccessRecords tables -- START"); // will not copied to Clipboard.

            var accessRecordsTables = args.TableService.ListTablesService(new() { Path = "MicroListPersonServiceAccessRecords" });

            args.ServiceInfo.Tables.AddRange(accessRecordsTables.Tables);

            args.ServiceInfo.Tables.Add("Include MicroListPersonServiceAccessRecords tables -- END"); // will not copied to Clipboard.

            //if (!args.ServiceInfo.Tables.Contains("IFR700_PERSON"))
            //    args.ServiceInfo.Tables.Add("IFR700_PERSON");

            //if (!args.ServiceInfo.Tables.Contains("IFR701_C_PERSON_XT"))
            //    args.ServiceInfo.Tables.Add("IFR701_C_PERSON_XT");

            //if (!args.ServiceInfo.Tables.Contains("IFR702_A_PERSON_XT"))
            //    args.ServiceInfo.Tables.Add("IFR702_A_PERSON_XT");

            //if (!args.ServiceInfo.Tables.Contains("IFR703_V_PERSON_XT"))
            //    args.ServiceInfo.Tables.Add("IFR703_V_PERSON_XT");

            //if (!args.ServiceInfo.Tables.Contains("IFR706_P_PERSON_XT"))
            //    args.ServiceInfo.Tables.Add("IFR706_P_PERSON_XT");

            //if (!args.ServiceInfo.Tables.Contains("PT001_PUBLIC_USER"))
            //    args.ServiceInfo.Tables.Add("PT001_PUBLIC_USER");

            //if (!args.ServiceInfo.Tables.Contains("PT002_PUB_SECTOR"))
            //    args.ServiceInfo.Tables.Add("PT002_PUB_SECTOR");

            //if (!args.ServiceInfo.Tables.Contains("PT007_MOI_AUTH_P_U"))
            //    args.ServiceInfo.Tables.Add("PT007_MOI_AUTH_P_U");

            //if (!args.ServiceInfo.Tables.Contains("PT010_EXTRN_SERVIC"))
            //    args.ServiceInfo.Tables.Add("PT010_EXTRN_SERVIC");

            //if (!args.ServiceInfo.Tables.Contains("PT011_AUTH_EXN_SRV"))
            //    args.ServiceInfo.Tables.Add("PT011_AUTH_EXN_SRV");

            //if (!args.ServiceInfo.Tables.Contains("SRR220_LK_SEC_LOCA"))
            //    args.ServiceInfo.Tables.Add("SRR220_LK_SEC_LOCA");
        }

        StringBuilder copiedText = new();

        if (args.AddCommentAuth && !args.IsMicroProject)
        {
            log.LogInfo("Add comment \"In AuthServer: Create a new user...\" is checked! Adding the comments");

            if (getAuthServerComment != null)
                copiedText = getAuthServerComment();

            copiedText.AppendLine("");
            copiedText.AppendLine("==================================================");
            copiedText.AppendLine("");
        }

        log.LogTrace("Check Options -- END");
        // Add Option -- END

        if (args.ResultGridView != null)
            UpdateDataGridView(args.ResultGridView, args.ServiceInfo);

        args.ServiceInfo.Tables.Sort();

        // Copy -- START
        HashSet<string> tables = new(); // Get only tables, no duplicates

        var numServices = 0;

        foreach (var name in args.ServiceInfo.Tables)
        {
            if (!name.Contains("-- START") && !name.Contains("-- END")) // will not copied if contains
                tables.Add(name);
            else if (!name.StartsWith("Include "))
                numServices++;
        }

        if (numServices == 0) // in one service mode
            numServices = 2;

        if (args.NumServicesMsgLabel != null)
            args.NumServicesMsgLabel.Text = $"{numServices / 2} service(s) and {tables.Count} table(s)";

        //                                     //        ODSASIS          //              OLSDB (Absher)
        string dbNames = args.DbName == "ODSASIS" ? "ODSASIS and ODSASIS_VS" : "ODSASIS, ODSASIS_VS, OLSDB and OLSDB_VS";

        copiedText.AppendLine($"The following tables are missing in PDS ({dbNames}):");
        copiedText.AppendLine("");
        copiedText.AppendLine("Tables:");
        //copiedText.Append("  - "); // for the first table
        copiedText.AppendLine(string.Join(Environment.NewLine, tables)); // to list all tables as bellow:
        copiedText.AppendLine("");
        // Ex:
        // Tables:
        // - Table1
        // - Table2

        copiedText.AppendLine("");
        copiedText.AppendLine($"Please grant SELECT permission to the following PDS users on PDS ({dbNames}):");
        copiedText.AppendLine("");
        copiedText.AppendLine("- Integration users:");
        copiedText.AppendLine("MOIAPP01qa");
        copiedText.AppendLine("ODSAPP01qa");

        if (!args.IsMicroProject)
            copiedText.AppendLine($"ws_{args.ContractName}");

        copiedText.AppendLine("");
        copiedText.AppendLine("- Production users:");

        if (args.IsMicroProject)
        {
            copiedText.AppendLine("MOIAPP01");
            copiedText.AppendLine("GWSAPP01");
        }
        else
        {
            copiedText.AppendLine($"ws_{args.ContractName}");
        }

        copiedText.AppendLine("");

        Clipboard.SetText(copiedText.ToString());

        log.LogSuccess("Copied Successfully!");
        // Copy -- END
    }

    public void UpdateDataGridView(DataGridView gridView, PdsTableResult serviceInfo)
    {
        // Table -- START
        gridView.Columns.Add("", "Result");

        gridView.Rows.Add(serviceInfo.Name);

        foreach (string tbName in serviceInfo.Tables)
        {
            gridView.Rows.Add(tbName);
        }

        var fontSize = 9;

        DataGridViewCellStyle style = new();
        Font font = new("Calibri Light", fontSize, FontStyle.Bold);
        style.Font = font;

        gridView.Columns[0].DefaultCellStyle = style;

        gridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;

        gridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        // Table -- END
    }
}

public class TableProcessingArgs
{
    public PdsTableResult ServiceInfo { get; set; }
    public bool IncludeDisplayIrregular { get; set; }
    public bool IsMicroProject { get; set; }
    public bool IncludeBirthDate { get; set; }
    public bool IncludeServiceAccess { get; set; }
    public bool AddCommentAuth { get; set; }
    public string DbName { get; set; }
    public string ContractName { get; set; }
    public DataGridView ResultGridView { get; set; }
    public Label NumServicesMsgLabel { get; set; }
    public PdsTableService TableService { get; set; }
}

