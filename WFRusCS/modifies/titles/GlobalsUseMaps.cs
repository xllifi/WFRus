using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace WFRus.modifies.titles;

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
                yield return new Token(TokenType.Newline, 1); // Indent once
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("file_name");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("rfind");
                yield return new Token(TokenType.ParenthesisOpen);
                /**/yield return new ConstantToken(new StringVariant("title_"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpNotEqual);
                yield return new ConstantToken(new IntVariant(-1));
                yield return new Token(TokenType.Colon);
                
                yield return new Token(TokenType.Newline, 2); // Indent twice
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("new");
                yield return new Token(TokenType.BracketOpen);
                /**/yield return new ConstantToken(new StringVariant("file"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("title");
                yield return new Token(TokenType.OpIn);
                yield return new IdentifierToken("titlenamemap");
                yield return new Token(TokenType.Colon);
                
                yield return new Token(TokenType.Newline, 3); // Indent thrice
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("titlename");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("titlenamemap");
                yield return new Token(TokenType.BracketOpen);
                /**/yield return new IdentifierToken("new");
                /**/yield return new Token(TokenType.BracketOpen);
                /**//**/yield return new ConstantToken(new StringVariant("file"));
                /**/yield return new Token(TokenType.BracketClose);
                /**/yield return new Token(TokenType.Period);
                /**/yield return new IdentifierToken("title");
                yield return new Token(TokenType.BracketClose);
                
                yield return new Token(TokenType.Newline, 3); // Indent thrice
                yield return new IdentifierToken("new");
                yield return new Token(TokenType.BracketOpen);
                /**/yield return new ConstantToken(new StringVariant("file"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("title");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("titlename");
                
                yield return new Token(TokenType.Newline, 3); // Indent thrice
                yield return new IdentifierToken("new");
                yield return new Token(TokenType.BracketOpen);
                /**/yield return new ConstantToken(new StringVariant("file"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("name");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("Звание «"));
                yield return new Token(TokenType.OpAdd);
                yield return new IdentifierToken("titlename");
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new StringVariant("»"));
                
                yield return new Token(TokenType.Newline, 3); // Indent thrice
                yield return new IdentifierToken("new");
                yield return new Token(TokenType.BracketOpen);
                /**/yield return new IdentifierToken("file");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("desc");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("titledescmap");
                yield return new Token(TokenType.BracketOpen);
                /**/yield return new IdentifierToken("new");
                /**/yield return new Token(TokenType.BracketOpen);
                /******/yield return new IdentifierToken("file");
                /**/yield return new Token(TokenType.BracketClose);
                /**/yield return new Token(TokenType.Period);
                /**/yield return new IdentifierToken("title");
                yield return new Token(TokenType.BracketClose);

                IEnumerable<Token> tkns = [];
                tkns = tkns.Concat(ScriptTokenizer.Tokenize($"elif new[\"file\"].name == \"No Title\":", 2));
                tkns = tkns.Concat(ScriptTokenizer.Tokenize($"new[\"file\"].name = \"Нет звания\"", 3));
                tkns = tkns.Concat(ScriptTokenizer.Tokenize($"new[\"file\"].desc = \"Без звания!\"", 3));

                foreach (var tkn in tkns) {
                    yield return tkn;
                }
                
                // don't forget another newline!
                yield return new Token(TokenType.Newline, 1);
            } else {
                // return the original token
                yield return token;
            }
        }
    }
}