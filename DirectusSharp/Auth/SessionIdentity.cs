namespace DirectusSharp.Auth;

public class SessionIdentity : IDirectusIdentity
{
    public required string Token { get; init; }
}