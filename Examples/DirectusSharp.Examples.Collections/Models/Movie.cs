using DirectusSharp.Generators.Attributes;

namespace DirectusSharp.Examples.Collections.Models;

[DirectusCollection("movies")]
public class Movie
{
    // Id must be a property and be called "Id" or "MovieId"
    public int MovieId { get; set; } = 0;
    
    public string Title { get; set; } = string.Empty;
}