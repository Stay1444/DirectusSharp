using System.Text.Json.Serialization;
using DirectusSharp.Requests.Login;

namespace DirectusSharp.Auth;

public class TemporaryIdentity : IDirectusIdentity
{
    [JsonPropertyName("access_token")] public required string AccessToken { get; init; }

    [JsonPropertyName("refresh_token")] public required string RefreshToken { get; init; }

    [JsonPropertyName("expires")] public int ExpiresIn { get; init; } = 0;
    
    public static explicit operator TemporaryIdentity(LoginResponse loginResponse) => new()
    {
        AccessToken = loginResponse.AccessToken,
        RefreshToken = loginResponse.RefreshToken ?? string.Empty,
        ExpiresIn = loginResponse.Expires
    };
}