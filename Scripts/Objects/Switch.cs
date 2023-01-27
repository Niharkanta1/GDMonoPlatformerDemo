using Godot;

/*
 * @auther  Nihar
 * @company	DeadW0Lf Games
 */
public class Switch : Area2D
{
    private AnimatedSprite animatedSprite;
    private bool isSwitchOpen = false;
    private Gate gate;
    private bool isPerformingOperation = false;

    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        gate = GetNode<Gate>("Gate");
        gate.Connect("OperationFinished", this, nameof(OnGateOperationFinished));
    }

    public void OnArea2DAreaEntered(Area2D other)
    {
        if (other.Owner.Name == "Player" && !isPerformingOperation)
        {
            if (!isSwitchOpen)
            {
                animatedSprite.Play("Open");
                PerformOperation(true);
            }
            else
            {
                animatedSprite.Play("Close");
                PerformOperation(false);
            }
            isSwitchOpen = !isSwitchOpen;
            isPerformingOperation = true;
        }
    }

    public void PerformOperation(bool flag)
    {

        if (flag)
            gate.OpenGate();
        else
            gate.CloseGate();
    }

    // Signals

    public void OnGateOperationFinished(bool val)
    {
        isPerformingOperation = false;
    }
}
