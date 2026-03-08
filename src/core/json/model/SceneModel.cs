using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TemplateGame;
public class SceneModel
{
    [JsonPropertyName("internal_name")]
    public string internalName { get; set; } = "";

    [JsonPropertyName("gameobjects")]
    public List<SaveData> gameObjectsSaveData { get; set; } = new List<SaveData>();

    public SceneModel(string internalName, List<SaveData> gameObjectsSaveData)
    {
        this.internalName = internalName;
        this.gameObjectsSaveData = gameObjectsSaveData;
    }
}
