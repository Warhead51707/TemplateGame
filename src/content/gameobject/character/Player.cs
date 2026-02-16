using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace TemplateGame;
public class Player : GameObject
{
    // Speeds
    private float movementSpeed = 40f;
    private float debugCamSpeed = 4.5f;

    private Vector2 facingDirection = Vector2.Zero;

    // Input
    private KeyboardState previousKeyboardState = Keyboard.GetState();
    private MouseState previousMouseState = Mouse.GetState();
    public Player(Vector2 position) : base("player", () => new Player(Vector2.Zero), position)
    {
    }

    public override void Initialize()
    {
        RenderLayer.Order = 1;

        //Sprite sprite = new Sprite(this, Name);
        //AddComponent(sprite);

        AnimationTree animationTree = new AnimationTree(this);
        animationTree.AddAnimation("player_idle_foward", _ => facingDirection == Vector2.Zero || facingDirection == new Vector2(0,1));
        animationTree.AddAnimation("player_idle_left", _ => facingDirection == new Vector2(-1, 0));
        animationTree.AddAnimation("player_idle_right", _ => facingDirection == new Vector2(1, 0));
        animationTree.AddAnimation("player_move_up", _ => facingDirection == new Vector2(0, -1));

        AddComponent(animationTree);

        base.Initialize();
    }

    public override void Update()
    {
        base.Update();

        if (Main.DebugMode)
        {
            DebugFreeCam();
            return;
        }

        MovementController();
    }

    private void DebugFreeCam()
    {
        KeyboardState keyboardState = Keyboard.GetState();
        MouseState mouseState = Mouse.GetState();

        // Camera Movement
        if (keyboardState.IsKeyDown(Keys.W))
        {
            Main.SceneManager.CurrentScene.Camera.Translate(new Vector2(0, -debugCamSpeed));
        }

        if (keyboardState.IsKeyDown(Keys.S))
        {
            Main.SceneManager.CurrentScene.Camera.Translate(new Vector2(0, debugCamSpeed));
        }

        if (keyboardState.IsKeyDown(Keys.A))
        {
            Main.SceneManager.CurrentScene.Camera.Translate(new Vector2(-debugCamSpeed, 0));
        }

        if (keyboardState.IsKeyDown(Keys.D))
        {
            Main.SceneManager.CurrentScene.Camera.Translate(new Vector2(debugCamSpeed, 0));
        }

        // Camera Zoom
        int scrollChange = mouseState.ScrollWheelValue - previousMouseState.ScrollWheelValue;

        if (scrollChange !=  0)
        {
            float zoomAmount = 0.008f * scrollChange;
            float zoomTotal = Main.SceneManager.CurrentScene.Camera.Zoom + zoomAmount;

            float fixedZoom = Math.Clamp(MathF.Round(zoomTotal * 8) / 8, 2f, 8f);

            Vector2 mousePosition = new Vector2(mouseState.Position.X, mouseState.Position.Y);
            Vector2 screenCenter = new Vector2(Main.GraphicsDeviceManager.PreferredBackBufferWidth / 2, Main.GraphicsDeviceManager.PreferredBackBufferHeight / 2);
            Vector2 worldPositionBeforeZoom = ((mousePosition - screenCenter) / Main.SceneManager.CurrentScene.Camera.Zoom) + Main.SceneManager.CurrentScene.Camera.Position;

            Main.SceneManager.CurrentScene.Camera.Zoom = fixedZoom;

            Vector2 worldPostionAfterZoom = ((mousePosition - screenCenter) / Main.SceneManager.CurrentScene.Camera.Zoom) + Main.SceneManager.CurrentScene.Camera.Position;

            Vector2 zoomTranslation = worldPositionBeforeZoom - worldPostionAfterZoom;

            Main.SceneManager.CurrentScene.Camera.Translate(zoomTranslation);
        }

        previousMouseState = mouseState;
    }

    private void MovementController()
    {
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
