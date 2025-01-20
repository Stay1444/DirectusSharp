namespace DirectusSharp.Requests;

public interface IDirectusRawRequest
{
    public HttpRequestMessage GetMessage();
}