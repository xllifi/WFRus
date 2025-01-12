using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace WFRus.modifies;

public class PlayerVoice : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "_sync_talk"},
            t => t is IdentifierToken {Name: "blacklist"},
            t => t.Type is TokenType.BracketClose
        ], allowPartialMatch: true);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (waiter.Check(token)) {
                List<string> ruChars = "абвгдеёжзийклмнопрстуфхцчшщьыэюя".ToList().Select(c => c.ToString()).ToList();
                foreach (var ch in ruChars) {
                    yield return new Token(TokenType.Comma);
                    yield return new ConstantToken(new StringVariant(ch));
                }

                yield return token;
            } else {
                // return the original token
                yield return token;
            }
        }
    }
}
