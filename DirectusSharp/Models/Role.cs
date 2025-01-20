namespace DirectusSharp.Models;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public string? Description { get; set; }
    public Guid[] Policies { get; set; }
    public Guid? Parent { get; set; }
    public Guid[] Children { get; set; }
}