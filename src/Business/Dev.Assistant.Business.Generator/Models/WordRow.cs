namespace Dev.Assistant.Business.Generator.Models;

/// <summary>
/// Represents a row within a Word table.
/// </summary>
public class WordRow
{
    /// <summary>
    /// Gets the cells in the row.
    /// </summary>
    public List<WordRowCell> Cells { get; set; }

    public WordRow(List<WordRowCell> cells) => Cells = cells;
}