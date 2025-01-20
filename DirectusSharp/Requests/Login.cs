using DirectusSharp.Auth;

namespace DirectusSharp.Requests;

public class LoginRequest : IDirectusRequest<LoginResponse>
{
    public required string Email { get; init; }
    public required string Password { get; init; }

    public AuthenticationMode Mode { get; init; } = AuthenticationMode.Json;

    public HttpRequestMessage GetMessage()
    {
        return new HttpRequestMessage(HttpMethod.Post, "/auth/login");
    }
}

public enum AuthenticationMode
{
    Json,
    Cookie,
    Session
}

public class LoginResponse
{
    public required string AccessToken { get; init; }
    public string? RefreshToken { get; init; }
    public int Expires { get; init; } = 0;
}