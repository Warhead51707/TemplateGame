using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGame;

public class SnowballOverlayParticle : GameObject
{
    private float speed = 1f;

    public SnowballOverlayParticle(Vector2 position) : base("snowball_overlay_particle", position, 0)
    {
        RenderLayer = RenderLayers.Overlay;
    }

    public override void SetComponents()
    {
        Sprite sprite = new Sprite(this, "snowball");

        AddComponents(sprite);
    }

    public override void Update()
    {
        base.Update();

        Position += new Vector2(0, 0.5f) * speed;

        if (Position.Y < -Main.GameWindow.ClientBounds.Height)
        {
            Destroy();
        }
    }
}
