using System.Collections.Generic;
using TemplateGame.src.core;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Linq;
using System;
using System.Diagnostics;

namespace TemplateGame;

 public abstract class Scene
{
    public string Name { get; protected set; }
    public SceneCamera Camera { get; protected set; } = new SceneCamera();
    public List<Updater> Updators { get; protected set; } = new List<Updater>();
    public List<Drawer> Drawers { get; protected set; } = new List<Drawer>();

    public Scene(string name)
    {
        Name = name;
    }

    public virtual void Initialize() { }

    public virtual void Update()
    {
        Camera.Update();

        foreach (Updater updater in Updators)
        {
            updater.Update();
        }
    }

    public virtual void Draw()
    {
        var drawersByLayer = Drawers.GroupBy(d => d.RenderLayer).OrderBy(g => g.Key.Order);

        foreach (var drawerGroup in drawersByLayer)
        {
            RenderLayer renderLayer = drawerGroup.Key;
            RenderSettings renderSettings = renderLayer.RenderSettings;

            Main.MainGraphicsDevice.SetRenderTarget(renderLayer.RenderTarget);

            DrawManager.SpriteBatch.Begin(renderSettings.SortMode, renderSettings.BlendState, renderSettings.SamplerState, renderSettings.DepthStencilState, renderSettings.RasterizerState, renderSettings.Effect, Camera.Matrix);

            foreach (Drawer drawer in drawerGroup)
            {
                drawer.Draw();
            }

            DrawManager.SpriteBatch.End();
        }

        foreach (var drawerGroup in drawersByLayer.OrderBy(dg => dg.Key.Order))
        {
            RenderLayer renderLayer = drawerGroup.Key;

            if (renderLayer.RenderTarget == null) continue;

            DrawManager.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            DrawManager.SpriteBatch.Draw(renderLayer.RenderTarget, Vector2.Zero, Color.White);
            DrawManager.SpriteBatch.End();
        }
    }

    public void AddGameObject(GameObject gameObject)
    {
        gameObject.Initialize();

        Updators.Add(gameObject);
        Drawers.Add(gameObject);
    }
}
