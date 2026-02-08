using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TemplateGame;
public class SaveData
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement> Json { get; set; } = new Dictionary<string, JsonElement>();
}
