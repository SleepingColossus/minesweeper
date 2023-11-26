using Godot;
using Minesweeper.Scenes.Gameplay;

public class CustomGame : Control
{
    public override void _Ready()
    {

    }

    private void _on_Start_pressed()
    {
        var width = GetNode<CustomSetting>("Items/Width").GetValue();
        var height = GetNode<CustomSetting>("Items/Height").GetValue();
        var mines = GetNode<CustomSetting>("Items/Mines").GetValue();
        var lives = GetNode<CustomSetting>("Items/Lives").GetValue();

        Settings.SetSettings(width, height, mines, lives);

        GetTree().ChangeScene("res://Scenes/Gameplay/Gameplay.tscn");
    }
}

