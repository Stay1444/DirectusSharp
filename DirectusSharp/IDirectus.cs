using DirectusSharp.Auth;
using DirectusSharp.Requests;
using DirectusSharp.Response;

namespace DirectusSharp;

public interface IDirectus
{
    public IDirectusIdentity Identity { get; set; }

    public Task<DirectusResponse<TResponse>> ExecuteAsync<TResponse>(IDirectusRequest<TResponse> request,
        CancellationToken cancellationToken = default);

    public Task<HttpResponseMessage> ExecuteAsync(IDirectusRawRequest request,
        CancellationToken cancellationToken = default);
}