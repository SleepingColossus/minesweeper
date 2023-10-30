using Godot;

public class ResetButton : TextureButton
{
    [Export] private Texture WinTexture;
    [Export] private Texture LoseTexture;

    public void SetWinTexture()
    {
        TextureNormal = WinTexture;
    }

    public void SetLoseTexture()
    {
        TextureNormal = LoseTexture;
    }
}
