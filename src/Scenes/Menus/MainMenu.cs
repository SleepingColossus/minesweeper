using Godot;
using Minesweeper.Scenes.Gameplay;

public class MainMenu : Node
{
    private void LoadGameplayScene()
    {
        GetTree().ChangeScene("res://Scenes/Gameplay/Gameplay.tscn");
    }

    private void _on_Easy_pressed()
    {
        Settings.SetSettings(Difficulty.Easy);
        LoadGameplayScene();
    }


    private void _on_Medium_pressed()
    {
        Settings.SetSettings(Difficulty.Medium);
        LoadGameplayScene();
    }


    private void _on_Expert_pressed()
    {
        Settings.SetSettings(Difficulty.Expert);
        LoadGameplayScene();
    }


    private void _on_Custom_pressed()
    {
        GetTree().ChangeScene("");
    }
}
