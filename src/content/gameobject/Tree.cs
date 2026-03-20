using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGame;

public class Tree : GameObject
{
    public Tree(Vector2 position) : base("tree", position)
    {
    }

    public override void SetComponents()
    {
        Sprite sprite = new Sprite(this, "tree");

        AddComponent(sprite);
    }
}
