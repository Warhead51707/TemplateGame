using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace TemplateGame;
public class Player : GameObject
{
    // Speeds
    private float movementSpeed = 40f;

    private Vector2 facingDirection = Vector2.Zero;

    // Input
    private KeyboardState previousKeyboardState = Keyboard.GetState();
    
    public Player(Vector2 position) : base("player", position)
    {
    }

    public override Func<GameObject> Register()
    {
        return () => new Player(Vector2.Zero);
    }

    public override void SetComponents()
    {
        AnimationTree animationTree = new AnimationTree(this);
        animationTree.AddAnimation("player_idle_foward", _ => facingDirection == Vector2.Zero || facingDirection == new Vector2(0, 1));
        animationTree.AddAnimation("player_idle_left", _ => facingDirection == new Vector2(-1, 0));
        animationTree.AddAnimation("player_idle_right", _ => facingDirection == new Vector2(1, 0));
        animationTree.AddAnimation("player_move_up", _ => facingDirection == new Vector2(0, -1));

        AddComponent(animationTree);

        PlayerDebugTools playerDebugTools = new PlayerDebugTools(this);

        AddComponent(playerDebugTools);
    }

    public override void Initialize()
    {
        RenderLayer.Order = 1;

        base.Initialize();
    }

    public override void Update()
    {
        base.Update();

        MovementController();
    }

    private void MovementController()
    {
        if (GetComponent<PlayerDebugTools>().debugCamEnabled) return;

        KeyboardState keyboardState = Keyboard.GetState();

        Vector2 movementDirection = Vector2.Zero;

        if (keyboardState.IsKeyDown(Keys.W)) movementDirection = new Vector2(0,-1);
        if (keyboardState.IsKeyDown(Keys.A)) movementDirection = new Vector2(-1,0);
        if (keyboardState.IsKeyDown(Keys.S)) movementDirection = new Vector2(0,1);
        if (keyboardState.IsKeyDown(Keys.D)) movementDirection = new Vector2(1,0);

        if (previousKeyboardState.IsKeyDown(Keys.W) && movementDirection == Vector2.Zero) facingDirection = Vector2.Zero;

        if (movementDirection.X == 0 && movementDirection.Y == 0) return;

        previousKeyboardState = keyboardState;

        facingDirection = movementDirection;

        movementDirection.Normalize();

        Position += movementDirection * movementSpeed * Main.DeltaTime;
    }
}
