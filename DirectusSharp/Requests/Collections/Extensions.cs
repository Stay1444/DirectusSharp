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

    private class DeleteGenericItemRequest : DeleteItemRequest
    {
        public required string Collection { get; init; }
        public required string Id { get; init; }

        protected override string GetCollection() => Collection;
        protected override string GetItemId() => Id;
    }
    
    private class DeleteMultipleGenericItemRequest : DeleteItemsRequest
    {
        public required string Collection { get; init; }
        public required string[] Ids { get; init; }

        protected override string GetCollection() => Collection;
        protected override string[] GetItemIds() => Ids;
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

    public static async Task<DirectusResponse<Nothing>> DeleteItemAsync(this IDirectus client, string collection,
        string itemId)
    {
        return await client.ExecuteAsync(new DeleteGenericItemRequest()
        {
            Collection = collection,
            Id = itemId,
        });
    }
    
    public static Task<DirectusResponse<Nothing>> DeleteItemAsync(this IDirectus client, string collection, int itemId)
        => DeleteItemAsync(client, collection, itemId.ToString());
    public static Task<DirectusResponse<Nothing>> DeleteItemAsync(this IDirectus client, string collection, ulong itemId)
        => DeleteItemAsync(client, collection, itemId.ToString());
    public static Task<DirectusResponse<Nothing>> DeleteItemAsync(this IDirectus client, string collection, Guid itemId)
        => DeleteItemAsync(client, collection, itemId.ToString());

    public static async Task<DirectusResponse<Nothing>> DeleteItemAsync(this IDirectus client, string collection,
        params IEnumerable<string> items)
    {
        return await client.ExecuteAsync(new DeleteMultipleGenericItemRequest()
        {
            Collection = collection,
            Ids = items.ToArray()
        });
    }
    
    public static Task<DirectusResponse<Nothing>> DeleteItemAsync(this IDirectus client, string collection, params IEnumerable<int> itemId)
        => DeleteItemAsync(client, collection, itemId.Select(x => x.ToString()));
    public static Task<DirectusResponse<Nothing>> DeleteItemAsync(this IDirectus client, string collection, params IEnumerable<ulong> itemId)
        => DeleteItemAsync(client, collection, itemId.Select(x => x.ToString()));
    public static Task<DirectusResponse<Nothing>> DeleteItemAsync(this IDirectus client, string collection, params IEnumerable<Guid> itemId)
        => DeleteItemAsync(client, collection, itemId.Select(x => x.ToString()));
}