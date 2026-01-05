using Microsoft.Xna.Framework;

namespace TemplateGame;
public class Test : GameObject
{
    public Test(Vector2 position) : base("test", position)
    {
    }

    public override void Initialize()
    {
        Sprite sprite = new Sprite(this, Name);

        AddComponent(sprite);

        base.Initialize();
    }
}
