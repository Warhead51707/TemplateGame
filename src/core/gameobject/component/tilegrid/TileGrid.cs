using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TemplateGame;
public class TileGrid : Component
{
    public Dictionary<Vector2, Tile> Tiles = new Dictionary<Vector2, Tile>();
    public RenderLayer RenderLayer { get; protected set; }
    public Vector2 TileSize { get; protected set; }

    private List<Tile> collisionTileCache = new List<Tile>();

    public TileGrid(GameObject parent, Vector2 tileSize) : base("tilegrid", parent)
    {
        RenderLayer = RenderLayers.Default;
        TileSize = tileSize;
    }

    public List<Rectangle> GetCollisionBoxes()
    {
        List<Rectangle> collisionBoxes = new List<Rectangle>();

        foreach (Tile tile in collisionTileCache)
        {
            collisionBoxes.AddRange(tile.GetCollisionBoxes());
        }

        return collisionBoxes;
    }

    public override SaveData Save()
    {
        SaveData.Json["tile_size"] = JsonSerializer.SerializeToElement(new { x = TileSize.X, y = TileSize.Y });

        var tilesList = Tiles.Select(t => new
        {
            position = new { x = t.Key.X, y = t.Key.Y },
            name = t.Value.Name
        }).ToList();

        SaveData.Json["tiles"] = JsonSerializer.SerializeToElement(tilesList);

        return base.Save();
    }

    public override void Load(SaveData saveData)
    {
        base.Load(saveData);

        Tiles.Clear();
        collisionTileCache.Clear();

        if (saveData.Json.TryGetValue("components", out JsonElement components))
        {
            foreach (JsonElement component in components.EnumerateArray())
            {
                if (component.TryGetProperty("tiles", out JsonElement tiles))
                {
                    foreach (JsonElement tile in tiles.EnumerateArray())
                    {
                        string tileName = tile.GetProperty("name").GetString();

                        JsonElement pos = tile.GetProperty("position");
                        int x = pos.GetProperty("x").GetInt32();
                        int y = pos.GetProperty("y").GetInt32();

                        PlaceTile(new Vector2(x, y), tileName);
                    }
                }
            }
        }
    }

    public Tile GetTile(Vector2 gridPosition)
    {
        if (Tiles.ContainsKey(gridPosition))
        {
            return Tiles[gridPosition];
        }

        return null;
    }

    public void PlaceTile(Vector2 gridPosition, string name)
    {
        string jsonFileContents = File.ReadAllText("Content/data/tile/" + name + ".json");
        TileModel tileModel = JsonSerializer.Deserialize<TileModel>(jsonFileContents);

        if (tileModel == null) {
            Console.WriteLine("Failed to load tile: " + name);
            return;
        }

        Tile tile = new Tile(this, tileModel);

        if (tile == null) return;

        tile.GridPosition = gridPosition;

        if (tile.HasCollision())
        {
            collisionTileCache.Add(tile);
        }

        tile.OnPlace();

        foreach (Tile neighbor in tile.GetNeighbors())
        {
            if (neighbor == null) continue;

            neighbor.NeighborUpdate();
        }

        if (Tiles.ContainsKey(gridPosition))
        {
            Tiles[gridPosition] = tile;
            return;
        }

        Tiles.Add(gridPosition, tile);
    } 

    public override void Draw()
    {
        SceneCamera camera = Main.SceneManager.CurrentScene.Camera;

        Vector2 cameraPosition = camera.GetScreenPostion();
        Vector2 cameraGridPosition = new Vector2((int)(cameraPosition.X / TileSize.X), (int)(cameraPosition.Y / TileSize.Y));

        float cameraWidth = Main.GraphicsDeviceManager.GraphicsDevice.Viewport.Width / camera.Zoom;
        float cameraHeight = Main.GraphicsDeviceManager.GraphicsDevice.Viewport.Height / camera.Zoom;

        int horizontalTileCount = (int)(cameraWidth / TileSize.X) + 8;
        int verticalTileCount = (int)(cameraHeight / TileSize.Y) + 8;

        int horizontalStart = (int)Math.Ceiling(-cameraGridPosition.X / camera.Zoom) - 2;
        int verticalStart = (int)Math.Ceiling(-cameraGridPosition.Y / camera.Zoom) - 2;

        for (int y = -4; y < verticalTileCount; y++)
        {
            for (int x = -4; x < horizontalTileCount; x++)
            {
                Vector2 gridCoordinates = new Vector2(horizontalStart + x, verticalStart + y);

                if (Tiles.ContainsKey(gridCoordinates))
                {
                    Tiles[gridCoordinates].Draw();
                }
            }
        }
    }
}
