using Microsoft.Xna.Framework.Graphics;
using System.Numerics;

namespace TemplateGame;

public class RenderLayer
{
    public int Order { get; set; } = 0;
    public RenderSettings RenderSettings { get; protected set; }
    public RenderTarget2D RenderTarget { get; set; } = null;
    public bool HUD { get; protected set; } = false;

    public RenderLayer(RenderSettings renderSettings, int order, RenderTarget2D renderTarget = null, bool hud = false)
    {
        RenderSettings = renderSettings;
        Order = order;
        RenderTarget = renderTarget;
        HUD = hud;
    }
}
