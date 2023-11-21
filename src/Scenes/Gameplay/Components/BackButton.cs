using Godot;

public class BackButton : TextureButton
{
    private void _on_BackButton_pressed()
    {
        GetTree().ChangeScene("res://Scenes/Menus/MainMenu.tscn");
    }
}



