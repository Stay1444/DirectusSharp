namespace DirectusSharp.Requests.Collections;

public abstract class GetItemsRequest<TItemType> : IDirectusRequest<TItemType[]>
{
    protected abstract string GetCollection();
    public HttpRequestMessage GetMessage()
    {
        return new HttpRequestMessage(HttpMethod.Get, $"/items/{GetCollection()}");
    }

    public object? GetMessageObject() => null;
}
