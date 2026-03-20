using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TemplateGame;
public class RenderTileRule : TileRule
{
    public Texture2D Texture;
    public Rectangle SourceRectangle;
    public Color Color;
    public int Layer;

    public RenderTileRule(Tile parent, TileRuleModel.RuleProperties ruleProperties) : base(parent, ruleProperties)
    {
        if (ruleProperties.Texture != null && ruleProperties.Texture != string.Empty)
        {
            Texture = Main.ContentManager.Load<Texture2D>("assets/tile/" + ruleProperties.Texture);
        } else
        {
            Texture = Parent.Texture;
        }

        if (ruleProperties.SourceRectangle.Width != 0 && ruleProperties.SourceRectangle.Height != 0)
        {
            SourceRectangle = new Rectangle(ruleProperties.SourceRectangle.X, ruleProperties.SourceRectangle.Y, ruleProperties.SourceRectangle.Width, ruleProperties.SourceRectangle.Height);
        } else
        {
            SourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
        }

        Color = new Color(ruleProperties.Color.R, ruleProperties.Color.G, ruleProperties.Color.B, ruleProperties.Color.A);

        Layer = ruleProperties.Layer;
    }

    public override void Draw()
    {
        Vector2 gridPosition = new Vector2(Parent.TileGrid.Parent.Position.X + (Parent.GridPosition.X * Parent.TileGrid.TileSize.X), Parent.TileGrid.Parent.Position.Y + (Parent.GridPosition.Y * Parent.TileGrid.TileSize.Y));

        Vector2 fixedGridPostion = Vector2.Round(gridPosition * (Main.SceneManager.CurrentScene.Camera.Zoom * 16)) / (Main.SceneManager.CurrentScene.Camera.Zoom * 16);

        if (Properties.LayerTransformations == null || Properties.LayerTransformations.Count == 0)
        {
            DrawManager.SpriteBatch.Draw(Texture, fixedGridPostion, SourceRectangle, Color, 0f, new Vector2(Texture.Width / 2, Texture.Height / 2), Vector2.One, SpriteEffects.None, (Parent.TileGrid.Parent.RenderLayer.Order + Layer) * 0.001f);
            return;
        }

        foreach (TileRuleModel.LayerTransformation layerTransformation in Properties.LayerTransformations)
        {
            Rectangle source = new Rectangle(layerTransformation.SourceRectangle.X, layerTransformation.SourceRectangle.Y, layerTransformation.SourceRectangle.Width, layerTransformation.SourceRectangle.Height);
            Vector2 origin = new Vector2(source.Width / 2, source.Height / 2);
            Vector2 position = fixedGridPostion + new Vector2((source.Width - Texture.Width) / 2, (source.Height - Texture.Height) / 2);

            DrawManager.SpriteBatch.Draw(Texture, position, source, Color, 0f, origin, Vector2.One, SpriteEffects.None, (Parent.TileGrid.Parent.RenderLayer.Order + layerTransformation.Layer) * 0.001f);
        }
    }
}
