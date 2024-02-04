using Microsoft.AspNetCore.Http.Features;
using NowPlaying.Clients;
using System.Buffers.Text;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateSlimBuilder(args);

// Add services to the container.
builder.Services.AddTransient<SpotifyAuthHandler>();
builder.Services
  .AddHttpClient<SpotifyClient>(builder => builder.BaseAddress = new Uri("https://api.spotify.com/v1/"))
  .AddHttpMessageHandler<SpotifyAuthHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGet("/now-playing", getNowPlaying);
app.MapGet("/top-tracks", getTopTracks);
app.MapGet("/health", getHealth);

app.Run();

//Return svg of playing song
static async Task<IResult> getNowPlaying(HttpContext http, HttpRequest request, SpotifyClient spotifyClient)
{
  var (Item, IsPlaying, ProgressMs, ExternalUrls) = await spotifyClient.GetCurrentSong() ?? new CurrentlyPlayingResponse();
  if (request.Query.ContainsKey("open"))
  {
    if (ExternalUrls is not null && ExternalUrls.TryGetValue("spotify", out string? location))
    {
      return Results.Redirect(location);
    }
    return Results.Ok();
  }

  //set headers svg file
  http.Response.Headers.ContentType = "image/svg+xml";
  http.Response.Headers.CacheControl = "s-maxage=1, stale-while-revalidate";

  var (durationMs, name) = Item;
  var images = Item?.Album.Images ?? [];

  var cover = images[images.Count - 1]?.Url;
  var coverImg = string.Empty;
  if (cover is not null)
  {
    var buff = await spotifyClient.GetAlbumCover(cover);
    coverImg = $"data:image/jpeg;base64,{Convert.ToBase64String(buff)}";
  }

  var artist = string.Join(", ", Item?.Artists.Select(x => x.Name) ?? []);

  //TODO : Render SVG to string 

  return Results.Ok($@"<svg xmlns=""http://www.w3.org/2000/svg"" width=""400"" height=""100"">
    <text x=""10"" y=""20"" font-family=""Verdana"" font-size=""20"" fill=""black"">{Item?.Name} - {Item?.Artists.First().Name}</text>");
}

//Return list of top tracks
static async Task<IResult> getTopTracks(HttpRequest request)
{
  return Results.Json(new { Foo = "Bar" });
}

// getHealth responds with a HTTP 200 or 5xx on error.
static IResult getHealth()
{
  return Results.Json(new { Status = "up" });
}