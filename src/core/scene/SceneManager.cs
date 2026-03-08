using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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

    public void LoadScene(string fileName)
    {
        string jsonFileContents = File.ReadAllText("Content/data/scene/" + fileName + ".json");
        SceneModel sceneModel = JsonSerializer.Deserialize<SceneModel>(jsonFileContents);

        Scene scene = SceneRegistry.Create(sceneModel.internalName);

        scene.Load(sceneModel);

        CurrentScene = scene;
    }
}

