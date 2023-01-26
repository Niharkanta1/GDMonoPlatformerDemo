using Godot;

/*
 * @auther  Nihar
 * @company	DeadW0Lf Games
 */
public class GroundButton : StaticBody2D
{
    private AnimatedSprite animatedSprite;
    private CollisionPolygon2D unpressedCollision, pressedCollision;

    private bool isButtonPressed = false;

    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        unpressedCollision = GetNode<CollisionPolygon2D>("UnpressedCollision");
        pressedCollision = GetNode<CollisionPolygon2D>("PressedCollision");
    }

    private void PressButton()
    {
        isButtonPressed = true;
        unpressedCollision.SetDeferred("disabled", true);
        pressedCollision.SetDeferred("diabled", false);
        animatedSprite.Play("Pressed");
    }

    private void UnPressButton()
    {
        isButtonPressed = false;
        unpressedCollision.SetDeferred("disabled", false);
        pressedCollision.SetDeferred("diabled", true);
        animatedSprite.Play("Unpressed");
    }

    private void OnPressDetectorBodyEentered(Node body)
    {
        if (body.Name == "RigidBox" || body.Name == "Player")
        {
            if (!isButtonPressed)
                PressButton();
        }
    }

    private void OnPressDetectorBodyExited(Node body)
    {
        if (body.Name == "RigidBox" || body.Name == "Player")
        {
            if (isButtonPressed)
                UnPressButton();
        }
    }
}
