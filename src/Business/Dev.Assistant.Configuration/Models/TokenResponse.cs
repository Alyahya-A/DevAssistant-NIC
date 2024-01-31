using System.Text.Json.Serialization;

namespace Dev.Assistant.Configuration.Models;

public class TokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonPropertyName("expires_on")]
    public long ExpiresOn { get; set; }

    [JsonIgnore]
    public DateTime ValidTo { get; set; }

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }
}