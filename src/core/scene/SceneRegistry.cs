using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGame;

public static class SceneRegistry
{
    public static Dictionary<string, Func<Scene>> registry = new Dictionary<string, Func<Scene>>();

    public static void Register(Func<Scene> func)
    {
        if (func == null || registry.ContainsValue(func)) return;

        registry.Add(func().Name, func);
    }

    public static Scene Create(string name)
    {
        if (registry.ContainsKey(name))
        {
            return registry[name]();
        }

        return null;
    }
}


