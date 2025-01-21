using DirectusSharp.Response;

namespace DirectusSharp.Requests.Collections;

public abstract class DeleteItemRequest : IDirectusRequest<Nothing>
{
    protected abstract string GetCollection();
    protected abstract string GetItemId();

    public HttpRequestMessage GetMessage()
    {
        return new HttpRequestMessage(HttpMethod.Delete, $"/items/{GetCollection()}/{GetItemId()}");
    }
    
    public object? GetMessageObject() => null;
}
