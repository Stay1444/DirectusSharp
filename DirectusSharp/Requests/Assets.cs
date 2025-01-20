namespace DirectusSharp.Requests;

public class GetAssetRaw : IDirectusRawRequest
{
    public required Guid Id { get; init; }

    public HttpRequestMessage GetMessage()
    {
        return new HttpRequestMessage(HttpMethod.Get, $"/assets/{Id}");
    }
}