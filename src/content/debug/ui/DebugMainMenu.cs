using ImGuiNET;
using System.IO;
using System;
using System.Linq;

namespace TemplateGame;

public class DebugMainMenu : DebugUI
{
    // Editor Windows
    public bool tilemapEditor = false;

    // Tilemap editor
    public string[] tiles = null;
    public int selectedTileIndex = 0;

    // Scene Windows
    public bool sceneSaveLoad = false;
    public bool sceneSettings = false;

    // Scene Loading
    public string[] scenesToLoad = null;
    public int selectedSceneIndex = 0;

    public override void Draw()
    {
        MenuBar();

        if (tilemapEditor)
        {
            TileMapEditor();
        }

        if (sceneSettings)
        {
            SceneSettings();
        }

        if (sceneSaveLoad)
        {
            SceneSaveLoad();
        }
    }

    private void MenuBar()
    {
        if (ImGui.BeginMainMenuBar())
        {
            if (ImGui.BeginMenu("Editor"))
            {
                if (ImGui.MenuItem("Tilemap Editor"))
                {
                    tilemapEditor = true;
                }
                ImGui.EndMenu();
            }

            if (ImGui.BeginMenu("Scene"))
            {
                if (ImGui.MenuItem("Settings"))
                {
                    sceneSettings = true;
                }

                if (ImGui.MenuItem("Save/Load"))
                {
                    sceneSaveLoad = true;
                }

                ImGui.EndMenu();
            }

            ImGui.EndMainMenuBar();
        }
    }

    public void TileMapEditor()
    {
        Scene currentScene = Main.SceneManager.CurrentScene;
        PlayerDebugTools playerDebugTools = currentScene.GetGameObject<Player>().GetComponent<PlayerDebugTools>();

        if (tiles == null)
        {
            string contentPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content/tiles");

            if (Directory.Exists(contentPath))
            {
                string[] fileNames = Directory.GetFiles(contentPath, "*.png").Select(Path.GetFileNameWithoutExtension).ToArray();

                scenesToLoad = fileNames;
            }
        }

        ImGui.Begin("Tilemap Editor", ref tilemapEditor);

        if (ImGui.Button("Toggle Tilemap Editor"))
        {
            playerDebugTools.ToggleTilemapEditor();
        }

        ImGui.Text("Tilemap Editor - " + (playerDebugTools.tilemapEditorEnabled ? "Enabled" : "Disabled"));

        ImGui.Text("Tiles:");
        ImGui.Combo("", ref selectedTileIndex, tiles, tiles.Length);
    }

    public void SceneSettings()
    {
        Scene currentScene = Main.SceneManager.CurrentScene;
        PlayerDebugTools playerDebugTools = currentScene.GetGameObject<Player>().GetComponent<PlayerDebugTools>();

        ImGui.Begin("Scene - Settings", ref sceneSettings);

        ImGui.Text("Settings:");

        if (ImGui.Button("Toggle Free Cam"))
        {
            if (playerDebugTools.debugCamEnabled)
            {
                currentScene.Camera.Zoom = currentScene.Camera.DefaultZoom;
                currentScene.Camera.SetTarget(currentScene.GetGameObject<Player>());
            } else
            {
                currentScene.Camera.ResetTarget();
            }

            playerDebugTools.ToggleDebugCam();
        }

        ImGui.End();
    }

    public void SceneSaveLoad()
    {
        if (scenesToLoad == null)
        {
            string contentPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content/scene");

            if (Directory.Exists(contentPath))
            {
                string[] fileNames = Directory.GetFiles(contentPath, "*.json").Select(Path.GetFileNameWithoutExtension).ToArray();

                scenesToLoad = fileNames;
            }
        }

        Scene currentScene = Main.SceneManager.CurrentScene;

        ImGui.Begin("Scene - Save/Load", ref sceneSaveLoad);

        ImGui.Text("Save:");

        if (ImGui.Button("Save Scene"))
        {
            currentScene.Save();
        }

        ImGui.Text("Load:");

        ImGui.Combo("Scenes", ref selectedSceneIndex, scenesToLoad, scenesToLoad.Length);

        if (ImGui.Button("Load Scene"))
        {
            Main.SceneManager.LoadScene(scenesToLoad[selectedSceneIndex]);
        }

        ImGui.End();
    }
}
