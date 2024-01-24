using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

namespace NowPlaying.Clients
{
  public class SpotifyAuthHandler : DelegatingHandler
  {
    private readonly IHttpClientFactory _httpClientFactory;

    public SpotifyAuthHandler(IHttpClientFactory httpClientFactory)
    {
      _httpClientFactory = httpClientFactory;
    }
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      var token = await getAuthorizationToken();
      request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
      return await base.SendAsync(request, cancellationToken);
    }

    public async Task<string?> getAuthorizationToken()
    {
      var clientId = Environment.GetEnvironmentVariable("SPOTIFY_CLIENT_ID");
      var clientSecret = Environment.GetEnvironmentVariable("SPOTIFY_CLIENT_SECRET");
      var refreshToken = Environment.GetEnvironmentVariable("SPOTIFY_REFRESH_TOKEN")!;

      var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");
      request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}")));
      request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
      {
          {"grant_type", "refresh_token"},
          {"refresh_token", refreshToken}
      });
      var response = await _httpClientFactory.CreateClient().SendAsync(request);
      response.EnsureSuccessStatusCode();
      var spotifyToken = await response.Content.ReadFromJsonAsync<SpotifyToken>();
      return spotifyToken?.AccessToken;
    }
  }
}
