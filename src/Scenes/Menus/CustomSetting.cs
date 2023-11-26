using Godot;

public class CustomSetting : HBoxContainer
{
    [Signal] public delegate void ValueChanged();

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

    public int GetValue()
    {
        return int.Parse(_valueLabel.Text);
    }

    public void SetValue(int value)
    {
        _valueLabel.Text = value.ToString();
    }

    public void SetValidColor(bool isValid)
    {
        _valueLabel.Modulate = isValid ? Colors.White : Colors.Red;
    }

    private void _on_ButtonReduce_pressed()
    {
        var value = int.Parse(_valueLabel.Text);

        if (value > MinValue)
        {
            value--;
            _valueLabel.Text = value.ToString();

            EmitSignal(nameof(ValueChanged));
        }
    }


    private void _on_ButtonIncrease_pressed()
    {
        var value = int.Parse(_valueLabel.Text);

        if (value < MaxValue)
        {
            value++;
            _valueLabel.Text = value.ToString();

            EmitSignal(nameof(ValueChanged));
        }
    }
}
