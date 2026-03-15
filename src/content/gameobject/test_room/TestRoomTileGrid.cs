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
    }
}
