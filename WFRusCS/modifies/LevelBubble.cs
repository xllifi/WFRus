using GDWeave.Godot;
using GDWeave.Modding;

namespace WFRus.modifies;

public class LevelBubble : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/SpeechBubble/level_bubble.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        // wait for any newline after any reference to "_ready"
        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "time"},
            t => t.Type is TokenType.Newline
        ], allowPartialMatch: true);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (waiter.Check(token)) {
                IEnumerable<Token> tkns = [];

                tkns = tkns.Concat(ScriptTokenizer.Tokenize("""func _ready():"""));
                tkns = tkns.Concat(ScriptTokenizer.Tokenize("""	$RichTextLabel.visible_characters = 20""", 1));
                tkns = tkns.Concat(ScriptTokenizer.Tokenize("""	$RichTextLabel.bbcode_text = "[center][wave amp=80.0 freq=5.0 connected=1]НОВЫЙ УРОВЕНЬ!" """, 1));

                foreach (var tkn in tkns) yield return tkn;
            } else {
                // return the original token
                yield return token;
            }
        }
    }
}