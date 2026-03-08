using System.Text.Json.Serialization;

namespace TemplateGame;

public class TileModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("texture")]
    public string Texture { get; set; }

    [JsonPropertyName("collision")]
    public CollisionData Collision { get; set; }

    public class CollisionData
    {
        [JsonPropertyName("size")]
        public SizeData Size { get; set; }

        [JsonPropertyName("offset")]
        public OffsetData Offset { get; set; }
    }

    public class SizeData
    {
        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }
    }

    public class OffsetData
    {
        [JsonPropertyName("x")]
        public int X { get; set; }

        [JsonPropertyName("y")]
        public int Y { get; set; }
    }
}
