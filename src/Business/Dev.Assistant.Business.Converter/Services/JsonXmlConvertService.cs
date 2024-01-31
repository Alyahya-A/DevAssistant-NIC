using Dev.Assistant.Business.Core.DevErrors;
using Newtonsoft.Json;
using Serilog;
using System.Xml;

// Error code start with 5000

namespace Dev.Assistant.Business.Converter.Services;

/// <summary>
/// Service for converting between XML and JSON formats.
/// </summary>
public static class JsonXmlConvertService
{
    #region Public Methods

    /// <summary>
    /// Converts XML to JSON.
    /// </summary>
    /// <param name="xml">XML text.</param>
    /// <returns>Formatted JSON text.</returns>
    /// <exception cref="DevAssistantException">Thrown if conversion fails.</exception>
    public static string XmlToJSON(string xml)
    {
        Log.Logger.Information("XmlToJSON Called");

        try
        {
            XmlDocument xmlDocument = new();
            xmlDocument.LoadXml(xml);
            string json = JsonConvert.SerializeXmlNode(xmlDocument, Newtonsoft.Json.Formatting.Indented, omitRootObject: false);

            return json;
        }
        catch (Exception ex)
        {
            Log.Logger.Error("Error converting XML to JSON. Input XML: {xml}, Details: {message}", xml, ex.Message);

            throw DevErrors.Converter.E5001XmlToJsonConversionError;
        }
    }

    /// <summary>
    /// Converts JSON to XML.
    /// </summary>
    /// <param name="json">JSON text.</param>
    /// <returns>XML text.</returns>
    /// <exception cref="DevAssistantException">Thrown if conversion fails.</exception>
    public static string JsonToXml(string json)
    {
        Log.Logger.Information("JsonToXml Called");

        try
        {
            var doc = JsonConvert.DeserializeXNode(json);
            return doc.ToString();
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "Error during Json to XML conversion");

            try
            {
                // Try to deserialize JSON with a specified root element ("root")
                var doc = JsonConvert.DeserializeXNode(json, "root");
                return doc.ToString();
            }
            catch (Exception innerEx)
            {
                Log.Logger.Error(innerEx, "Inner Error during Json to XML conversion");

                // If the nested deserialization with a specified root element also fails, throw an exception
                throw DevErrors.Converter.E5002JsonToXmlConversionError;
            }
        }
    }

    #endregion Public Methods
}