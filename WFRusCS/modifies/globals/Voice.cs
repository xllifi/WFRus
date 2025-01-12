using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace WFRus.modifies.globals;

public class Voice : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Singletons/globals.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "_generate_voice_bank"},
            t => t is IdentifierToken {Name: "voice_bank"},
            t => t is IdentifierToken {Name: "path"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken
        ], allowPartialMatch: true);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (waiter.Check(token)) {
                yield return new ConstantToken(new StringVariant("res://mods/WFRus/RuVoice/"));
            } else {
                // return the original token
                yield return token;
            }
        }
    }
}