using Microsoft.Xna.Framework;
using System;

namespace TemplateGame;
public class TestRoom : Scene
{
    public TestRoom() : base("test_room", () => new TestRoom())
    {
        Camera.DefaultZoom = 4f;
        Camera.Zoom = 4f;
    }

    public override void Initialize()
    {
        Player player = new Player(Vector2.Zero);

        TestRoomTileGrid tileGrid = new TestRoomTileGrid(Vector2.Zero);

        AddGameObject(player);
        AddGameObject(tileGrid);

        Camera.SetTarget(player);

        base.Initialize();
    }

    public override void Load(SceneModel sceneSaveData)
    {
        base.Load(sceneSaveData);

        Player player = GetGameObject<Player>();
        PlayerDebugTools playerDebugTools = player.GetComponent<PlayerDebugTools>();

        if (!playerDebugTools.debugCamEnabled) Camera.SetTarget(player);
    }
}
