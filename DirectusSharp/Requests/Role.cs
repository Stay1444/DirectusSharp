using DirectusSharp.Models;

namespace DirectusSharp.Requests;

public class GetRoleRequest : IDirectusRequest<Role>
{
    public required Guid Id { get; init; }

    public HttpRequestMessage GetMessage()
    {
        return new HttpRequestMessage(HttpMethod.Get, $"/roles/{Id}");
    }
}