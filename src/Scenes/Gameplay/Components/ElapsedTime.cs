using Godot;

public class ElapsedTime : Panel
{
    private Timer _timer;
    private Label _label;

    private int _elapsedTime = 0;

    public override void _Ready()
    {
        _timer = GetNode<Timer>("Timer");
        _label = GetNode<Label>("Container/Label");
    }

    private void _on_Timer_timeout()
    {
        _elapsedTime++;
        if (_elapsedTime < 999)
        {
            _label.Text = _elapsedTime.ToString().PadLeft(3, '0');
        }
    }

    public void Start()
    {
        _timer.Start();
    }

    public void Stop()
    {
        _timer.Stop();
    }
}
