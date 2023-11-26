using Godot;
using Minesweeper.Scenes.Gameplay;

public class CustomGame : Control
{
    private CustomSetting _width;
    private CustomSetting _height;
    private CustomSetting _mines;
    private CustomSetting _lives;
    private Button _startButton;

    private bool _isValid = true;

    public override void _Ready()
    {
        _width = GetNode<CustomSetting>("Items/Width");
        _height = GetNode<CustomSetting>("Items/Height");
        _mines = GetNode<CustomSetting>("Items/Mines");
        _lives = GetNode<CustomSetting>("Items/Lives");
        _startButton = GetNode<Button>("Start");

        // load last used settings
        var settings = Settings.GetInstance();
        _width.SetValue(settings.GridWidth);
        _height.SetValue(settings.GridHeight);
        _mines.SetValue(settings.NumberOfMines);
        _lives.SetValue(settings.NumberOfLives);

        _width.Connect(nameof(CustomSetting.ValueChanged), this, nameof(ValidateFields));
        _height.Connect(nameof(CustomSetting.ValueChanged), this, nameof(ValidateFields));
        _mines.Connect(nameof(CustomSetting.ValueChanged), this, nameof(ValidateFields));
        _lives.Connect(nameof(CustomSetting.ValueChanged), this, nameof(ValidateFields));
    }

    public void ValidateFields()
    {
        // check if the number of mines is less than the number of cells
        var width = _width.GetValue();
        var height = _height.GetValue();
        var mines = _mines.GetValue();

        _isValid = mines < width * height;

        _mines.SetValidColor(_isValid);
        _startButton.Disabled = !_isValid;
    }

    private void _on_Start_pressed()
    {
        if (!_isValid)
        {
            return;
        }

        var width = _width.GetValue();
        var height = _height.GetValue();
        var mines = _mines.GetValue();
        var lives = _lives.GetValue();

        Settings.SetSettings(width, height, mines, lives);

        GetTree().ChangeScene("res://Scenes/Gameplay/Gameplay.tscn");
    }
}
