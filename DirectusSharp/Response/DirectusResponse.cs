using System.Net;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace DirectusSharp.Response;

public class DirectusResponse<TData>
{
    [JsonIgnore] public HttpStatusCode Code { get; set; } = HttpStatusCode.OK;
    [JsonIgnore] public bool IsSuccess { get; set; } = false;
    [JsonIgnore] public HttpResponseHeaders Headers { get; set; } = null!;

    public TData? Data { get; set; } = default;
    public DirectusError[] Errors { get; set; } = [];

    public override string ToString()
    {
        return
            $"Response {{ Success = {IsSuccess}, Code = {Code}, Data = {Data}, Errors = {string.Join(", ", Errors.Select(x => x.Message))} }}";
    }
}