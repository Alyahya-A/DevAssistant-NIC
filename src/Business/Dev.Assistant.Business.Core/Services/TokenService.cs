using Dev.Assistant.Business.Core.Utilities;
using Dev.Assistant.Configuration;
using Dev.Assistant.Configuration.Models;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Text.Json;

namespace Dev.Assistant.Business.Core.Services;

/// <summary>
/// Service for managing access tokens.
/// </summary>
public static class TokenService
{
    #region Public Methods

    /// <summary>
    /// Gets the access token from cache. If not found or expired, then gets a fresh one from the Auth server.
    /// </summary>
    /// <param name="accessToken">The retrieved or refreshed access token.</param>
    /// <returns>True if the access token is retrieved or refreshed, false if it's the first initialization.</returns>
    public static bool GetAccessToken(out string accessToken)
    {
        Log.Logger.Information("GetAccessToken Called");

        // If it's the first run, check for a token in user settings.
        // If the token is available, but the AuthServerClient is not initialized yet,
        // return false to handle it in the RefreshToken() function.
        bool firstInit = false;

        if (ApiClient.Client == null)
        {
            ApiClient.Init();
            firstInit = true;
        }

        var token = Consts.UserSettings.Token;

        if (token != null && !string.IsNullOrWhiteSpace(token.AccessToken) && token.ValidTo > DateTime.Now)
        {
            accessToken = token.AccessToken;
            return !firstInit;
        }

        var newToken = GetTokenResponse();
        Consts.UserSettings.Token = newToken;

        accessToken = newToken.AccessToken;
        return false;
    }

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Gets the access token from NIC Auth Server.
    /// </summary>
    /// <returns>The <see cref="TokenResponse"/> containing the access token.</returns>
    private static TokenResponse GetTokenResponse()
    {
        Log.Logger.Information("GetTokenResponse Called");

        var payload = new
        {
            username = "apiadmin",
            password = "Aa@123456",
            grant_type = "password"
        };

        // Calls the token endpoint to get the access token.
        var response = ApiClient.Client.PostAsJsonAsync("oauth/token", payload).Result;

        // Makes sure the request went successfully.
        response.EnsureSuccessStatusCode();

        // Reads the response body into a string.
        var json = response.Content.ReadAsStringAsync().Result;

        // Parsing the response body into a json object.
        var token = JsonSerializer.Deserialize<TokenResponse>(json);

        // Get the expiration date from the AccessToken.
        JwtSecurityTokenHandler tokenHandler = new();
        token.ValidTo = tokenHandler.ReadJwtToken(token.AccessToken).ValidTo.ToLocalTime();

        return token;
    }

    #endregion Private Methods
}