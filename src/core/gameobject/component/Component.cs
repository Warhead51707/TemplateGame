using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGame;

public abstract class Component
{
    public GameObject Parent { get; private set; }

    public Component(GameObject parent)
    {
        Parent = parent;
    }

    public virtual void Initialize()
    {
        
    }
}
