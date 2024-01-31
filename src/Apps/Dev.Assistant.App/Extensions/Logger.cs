namespace Dev.Assistant.App.Extensions;

public static class RichTextBoxExtensions
{
    public static void AppendText(this RichTextBox textBox, string text, Color color)
    {
        //if (textBox.Text.Trim().EndsWith("duplicated table"))
        //{
        //    textBox.Text = string.Empty;
        //}

        textBox.SelectionStart = textBox.TextLength;
        textBox.SelectionLength = 0;

        textBox.SelectionColor = color;

        // LogNote
        if (color == Color.MidnightBlue)
            textBox.SelectionFont = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
        else
            textBox.SelectionFont = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);

        textBox.AppendText(text);
        textBox.SelectionColor = textBox.ForeColor;

        // scroll to bottom (end line)
        textBox.ScrollToCaret();
    }
}