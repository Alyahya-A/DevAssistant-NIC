using Dev.Assistant.Configuration;
using Serilog;
using System.Net.Http.Headers;

namespace Dev.Assistant.Business.Core.Utilities;

/// <summary>
/// Utility class for managing an API client.
/// </summary>
public static class ApiClient
{
    /// <summary>
    /// The instance of <see cref="HttpClient"/> for making API AuthServer requests.
    /// </summary>
    public static HttpClient Client;

    /// <summary>
    /// Initializes the API client.
    /// </summary>
    public static void Init()
    {
        Log.Logger.Information("ApiClientInit Called");

        // Allow any certificate validation
        HttpClientHandler handler = new()
        {
            ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
        };

        // Get saved Token for the first time. Checking will be in AuthService (RefreshToken method)
        var token = Consts.UserSettings.Token;

        // Initialize the HttpClient instance
        Client = new HttpClient(handler)
        {
            BaseAddress = new Uri(Consts.AuthServerUrl)
        };

        // Set authorization header if a token is available
        if (token != null)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
        }

        // Set accepted media type
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
}