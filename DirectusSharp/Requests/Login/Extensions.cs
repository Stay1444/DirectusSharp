using DirectusSharp.Response;

namespace DirectusSharp.Requests.Login;

public static class Extensions
{
    public static async Task<DirectusResponse<LoginResponse>> LoginAsync(this IDirectus client, LoginRequest loginRequest)
    {
        return await client.ExecuteAsync(loginRequest);
    }

    public static async Task<DirectusResponse<LoginResponse>> LoginAsync(this IDirectus client, string email,
        string password, AuthenticationMode mode = AuthenticationMode.Json)
    {
        return await client.ExecuteAsync(new LoginRequest()
        {
            Email = email,
            Password = password,
            Mode = mode
        });
    }

    public static async Task<DirectusResponse<LoginResponse>> RefreshTokenAsync(this IDirectus client, string refreshToken, AuthenticationMode mode = AuthenticationMode.Json)
    {
        return await client.ExecuteAsync(new RefreshTokenRequest()
        {
            RefreshToken = refreshToken,
            Mode = mode
        });
    }
}