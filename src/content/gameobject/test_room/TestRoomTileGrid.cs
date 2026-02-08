using Microsoft.Xna.Framework;

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

        tileGrid.RegisterTile("debug_tile");

        for (int x = -6; x <= 6; x++)
        {
            for (int y = -4; y <= 4; y++)
            {
                Vector2 pos = new Vector2(x, y);

                tileGrid.PlaceTile(pos, "debug_tile");
            }
        }

        base.Initialize();
    }
}
