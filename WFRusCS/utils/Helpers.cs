using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace WFRus;

public static class Helpers {
    public static IEnumerable<Token> SetFont(string node, uint indents = 0) {
        IEnumerable<Token> tkns = [];
    
        tkns = tkns.Append(new Token(TokenType.Dollar));
        tkns = tkns.Append(new IdentifierToken(node));
        tkns = tkns.Append(new Token(TokenType.Period));
        tkns = tkns.Append(new IdentifierToken("get"));
        tkns = tkns.Append(new Token(TokenType.ParenthesisOpen));
        tkns = tkns.Append(new ConstantToken(new StringVariant("custom_fonts/font")));
        tkns = tkns.Append(new Token(TokenType.ParenthesisClose));
        tkns = tkns.Append(new Token(TokenType.Period));
        tkns = tkns.Append(new IdentifierToken("set"));
        tkns = tkns.Append(new Token(TokenType.ParenthesisOpen));
        tkns = tkns.Append(new ConstantToken(new StringVariant("font_data")));
        tkns = tkns.Append(new Token(TokenType.Comma));
        tkns = tkns.Append(new Token(TokenType.BuiltInFunc, (uint?) BuiltinFunction.ResourceLoad));
        tkns = tkns.Append(new Token(TokenType.ParenthesisOpen));
        tkns = tkns.Append(new ConstantToken(new StringVariant("res://mods/WFRus/Cyrillic/accidCyr.ttf")));
        tkns = tkns.Append(new Token(TokenType.ParenthesisClose));
        tkns = tkns.Append(new Token(TokenType.ParenthesisClose));
        tkns = tkns.Append(new Token(TokenType.Newline, indents));

        return tkns;
    }

    public static IEnumerable<Token> SetText(string node, string param, string str, uint indents = 0) {
        IEnumerable<Token> tkns = [];

        tkns = tkns.Append(new Token(TokenType.Dollar));
        tkns = tkns.Append(new IdentifierToken(node));
        tkns = tkns.Append(new Token(TokenType.Period));
        tkns = tkns.Append(new IdentifierToken(param));
        tkns = tkns.Append(new Token(TokenType.OpAssign));
        tkns = tkns.Append(new ConstantToken(new StringVariant(str)));
        tkns = tkns.Append(new Token(TokenType.Newline, indents));

        return tkns;
    }

    public static IEnumerable<Token> LoadExtImage(string node, string path) {
        IEnumerable<Token> tkns = [];
        
        tkns = tkns.Concat(ScriptTokenizer.Tokenize($"var img{node} = Image.new()", 1));
        tkns = tkns.Concat(ScriptTokenizer.Tokenize($"var result{node} = img{node}.load(\"{path}\")", 1));
        tkns = tkns.Concat(ScriptTokenizer.Tokenize($"if result{node} == OK:", 1));
        tkns = tkns.Concat(ScriptTokenizer.Tokenize($"var texture{node} = ImageTexture.new()", 2));
        tkns = tkns.Concat(ScriptTokenizer.Tokenize($"texture{node}.create_from_image(img{node})", 2));
        tkns = tkns.Concat(ScriptTokenizer.Tokenize($"texture{node}.flags = 0", 2));
        tkns = tkns.Concat(ScriptTokenizer.Tokenize($"${node}.texture = texture{node}", 2));
        
        return tkns;
    }
}