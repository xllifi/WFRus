using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace WFRus.modifies;

public class MainMenu : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Menus/Main Menu/main_menu.gdc";

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
                // found our match, return the original newline
                yield return token;
                
                // print [WFRusCS] Injected into main_menu.gd!
                yield return new Token(TokenType.BuiltInFunc, (uint?) BuiltinFunction.TextPrint);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("[WFRusCS] Injected into main_menu.gd!"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 1); // Indent once
                
                // load cyrillic font
                yield return new Token(TokenType.BuiltInFunc, (uint?) BuiltinFunction.ResourceLoad);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("res://Assets/Themes/main.tres"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("default_font");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, (uint?) BuiltinFunction.ResourceLoad);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("res://mods/WFRus/Cyrillic/cyrillic.tres"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 1); // Indent once
                
                // set game logo
                yield return new Token(TokenType.Dollar);
                yield return new IdentifierToken("TextureRect");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("texture");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, (uint?) BuiltinFunction.ResourceLoad);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("res://mods/WFRus/Cyrillic/WFlogo.png"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 1); // Indent once
                
                // regen voice bank
                yield return new IdentifierToken("Globals");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_generate_voice_bank");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 1); // Indent once
                
                IEnumerable<Token> tkns = [];
                
                tkns = tkns.Concat(Helpers.SetFont("VBoxContainer/create_game"));
                tkns = tkns.Concat(Helpers.SetText("VBoxContainer/create_game", "text", "Создать лобби"));
                tkns = tkns.Concat(Helpers.SetText("VBoxContainer/create_game/TooltipNode", "header", "[color=#6a4420]Создать лобби"));
                tkns = tkns.Concat(Helpers.SetText("VBoxContainer/create_game/TooltipNode", "body", "Создайте своё лобби!"));
                
                tkns = tkns.Concat(Helpers.SetFont("VBoxContainer/join_game"));
                tkns = tkns.Concat(Helpers.SetText("VBoxContainer/join_game", "text", "Найти лобби"));
                tkns = tkns.Concat(Helpers.SetText("VBoxContainer/join_game/TooltipNode", "header", "[color=#6a4420]Найти лобби"));
                tkns = tkns.Concat(Helpers.SetText("VBoxContainer/join_game/TooltipNode", "body", "Играйте в чужом лобби!"));
                
                tkns = tkns.Concat(Helpers.SetFont("VBoxContainer/settings"));
                tkns = tkns.Concat(Helpers.SetText("VBoxContainer/settings", "text", "Настройки"));
                tkns = tkns.Concat(Helpers.SetText("VBoxContainer/settings/TooltipNode", "header", "[color=#6a4420]Настройки"));
                tkns = tkns.Concat(Helpers.SetText("VBoxContainer/settings/TooltipNode", "body", "Меняйте всякие параметры!"));
                
                tkns = tkns.Concat(Helpers.SetFont("VBoxContainer/quit"));
                tkns = tkns.Concat(Helpers.SetText("VBoxContainer/quit", "text", "Выйти"));
                tkns = tkns.Concat(Helpers.SetText("VBoxContainer/quit/TooltipNode", "header", "[color=#6a4420]Выйти"));
                tkns = tkns.Concat(Helpers.SetText("VBoxContainer/quit/TooltipNode", "body", "всё нормально. можешь уходить, да. я не обижусь, всё нормально. уходи."));
                
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
