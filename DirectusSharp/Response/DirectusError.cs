namespace DirectusSharp.Response;

public record DirectusError(string Message, DirectusErrorExtension Extensions);

public record DirectusErrorExtension(string? Reason, string Code);