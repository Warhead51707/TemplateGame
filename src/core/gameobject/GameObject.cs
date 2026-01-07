using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateGame.src.core;

namespace TemplateGame;
public class GameObject : Updater, Drawer
{
    public string Name { get; protected set; }
    public Vector2 Position { get; protected set; }
    public RenderLayer RenderLayer { get; protected set; }

    private List<Component> Components = new List<Component>();

    public GameObject(string name, Vector2 position)
    {
       Name = name;
       Position = position;
       RenderLayer = new RenderLayer(RenderSettings.Default, 0, null);
    }

    public virtual void Initialize()
    {
        InitializeComponents();
    }

    public virtual void Update()
    {
        foreach (Component component in Components)
        {
            if (component is Updater updater == false) continue;

            updater.Update();
        }
    }

    public virtual void Draw()
    {
        foreach (Component component in Components)
        {
            if (component is Drawer drawer == false) continue;

            drawer.Draw();
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
    private void InitializeComponents()
    {
        foreach (Component component in Components)
        {
            component.Initialize();
        }
    }
}
