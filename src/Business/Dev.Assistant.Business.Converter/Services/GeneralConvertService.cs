using Dev.Assistant.Business.Core.Extensions;
using Dev.Assistant.Business.Core.Utilities;
using Dev.Assistant.Business.Decoder.Models;
using Dev.Assistant.Business.Decoder.Services;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Text;

namespace Dev.Assistant.Business.Converter.Services;

public static class GeneralConvertService
{

    public static string ConvertToCSharp(string code, string suffixTxt, bool prepareXml)
    {
        List<ClassModel> classes = ModelExtractionService.GetClassesByCode(code, new GetClassesOptions() { DataTypeAsPascalCase = false });

        StringBuilder sb = new();
        string suffix = suffixTxt.Trim();

        foreach (var model in classes)
        {
            if (prepareXml)
            {
                sb.AppendLine("/// <summary>");

                if (!string.IsNullOrWhiteSpace(model.ClassDesc))
                {
                    sb.AppendLine($"/// {model.ClassDesc}");
                }
                else
                {
                    //sb.AppendLine($"/// ");
                    sb.AppendLine($"/// {model.Name.SplitByCapitalLetter().FirstCharToUpper()}");
                }

                sb.AppendLine("/// </summary>");
            }

            if (!string.IsNullOrWhiteSpace(suffix))
                sb.AppendLine($"public class {model.Name}{suffix} {"{"}");
            else
                sb.AppendLine($"public class {model.Name} {"{"}");

            sb.AppendLine("");

            foreach (var prop in model.Properties)
            {
                string datatype = prop.DataType;

                //if (char.IsUpper(datatype[0]))
                //{
                if (prop.Name.Contains("suspendedSideList"))
                {
                }


                switch (prop.DataType.ToLower().Replace("?", ""))
                {
                    case "integer" or "short":

                        datatype = "int";

                        if (prop.IsNullable)
                            datatype += "?";

                        break;

                    case "double" or "float":
                        datatype = "double";

                        if (prop.IsNullable)
                            datatype += "?";

                        break;

                    case "decimal":
                        datatype = prop.DataType.ToLower();
                        break;

                    case "string":
                        datatype = prop.DataType.ToLower();
                        break;

                    case "date":
                        datatype = "DateTime";

                        if (prop.IsNullable)
                            datatype += "?";

                        break;

                    case "boolean" or "bool":
                        datatype = "bool";

                        if (prop.IsNullable)
                            datatype += "?";

                        break;

                        //case "float":
                        //    datatype = "bool";
                        //    break;
                }

                if (prepareXml)
                {
                    sb.AppendLine("/// <summary>");

                    // English
                    if (!string.IsNullOrWhiteSpace(prop.DescEn))
                    {
                        sb.AppendLine($"/// {prop.DescEn.RemoveAppendedLine()}<br/>");
                    }
                    else
                    {
                        //sb.AppendLine($"/// <br/>");
                        sb.AppendLine($"/// {prop.Name.SplitByCapitalLetter().FirstCharToUpper().RemoveAppendedLine()}<br/>");
                    }

                    // Arabic
                    if (!string.IsNullOrWhiteSpace(prop.DescAr))
                    {
                        sb.AppendLine($"/// {prop.DescAr.RemoveAppendedLine()}");
                    }
                    else
                    {
                        sb.AppendLine("///");
                    }

                    sb.AppendLine("/// </summary>");
                }

                if (prop.IsRequired)
                    sb.AppendLine("[Required]");

                if (prop.IsPrimitive())
                {
                    sb.AppendLine($"public {datatype} {prop.Name} {"{ get; set; }"}");
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(suffix))
                        sb.AppendLine($"public {DevHelper.AddSuffixToDatatype(datatype, suffix)} {prop.Name} {"{ get; set; }"}");
                    else
                        sb.AppendLine($"public {datatype} {prop.Name} {"{ get; set; }"}");
                }

                sb.AppendLine("");
            }

            sb.AppendLine("}");
        }

        CSharpParseOptions opt = new();
        opt.WithLanguageVersion(LanguageVersion.CSharp10);

        //var a = TypeDescriptor.GetConverter("int");
        //var b = a.ConvertFromString(sb.ToString());

        var tree = CSharpSyntaxTree.ParseText(sb.ToString(), options: opt);
        var root = tree.GetRoot().NormalizeWhitespace();
        return root.ToFullString();
    }
}
