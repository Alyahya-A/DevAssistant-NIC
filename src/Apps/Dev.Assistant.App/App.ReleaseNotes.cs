using Dev.Assistant.Configuration;
using Markdig;
using Serilog;

namespace Dev.Assistant.App;

public partial class AppReleaseNotes : Form
{
    public AppReleaseNotes()
    {
        InitializeComponent();

        DialogResult = DialogResult.Cancel;

        Log.Logger.Information("Reading release-note file from: {a}", Consts.AppReleaseNotes);

        try
        {
            using StreamReader file = File.OpenText(Consts.AppReleaseNotes);
            var html = Markdown.ToHtml(file.ReadToEnd());

            webBrowser.DocumentText = html;

            Log.Logger.Information("Reading release-note file done!");
        }
        catch (Exception ex)
        {
            Log.Logger.Error("Couldn't read the file", ex);
        }
    }
}