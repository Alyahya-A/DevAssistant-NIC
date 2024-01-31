using AuthServer;
using Dev.Assistant.Business.Core.Utilities;
using Dev.Assistant.Configuration;
using Serilog;
using System.Net.Http.Headers;

namespace Dev.Assistant.Business.Core.Services;

/// <summary>
/// Service for handling authentication-related operations.
/// </summary>
public static class AuthService
{
    private static AuthServerClient Client { get; set; }

    /// <summary>
    /// Refreshes the token and creates a new instance for AuthServerClient.
    /// </summary>
    public static void RefreshToken()
    {
        try
        {
            Log.Logger.Information("RefreshToken Called");

            // Get the Token. If true, that means the saved token in userSettings is valid.
            if (TokenService.GetAccessToken(out string accessToken))
                return;

            // Assign the token to ApiClient.Client
            ApiClient.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Initialize a new AuthServerClient with ApiClient.Client that has the new token
            Client = new AuthServerClient(Consts.AuthServerUrl, ApiClient.Client);

            Log.Logger.Information("Token refreshed successfully.");
        }
        catch (Exception ex)
        {
            Log.Logger.Error("Error refreshing token: {error}", ex.Message);

            throw;
        }
    }

    /// <summary>
    /// Gets account (contract) information for the specified username.
    /// </summary>
    /// <param name="username">The username for which to retrieve account information.</param>
    /// <returns>The account information for the specified username.</returns>
    public static UserReturnModel GetAccountInfo(string username)
    {
        try
        {
            Log.Logger.Information("GetAccountInfo Called - paramValue: {username}", username);

            RefreshToken();

            return Client.AccountGETAsync(username).Result;
        }
        catch (Exception ex)
        {
            Log.Logger.Error("Error getting account info: {error}", ex.Message);

            throw;
        }
    }
}