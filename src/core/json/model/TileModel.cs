using System.Text.Json.Serialization;

namespace TemplateGame;

public class TileModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("texture")]
    public string Texture { get; set; }
}
