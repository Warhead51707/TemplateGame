using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGame;
public abstract class GameObject
{
    public int Priority { get; set; } = 0;
    public string Name { get; protected set; }
    public Vector2 Position { get; protected set; }
    public RenderLayer RenderLayer { get; protected set; }

    private List<Component> Components = new List<Component>();

    public GameObject(string name, Vector2 position, int priority = 0)
    {
       Priority = priority;
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
