using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.ImGuiNet;

namespace TemplateGame;

public class Main : Game
{
    // References
    public static GraphicsDeviceManager GraphicsDeviceManager;
    public static GraphicsDevice MainGraphicsDevice;
    public static ContentManager ContentManager;
    public static SceneManager SceneManager = new SceneManager();

    public ImGuiManager ImGuiManager;

    // Properties
    public static GameTime GameTime { get ; private set; }
    public static float DeltaTime { get; private set; }

    public Main()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // Settings
        GraphicsDeviceManager.HardwareModeSwitch = false;
        GraphicsDeviceManager.IsFullScreen = false;
        GraphicsDeviceManager.ApplyChanges();
        IsFixedTimeStep = false;

        // Window Properties
        Window.AllowUserResizing = true;

        //References 
        MainGraphicsDevice = GraphicsDevice;
        ContentManager = Content;

        // Debug UI
        ImGuiManager = new ImGuiManager(this);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // Main Spritebatch
        DrawManager.Initialize(GraphicsDevice);

        // Debug UI
        ImGuiManager.Add(new DebugMainMenu());

        // Set Scene
        TestRoom testRoom = new TestRoom();

        SceneManager.SetCurrentScene(testRoom);
    }

    protected override void Update(GameTime gameTime)
    {
        GameTime = gameTime;
        DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        SceneManager.Update();
  
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        SceneManager.Draw();

#if DEBUG
        ImGuiManager.Draw();
#endif

        base.Draw(gameTime);
    }
}
