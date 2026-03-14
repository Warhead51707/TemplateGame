using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGame;

public class Projectile : GameObject
{
    public Vector2 FacingDirection { get; set; }
    public float Speed { get; set; }
    public Projectile(string name, Vector2 position, Vector2 facingDirection, float speed, Func<GameObject> register) : base(name, position, register)
    {
        FacingDirection = facingDirection;
        Speed = speed;
    }

    public override void Update()
    {
        Position += FacingDirection * Speed * Main.DeltaTime;
        base.Update();
    }


}
