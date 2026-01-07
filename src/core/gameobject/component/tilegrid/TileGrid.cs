using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using TemplateGame.src.core;

namespace TemplateGame;
public class TileGrid : Component, Drawer
{
    public Dictionary<string, Func<Tile>> TileRegistry = new Dictionary<string, Func<Tile>>();
    public Dictionary<Vector2, Tile> Tiles = new Dictionary<Vector2, Tile>();
    public RenderLayer RenderLayer { get; protected set; }
    public Vector2 TileSize { get; protected set; }

    public TileGrid(GameObject parent, Vector2 tileSize) : base(parent)
    {
        RenderLayer = RenderLayers.Default;
        TileSize = tileSize;
    }

    public void RegisterTile(string name)
    {
        TileRegistry.Add(name, () =>
        {
            Tile tile = new Tile(this, name);
            return tile;
        });
    }

    public Tile GetTileInstance(string name)
    {
        return TileRegistry[name]();
    }

    public void PlaceTile(Vector2 gridPosition, string name)
    {
        Tile tile = GetTileInstance(name);

        if (tile == null) return;

        tile.GridPosition = gridPosition;

        if (Tiles.ContainsKey(gridPosition))
        {
            Tiles[gridPosition] = tile;
            return;
        }

        Tiles.Add(gridPosition, tile);
    } 

    public void Draw()
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
