using System.Text.Json.Serialization;

namespace NowPlaying.Clients;

public class SpotifyToken
{
  [JsonPropertyName("access_token")]
  public string? AccessToken { get; set; }
}
public class CurrentlyPlayingResponse
{
  [JsonPropertyName("is_playing")]
  public bool IsPlaying { get; set; }

  [JsonPropertyName("progress_ms")]
  public int ProgressMs { get; set; }

  [JsonPropertyName("item")]
  public Item Item { get; set; } = new Item();

  [JsonPropertyName("external_urls")]
  public Dictionary<string, string> ExternalUrls { get; set; } = new Dictionary<string, string>();
}
public class Item
{
  [JsonPropertyName("duration_ms")]
  public int DurationMs { get; set; }
  [JsonPropertyName("name")]
  public string Name { get; set; } = string.Empty;

  public Album Album { get; set; }
  public List<Artist> Artists { get; set; }
}
public class Album
{
  [JsonPropertyName("images")]
  public List<Image> Images { get; set; }
}
public class Image
{
  [JsonPropertyName("url")]
  public string Url { get; set; } = string.Empty;
}
public class Artist
{
  [JsonPropertyName("name")]
  public string Name { get; set; } = string.Empty;
}
