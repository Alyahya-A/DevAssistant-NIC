namespace Dev.Assistant.Business.Converter.Models;

public class SqlConvertOptions
{
    public SqlConvertOptions()
    {
        RemoveComments = true;
        CommentStyle = DevCommentType.None;
        RemoveWhiteSpace = true;
        IsSqlToCode = true;
    }

    /// <summary>
    /// To remove comments from clean service. Default is true
    /// </summary>
    public bool RemoveComments { get; set; }

    /// <summary>
    /// if RemoveComments is false then provide CommentStyle that you want to release it. Default return as it is ( //, ', --).
    /// </summary>
    public DevCommentType CommentStyle { get; set; }

    /// <summary>
    /// True to replace multi-spaces whith one space. Default is true
    /// </summary>
    public bool RemoveWhiteSpace { get; set; }

    /// <summary>
    /// Indicate wether this called to clean from code to sql. Default is true
    /// </summary>
    public bool IsSqlToCode { get; set; }
}