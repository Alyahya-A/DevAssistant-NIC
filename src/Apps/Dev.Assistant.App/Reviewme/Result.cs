using Dev.Assistant.Business.Core.Models;
using Dev.Assistant.Business.Decoder.Models;
using Dev.Assistant.Business.Decoder.Services;

namespace Dev.Assistant.App.Reviewme;

public partial class Result : Form
{
    private readonly List<ClassModel> currentClasses;
    private readonly List<ClassModel> newClasses;
    private readonly ReviewmeHome homeForm;

    public Result(ReviewmeHome homeForm, List<ClassModel> currentClasses, List<ClassModel> newClasses)
    {
        this.homeForm = homeForm;
        this.currentClasses = currentClasses;
        this.newClasses = newClasses;

        InitializeComponent();
        ShowResult();
    }

    private void ShowResult()
    {
        ResultGridView.Columns.Add("", "Current Name");
        ResultGridView.Columns.Add("", "Current Model");
        ResultGridView.Columns.Add("", "Data Type");
        //ResultGridView.Columns.Add("", "Is name found in New Model");
        ResultGridView.Columns.Add("", "In which model found?");
        ResultGridView.Columns.Add("", "Similar/Matching Name?");
        ResultGridView.Columns.Add("", "Is Name Match?");
        ResultGridView.Columns.Add("", "Data Types");
        ResultGridView.Columns.Add("", "Is Data Type Match?");

        int propsCount = 0;

        for (var i = 0; i < currentClasses.Count; i++)
        {
            propsCount += currentClasses[i].Properties.Count;
            ResultGridView.Rows.Add(currentClasses[i].Name + " Model");

            for (var j = 0; j < currentClasses[i].Properties.Count; j++)
            {
                for (var k = 0; k < newClasses.Count; k++)
                {
                    for (var n = 0; n < newClasses[k].Properties.Count; n++)
                    {
                        string currentName = currentClasses[i].Properties[j].Name;
                        string newName = newClasses[k].Properties[n].Name;

                        if (currentName == newName)
                        {
                            currentClasses[i].Properties[j].SimilarName += $" {newName} ";
                            newClasses[k].Properties[n].SimilarName += $" {newName} ";
                            currentClasses[i].Properties[j].IsNameMatch = "Yes";

                            currentClasses[i].Properties[j].ModelName += $" {newClasses[k].Name} ";

                            string currentDataType = currentClasses[i].Properties[j].DataType.ToLower();
                            string newDataType = newClasses[k].Properties[n].DataType.ToLower();

                            if (currentDataType == newDataType)
                            {
                                currentClasses[i].Properties[j].IsDataTypeMatch = "Yes";
                                currentClasses[i].Properties[j].DataType = newClasses[k].Properties[n].DataType;
                                currentClasses[i].Properties[j].DataTypes += $" {newClasses[k].Properties[n].DataType} ";
                            }
                            else
                            {
                                currentClasses[i].Properties[j].DataTypes += $" {newClasses[k].Properties[n].DataType} ";
                            }
                        }
                        else
                        {
                            // check if rules applyed to new name. if yes show it as SimilarName
                            string newNameTemp = currentName.ToLower();

                            newNameTemp = newNameTemp.Replace("_", "");
                            newNameTemp = newNameTemp.Replace("description", "desc");

                            if (newNameTemp == newName.ToLower())
                            {
                                currentClasses[i].Properties[j].SimilarName += $" {newName} ";
                                newClasses[k].Properties[n].SimilarName += $" {newName} ";
                                currentClasses[i].Properties[j].IsNameMatch = string.IsNullOrWhiteSpace(newName) ? "No" : "No, similar";

                                currentClasses[i].Properties[j].ModelName += $" {newClasses[k].Name} ";

                                string currentDataType = currentClasses[i].Properties[j].DataType.ToLower();
                                string newDataType = newClasses[k].Properties[n].DataType.ToLower();

                                if (currentDataType == newDataType)
                                {
                                    currentClasses[i].Properties[j].IsDataTypeMatch = "Yes";
                                    currentClasses[i].Properties[j].DataType = newClasses[k].Properties[n].DataType;
                                    currentClasses[i].Properties[j].DataTypes += $" {newClasses[k].Properties[n].DataType} ";
                                }
                                else
                                {
                                    currentClasses[i].Properties[j].DataTypes += $" {newClasses[k].Properties[n].DataType} ";
                                }
                            }
                        }
                    }
                }

                ResultGridView.Rows.Add(
                    currentClasses[i].Properties[j].Name,
                    currentClasses[i].Name,
                    currentClasses[i].Properties[j].DataType,
                    currentClasses[i].Properties[j].ModelName,
                    currentClasses[i].Properties[j].SimilarName,
                    currentClasses[i].Properties[j].IsNameMatch ?? "No",
                    currentClasses[i].Properties[j].DataTypes,
                    currentClasses[i].Properties[j].IsDataTypeMatch ?? "No"
                );
            }
        }

        ResultGridView.Rows.Add("-- END --");
        ResultGridView.Rows.Add("# of Properties:");

        foreach (ClassModel model in currentClasses)
        {
            ResultGridView.Rows.Add(model.Name, model.Properties.Count);
        }

        foreach (ClassModel model in newClasses)
        {
            ResultGridView.Rows.Add(model.Name, model.Properties.Count);
        }

        HashSet<string> remainCurrentProps = new();

        foreach (ClassModel model in currentClasses)
        {
            foreach (var prop in model.Properties)
            {
                if (string.IsNullOrWhiteSpace(prop.SimilarName))
                {
                    remainCurrentProps.Add($"{remainCurrentProps.Count + 1}- {prop.Name}");
                }
            }
        }

        HashSet<string> remainNewProps = new();

        foreach (ClassModel model in newClasses)
        {
            foreach (var prop in model.Properties)
            {
                if (string.IsNullOrWhiteSpace(prop.SimilarName))
                {
                    remainNewProps.Add($"{remainNewProps.Count + 1}- {prop.Name}");
                }
            }
        }

        //ResultGridView.Rows.Add("Fields from Current Model are not found in New Model", string.Join("", remainCurrentProps));
        //ResultGridView.Rows.Add("Fields from New Model are not found in Current Model", string.Join("", remainNewProps));

        NotFoundInNew.Text = remainCurrentProps.Count > 0 ? string.Join(Environment.NewLine, remainCurrentProps) : "It seem everything is found";
        NotFoundInCurrent.Text = remainNewProps.Count > 0 ? string.Join(Environment.NewLine, remainNewProps) : "It seem everything is found";

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
        ResultGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
    }

    private void Close_Click(object sender, System.EventArgs e)
    {
        this.Close();
    }

    private void CheckSpllingBtn_Click(object sender, System.EventArgs e)
    {
        try
        {
            List<ClassModel> models;

            if (CurrentRBtn.Checked)
            {
                models = ModelExtractionService.GetClassesByCode(homeForm.Input1, new GetClassesOptions { CheckSpellingAndRules = true });
            }
            else if (NewRBtn.Checked)
            {
                models = ModelExtractionService.GetClassesByCode(homeForm.NewInput, new GetClassesOptions { CheckSpellingAndRules = true });
            }
            else
            {
                throw new Exception(message: "No data in your selected model");
            }

            string propsName = DecodeHelperService.CopyOutputOfSpellingAndRules(models);

            Clipboard.SetText(propsName);

            homeForm.appHome.LogSuccess("Successfully copied to your clipboard. Paste it into Word file then check for Spelling.");
        }
        catch (DevAssistantException error)
        {
            homeForm.appHome.LogError(error);
        }
    }
}