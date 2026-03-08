using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGame;

public class PlayerDebugTools : Component
{
    private float debugCamSpeed = 4.5f;

    // toggles
    public bool debugCamEnabled = false;
    public bool tilemapEditorEnabled = false;

    // Inputs
    private MouseState previousMouseState = Mouse.GetState();
    public PlayerDebugTools(GameObject parent) : base("player_debug_tools", parent)
    {
    }

    public override void Update()
    {
        if (!Main.DebugMode) return;

        if (debugCamEnabled) DebugFreeCam();
        if (tilemapEditorEnabled) tilemapEditor();
    }

    // toggle methods
    public bool ToggleDebugCam()
    {
        debugCamEnabled = !debugCamEnabled;

        return debugCamEnabled;
    }

    public bool ToggleTilemapEditor()
    {
        tilemapEditorEnabled = !tilemapEditorEnabled;

        return tilemapEditorEnabled;
    }

    private void DebugFreeCam()
    {
        if (ImGui.GetIO().WantCaptureKeyboard) return;

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

        if (scrollChange != 0 && !ImGui.GetIO().WantCaptureMouse)
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

    private void tilemapEditor()
    {

    }
}
