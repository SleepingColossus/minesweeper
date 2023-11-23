using Godot;

public class RemainingLives : Panel
{
    private Label _label;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _label = GetNode<Label>("Container/Label");
    }

    public void UpdateRemainingLives(int remainingLives)
    {
        _label.Text = remainingLives.ToString();
    }
}
