namespace DirectusSharp.Requests.Role;

public class GetRoleRequest : IDirectusRequest<Models.Role>
{
    public required Guid Id { get; init; }

    public HttpRequestMessage GetMessage()
    {
        return new HttpRequestMessage(HttpMethod.Get, $"/roles/{Id}");
    }
}