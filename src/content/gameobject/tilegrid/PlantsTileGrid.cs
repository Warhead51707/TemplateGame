using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;

namespace TemplateGame;

public class PlantsTileGrid : GameObject
{
    public PlantsTileGrid(Vector2 position) : base("plants_tile_grid", position, () => new PlantsTileGrid(Vector2.Zero))
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

        RenderLayer.Order = 2;

        TileGrid tileGrid = GetComponent<TileGrid>();
    }
}
