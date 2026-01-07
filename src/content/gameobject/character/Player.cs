using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D11;

namespace TemplateGame;
public class Player : GameObject
{
    private float movementSpeed = 40f;
    public Player(Vector2 position) : base("test", position)
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

        if (keyboardState.IsKeyDown(Keys.W)) Position += new Vector2(0, -1) * movementSpeed * Main.DeltaTime;
        if (keyboardState.IsKeyDown(Keys.A)) Position += new Vector2(-1, 0) * movementSpeed * Main.DeltaTime;
        if (keyboardState.IsKeyDown(Keys.S)) Position += new Vector2(0, 1) * movementSpeed * Main.DeltaTime;
        if (keyboardState.IsKeyDown(Keys.D)) Position += new Vector2(1, 0) * movementSpeed * Main.DeltaTime;
    }
}
