var builder = WebApplication.CreateSlimBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGet("/now-playing", getNowPlaying);
app.MapGet("/top-tracks", getTopTracks);
app.MapGet("/health", getHealth);

app.Run();

//Return svg of playing song
static async Task<IResult> getNowPlaying()
{
  return Results.Json(new { Foo = "Bar"});
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