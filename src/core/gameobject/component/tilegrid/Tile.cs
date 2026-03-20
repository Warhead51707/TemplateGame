using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TemplateGame;
public class Tile
{
    public string Name { get; private set; }
    public Texture2D Texture { get; private set; }
    public TileGrid TileGrid { get; private set; }
    public Vector2 GridPosition { get; set; } = Vector2.Zero;
    public List<TileRule> TileRules { get; set; } = new List<TileRule>();
    protected TileModel tileModel { get; private set; }
    protected TileRuleModel tileRuleModel { get; private set; }

    public Tile(TileGrid tileGrid, TileModel tileModel)
    {
        this.tileModel = tileModel;
        TileGrid = tileGrid;
        Name = tileModel.Name;
        Texture = Main.ContentManager.Load<Texture2D>("assets/tile/" + tileModel.Texture);

        try
        {
            string jsonFileContents = File.ReadAllText("Content/data/tile/tile_rule/" + Name + "_tile_rule.json");
            tileRuleModel = JsonSerializer.Deserialize<TileRuleModel>(jsonFileContents);
            tileRuleModel.Name = Name;
        } catch
        {
            string jsonFileContents = File.ReadAllText("Content/data/tile/tile_rule/default.json");
            tileRuleModel = JsonSerializer.Deserialize<TileRuleModel>(jsonFileContents);
        }

        foreach (TileRuleModel.Rule rule in tileRuleModel.Rules)
        {
            if (rule.Type == "render")
            {
                TileRules.Add(new RenderTileRule(this, rule.Properties));
            }

            if (rule.Type == "collision")
            {
                TileRules.Add(new CollisionTileRule(this, rule.Properties));
            }
        }

    }

    public bool HasCollision()
    {
        return TileRules.Any(rule => rule is CollisionTileRule);
    }

    public List<Rectangle> GetCollisionBoxes()
    {
        List<Rectangle> collisionBoxes = new List<Rectangle>();
        foreach (TileRule tileRule in TileRules)
        {
            if (tileRule is CollisionTileRule collisionTileRule)
            {
                collisionBoxes.Add(collisionTileRule.collisionBox);
            }
        }
        return collisionBoxes;
    }

    public virtual void OnPlace()
    {
        foreach (TileRule tileRule in TileRules)
        {
            tileRule.OnPlace();
        }
    }

    public virtual void Draw()
    {
        List<RenderTileRule> renderTileRules = TileRules.Where(rule => rule is RenderTileRule).Select(rule => (RenderTileRule)rule).ToList();

        foreach (RenderTileRule renderTileRule in renderTileRules)
        {
            renderTileRule.Draw();
        }

        List<CollisionTileRule> collisionTileRules = TileRules.Where(rule => rule is CollisionTileRule).Select(rule => (CollisionTileRule)rule).ToList();

        foreach (CollisionTileRule collisionTileRule in collisionTileRules)
        {
            collisionTileRule.Draw();
        }
    }
}
