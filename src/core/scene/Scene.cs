using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Linq;
using System;
using System.Diagnostics;
using System.Text.Json;
using System.IO;

namespace TemplateGame;

public enum SceneState
{
    Play, Pause
}

 public abstract class Scene
{
    public string Name { get; protected set; }
    public SceneCamera Camera { get; protected set; } = new SceneCamera();
    public SceneState PlayState { get; set; } = SceneState.Play;
    public bool DebugMode
    {
        get
        {
            return Main.DebugMode;
        }
    }

    private List<GameObject> gameObjects = new List<GameObject>();
    private IEnumerable<IGrouping<RenderLayer, GameObject>> drawCache = Enumerable.Empty<IGrouping<RenderLayer, GameObject>>();

    public Scene(string name)
    {
        Name = name;
    }

    public virtual void Initialize() { }

    public virtual void Update()
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.Update();
        }

        Camera.Update();
    }

    public virtual void Draw()
    {
        foreach (var drawerGroup in drawCache)
        {
            RenderLayer renderLayer = drawerGroup.Key;
            RenderSettings renderSettings = renderLayer.RenderSettings;

            bool hasRenderTarget = false;

            if (renderLayer.RenderTarget != null)
            {
                hasRenderTarget = true;
                Main.MainGraphicsDevice.SetRenderTarget(renderLayer.RenderTarget);
            }

            DrawManager.SpriteBatch.Begin(renderSettings.SortMode, renderSettings.BlendState, renderSettings.SamplerState, renderSettings.DepthStencilState, renderSettings.RasterizerState, renderSettings.Effect, Camera.Matrix);

            foreach (GameObject drawer in drawerGroup)
            {
                drawer.Draw();
            }

            DrawManager.SpriteBatch.End();

            if (hasRenderTarget)
            {
                Main.MainGraphicsDevice.SetRenderTarget(null);

                DrawManager.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                DrawManager.SpriteBatch.Draw(renderLayer.RenderTarget, Vector2.Zero, Color.White);
                DrawManager.SpriteBatch.End();
            }
        }
    }

    public void AddGameObject(GameObject gameObject)
    {
        gameObject.Initialize();

        gameObjects.Add(gameObject);

        gameObjects = gameObjects.OrderByDescending(g => g.Priority).ToList();
        drawCache = gameObjects.GroupBy(d => d.RenderLayer).OrderBy(g => g.Key.Order);
    }

    public T GetGameObject<T>() where T : GameObject
    {
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject is T)
            {
                return (T)gameObject;
            }
        }
        return null;
    }

    public virtual void Save()
    {
        List<SaveData> saveData = new List<SaveData>();

        foreach (GameObject gameObject in gameObjects)
        {
            saveData.Add(gameObject.Save());
        }

        SceneModel model = new SceneModel(saveData);

        JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(model, jsonSerializerOptions);

        File.WriteAllText("Content/scene/" + Name + ".json", json);
    }
}
