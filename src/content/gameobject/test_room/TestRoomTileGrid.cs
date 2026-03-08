using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;

namespace TemplateGame;
public class TestRoomTileGrid : GameObject
{
    public TestRoomTileGrid(Vector2 position) : base("test_tile_grid", position, () => new TestRoomTileGrid(Vector2.Zero))
    {
    }

    public override void SetComponents()
    {
        TileGrid tileGrid = new TileGrid(this, new Vector2(16, 16));

        AddComponent(tileGrid);
    }

    public override void Initialize()
    {
        base.Initialize();

        TileGrid tileGrid = GetComponent<TileGrid>();

        for (int x = -60; x <= 60; x++)
        {
            for (int y = -40; y <= 40; y++)
            {
                Vector2 pos = new Vector2(x, y);

                if (pos == new Vector2(-2, -2))
                {
                    tileGrid.PlaceTile(pos, "test_collision_tile");
                    continue;
                }

                Random random = new Random();

                int number = random.Next(0, 10);

                if (number < 7)
                {
                    tileGrid.PlaceTile(pos, "snow_1");
                    continue;
                }

                if (number >= 7 && number < 9)
                {
                    tileGrid.PlaceTile(pos, "snow_2");
                    continue;
                }

                tileGrid.PlaceTile(pos, "snow_3");
            }
        }
    }

    public override void Load(SaveData saveData)
    {
        base.Load(saveData);

        TileGrid tileGrid = GetComponent<TileGrid>();

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

                        tileGrid.PlaceTile(new Vector2(x, y), tileName);

                        //Debug.WriteLine(tileName + " " + x + ", " + y);
                    }
                }
            }
        }
    }
}
