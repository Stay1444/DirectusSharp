namespace DirectusSharp.Requests.Login;

public class LoginResponse
{
    public required string AccessToken { get; init; }
    public string? RefreshToken { get; init; }
    public int Expires { get; init; } = 0;
}
