using Dev.Assistant.Business.Core.Services;

namespace Dev.Assistant.Business.Core.Models;

/// <summary>
/// Represents a property in a class.
/// </summary>
public class Property
{
    /// <summary>
    /// Gets or sets the name of the property.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the data type of the property.
    /// </summary>
    public string DataType { get; set; }

    /// <summary>
    /// Gets or sets the description of the property in Arabic.
    /// </summary>
    public string DescAr { get; set; }

    /// <summary>
    /// Gets or sets the description of the property in English.
    /// </summary>
    public string DescEn { get; set; }

    /// <summary>
    /// Gets or sets the name of the model to which the property belongs.
    /// </summary>
    public string ModelName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the property is required.
    /// </summary>
    public bool IsRequired { get; set; } = false;

    /// <summary>
    /// Gets or sets remarks used in a generated word report (Misspelling Details) under each property.
    /// </summary>
    public string Remarks { get; set; }

    /// <summary>
    /// Gets or sets long remarks used in a generated word report summary at the top (Remarks (rules) Summary).
    /// </summary>
    public string LongRemarks { get; set; }

    // Below properties are used in model comparison

    /// <summary>
    /// Gets or sets whether the data type matches during model comparison.
    /// </summary>
    public string IsDataTypeMatch { get; set; }

    /// <summary>
    /// Gets or sets the data types used in model comparison.
    /// </summary>
    public string DataTypes { get; set; }

    /// <summary>
    /// Gets or sets a similar name used in model comparison.
    /// </summary>
    public string SimilarName { get; set; }

    /// <summary>
    /// Gets or sets whether the name matches during model comparison.
    /// </summary>
    public string IsNameMatch { get; set; }

    /// <summary>
    /// Gets or sets whether the datatype is nullable "?".
    /// </summary>
    public bool IsNullable { get; set; }

    /// <summary>
    /// Check if prop datatype is primitive
    /// </summary>
    /// <returns></returns>
    public bool IsPrimitive() => ValidationService.IsPrimitiveDatatype(DataType);


    public string DateTypeWithoutListOrArray()
    {
        return DataType.Replace("List<", "").Replace(">", "").Replace("[]", "").Trim();
    }
}