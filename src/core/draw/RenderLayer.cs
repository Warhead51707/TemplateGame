using Microsoft.Xna.Framework.Graphics;
using System.Numerics;

namespace TemplateGame;

public class RenderLayer
{
    public int Order { get; protected set; }
    public RenderSettings RenderSettings { get; protected set; }
    public RenderTarget2D RenderTarget { get; protected set; } = null;

    public RenderLayer(RenderSettings renderSettings, int order, RenderTarget2D renderTarget = null)
    {
        RenderSettings = renderSettings;
        Order = order;
        RenderTarget = renderTarget;
    }
}
