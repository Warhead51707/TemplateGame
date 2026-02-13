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
        ImGui.Begin("Scene - Settings", ref sceneSettings);

        ImGui.Text("Settings:");

        ImGui.Text("Debug Mode - " + Main.DebugMode);

        if (ImGui.Button("Toggle Debug Mode"))
        {
            if (Main.DebugMode)
            {
                Main.DebugMode = false;
                Main.SceneManager.CurrentScene.Camera.SetTarget(Main.SceneManager.CurrentScene.GetGameObject<Player>());
            } else
            {
                Main.DebugMode = true;
                Main.SceneManager.CurrentScene.Camera.ResetTarget();
            }
        }

        ImGui.End();
    }

    public void SceneSaveLoad()
    {
        ImGui.Begin("Scene - Save/Load", ref sceneSaveLoad);

        ImGui.Text("Save:");

        if (ImGui.Button("Save Scene"))
        {
            Main.SceneManager.CurrentScene.Save();
        }

        ImGui.End();
    }
}
