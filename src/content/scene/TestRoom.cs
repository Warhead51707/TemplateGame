using Microsoft.Xna.Framework;

namespace TemplateGame;
public class TestRoom : Scene
{
    public TestRoom() : base("test_room")
    {
    }

    public override void Initialize()
    {
        Camera.Zoom = 3f;

        Player player = new Player(Vector2.Zero);
        TestRoomTileGrid tileGrid = new TestRoomTileGrid(Vector2.Zero);

        AddGameObject(player);
        AddGameObject(tileGrid);

        Camera.SetTarget(player);
    }
}
