using Godot;
using System;

public class Walk : PlayerState
{
    public override void Enter()
    {
        player.animationState.Travel("Walk");
    }

    public override void Exit()
    {
        base.Exit();
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
            inputDirectionX = (player.direction == "right") ? 1 : -1;
        }

        player.UpdateDirection(inputDirectionX);
        player.velocity.x = player.walkSpeed * inputDirectionX;
        player.ApplyGravity(delta);
        player.velocity = player.MoveAndSlide(player.velocity, Vector2.Up);

        // Handle Collisions here

        // Handle Other Transitions

        if (MathUtil.IsEqualApprox(inputDirectionX, 0.0f))
        {
            PlayerStateMachine.TransitionTo("Idle");
        }
        else if (Input.IsActionJustPressed("Jump"))
        {
            PlayerStateMachine.TransitionTo("Jump");
        }
        else if (Input.IsActionJustPressed("Attack"))
        {
            PlayerStateMachine.TransitionTo("Attack");
        }
        else if (Input.IsActionJustPressed("Dash") && player.HasDashes())
        {
            PlayerStateMachine.TransitionTo("Dash");
        }


    }

}
