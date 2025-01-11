using GDWeave;
using Serilog;
using WFRus.modifies;
using WFRus.modifies.titles;

namespace WFRus;

public class Mod : IMod {
    public Config Config;
    public ILogger ModLogger;

    public Mod(IModInterface modInterface) {
        this.Config = modInterface.ReadConfig<Config>();
        this.ModLogger = modInterface.Logger;
        modInterface.RegisterScriptMod(new MainMenu());
        modInterface.RegisterScriptMod(new SpeechBubble());
        modInterface.RegisterScriptMod(new GlobalsAddMaps());
        modInterface.RegisterScriptMod(new GlobalsUseMaps());
        this.ModLogger.Information("[WFRusCS] C# Mod initialized!");
    }

    public void Dispose() {
        // Cleanup anything you do here
    }
}
