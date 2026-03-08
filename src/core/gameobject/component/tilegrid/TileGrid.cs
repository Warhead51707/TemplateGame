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

    private List<CollisionTile> collisionTileCache = new List<CollisionTile>();

    public TileGrid(GameObject parent, Vector2 tileSize) : base("tilegrid", parent)
    {
        RenderLayer = RenderLayers.Default;
        TileSize = tileSize;
    }

    public List<Rectangle> GetCollisionBoxes()
    {
        List<Rectangle> collisionBoxes = new List<Rectangle>();

        foreach (CollisionTile tile in collisionTileCache)
        {
            collisionBoxes.Add(tile.collisionBox);

           // Debug.WriteLine("Collision box for tile at " + tile.GridPosition + ": " + tile.collisionBox);
        }

        return collisionBoxes;
    }

    public List<CollisionTile> GetCollisionTiles()
    {
        return collisionTileCache;
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

        bool hasCollision = tileModel.Collision != null;

        if (hasCollision)
        {
            tile = new CollisionTile(this, tileModel);
        }

        if (tile == null) return;

        tile.GridPosition = gridPosition;

        if (hasCollision)
        {
            collisionTileCache.Add((CollisionTile)tile);

            Debug.WriteLine("Added collision tile at " + gridPosition + " with collision box: " + ((CollisionTile)tile).collisionBox);
        }

        tile.OnPlace();

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
