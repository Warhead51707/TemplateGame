using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGame;

public class RenderLayers
{
    public static RenderLayer Default = new RenderLayer(RenderSettings.Default, 0, new RenderTarget2D(Main.MainGraphicsDevice, Main.GraphicsDeviceManager.PreferredBackBufferWidth, Main.GraphicsDeviceManager.PreferredBackBufferHeight, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents));
    public static RenderLayer Overlay = new RenderLayer(RenderSettings.Overlay, 0, new RenderTarget2D(Main.MainGraphicsDevice, Main.GraphicsDeviceManager.PreferredBackBufferWidth, Main.GraphicsDeviceManager.PreferredBackBufferHeight, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents), true);

    public static void UpdateRenderTargets()
    {
        Default.RenderTarget.Dispose();
        Overlay.RenderTarget.Dispose();

        Default.RenderTarget = new RenderTarget2D(Main.MainGraphicsDevice, Main.GameWindow.ClientBounds.Width, Main.GameWindow.ClientBounds.Height, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
        Overlay.RenderTarget = new RenderTarget2D(Main.MainGraphicsDevice, Main.GameWindow.ClientBounds.Width, Main.GameWindow.ClientBounds.Height, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
    }
}
