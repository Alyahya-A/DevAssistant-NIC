using Dev.Assistant.Business.Core.Models;

namespace Dev.Assistant.Business.Core.Utilities;

public class DevEvents
{
    static DevEvents()
    {
        Events.Add(None);
        Events.Add(Testing);

        Events.Add(AppStartup);
        Events.Add(UpdateTheApp);
        Events.Add(RestartApp);
        Events.Add(ReleaseNotes);
        Events.Add(SessionExpired);
        Events.Add(ClosingApp);
        Events.Add(UpdateAssets);

        Events.Add(CGetUserInfoAuthServer);
        Events.Add(MWByLastActivity);
        Events.Add(MWByCRNum);
        Events.Add(MWByTitle);
        Events.Add(MWReviewedByMe);

        Events.Add(RCompareModel);
        Events.Add(RCompareJson);
        Events.Add(RCompareSql);
        Events.Add(RCompareText);
        Events.Add(RGenerateApiIG);
        Events.Add(RGetPropsInfo);
        Events.Add(RGetDbLength);
        Events.Add(RGetAllMethods);
        Events.Add(RCodeToSql);
        Events.Add(RSqlToCode);
        Events.Add(RGetAllServices);
        Events.Add(RCheckRules);
        Events.Add(RXmlToJson);
        Events.Add(RJsonToXml);
        Events.Add(RJsonToCsharp);
        Events.Add(RReviewReport);
        Events.Add(RGenerateAllIGs);
        Events.Add(RMoveCommonsettings);
        Events.Add(RVbToCSharp);
        Events.Add(RGetPackageReferences);
        Events.Add(RConvertByteArrayToImage);
        Events.Add(RConvertBase64ToImage);
        Events.Add(RConvertBlobToImage);
        Events.Add(RDecryptToken);
        Events.Add(RGenerateNdbJson);
        Events.Add(RGenerateMicroIG);
        Events.Add(RConvertVbToCharpClass);

        Events.Add(UGetTableNames);
        Events.Add(UGetAuthComment);
        Events.Add(UGetIDTypes);

        Events.Add(PrCreateTaskReview);
        Events.Add(PrGetPullRequests);
        Events.Add(PrGenerateIGFromTfs);
        Events.Add(PrCheckRules);
        Events.Add(PrGetPdsTable);

        Events.Add(SAppOpned);
        Events.Add(SSearching);
    }

    public static List<Event> Events = new();

    #region App

    public static Event None = new(1, "All events");
    public static Event Testing = new(0, "For testing only");

    public static Event AppStartup = new(1000, "Startup");
    public static Event UpdateTheApp = new(1001, "Update the app by clicking Update button");
    public static Event RestartApp = new(1002, "Restart app");
    public static Event ReleaseNotes = new(1003, "Release notes clicked");
    public static Event SessionExpired = new(1004, "The session has been expired");
    public static Event ClosingApp = new(1005, "Close app");
    public static Event UpdateAssets = new(1006, "Update assets files such as IG template or StaffInfo, etc..");

    public static Event CGetUserInfoAuthServer = new(2000, "Get user info from AuthServer");

    #endregion App

    #region MyWork

    public static Event MWByLastActivity = new(3000, "By last activity");
    public static Event MWByCRNum = new(3001, "By CR Num");
    public static Event MWByTitle = new(3002, "By title");
    public static Event MWReviewedByMe = new(3003, "Reviewed by me");

    #endregion MyWork

    #region Reviewme

    public static Event RCompareModel = new(4000, "Compare two models");
    public static Event RCompareJson = new(4001, "Compare two json");
    public static Event RCompareSql = new(4002, "Compare two SQL");
    public static Event RCompareText = new(4003, "Compare two text");
    public static Event RGenerateApiIG = new(4004, "Generate Api IG");
    public static Event RGetPropsInfo = new(4005, "Get props info for IG");
    public static Event RGetDbLength = new(4006, "Get Db length");
    public static Event RGetAllMethods = new(4007, "Get all methods in a class");
    public static Event RCodeToSql = new(4008, "Convert code to SQL");
    public static Event RSqlToCode = new(4009, "Convert SQL to code");
    public static Event RGetAllServices = new(4010, "Get all services in specefic contracts");
    public static Event RCheckRules = new(4011, "Check rules and spelling for RestApi services");
    public static Event RXmlToJson = new(4012, "Convert Xml to Json");
    public static Event RJsonToXml = new(4013, "Convert Json to Xml");
    public static Event RJsonToCsharp = new(4014, "Convert Json to C# class");
    public static Event RReviewReport = new(4015, "Check rules, BL and controller and for RestApi services");
    public static Event RGenerateAllIGs = new(4016, "Generate all IGs for a contract");
    public static Event RMoveCommonsettings = new(4017, "Move commonsettings to sln");
    public static Event RVbToCSharp = new(4018, "Convert VB to C#");
    public static Event RGetPackageReferences = new(4019, "Get all package references (NuGet) from .csproj");
    public static Event RConvertByteArrayToImage = new(4020, "Convert Byte array to image");
    public static Event RConvertBase64ToImage = new(4021, "Convert Base64 to image");
    public static Event RConvertBlobToImage = new(4022, "Convert Blob to image");
    public static Event RDecryptToken = new(4023, "Decrypt Jwt Token");
    public static Event RGenerateNdbJson = new(4024, "Generate Ndb json");
    public static Event RGenerateMicroIG = new(4025, "Generate Micro IG");
    public static Event RConvertVbToCharpClass = new(4026, "Convert VB class to C# class");

    #endregion Reviewme

    #region UtilitiesOps

    public static Event UGetTableNames = new(5000, "Get pds tables names for CM");
    public static Event UGetAuthComment = new(5001, "Get AuthServer comment for CM");
    public static Event UGetIDTypes = new(5002, "Get ID types for a speceifc Micro");

    #endregion UtilitiesOps

    #region PrReview

    public static Event PrCreateTaskReview = new(6000, "Create task review");
    public static Event PrGetPullRequests = new(6001, "Get all pull requests");
    public static Event PrGenerateIGFromTfs = new(6002, "Generate IG from TFS by pull request");
    public static Event PrCheckRules = new(6003, "Check rules and spelling from TFS by pull request");
    public static Event PrGetPdsTable = new(6004, "Get PDS Tabels from TFS by pull request");

    #endregion PrReview

    #region Staff

    public static Event SAppOpned = new(7001, "Staff app oppned");
    public static Event SSearching = new(7002, "Search in Staff app");

    #endregion Staff

    #region TasksBoard

    public static Event TAppOpned = new(8000, "Tasksboard app oppned");

    #endregion TasksBoard
}