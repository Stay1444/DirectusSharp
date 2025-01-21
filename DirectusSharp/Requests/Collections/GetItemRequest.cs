namespace DirectusSharp.Requests.Collections;

public abstract class GetItemRequest<TItemType> : IDirectusRequest<TItemType>
{
    protected abstract string GetCollection();
    protected abstract string GetItemId();

    public HttpRequestMessage GetMessage()
    {
        return new HttpRequestMessage(HttpMethod.Get, $"/items/{GetCollection()}/{GetItemId()}");
    }
}
