using Microsoft.Xna.Framework;

namespace TemplateGame;
public class TestRoomTileGrid : GameObject
{
    public TestRoomTileGrid(Vector2 position) : base("test_tile_grid", position)
    {
    }

    public override void Initialize()
    {
        TileGrid tileGrid = new TileGrid(this, new Vector2(16, 16));

        AddComponent(tileGrid);

        tileGrid.RegisterTile("checkertile_1");
        tileGrid.RegisterTile("checkertile_2");

        for (int x = -6; x <= 6; x++)
        {
            for (int y = -4; y <= 4; y++)
            {
                Vector2 pos = new Vector2(x, y);

                if ((x + y) % 2 == 0)
                {
                    tileGrid.PlaceTile(pos, "checkertile_1");
                    continue;
                }

                tileGrid.PlaceTile(pos, "checkertile_2");
            }
        }

        base.Initialize();
    }
}
