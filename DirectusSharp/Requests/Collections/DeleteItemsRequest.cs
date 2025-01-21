using DirectusSharp.Response;

namespace DirectusSharp.Requests.Collections;

public abstract class DeleteItemsRequest : IDirectusRequest<Nothing>
{
    protected abstract string GetCollection();
    protected abstract string[] GetItemIds();

    public HttpRequestMessage GetMessage()
    {
        return new HttpRequestMessage(HttpMethod.Delete, $"/items/{GetCollection()}");
    }
    
    public object? GetMessageObject() => GetItemIds();
    public Type GetMessageObjectType() => typeof(string[]);
}
