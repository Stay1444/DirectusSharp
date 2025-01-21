namespace DirectusSharp.Requests.Collections;

public abstract class CreateItemRequest<TItemType> : IDirectusRequest<TItemType>
{
    protected abstract string GetCollection();
    protected abstract TItemType GetItem();

    public HttpRequestMessage GetMessage()
    {
        return new HttpRequestMessage(HttpMethod.Post, $"/items/{GetCollection()}");
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
