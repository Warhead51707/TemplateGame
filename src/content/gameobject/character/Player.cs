using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TemplateGame;
public class Player : GameObject
{
    private float movementSpeed = 40f;
    public Player(Vector2 position) : base("player", position)
    {
    }

    public override void Initialize()
    {
        RenderLayer.Order = 1;

        Sprite sprite = new Sprite(this, Name);

        AddComponent(sprite);

        base.Initialize();
    }

    public override void Update()
    {
        base.Update();

        MovementController();
    }

    private void MovementController()
    {
        KeyboardState keyboardState = Keyboard.GetState();
        Vector2 movementDirection = Vector2.Zero;

        if (keyboardState.IsKeyDown(Keys.W)) movementDirection.Y -= 1;
        if (keyboardState.IsKeyDown(Keys.A)) movementDirection.X -= 1;
        if (keyboardState.IsKeyDown(Keys.S)) movementDirection.Y += 1;
        if (keyboardState.IsKeyDown(Keys.D)) movementDirection.X += 1;

        if (movementDirection.X == 0 && movementDirection.Y == 0) return;

        movementDirection.Normalize();

        Position += movementDirection * movementSpeed * Main.DeltaTime;
    }
}
