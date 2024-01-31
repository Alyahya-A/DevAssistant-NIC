namespace Dev.Assistant.Business.Core.Models;

public class PdsTableResult
{
    public string Name { get; set; }
    public List<string> Tables { get; set; }

    public override bool Equals(object obj)
    {
        if (obj is PdsTableResult)
        {
            var that = obj as PdsTableResult;
            return Name == that.Name && Tables.SequenceEqual(that.Tables);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}