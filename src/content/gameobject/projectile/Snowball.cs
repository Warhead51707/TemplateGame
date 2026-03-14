using Microsoft.Xna.Framework;

namespace TemplateGame;
public class Snowball : Projectile
{
    public Snowball(Vector2 position, Vector2 facingDirection) : base("snowball", position, facingDirection, 40f, () => new Snowball(Vector2.Zero, Vector2.Zero))
    {
    }

    public override void SetComponents()
    {
        Sprite sprite = new Sprite(this, "snowball");

        Collider collider = new Collider(this, new Rectangle(-3, -3, 6, 6));

        AddComponents(collider, sprite);
    }

    public override void Update()
    {
        base.Update();

        Collider collider = GetComponent<Collider>();

        if (collider.IsColliding)
        {
            Destroy();
        }
    }
}
