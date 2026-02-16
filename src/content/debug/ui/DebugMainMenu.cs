using ImGuiNET;

namespace TemplateGame;

public class DebugMainMenu : DebugUI
{
    public bool sceneSaveLoad = false;
    public bool sceneSettings = false;

    public override void Draw()
    {
        MenuBar();

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
            if (ImGui.BeginMenu("Engine"))
            {

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

    public void SceneSettings()
    {
        Scene currentScene = Main.SceneManager.CurrentScene;

        ImGui.Begin("Scene - Settings", ref sceneSettings);

        ImGui.Text("Settings:");

        ImGui.Text("Debug Mode - " + Main.DebugMode);

        if (ImGui.Button("Toggle Debug Mode"))
        {
            if (Main.DebugMode)
            {
                Main.DebugMode = false;
                currentScene.Camera.Zoom = currentScene.Camera.DefaultZoom;
                currentScene.Camera.SetTarget(currentScene.GetGameObject<Player>());
            } else
            {
                Main.DebugMode = true;
                currentScene.Camera.ResetTarget();
            }
        }

        ImGui.End();
    }

    public void SceneSaveLoad()
    {
        Scene currentScene = Main.SceneManager.CurrentScene;

        ImGui.Begin("Scene - Save/Load", ref sceneSaveLoad);

        ImGui.Text("Save:");

        if (ImGui.Button("Save Scene"))
        {
            currentScene.Save();
        }

        ImGui.End();
    }
}
