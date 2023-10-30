using Godot;
using Minesweeper.Constants;

public class Cell : TextureButton
{
    [Export] private Texture TextureDefault;
    [Export] private Texture TextureFlag;
    [Export] private Texture TextureFalseFlag;
    [Export] private Texture Texture0;
    [Export] private Texture Texture1;
    [Export] private Texture Texture2;
    [Export] private Texture Texture3;
    [Export] private Texture Texture4;
    [Export] private Texture Texture5;
    [Export] private Texture Texture6;
    [Export] private Texture Texture7;
    [Export] private Texture Texture8;
    [Export] private Texture TextureMineLose;
    [Export] private Texture TextureMineReveal;

    public Vector2 GridPosition = Vector2.Zero;
    public bool Revealed { get; private set; }
    public bool Flagged { get; private set; }
    public CellType Type { get; private set; }

    public void SetType(CellType type) => Type = type;

    public void Reveal()
    {
        if (Flagged)
        {
            return;
        }

        Revealed = true;

        if (Type == CellType.Safe0) { TextureNormal = Texture0; }
        if (Type == CellType.Safe1) { TextureNormal = Texture1; }
        if (Type == CellType.Safe2) { TextureNormal = Texture2; }
        if (Type == CellType.Safe3) { TextureNormal = Texture3; }
        if (Type == CellType.Safe4) { TextureNormal = Texture4; }
        if (Type == CellType.Safe5) { TextureNormal = Texture5; }
        if (Type == CellType.Safe6) { TextureNormal = Texture6; }
        if (Type == CellType.Safe7) { TextureNormal = Texture7; }
        if (Type == CellType.Safe8) { TextureNormal = Texture8; }
        if (Type == CellType.Mine) { TextureNormal = TextureMineLose; }
    }

    public void RevealIfMine()
    {
        if (!Flagged && Type != CellType.Mine)
        {
            return;
        }

        if (Flagged && Type != CellType.Mine)
        {
            TextureNormal = TextureFalseFlag;
        }

        if (!Revealed && !Flagged)
        {
            TextureNormal = TextureMineReveal;
        }
    }

    public void ToggleFlag()
    {
        if (Revealed)
        {
            return;
        }

        Flagged = !Flagged;
        TextureNormal = Flagged ? TextureFlag : TextureDefault;
    }

    // does nothing practical
    // just suppresses stupid Godot errors
    private void _on_Cell_pressed()
    {
    }
}
