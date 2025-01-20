namespace DirectusSharp.Requests;

public abstract class GetItemRequest<TItemType> : IDirectusRequest<TItemType>
{
    protected abstract string GetCollection();
    protected abstract string GetItemId();

    public HttpRequestMessage GetMessage()
    {
        return new HttpRequestMessage(HttpMethod.Get, $"/items/{GetCollection()}/{GetItemId()}");
    }
}

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
