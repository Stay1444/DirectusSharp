using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using DirectusSharp.Auth;
using DirectusSharp.Requests;
using DirectusSharp.Response;
using Microsoft.Extensions.Logging;

namespace DirectusSharp;

public class DirectusClient : IDirectus
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<DirectusClient>? _logger;
    private readonly JsonSerializerOptions _serializerOptions;

    private DirectusClient(IDirectusIdentity identity, HttpClient httpClient, ILogger<DirectusClient>? logger = null)
    {
        Identity = identity;
        _httpClient = httpClient;
        _logger = logger;

        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower) }
        };
    }

    public IDirectusIdentity Identity { get; set; }

    public async Task<DirectusResponse<TResponse>> ExecuteAsync<TResponse>(IDirectusRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        var message = request.GetMessage();
        _logger?.LogTrace("Executing {method} {requestUri}", message.Method, message.RequestUri);

        message.Headers.Authorization = Identity switch
        {
            StaticIdentity staticIdentity => new AuthenticationHeaderValue("Bearer", staticIdentity.Token),
            TemporaryIdentity temporaryIdentity => new AuthenticationHeaderValue("Bearer",
                temporaryIdentity.AccessToken),
            _ => message.Headers.Authorization
        };
        
        if (request.GetMessageObject() is not null)
        {
            message.Content = new StringContent(JsonSerializer.Serialize(request.GetMessageObject(), request.GetMessageObjectType(), _serializerOptions),
                Encoding.UTF8,
                "application/json");
        }

        var response = await _httpClient.SendAsync(message, HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);

        var responseData =
            await response.Content.ReadFromJsonAsync<DirectusResponse<TResponse>>(_serializerOptions,
                cancellationToken) ??
            new DirectusResponse<TResponse>
            {
                Data = default
            };

        responseData.IsSuccess = response.IsSuccessStatusCode;
        responseData.Code = response.StatusCode;
        responseData.Headers = response.Headers;

        return responseData;
    }

    public async Task<HttpResponseMessage> ExecuteAsync(IDirectusRawRequest request,
        CancellationToken cancellationToken = default)
    {
        var message = request.GetMessage();
        _logger?.LogTrace("Executing {method} {requestUri}", message.Method, message.RequestUri);

        message.Headers.Authorization = Identity switch
        {
            StaticIdentity staticIdentity => new AuthenticationHeaderValue("Bearer", staticIdentity.Token),
            TemporaryIdentity temporaryIdentity => new AuthenticationHeaderValue("Bearer",
                temporaryIdentity.AccessToken),
            _ => message.Headers.Authorization
        };

        var response = await _httpClient.SendAsync(message, HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);

        return response;
    }

    /// <summary>
    ///     Creates a Directus Client
    /// </summary>
    /// <param name="identity">The identity to use</param>
    /// <param name="httpClient">The HttpClient to use. It must have the BaseUri set previously</param>
    /// <param name="logger">Optional logger</param>
    /// <returns>A DirectusClient instance</returns>
    public static DirectusClient Create(IDirectusIdentity identity, HttpClient httpClient,
        ILogger<DirectusClient>? logger = null)
    {
        return new DirectusClient(identity, httpClient, logger);
    }
    
    /// <summary>
    ///     Creates an unauthenticated directus client
    /// </summary>
    /// <param name="httpClient">The HttpClient to use. It must have the BaseUri set previously</param>
    /// <param name="logger">Optional logger</param>
    /// <returns>An unauthenticated DirectusClient instance</returns>
    public static DirectusClient Create(HttpClient httpClient, ILogger<DirectusClient>? logger = null)
    {
        return new DirectusClient(new NoIdentity(), httpClient, logger);
    }
}