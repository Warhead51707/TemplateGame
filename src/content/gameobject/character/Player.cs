using ImGuiNET;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace TemplateGame;
public class Player : GameObject
{
    // Speeds
    private float movementSpeed = 55f;

    // Movement
    private Vector2 facingDirection = Vector2.Zero;
    private bool isMoving = false;

    // Input
    private KeyboardState previousKeyboardState = Keyboard.GetState();
    
    public Player(Vector2 position) : base("player", position, () => new Player(Vector2.Zero))
    {
    }

    public override void SetComponents()
    {
        AnimationTree animationTree = new AnimationTree(this);
        animationTree.AddAnimation("player_idle_foward", _ => (facingDirection == Vector2.Zero || facingDirection == new Vector2(0, 1)) && !isMoving);
        animationTree.AddAnimation("player_idle_left", _ => facingDirection == new Vector2(-1, 0) && !isMoving);
        animationTree.AddAnimation("player_idle_right", _ => facingDirection == new Vector2(1, 0) && !isMoving);

        animationTree.AddAnimation("player_move_down", _ => facingDirection == new Vector2(0, 1) && isMoving);
        animationTree.AddAnimation("player_move_up", _ => facingDirection == new Vector2(0, -1) && isMoving);
        animationTree.AddAnimation("player_move_left", _ => facingDirection == new Vector2(-1, 0) && isMoving);
        animationTree.AddAnimation("player_move_right", _ => facingDirection == new Vector2(1, 0) && isMoving);


        PlayerDebugTools playerDebugTools = new PlayerDebugTools(this);

        Collider collider = new Collider(this, new Rectangle(-4, -5, 8, 12));

        AddComponents(animationTree, playerDebugTools, collider);
    }

    public override void Initialize()
    {
        RenderLayer.Order = 1;

        base.Initialize();
    }

    public override void Update()
    {
        base.Update();

        SnowballController();

        MovementController();
    }

    private void SnowballController()
    {
        if (GetComponent<PlayerDebugTools>().debugCamEnabled) return;

        KeyboardState keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space))
        {
            Vector2 snowballDirection = facingDirection;

            if (facingDirection == Vector2.Zero) snowballDirection = new Vector2(0, 1);

            Snowball snowball = new Snowball(Position, snowballDirection);
            Main.SceneManager.CurrentScene.AddGameObject(snowball);
        }
    }

    private void MovementController()
    {
        if (GetComponent<PlayerDebugTools>().debugCamEnabled) return;
        if (ImGui.GetIO().WantCaptureKeyboard) return;

        KeyboardState keyboardState = Keyboard.GetState();

        Vector2 movementDirection = Vector2.Zero;

        if (keyboardState.IsKeyDown(Keys.W)) movementDirection = new Vector2(0,-1);
        if (keyboardState.IsKeyDown(Keys.A)) movementDirection = new Vector2(-1,0);
        if (keyboardState.IsKeyDown(Keys.S)) movementDirection = new Vector2(0,1);
        if (keyboardState.IsKeyDown(Keys.D)) movementDirection = new Vector2(1,0);

        if (previousKeyboardState.IsKeyDown(Keys.W) && movementDirection == Vector2.Zero) facingDirection = Vector2.Zero;

        Collider collider = GetComponent<Collider>();

        if (collider.IsColliding)
        {
            Rectangle overlapRectangle = collider.OverlapRectangle;

            if (overlapRectangle.Width > 1 && overlapRectangle.Height > 1)
            {
                Position += new Vector2(overlapRectangle.Width, overlapRectangle.Height);
                return;
            }
        }

        previousKeyboardState = keyboardState;

        if (movementDirection.X == 0 && movementDirection.Y == 0)
        {
            isMoving = false;
            return;
        }

        facingDirection = movementDirection;

        movementDirection.Normalize();

        collider.MovementOffset = facingDirection;

        collider.CheckColliding();

        if (collider.IsColliding)
        {
            Rectangle overlapRectangle = collider.OverlapRectangle;

            if (overlapRectangle.Width > 0) movementDirection.X = 0;
            if (overlapRectangle.Height > 0) movementDirection.Y = 0;

            return;
        }

        isMoving = true;

        Position += movementDirection * movementSpeed * Main.DeltaTime;
    }
}
