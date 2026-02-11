using Microsoft.Xna.Framework;
using System;

namespace TemplateGame;
public class TestRoomTileGrid : GameObject
{
    public TestRoomTileGrid(Vector2 position) : base("test_tile_grid", () => new TestRoomTileGrid(Vector2.Zero), position)
    {
    }

    public override void Initialize()
    {
        TileGrid tileGrid = new TileGrid(this, new Vector2(16, 16));

        AddComponent(tileGrid);

        for (int x = -6; x <= 6; x++)
        {
            for (int y = -4; y <= 4; y++)
            {
                Vector2 pos = new Vector2(x, y);

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

        base.Initialize();
    }
}
