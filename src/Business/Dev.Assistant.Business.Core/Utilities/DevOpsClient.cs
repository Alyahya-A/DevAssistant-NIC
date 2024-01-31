using Dev.Assistant.Configuration;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System.Net;

namespace Dev.Assistant.Business.Core.Utilities;

/// <summary>
/// Base class that all client samples extend from.
/// </summary>
public class DevOpsClient
{
    public static readonly GitHttpClient GitClient;
    public static readonly TfvcHttpClient TfsClient;
    public static readonly WorkItemTrackingHttpClient TrackingClient;


    private static readonly Uri Url;

    public const string MicroProjectName = "MicroServices";
    private const string MicroScaopPath = "Core/Development";

    public const string ApiProjectName = "API";
    public const string ApiRepoName = "API";

    private const string queryPat = "-"; // Me
    private const string actionPat = "-"; // Me

    private static VssConnection _queryConnection;
    //private static VssConnection _actionConnection;

    // default constractor
    static DevOpsClient()
    {
        Url = new(Consts.TfsUrl);
        GitClient = QueryConnection.GetClient<GitHttpClient>();
        TfsClient = QueryConnection.GetClient<TfvcHttpClient>();
        TrackingClient = QueryConnection.GetClient<WorkItemTrackingHttpClient>();
    }

    public static VssConnection QueryConnection
    {
        get
        {
            if (_queryConnection == null)
            {
                //PatCredentials credentials = new("", _pat);
                //_connection = new(new Uri(_collectionUri), credentials);

                //VssHttpMessageHandler vssHandler = new(_credentials, VssClientHttpRequestSettings.Default.Clone());
                //string pat = Consts.UserSettings.PAT;
                string pat = queryPat;

                if (string.IsNullOrEmpty(pat))
                {
                    throw new ArgumentException("Personal access token not found. [QueryConnection]");
                }

                PatCredentials credentials = new("", pat);

                _queryConnection = new VssConnection(Url, credentials);
            }

            return _queryConnection;
        }
        private set
        {
            _queryConnection = value;
        }
    }

    //public static VssConnection ActionConnection
    //{
    //    get
    //    {
    //        if (_actionConnection == null)
    //        {
    //            //string pat = Consts.UserSettings.Pat;
    //            string pat = actionPat;

    //            if (string.IsNullOrEmpty(pat))
    //            {
    //                throw new ArgumentException("Personal access token not found. [ActionConnection]");
    //            }

    //            PatCredentials credentials = new("", pat);

    //            _actionConnection = new VssConnection(Url, credentials);
    //        }

    //        return _actionConnection;
    //    }
    //    private set
    //    {
    //        _actionConnection = value;
    //    }
    //}

    public static void Dispose()
    {
        //_actionConnection?.Dispose();
        _queryConnection?.Dispose();

        //ActionConnection?.Dispose();
        QueryConnection?.Dispose();
        GitClient?.Dispose();
        TfsClient?.Dispose();
        TrackingClient?.Dispose();
    }

    /// <summary>
    /// Returns the scope path, which is the path of the root folder of the micro without the project name
    /// </summary>
    public static string GetMicroScopePath(string microName) => $"{MicroScaopPath}/{microName}";

    /// <summary>
    /// Returns the full path, which is the path of the root folder of the micro with the project name
    /// </summary>
    public static string GetMicroFullPath(string microName) => $"{MicroProjectName}/{MicroScaopPath}/{microName}";

    /// <summary>
    /// Returns the service path, which is the path of the service file that end with '.cs'
    /// </summary>
    public static string GetMicroServicePath(string microName) => $"{MicroScaopPath}/{microName}/{microName}Service.cs";

}

/// <summary>
/// Same as VssBasicCredential, but doesn't throw when URL is a non SSL, i.e. http, URL.
/// </summary>
/// <inheritdoc cref="FederatedCredential"/>
internal sealed class PatCredentials : FederatedCredential
{
    public PatCredentials()
        : this((VssBasicToken)null)
    {
    }

    public PatCredentials(string userName, string password)
        : this(new VssBasicToken(new NetworkCredential(userName, password)))
    {
    }

    public PatCredentials(ICredentials initialToken)
        : this(new VssBasicToken(initialToken))
    {
    }

    public PatCredentials(VssBasicToken initialToken)
        : base(initialToken)
    {
    }

    public override VssCredentialsType CredentialType => VssCredentialsType.Basic;

    public override bool IsAuthenticationChallenge(IHttpResponse webResponse)
    {
        if (webResponse == null ||
            webResponse.StatusCode != HttpStatusCode.Found &&
            webResponse.StatusCode != HttpStatusCode.Found &&
            webResponse.StatusCode != HttpStatusCode.Unauthorized)
        {
            return false;
        }

        return webResponse.Headers.GetValues("WWW-Authenticate").Any(x => x.StartsWith("Basic", StringComparison.OrdinalIgnoreCase));
    }

    protected override IssuedTokenProvider OnCreateTokenProvider(Uri serverUrl, IHttpResponse response)
    {
        return new BasicAuthTokenProvider(this, serverUrl);
    }

    private sealed class BasicAuthTokenProvider : IssuedTokenProvider
    {
        public BasicAuthTokenProvider(IssuedTokenCredential credential, Uri serverUrl)
            : base(credential, serverUrl, serverUrl)
        {
        }

        protected override string AuthenticationScheme => "Basic";
        public override bool GetTokenIsInteractive => CurrentToken == null;
    }
}