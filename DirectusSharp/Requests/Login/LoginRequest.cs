namespace DirectusSharp.Requests.Login;

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
