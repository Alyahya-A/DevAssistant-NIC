namespace Dev.Assistant.Business.Converter.Models;

public class DbModel
{
    public DbModel() => SqlProps = new List<SqlProp>();

    public string DbName { get; set; }
    public string DbFullName { get; set; }
    public List<SqlProp> SqlProps { get; set; }
}