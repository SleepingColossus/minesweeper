using Godot;
using Minesweeper.Scenes.Gameplay;

public class SoundButton : TextureButton
{
    private void _on_SoundButton_pressed()
    {
        Settings.ToggleSound();
    }
}
