using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGame;
public class SceneManager
{
    public Scene CurrentScene { get; private set; }

    public SceneManager()
    {
    }

    public void SetCurrentScene(Scene scene)
    {
        scene.Initialize();

        CurrentScene = scene;
    }

    public void Update()
    {
        CurrentScene.Update();
    }

    public void Draw()
    {
        CurrentScene.Draw();
    }
}

