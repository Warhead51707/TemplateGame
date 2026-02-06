using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TemplateGame;
public class Tile
{
    public string Name { get; private set; }
    public Texture2D Texture { get; private set; }
    public TileGrid TileGrid { get; private set; }
    public Vector2 GridPosition { get; set; } = Vector2.Zero;

    public Tile(TileGrid tileGrid, string name)
    {
        TileGrid = tileGrid;
        Name = name;
        Texture = Main.ContentManager.Load<Texture2D>(name);
    }

    public void Draw()
    {
        Vector2 gridPosition = new Vector2(TileGrid.Parent.Position.X + (GridPosition.X * TileGrid.TileSize.X), TileGrid.Parent.Position.Y + (GridPosition.Y * TileGrid.TileSize.Y));

        Vector2 fixedGridPostion = Vector2.Round(gridPosition * (Main.SceneManager.CurrentScene.Camera.Zoom * 16)) / (Main.SceneManager.CurrentScene.Camera.Zoom * 16);

        DrawManager.SpriteBatch.Draw(Texture, fixedGridPostion, new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, 0f, new Vector2(Texture.Width / 2, Texture.Height / 2), Vector2.One, SpriteEffects.None, (TileGrid.Parent.RenderLayer.Order) * 0.001f);

    }
}
