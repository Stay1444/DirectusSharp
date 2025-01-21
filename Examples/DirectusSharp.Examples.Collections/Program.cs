using DirectusSharp;
using DirectusSharp.Examples.Collections.Models;

var client = DirectusClient.Create(new HttpClient()
{
    BaseAddress = new Uri("https://directus.io")
});

var createdMovie = await client.CreateMovieAsync(new Movie()
{
    Title = "Lion King"
});

await client.UpdateMovieAsync(new Movie()
{
    MovieId = createdMovie!.MovieId,
    Title = "Lion King II",
});

await client.DeleteMovieAsync(createdMovie.MovieId);