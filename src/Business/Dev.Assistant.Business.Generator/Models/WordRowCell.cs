namespace Dev.Assistant.Business.Generator.Models;

/// <summary>
/// Represents a cell within a Word table row.
/// </summary>
public class WordRowCell
{
    /// <summary>
    /// Gets or sets the placeholder in the cell.
    /// </summary>
    public string Placeholder { get; set; }

    /// <summary>
    /// Gets or sets the value (Replacement) to replace the placeholder in the cell.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="WordRowCell"/> class.
    /// </summary>
    /// <param name="placeholder">The placeholder in the cell.</param>
    /// <param name="value">The value to replace the placeholder in the cell.</param>
    public WordRowCell(string placeholder, string value)
    {
        Placeholder = placeholder;
        Value = value;
    }
}