using Godot;

/*
 * @auther  Nihar
 * @company	DeadW0Lf Games
 */
public class GroundButton : StaticBody2D
{
    private AnimatedSprite animatedSprite;
    private CollisionPolygon2D unpressedCollision, pressedCollision;
    private Gate gate;

    private bool isButtonPressed = false;
    private bool isPerformingOperation = false;
    private int state, nextState; // 0 for Close, 1 for Open

    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        unpressedCollision = GetNode<CollisionPolygon2D>("UnpressedCollision");
        pressedCollision = GetNode<CollisionPolygon2D>("PressedCollision");
        gate = GetNode<Gate>("Gate");
        gate.Connect("OperationFinished", this, nameof(OnGateOperationFinished));
    }

    public override void _Process(float delta)
    {
        if (isPerformingOperation) return;
        if (state != nextState)
        {
            if (nextState == 1)
            {
                state = 1;
                PressButton();
            }
            if (nextState == 0)
            {
                state = 0;
                UnPressButton();
            }
        }
    }

    private void PressButton()
    {
        unpressedCollision.SetDeferred("disabled", true);
        pressedCollision.SetDeferred("diabled", false);
        animatedSprite.Play("Pressed");
        PerformOperation(true);
    }

    private void UnPressButton()
    {

        unpressedCollision.SetDeferred("disabled", false);
        pressedCollision.SetDeferred("diabled", true);
        animatedSprite.Play("Unpressed");
        PerformOperation(false);
    }

    private void OnPressDetectorBodyEentered(Node body)
    {
        if (body.Name == "RigidBox" || body.Name == "Player")
        {
            if (!isButtonPressed)
            {
                nextState = 1;
                isButtonPressed = true;
            }

        }
    }

    private void OnPressDetectorBodyExited(Node body)
    {
        if (body.Name == "RigidBox" || body.Name == "Player")
        {
            if (isButtonPressed)
            {
                nextState = 0;
                isButtonPressed = false;
            }
        }
    }

    public void PerformOperation(bool flag)
    {
        if (isPerformingOperation) return;
        isPerformingOperation = true;
        if (flag)
            gate.OpenGate();
        else
            gate.CloseGate();
    }

    public void OnGateOperationFinished(bool val)
    {
        isPerformingOperation = false;
    }
}
