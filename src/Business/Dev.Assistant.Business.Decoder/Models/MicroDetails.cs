namespace Dev.Assistant.Business.Decoder.Models;

/// <summary>
/// Represents detailed information about a microservice.
/// </summary>
public class MicroDetails
{
    /// <summary>
    /// Description of the microservice.
    /// </summary>
    public string Description { get; set; }

    ///// <summary>
    ///// Category name of the microservice.
    ///// </summary>
    //public string CategoryName { get; set; }

    /// <summary>
    /// List of exceptions that the microservice might throw.
    /// </summary>
    public List<MicroException> MicroExceptions { get; set; }
}
