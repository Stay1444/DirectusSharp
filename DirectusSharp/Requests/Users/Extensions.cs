using DirectusSharp.Models;
using DirectusSharp.Response;

namespace DirectusSharp.Requests.Users;

public static class Extensions
{
    public static async Task<DirectusResponse<User>> GetCurrentUserAsync(this DirectusClient client)
    {
        return await client.ExecuteAsync(new GetCurrentUserRequest());
    }
}