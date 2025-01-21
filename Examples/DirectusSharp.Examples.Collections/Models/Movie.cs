using System.Text.Json.Serialization;
using DirectusSharp.Generators.Attributes;

namespace DirectusSharp.Examples.Collections.Models;

[DirectusCollection("movies")]
public class Movie
{
    // Id must be a property and be called "Id" or "MovieId"
    [JsonPropertyName("id")] // Directus uses snake_case, so the Id field needs to be called "id". If Id is NULL, Directus will automatically assign one for us
    public int? MovieId { get; set; }

    public string Title { get; set; } = string.Empty;
}