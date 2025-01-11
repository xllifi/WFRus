using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace WFRus.modifies;

public class OptionsMenu : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Singletons/OptionsMenu/options_menu.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        // wait for any newline after any reference to "_ready"
        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "_update_options"},
            t => t is IdentifierToken {Name: "altfont"},
            t => t is IdentifierToken {Name: "default_font"},
            t => t.Type is TokenType.OpAssign,
            t => t.Type is TokenType.PrPreload
        ], allowPartialMatch: true);
        var waiter2 = new TokenWaiter(t => false);

        var enmr = tokens.ToList().GetEnumerator();
        bool hasNext = enmr.MoveNext();
        
        while (hasNext) {
            var token = enmr.Current;
            hasNext = enmr.MoveNext();
            
            if (waiter.Check(token)) {
                Console.WriteLine("[WFRusCS] Injecting options menu (preload -> load)");
                Console.WriteLine(tokens.ToList().IndexOf(token));
                yield return new Token(TokenType.BuiltInFunc, (uint?) BuiltinFunction.ResourceLoad);
                waiter2 = new TokenWaiter(t => t is ConstantToken);
            } else if (waiter2.Check(token)) {
                Console.WriteLine("[WFRusCS] Injecting options menu (main_font.tres -> cyrillic.tres)");
                Console.WriteLine(tokens.ToList().IndexOf(token));
                yield return new ConstantToken(new StringVariant("res://mods/WFRus/Cyrillic/cyrillic.tres"));
                waiter2 = new TokenWaiter(t => false);
            } else {
                // return the original token
                yield return token;
            }
        }
    }
}