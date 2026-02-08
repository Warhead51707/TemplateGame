using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TemplateGame;
public class SceneModel
{
    [JsonPropertyName("gameobjects")]
    public List<SaveData> gameObjectsSaveData { get; set; } = new List<SaveData>();

    public SceneModel(List<SaveData> gameObjectsSaveData)
    {
        this.gameObjectsSaveData = gameObjectsSaveData;
    }
}
