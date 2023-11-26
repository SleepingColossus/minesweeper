using Godot;
using Minesweeper.Scenes.Gameplay;

public class SoundButton : TextureButton
{
    [Export] private Texture NormalSoundOn;
    [Export] private Texture HoverSoundOn;
    [Export] private Texture PressedSoundOn;
    [Export] private Texture NormalSoundOff;
    [Export] private Texture HoverSoundOff;
    [Export] private Texture PressedSoundOff;

    public override void _Ready()
    {
        SetSprite();
    }

    private void _on_SoundButton_pressed()
    {
        Settings.ToggleSound();
        SetSprite();
    }

    private void SetSprite()
    {
        var settings = Settings.GetInstance();
        if (settings.SoundEnabled)
        {
            TextureNormal = NormalSoundOn;
            TextureHover = HoverSoundOn;
            TexturePressed = PressedSoundOn;
        }
        else
        {
            TextureNormal = NormalSoundOff;
            TextureHover = HoverSoundOff;
            TexturePressed = PressedSoundOff;
        }
    }
}
