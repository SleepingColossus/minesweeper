using Godot;

public class CustomSetting : HBoxContainer
{
    [Export] private Texture Icon;
    [Export] private string Description;
    [Export] private int MinValue;
    [Export] private int MaxValue;
    [Export] private int InitialValue;

    private Label _valueLabel;

    public override void _Ready()
    {
        var icon = GetNode<TextureRect>("Icon");
        icon.Texture = Icon;

        var description = GetNode<Label>("Description");
        description.Text = Description;

        _valueLabel = GetNode<Label>("Panel/Value");
        _valueLabel.Text = InitialValue.ToString();
    }

    private void _on_ButtonReduce_pressed()
    {
        var value = int.Parse(_valueLabel.Text);

        if (value > MinValue)
        {
            value--;
            _valueLabel.Text = value.ToString();
        }
    }


    private void _on_ButtonIncrease_pressed()
    {
        var value = int.Parse(_valueLabel.Text);

        if (value < MaxValue)
        {
            value++;
            _valueLabel.Text = value.ToString();
        }
    }
}
