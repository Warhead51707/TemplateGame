using Microsoft.Xna.Framework;
using MonoGame.ImGuiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGame;
public class ImGuiManager
{
    public ImGuiRenderer Renderer { get; private set; }

    private List<DebugUI> debugUIs = new List<DebugUI>();

    public ImGuiManager(Game game)
    {
        Renderer = new ImGuiRenderer(game);

        Renderer.RebuildFontAtlas();
    }

    public void Add(DebugUI ui)
    {
        debugUIs.Add(ui);
    }

    public void Remove(DebugUI ui)
    {
        debugUIs.Remove(ui);
    }

    public void Draw()
    {
        Renderer.BeginLayout(Main.GameTime);

        foreach (DebugUI ui in debugUIs)
        {
            ui.Draw();
        }

        Renderer.EndLayout();
    }
}
