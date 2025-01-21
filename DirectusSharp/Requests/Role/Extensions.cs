using DirectusSharp.Response;

namespace DirectusSharp.Requests.Role;

public static class Extensions
{
    public static async Task<DirectusResponse<Models.Role>> GetRoleAsync(this DirectusClient client, Guid roleId)
    {
        return await client.ExecuteAsync(new GetRoleRequest()
        {
            Id = roleId
        });
    }
}