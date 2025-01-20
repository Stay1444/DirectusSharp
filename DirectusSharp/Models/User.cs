namespace DirectusSharp.Models;

public class User
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string? LastName { get; set; }
    public required string Email { get; set; }
    public Guid Role { get; set; }
    public Guid? Avatar { get; set; }
}