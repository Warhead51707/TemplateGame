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

        Test test = new Test(Vector2.Zero);

        AddGameObject(test);
    }
}
