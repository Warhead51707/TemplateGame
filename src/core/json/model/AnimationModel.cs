using System.Text.Json.Serialization;

namespace TemplateGame;
public class AnimationModel
{
    [JsonPropertyName("spritesheet")]
    public string SpriteSheet { get; set; }

    [JsonPropertyName("frame_size")]
    public FrameSizeData FrameSize { get; set; }

    public class FrameSizeData
    {
        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }
    }

    [JsonPropertyName("frame_length")]
    public float FrameLength { get; set; }

    [JsonPropertyName("loop")]
    public bool Loop { get; set; }
}