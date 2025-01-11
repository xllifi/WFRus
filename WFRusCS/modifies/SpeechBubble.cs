using GDWeave.Godot;
using GDWeave.Modding;

namespace WFRus.modifies;

public class SpeechBubble : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/SpeechBubble/speech_bubble.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "_ready"},
            t => t.Type is TokenType.Newline
        ], allowPartialMatch: true);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (waiter.Check(token)) {
                // found our match, return the original newline
                yield return token;

                IEnumerable<Token> tkns = Helpers.SetFont("RichTextLabel");
                foreach (var tkn in tkns) yield return tkn;

                // don't forget another newline!
                yield return new Token(TokenType.Newline, 1);
            } else {
                // return the original token
                yield return token;
            }
        }
    }
}
