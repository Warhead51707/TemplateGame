using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace TemplateGame;

public class CollisionTileRule : TileRule
{
    public Rectangle collisionBox = Rectangle.Empty;

    private Color collisionBoxColor = new Color(Color.Red, 0.3f);

    private Rectangle collisionRectangle;

    public CollisionTileRule(Tile parent, TileRuleModel.RuleProperties ruleProperties) : base(parent, ruleProperties)
    {
        collisionRectangle = new Rectangle(ruleProperties.Rectangle.X, ruleProperties.Rectangle.Y, ruleProperties.Rectangle.Width, ruleProperties.Rectangle.Height);
    }

    public override void OnPlace()
    {
        collisionBox = new Rectangle((int)Parent.TileGrid.Parent.Position.X + (int)(Parent.GridPosition.X * Parent.TileGrid.TileSize.X) + collisionRectangle.X, (int)Parent.TileGrid.Parent.Position.Y + (int)(Parent.GridPosition.Y * Parent.TileGrid.TileSize.Y) + collisionRectangle.Y, collisionRectangle.Width, collisionRectangle.Height);
    }

    public override void Draw()
    {
        base.Draw();

        if (!DebugToggles.ShowColliders) return;

        Vector2 boxPosition = new Vector2(Parent.TileGrid.Parent.Position.X + (Parent.GridPosition.X * Parent.TileGrid.TileSize.X) + collisionRectangle.X, Parent.TileGrid.Parent.Position.Y + (Parent.GridPosition.Y * Parent.TileGrid.TileSize.Y) + collisionRectangle.Y);
        Rectangle destination = new Rectangle((int)boxPosition.X, (int)boxPosition.Y, collisionRectangle.Width, collisionRectangle.Height);

        DrawManager.SpriteBatch.Draw(DrawManager.Pixel, destination, null, collisionBoxColor, 0f, Vector2.Zero, SpriteEffects.None, 0.0001f);
    }
}
