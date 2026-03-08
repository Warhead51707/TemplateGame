using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TemplateGame;

public class TilePlacer : GameObject
{
    private TileModel tileModel;
    private string tileName;

    private float blinkTimer = 0f;
    private MouseState previousMouseState = Mouse.GetState();

    public TilePlacer(string tileName) : base("tile_placer", Vector2.Zero)
    {
        string jsonFileContents = File.ReadAllText("Content/data/tile/" + tileName + ".json");
        TileModel tileModel = JsonSerializer.Deserialize<TileModel>(jsonFileContents);

        this.tileModel = tileModel;

        this.tileName = tileName;
        RenderLayer.Order = 100;
    }

    public override void SetComponents()
    {
        Sprite sprite = new Sprite(this, "tile/" + tileModel.Texture);
        
        AddComponent(sprite);
    }

    public override void Update()
    {
        base.Update();

        Sprite sprite = GetComponent<Sprite>();
        MouseState mouseState = Mouse.GetState();

        Vector2 mousePosition = new Vector2(mouseState.Position.X, mouseState.Position.Y);
        Vector2 screenCenter = new Vector2(Main.GraphicsDeviceManager.PreferredBackBufferWidth / 2, Main.GraphicsDeviceManager.PreferredBackBufferHeight / 2);
        Vector2 mouseWorldPosition = ((mousePosition - screenCenter) / Main.SceneManager.CurrentScene.Camera.Zoom) + Main.SceneManager.CurrentScene.Camera.Position;

        Position = Vector2.Round(mouseWorldPosition / 16) * 16;

        Color startColor = Color.White * 0.3f;
        Color endColor = Color.White * 0.7f;

        float blinkSpeed = 6f;
        blinkTimer += blinkSpeed * Main.DeltaTime;

        float lerpTime = (float)Math.Sin(blinkTimer) * 0.5f + 0.5f;

        sprite.Color = Color.Lerp(startColor, endColor, lerpTime);

        if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released && !ImGui.GetIO().WantCaptureMouse)
        {
            TileGrid tileGrid = Main.SceneManager.CurrentScene.GetGameObject<TestRoomTileGrid>().GetComponent<TileGrid>();

            tileGrid.PlaceTile(new Vector2((int)(Position.X / 16), (int)(Position.Y / 16)), tileName);
        }

        previousMouseState = mouseState;
    }

    public override SaveData Save()
    {
        return SaveData;
    }
}
