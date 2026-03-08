using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TemplateGame;
public abstract class GameObject
{
    public int Priority { get; set; } = 0;
    public string Name { get; set; }
    public Vector2 Position { get; set; }
    public RenderLayer RenderLayer { get; set; }
    public List<Component> Components { get; set; } = new List<Component>();
    public SaveData SaveData { get; set; } = new SaveData();
    public bool IsDirty { get; set; } = false;

    private Func<GameObject> registerFunc;

    public GameObject(string name, Vector2 position, int priority = 0)
    {
        Priority = priority;
        Name = name;
        Position = position;
        RenderLayer = new RenderLayer(RenderSettings.Default, 0, null);
    }

    public GameObject(string name, Vector2 position, Func<GameObject> register, int priority = 0)
    {
        Priority = priority;
        Name = name;
        Position = position;
        RenderLayer = new RenderLayer(RenderSettings.Default, 0, null);
        registerFunc = register;
    }

    protected void Register(Func<GameObject> func)
    {
        GameObjectRegistry.Register(func);
    }

    public virtual void SetComponents()
    {

    }

    public virtual void Initialize()
    {
        SetComponents();

        InitializeComponents();

        if (registerFunc != null)
        {
            Register(registerFunc);
        }
    }

    public virtual void Update()
    {
        foreach (Component component in Components)
        {
            component.Update();
        }
    }

    public virtual void Draw()
    {
        foreach (Component component in Components)
        {
            component.Draw();
        }
    }

    public virtual SaveData Save()
    {
        SaveData.Json["name"] = JsonSerializer.SerializeToElement(Name);
        SaveData.Json["position"] = JsonSerializer.SerializeToElement(new { x = Position.X, y = Position.Y });

        List<SaveData> componentSaveData = new List<SaveData>();

        foreach (Component component in Components)
        {
            SaveData cSaveData = component.Save();

            if (cSaveData.Json.Count == 1) continue;

            componentSaveData.Add(cSaveData);
        }

        SaveData.Json["components"] = JsonSerializer.SerializeToElement(componentSaveData);

        return SaveData;
    }

    public virtual void Load(SaveData saveData)
    {
        SaveData = saveData;

        foreach (Component component in Components)
        {
            component.Load(saveData);
        }
    }

    public void AddComponent(Component component)
    {
        Components.Add(component);
    }

    public void RemoveComponent(Component component)
    {
        Components.Remove(component);
    }

    public T GetComponent<T>() where T : Component
    {
        return Components.FirstOrDefault(component => component is T) as T;
    }

    public void Destroy()
    {
        Main.SceneManager.CurrentScene.RemoveGameObject(this);
    }
    private void InitializeComponents()
    {
        foreach (Component component in Components)
        {
            component.Initialize();
        }
    }
}
