using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TemplateGame;
public abstract class Component
{
    public GameObject Parent { get; private set; }
    public SaveData SaveData { get; private set; } = new SaveData();

    public Component(GameObject parent)
    {
        Parent = parent;
    }

    public virtual void Initialize()
    {
        
    }

    public virtual void Update()
    {

    }

    public virtual void Draw()
    {

    }

    public virtual SaveData Save()
    {
        return SaveData;
    }

    public virtual void Load(SaveData saveData)
    {
        SaveData = saveData;
    }
}
