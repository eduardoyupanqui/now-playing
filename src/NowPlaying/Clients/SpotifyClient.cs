using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace NowPlaying.Clients;

public class SpotifyClient
{
  private readonly HttpClient _httpClient;
  public SpotifyClient(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }

  public async Task<CurrentlyPlayingResponse?> GetCurrentSong()
  {
    var response = await _httpClient.GetAsync("me/player/currently-playing");
    if (response.StatusCode == HttpStatusCode.NoContent)
      return null;
    response.EnsureSuccessStatusCode();
    return await response.Content.ReadFromJsonAsync<CurrentlyPlayingResponse>();
  }
}
