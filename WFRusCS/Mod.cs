using GDWeave;
using Serilog;
using WFRus.modifies;

namespace WFRus;

public class Mod : IMod {
    public Config Config;
    public ILogger ModLogger;

    public Mod(IModInterface modInterface) {
        this.Config = modInterface.ReadConfig<Config>();
        this.ModLogger = modInterface.Logger;
        modInterface.RegisterScriptMod(new MainMenu());
        modInterface.RegisterScriptMod(new SpeechBubble());
        this.ModLogger.Information("[WFRusCS] C# Mod initialized!");
    }

    public void Dispose() {
        // Cleanup anything you do here
    }
}
