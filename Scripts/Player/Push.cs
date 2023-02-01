using Godot;

/*
 * @auther	Nihar
 * @company	DeadW0Lf Games
 */
public class Push : PlayerState
{

    public override void Enter()
    {
        player.animationState.Travel("Push");
    }

    public override void Exit()
    {

    }

    public override void PhysicsUpdate(float delta)
    {
        if (!player.IsOnFloor())
        {
            if (player.velocity.y > 0)
            {
                PlayerStateMachine.TransitionTo("Fall");
                return;
            }
        }

        var inputDirectionX = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        if (Input.IsActionPressed("ui_right") && Input.IsActionPressed("ui_left"))
        {
            if (player.direction == "right")
                inputDirectionX = 1;
            else
                inputDirectionX = -1;
        }

        player.UpdateDirection(inputDirectionX);
        player.velocity.x = player.walkSpeed * inputDirectionX;
        player.ApplyGravity(delta);
        player.velocity = player.MoveAndSlideWithSnap(
            player.velocity,
            player.snapVector,
            Vector2.Up,
            false,
            4,
            player.floorMaxAngle,
            false
        );

        // Handle Collisions:
        if (player.GetSlideCount() > 0)
        {
            for (int i = 0; i < player.GetSlideCount(); i++)
            {
                var collision = player.GetSlideCollision(i);
                var box = collision.Collider;
                if (box.GetType().Name == "RigidBox")
                {
                    ((RigidBody2D)box).ApplyCentralImpulse(-collision.Normal * player.rigidPush);
                }
            }
        }


        // Handle Other Transitions
        if (Input.IsActionJustReleased("ui_right") || Input.IsActionJustReleased("ui_left"))
            PlayerStateMachine.TransitionTo("Idle");
        else if (Input.IsActionJustPressed("Jump"))
            PlayerStateMachine.TransitionTo("Jump");

    }
}
