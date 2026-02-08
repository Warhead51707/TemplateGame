using ImGuiNET;

namespace TemplateGame;

public class DebugMainMenu : DebugUI
{
    public bool sceneSaveLoad = false;

    public override void Draw()
    {
        MenuBar();

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
                if (ImGui.MenuItem("Save/Load"))
                {
                    sceneSaveLoad = true;
                }

                ImGui.EndMenu();
            }

            ImGui.EndMainMenuBar();
        }
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
