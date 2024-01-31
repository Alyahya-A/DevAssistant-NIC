using Dev.Assistant.Business.Core.Enums;

namespace Dev.Assistant.Business.Decoder.Models;

public class GetClassesOptions
{
    public GetClassesOptions()
    {
        // default values
        CheckSpellingAndRules = false;
        ThrowException = false;
        DataTypeAsPascalCase = false;
        IncludProperties = true;
        IncludeFunctions = false;
        ProjectType = ProjectType.RestApi;
    }

    /// <summary>
    /// To check the rules of the Properties for RestApi. Default is false
    /// </summary>
    public bool CheckSpellingAndRules { get; set; }

    /// <summary>
    /// Indicator for throwing an exception if there is no Xml found for a specific prop. Default is false<br/>
    /// Used by Reader app
    /// </summary>
    public bool ThrowException { get; set; }

    ///// <summary>
    ///// Indicator for getting only the basic info for Model and Property (Prop name, type and descriptions only). Default is false <br/>
    ///// if it is Supplied, the other Options will be ignored (CheckSpelling and ThrowException).
    ///// Used by Differences app
    ///// </summary>
    //public bool BaiscInfoOnly { get; set; }

    /// <summary>
    /// Indicate for return data type as PascalCase, ex: int > Int. Default is false <br/>
    /// Used by Reader app
    /// </summary>
    public bool DataTypeAsPascalCase { get; set; }

    ///// <summary>
    ///// Indicate for return data type as PascalCase, ex: int > Int. Default is false <br/>
    ///// Used by Reader app
    ///// </summary>
    //public bool IsVB { get; set; }

    public string ControllerName { get; set; } = string.Empty;

    public bool AddStarForRequiredProp { get; set; }

    public bool ShowErrorCode { get; set; }


    public bool IncludProperties { get; set; }


    public bool IncludeFunctions { get; set; }


    public ProjectType ProjectType { get; set; }



    public override string ToString()
    {
        return $"CheckSpelling: {CheckSpellingAndRules}, ThrowException: {ThrowException}, DataTypeAsPascalCase: {DataTypeAsPascalCase}, ControllerName: {ControllerName}, AddStarForRequiredProp: {AddStarForRequiredProp}, ShowErrorCode: {ShowErrorCode}";
    }
}

public class GetApiExceptionOptions
{
    public GetApiExceptionOptions()
    {
    }

    // TODO: complate it هذي الفكرة تعثرت :(
    /// <summary>
    /// Return DevAssistantException as a list<br/>
    /// </summary>
    public bool IsReport { get; set; }
}

public class GetServiceDescOptions
{
    public GetServiceDescOptions()
    {
    }

    /// <summary>
    /// Return DevAssistantException as a list<br/>
    /// </summary>
    public bool IsReport { get; set; }
}

public class GetAllMethodsOptions
{
    public GetAllMethodsOptions()
    {
        GetDBType = false;
    }


    /// <summary>
    /// Is controller method for Api to check if there is "ActionResult" word after public
    /// </summary>
    public bool IsControllerMethod { get; set; }

    public bool GetDBType { get; set; }

    // after refactoring GetCSharpFunctions we don't need it anymore,
    // before we if there is 1+ classes we must get each class with its body, then for each class we will call GetFunctions
    //public bool CanHasMultiClasses { get; set; }

    /// <summary>
    /// Defualt returns public and protected methods only. Also, returns static methods<br/>
    /// if ture it will return all methods (public, private, protected, and internal)
    /// </summary>
    public bool ListAll { get; set; }
}

