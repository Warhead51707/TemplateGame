using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TemplateGame.src.core;

namespace TemplateGame;
public class Sprite : Component, Drawer
{
    public RenderLayer RenderLayer { get; set; }
    public Texture2D Texture { get; set; }

    // Sprite settings
    public Vector2 Scale { get; set; } = Vector2.One;
    public Vector2 Coordinates { get; set; } = Vector2.Zero;
    public float Rotation { get; set; } = 0f;
    public Color Color { get; set; } = Color.White;
    public float LayerOffset { get; set; } = 0;
    public SpriteEffects Effect { get; set; } = SpriteEffects.None;

    public Sprite(GameObject parent, string path) : base(parent)
    {
        RenderLayer = Parent.RenderLayer;
        Texture = Main.ContentManager.Load<Texture2D>(path);
    }

    public void Draw()
    {
        DrawManager.SpriteBatch.Draw(Texture, Parent.Position, new Rectangle((int)Coordinates.X, (int)Coordinates.Y, Texture.Width, Texture.Height), Color, Rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, Effect, (Parent.RenderLayer.Order + LayerOffset) * 0.001f);
    }
}
