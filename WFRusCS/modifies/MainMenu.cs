using GDWeave;
using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace WFRus.modifies;

public class MainMenu(WFRusMod mod) : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Menus/Main Menu/main_menu.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "_ready"},
            t => t.Type is TokenType.Newline
        ], allowPartialMatch: true);

        foreach (var token in tokens) {
            if (waiter.Check(token)) {
                yield return token;
                IEnumerable<Token> tkns = [];
                
                mod.Logger.Information($"Trying to load mainmenu logo from {mod.Config.mainmenuLogoPath}");
                tkns = tkns.Concat(ScriptTokenizer.Tokenize("""TextPrint("[WFRusCS>GD] Injected into main_menu.gd!")""", 1));
                tkns = tkns.Concat(Helpers.LoadExtImage("TextureRect", mod.Config.mainmenuLogoPath));
                
                tkns = tkns.Concat(ScriptTokenizer.Tokenize("""if PlayerData.player_options.altfont == 0:""", 1));
                tkns = tkns.Concat(ScriptTokenizer.Tokenize("""ResourceLoad("res://Assets/Themes/main.tres").default_font = ResourceLoad("res://mods/WFRus/Cyrillic/cyrillic.tres")""", 2));
                
                // Локализация кнопок
                tkns = tkns.Concat(Helpers.SetFont("VBoxContainer/create_game", 1));
                tkns = tkns.Concat(Helpers.SetText("VBoxContainer/create_game", "text", "Создать лобби", 1));
                tkns = tkns.Concat(Helpers.SetText("VBoxContainer/create_game/TooltipNode", "header", "[color=#6a4420]Создать лобби", 1));
                tkns = tkns.Concat(Helpers.SetText("VBoxContainer/create_game/TooltipNode", "body", "Создайте своё лобби!", 1));
                
                tkns = tkns.Concat(Helpers.SetFont("VBoxContainer/join_game", 1));
                tkns = tkns.Concat(Helpers.SetText("VBoxContainer/join_game", "text", "Найти лобби", 1));
                tkns = tkns.Concat(Helpers.SetText("VBoxContainer/join_game/TooltipNode", "header", "[color=#6a4420]Найти лобби", 1));
                tkns = tkns.Concat(Helpers.SetText("VBoxContainer/join_game/TooltipNode", "body", "Играйте в чужом лобби!", 1));
                
                tkns = tkns.Concat(Helpers.SetFont("VBoxContainer/settings", 1));
                tkns = tkns.Concat(Helpers.SetText("VBoxContainer/settings", "text", "Настройки", 1));
                tkns = tkns.Concat(Helpers.SetText("VBoxContainer/settings/TooltipNode", "header", "[color=#6a4420]Настройки", 1));
                tkns = tkns.Concat(Helpers.SetText("VBoxContainer/settings/TooltipNode", "body", "Меняйте всякие параметры!", 1));
                
                tkns = tkns.Concat(Helpers.SetFont("VBoxContainer/quit", 1));
                tkns = tkns.Concat(Helpers.SetText("VBoxContainer/quit", "text", "Выйти", 1));
                tkns = tkns.Concat(Helpers.SetText("VBoxContainer/quit/TooltipNode", "header", "[color=#6a4420]Выйти", 1));
                tkns = tkns.Concat(Helpers.SetText("VBoxContainer/quit/TooltipNode", "body", "всё нормально. можешь уходить, да. я не обижусь, всё нормально. уходи.", 1));
                
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
