namespace DirectusSharp.Requests;

public interface IDirectusRequest<Response>
{
    public HttpRequestMessage GetMessage();
    public virtual object? GetMessageObject() => this;
    public virtual Type GetMessageObjectType() => this.GetType();
}