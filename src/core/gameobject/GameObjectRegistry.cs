using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGame;
public static class GameObjectRegistry
{
    public static Dictionary<string, Func<GameObject>> registry = new Dictionary<string, Func<GameObject>>();

    public static void Register(Func<GameObject> func)
    {
        registry.Add(func().Name, func);
    }

    public static GameObject Create(string name)
    {
        if (registry.ContainsKey(name))
        {
            return registry[name]();
        }

        return null;
    }
}
