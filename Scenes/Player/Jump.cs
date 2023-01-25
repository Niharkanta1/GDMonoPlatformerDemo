using Godot;
using System;

public class Jump : PlayerState
{
    public override void Enter()
    {
        player.velocity.y = player.jumpSpeed;
        player.animationState.Travel("Jump");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void PhysicsUpdate(float delta)
    {
        if (player.velocity.y > 0)
        {
            PlayerStateMachine.TransitionTo("Fall");
            return;
        }

        // Allow left-right movement when falling
        var inputDirectionX = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        player.UpdateDirection(inputDirectionX);
        player.velocity.x = player.lateralFallMovementSpeed * inputDirectionX;
        player.ApplyGravity(delta);
        player.velocity = player.MoveAndSlide(player.velocity, Vector2.Up);

        // Handle Collisions here

        // Handle Other Transitions
        if (Input.IsActionJustPressed("Dash") && player.HasDashes())
        {
            PlayerStateMachine.TransitionTo("Dash");
        }

    }

}
