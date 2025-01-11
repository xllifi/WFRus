using System.Text.Json.Serialization;

namespace WFRus;

public class Config {
    [JsonInclude] public string mainmenuLogoPath = Directory.GetCurrentDirectory() + "/GDWeave/mods/WFRus/mainmenuLogo.png";
    [JsonInclude] public string splashLamePath = Directory.GetCurrentDirectory() + "/GDWeave/mods/WFRus/splashLame.png";
    [JsonInclude] public string splashGodotPath = Directory.GetCurrentDirectory() + "/GDWeave/mods/WFRus/splashGodot.png";
}
