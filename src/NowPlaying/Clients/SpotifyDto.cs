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

  public void Deconstruct(out Item Item, out bool IsPlaying, out int ProgressMs, out Dictionary<string, string>  ExternalUrls)
  {
    Item = this.Item;
    IsPlaying = this.IsPlaying;
    ProgressMs = this.ProgressMs;
    ExternalUrls = this.ExternalUrls;
  }
}
public class Item
{
  [JsonPropertyName("duration_ms")]
  public int DurationMs { get; set; }
  [JsonPropertyName("name")]
  public string Name { get; set; } = string.Empty;

  public Album Album { get; set; }
  public List<Artist> Artists { get; set; }

  public void Deconstruct(out int DurationMs, out string Name)
  {
    DurationMs = this.DurationMs;
    Name = this.Name;
  }
}
public class Album
{
  [JsonPropertyName("images")]
  public List<Image> Images { get; set; }

  public void Deconstruct(out List<Image> Images)
  {
    Images = this.Images;
  }
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
