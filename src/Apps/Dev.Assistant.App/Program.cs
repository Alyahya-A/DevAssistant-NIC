using Dev.Assistant.Configuration;

namespace Dev.Assistant.App;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        Application.SetHighDpiMode(HighDpiMode.DpiUnaware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        Consts.AppVersion = Application.ProductVersion;

        Application.Run(new AppHome());
    }
}