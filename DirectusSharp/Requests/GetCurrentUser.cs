using DirectusSharp.Models;

namespace DirectusSharp.Requests;

public class GetCurrentUserRequest : IDirectusRequest<User>
{
    public HttpRequestMessage GetMessage()
    {
        return new HttpRequestMessage(HttpMethod.Get, "/users/me");
    }
}