using DirectusSharp;
using DirectusSharp.Examples.Collections.Models;
using DirectusSharp.Requests.Collections;

var client = DirectusClient.Create(new HttpClient()
{
    BaseAddress = new Uri("https://directus.io")
});

var existingMovie = await client.GetMovieAsync(0);

var createdMovie = await client.CreateMovieAsync(new Movie()
{
    Title = "Lion King"
});

var updatedMovie = await client.UpdateMovieAsync(new Movie()
{
    MovieId = 4, // The MovieId to update
    Title = "Lion King II",
});