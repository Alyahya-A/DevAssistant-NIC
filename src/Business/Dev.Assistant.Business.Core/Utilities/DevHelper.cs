namespace Dev.Assistant.Business.Core.Utilities;

public static class DevHelper
{

    public static string AddSuffixToDatatype(string datatype, string suffix)
    {
        if (!datatype.Contains("List<") && !datatype.Contains("[]"))
            return datatype + suffix;

        bool isList = datatype.Contains("List<");

        string tempDatatype = datatype.Replace("List<", "").Replace(">", "").Replace("[]", "").Trim();

        tempDatatype += suffix;

        if (isList)
            tempDatatype = $"List<{tempDatatype}>";
        else
            tempDatatype = $"{tempDatatype}[]";


        return tempDatatype;
    }

}
