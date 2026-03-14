using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGame;

public class CollisionTile : Tile
{
    public Rectangle collisionBox = Rectangle.Empty;

    private Color collisionBoxColor = new Color(Color.Red, 0.3f);
    public CollisionTile(TileGrid tileGrid, TileModel tileModel) : base(tileGrid, tileModel)
    {
        
    }

    public override void OnPlace()
    {
        collisionBox = new Rectangle((int)TileGrid.Parent.Position.X + (int)(GridPosition.X * TileGrid.TileSize.X) + tileModel.Collision.Offset.X, (int)TileGrid.Parent.Position.Y + (int)(GridPosition.Y * TileGrid.TileSize.Y) + tileModel.Collision.Offset.Y, tileModel.Collision.Size.Width, tileModel.Collision.Size.Height);
    }

    public override void Draw()
    {
        base.Draw();

        if (!DebugToggles.ShowColliders) return;

        Vector2 boxPosition = new Vector2(TileGrid.Parent.Position.X + (GridPosition.X * TileGrid.TileSize.X) + tileModel.Collision.Offset.X, TileGrid.Parent.Position.Y + (GridPosition.Y * TileGrid.TileSize.Y) + tileModel.Collision.Offset.Y);
        Rectangle destination = new Rectangle((int)boxPosition.X, (int)boxPosition.Y, tileModel.Collision.Size.Width, tileModel.Collision.Size.Height);

        DrawManager.SpriteBatch.Draw(DrawManager.Pixel, destination, null, collisionBoxColor, 0f, Vector2.Zero, SpriteEffects.None, 0.0001f);
    }
}
