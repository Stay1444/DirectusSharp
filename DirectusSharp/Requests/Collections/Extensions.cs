using DirectusSharp.Response;

namespace DirectusSharp.Requests.Collections;

public static class Extensions
{
    private class GetGenericItemRequest<TItem> : GetItemRequest<TItem>
    {
        public required string Collection { get; init; }
        public required string Id { get; init; }
        protected override string GetCollection() => Collection;
        protected override string GetItemId() => Id;
    }

    private class CreateGenericItemRequest<TItem> : CreateItemRequest<TItem>
    {
        public required string Collection { get; init; }
        public required TItem Item { get; init; }
        protected override string GetCollection() => Collection;
        protected override TItem GetItem() => Item;
    }

    private class UpdateGenericItemRequest<TItem> : UpdateItemRequest<TItem>
    {
        public required string Collection { get; init; }
        public required TItem Item { get; init; }
        public required string Id { get; init; }
        
        protected override string GetCollection() => Collection;
        protected override TItem GetItem() => Item;
        protected override string GetItemId() => Id;
    }

    public static async Task<DirectusResponse<TItem>> GetItemAsync<TItem>(this IDirectus client, string collection, string itemId)
    {
        return await client.ExecuteAsync(new GetGenericItemRequest<TItem>()
        {
            Collection = collection,
            Id = itemId,
        });
    }
    public static Task<DirectusResponse<TItem>> GetItemAsync<TItem>(this IDirectus client, string collection, int itemId) => 
        GetItemAsync<TItem>(client, collection, itemId.ToString());
    public static Task<DirectusResponse<TItem>> GetItemAsync<TItem>(this IDirectus client, string collection, ulong itemId) => 
        GetItemAsync<TItem>(client, collection, itemId.ToString());
    public static Task<DirectusResponse<TItem>> GetItemAsync<TItem>(this IDirectus client, string collection, Guid itemId) => 
        GetItemAsync<TItem>(client, collection, itemId.ToString());

    public static async Task<DirectusResponse<TItem>> CreateItemAsync<TItem>(this IDirectus client, string collection,
        TItem item)
    {
        return await client.ExecuteAsync(new CreateGenericItemRequest<TItem>()
        {
            Collection = collection,
            Item = item,
        });
    }
    
    public static async Task<DirectusResponse<TItem>> UpdateItemAsync<TItem>(this IDirectus client, string collection,
        string itemId,
        TItem item)
    {
        return await client.ExecuteAsync(new UpdateGenericItemRequest<TItem>()
        {
            Collection = collection,
            Item = item,
            Id = itemId
        });
    }
    
    public static Task<DirectusResponse<TItem>> UpdateItemAsync<TItem>(this IDirectus client, string collection, int itemId, TItem item)
        => UpdateItemAsync(client, collection, itemId.ToString(), item);
    public static Task<DirectusResponse<TItem>> UpdateItemAsync<TItem>(this IDirectus client, string collection, ulong itemId, TItem item)
        => UpdateItemAsync(client, collection, itemId.ToString(), item);
    public static Task<DirectusResponse<TItem>> UpdateItemAsync<TItem>(this IDirectus client, string collection, Guid itemId, TItem item)
        => UpdateItemAsync(client, collection, itemId.ToString(), item);
}