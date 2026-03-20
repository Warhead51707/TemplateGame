using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGame;

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

public class TileRuleModel
{
    public string Name { get; set; } = "default";
    public TileRuleModel()
    {
        
    }

    [JsonPropertyName("rules")]
    public List<Rule> Rules { get; set; } 

    public class Rule
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("properties")]
        public RuleProperties Properties { get; set; } = new RuleProperties();
    }

    public class RuleProperties
    {
        // Collision Properties
        [JsonPropertyName("rectangle")]
        public Rectangle Rectangle { get; set; } = new Rectangle();

        // Render Properties
        [JsonPropertyName("texture")]
        public string Texture { get; set; } = string.Empty;

        [JsonPropertyName("source_rectangle")]
        public Rectangle SourceRectangle { get; set; } = new Rectangle();

        [JsonPropertyName("color")]
        public Color Color { get; set; } = new Color();

        [JsonPropertyName("layer")]
        public int Layer { get; set; } = 0;

        [JsonPropertyName("layer_transformations")]
        public List<LayerTransformation> LayerTransformations { get; set; } = null;

        // Placement Properties
        [JsonPropertyName("neighbor_transformations")]
        public List<NeighborTransformation> NeighborTransformations { get; set; } = null;
    }

    public class Rectangle
    {
        [JsonPropertyName("x")]
        public int X { get; set; } = 0;

        [JsonPropertyName("y")]
        public int Y { get; set; } = 0;

        [JsonPropertyName("width")]
        public int Width { get; set; } = 0;

        [JsonPropertyName("height")]
        public int Height { get; set; } = 0;
    }

    public class Color
    {
        [JsonPropertyName("r")]
        public int R { get; set; } = 255;

        [JsonPropertyName("g")]
        public int G { get; set; } = 255;

        [JsonPropertyName("b")]
        public int B { get; set; } = 255;

        [JsonPropertyName("a")]
        public int A { get; set; } = 255;
    }

    public class LayerTransformation
    {
        [JsonPropertyName("source_rectangle")]
        public Rectangle SourceRectangle { get; set; }

        [JsonPropertyName("layer")]
        public int Layer { get; set; }
    }

    public class NeighborTransformation
    {
        [JsonPropertyName("neighbors")]
        public List<string> Neighbors { get; set; }

        [JsonPropertyName("neighbor_rule")]
        public List<string> NeighborRule { get; set; }

        [JsonPropertyName("transformation_rules")]
        public List<Rule> TransformationRules { get; set; }
    }
}


