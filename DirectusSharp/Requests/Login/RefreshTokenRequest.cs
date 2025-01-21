using System.Text.Json.Serialization;

namespace DirectusSharp.Requests.Login;

public class RefreshTokenRequest : IDirectusRequest<LoginResponse>
{
    [JsonPropertyName("refresh_token")] public required string RefreshToken { get; init; }

    public AuthenticationMode Mode { get; init; } = AuthenticationMode.Json;

    public HttpRequestMessage GetMessage()
    {
        return new HttpRequestMessage(HttpMethod.Post, "/auth/refresh");
    }
}