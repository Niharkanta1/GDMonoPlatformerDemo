using Godot;
using System;

public class Fall : PlayerState
{
    public override void Enter()
    {
        player.animationState.Travel("Fall");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void PhysicsUpdate(float delta)
    {
        if (player.IsOnFloor())
        {
            PlayerStateMachine.TransitionTo("Idle");
        }

        // Allow left-right movement when falling
        var inputDirectionX = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        player.UpdateDirection(inputDirectionX);
        player.velocity.x = player.lateralFallMovementSpeed * inputDirectionX;
        player.ApplyGravity(delta);
        //player.velocity = player.MoveAndSlide(player.velocity, Vector2.Up);
        player.velocity = player.MoveAndSlideWithSnap(
            player.velocity,
            player.snapVector,
            Vector2.Up,
            true,
            4,
            player.floorMaxAngle,
            false
        );

        // Handle Collisions here

        // Handle Other Transitions
        if (Input.IsActionJustPressed("Dash") && player.HasDashes())
        {
            PlayerStateMachine.TransitionTo("Dash");
        }
    }

}
