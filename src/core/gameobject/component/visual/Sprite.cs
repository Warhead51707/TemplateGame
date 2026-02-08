using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text.Json.Serialization;

namespace TemplateGame;
public class Sprite : Component
{
    [JsonIgnore]
    public Texture2D Texture { get; set; }

    // Sprite settings
    public Vector2 Scale { get; set; } = Vector2.One;
    public Vector2 Coordinates { get; set; } = Vector2.Zero;
    public float Rotation { get; set; } = 0f;
    public Color Color { get; set; } = Color.White;
    public float LayerOffset { get; set; } = 0;
    public SpriteEffects Effect { get; set; } = SpriteEffects.None;
    public Vector2 Size { get; set; } = Vector2.Zero;

    public Sprite(GameObject parent, string path) : base("sprite", parent)
    {
        Texture = Main.ContentManager.Load<Texture2D>(path);
        Size = new Vector2(Texture.Width, Texture.Height);
    }

    public override void Draw()
    {
        DrawManager.SpriteBatch.Draw(Texture, Vector2.Round(Parent.Position * Main.SceneManager.CurrentScene.Camera.Zoom * 16) / (Main.SceneManager.CurrentScene.Camera.Zoom * 16), new Rectangle((int)Coordinates.X, (int)Coordinates.Y, Texture.Width, Texture.Height), Color, Rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, Effect, (Parent.RenderLayer.Order + LayerOffset) * 0.001f);
    }
    public void Draw(Vector2 frameSize, Vector2 frames)
    {
        DrawManager.SpriteBatch.Draw(Texture, Vector2.Round(Parent.Position * Main.SceneManager.CurrentScene.Camera.Zoom * 16) / (Main.SceneManager.CurrentScene.Camera.Zoom * 16), new Rectangle((int)Coordinates.X * (int)frameSize.X, (int)Coordinates.Y * (int)frameSize.Y, Texture.Width / (int)frames.X, Texture.Height / (int)frames.Y), Color, Rotation, new Vector2((Texture.Width / (int)frames.X) / 2, (Texture.Height / (int)frames.Y) / 2), Scale, Effect, (Parent.RenderLayer.Order + LayerOffset) * 0.001f);
    }
}
