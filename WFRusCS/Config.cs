using System.Text.Json.Serialization;

namespace WFRus;

public class Config {
    [JsonInclude] public bool SomeSetting = true;
}
