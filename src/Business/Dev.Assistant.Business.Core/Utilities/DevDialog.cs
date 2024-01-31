namespace Dev.Assistant.Business.Core.Utilities;

public static class DevDialog
{
    public static DialogResult Show(DevDialogInfo inputs)
    {
        return MessageBox.Show(inputs.Message, inputs.Title, inputs.Buttons, inputs.BoxIcon);
    }
}

public class DevDialogInfo
{
    public string Title { get; set; }
    public string Message { get; set; }
    public MessageBoxButtons Buttons { get; set; }
    public MessageBoxIcon BoxIcon { get; set; }
}