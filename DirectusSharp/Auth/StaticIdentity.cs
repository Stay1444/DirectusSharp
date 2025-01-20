namespace DirectusSharp.Auth;

public class StaticIdentity : IDirectusIdentity
{
    public required string Token { get; init; }
}