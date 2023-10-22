using Godot;
using Minesweeper.Constants;

public class Cell : TextureButton
{
    [Export] public Vector2 GridPosition = Vector2.Zero;
    [Export] public Texture Texture0;
    [Export] public Texture Texture1;
    [Export] public Texture Texture2;
    [Export] public Texture Texture3;
    [Export] public Texture Texture4;
    [Export] public Texture Texture5;
    [Export] public Texture Texture6;
    [Export] public Texture Texture7;
    [Export] public Texture Texture8;
    [Export] public Texture TextureMineLose;
    [Export] public Texture TextureMineReveal;

    public void SetTexture(CellType cellType)
    {
        if (cellType == CellType.Safe0) { TextureNormal = Texture0; }
        if (cellType == CellType.Safe1) { TextureNormal = Texture1; }
        if (cellType == CellType.Safe2) { TextureNormal = Texture2; }
        if (cellType == CellType.Safe3) { TextureNormal = Texture3; }
        if (cellType == CellType.Safe4) { TextureNormal = Texture4; }
        if (cellType == CellType.Safe5) { TextureNormal = Texture5; }
        if (cellType == CellType.Safe6) { TextureNormal = Texture6; }
        if (cellType == CellType.Safe7) { TextureNormal = Texture7; }
        if (cellType == CellType.Safe8) { TextureNormal = Texture8; }
        if (cellType == CellType.MineLose) { TextureNormal = TextureMineLose; }
        if (cellType == CellType.MineReveal) { TextureNormal = TextureMineReveal; }
    }
}
