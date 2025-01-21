namespace DirectusSharp.Requests.Collections;

public abstract class UpdateItemRequest<TItemType> : IDirectusRequest<TItemType>
{
    protected abstract string GetCollection();
    protected abstract TItemType GetItem();
    protected abstract string GetItemId();

    public HttpRequestMessage GetMessage()
    {
        return new HttpRequestMessage(HttpMethod.Patch, $"/items/{GetCollection()}/{GetItemId()}");
    }

    public object? GetMessageObject()
    {
        return GetItem();
    }

    public Type GetMessageObjectType()
    {
        return typeof(TItemType);
    }
}
