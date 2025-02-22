using DirectusSharp.Models;

namespace DirectusSharp.Requests.Users;

public class GetCurrentUserRequest : IDirectusRequest<User>
{
    public HttpRequestMessage GetMessage()
    {
        return new HttpRequestMessage(HttpMethod.Get, "/users/me");
    }

    public object? GetMessageObject() => null;
}