using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace WFRus.modifies;

public class SplashScreen(WFRusMod mod) : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Menus/Splash/splash.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        // wait for any newline after any reference to "_ready"
        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "_ready"},
            t => t.Type is TokenType.Newline
        ], allowPartialMatch: true);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (waiter.Check(token)) {
                yield return new Token(TokenType.Newline, 1);
                IEnumerable<Token> tkns = [];
                
                tkns = tkns.Concat(Helpers.LoadExtImage("lame", mod.Config.splashLamePath));
                tkns = tkns.Concat(Helpers.LoadExtImage("god", mod.Config.splashGodotPath));
                
                foreach (var tkn in tkns) yield return tkn;
                yield return new Token(TokenType.Newline, 1);
            } else {
                // return the original token
                yield return token;
            }
        }
    }
}
