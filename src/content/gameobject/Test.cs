using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
