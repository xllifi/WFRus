using GDWeave;
using Serilog;
using WFRus.modifies;
using WFRus.modifies.globals;

namespace WFRus;

public class WFRusMod : IMod {
    public Config Config;
    public ILogger Logger;

    public WFRusMod(IModInterface modInterface) {
        Config = modInterface.ReadConfig<Config>();
        Logger = modInterface.Logger;
        modInterface.RegisterScriptMod(new Voice());
        modInterface.RegisterScriptMod(new PlayerVoice());
        modInterface.RegisterScriptMod(new GlobalsAddMaps());
        modInterface.RegisterScriptMod(new GlobalsUseMaps());
        modInterface.RegisterScriptMod(new SplashScreen(this));
        modInterface.RegisterScriptMod(new MainMenu(this));
        modInterface.RegisterScriptMod(new OptionsMenu());
        modInterface.RegisterScriptMod(new SpeechBubble());
        modInterface.RegisterScriptMod(new LevelBubble());
        Logger.Information("[WFRusCS] C# Mod initialized!");
    }

    public void Dispose() {
        // Cleanup anything you do here
    }
}
