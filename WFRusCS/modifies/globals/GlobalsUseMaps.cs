using GDWeave.Godot;
using GDWeave.Modding;

namespace WFRus.modifies.globals;

public class GlobalsUseMaps : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Singletons/globals.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "_add_resource"},
            t => t is IdentifierToken {Name: "new"},
            t => t is {Type: TokenType.BuiltInFunc, AssociatedData: (uint?) BuiltinFunction.ResourceLoad},
            t => t.Type is TokenType.Newline
        ], allowPartialMatch: true);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (waiter.Check(token)) {
                IEnumerable<Token> tkns = [];

                // Titles
                tkns = tkns.Concat(ScriptTokenizer.Tokenize("""if file_name.rfind("title_") != -1:""", 1));
                tkns = tkns.Concat(ScriptTokenizer.Tokenize("""if new["file"].title in titlenamedict:""", 2));
                tkns = tkns.Concat(ScriptTokenizer.Tokenize("""var titlename = titlenamedict[new["file"].title]""", 3));
                tkns = tkns.Concat(ScriptTokenizer.Tokenize("""new["file"].title = titlename""", 3));
                tkns = tkns.Concat(ScriptTokenizer.Tokenize("""new["file"].name = "Звание «" + titlename + "»" """, 3));
                tkns = tkns.Concat(ScriptTokenizer.Tokenize("""new["file"].desc = titledescdict[new["file"].desc]""", 3));
                tkns = tkns.Concat(ScriptTokenizer.Tokenize("""elif new["file"].name == "No Title":""", 2));
                tkns = tkns.Concat(ScriptTokenizer.Tokenize("""new["file"].name = "Нет звания" """, 3));
                tkns = tkns.Concat(ScriptTokenizer.Tokenize("""new["file"].desc = "Без звания!" """, 3));

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