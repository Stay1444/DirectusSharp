namespace DirectusSharp.Requests.Assets;

public class GetAssetRawRequest : IDirectusRawRequest
{
    public required Guid Id { get; init; }

    public HttpRequestMessage GetMessage()
    {
        return new HttpRequestMessage(HttpMethod.Get, $"/assets/{Id}");
    }
}