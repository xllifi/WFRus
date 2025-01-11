using GDWeave.Godot;
using GDWeave.Modding;

namespace WFRus.modifies;

public class PlayerLabel : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player_label.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        throw new NotImplementedException();
    }
}