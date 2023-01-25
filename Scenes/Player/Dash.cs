using Godot;
using System;

public class Dash : PlayerState
{
    public override void Enter()
    {
        player.numDash--;
        player.isDashing = true;
        player.animationState.Travel("Dash");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void PhysicsUpdate(float delta)
    {
        player.velocity.y = 0;
        player.velocity.x = player.direction == "right" ? player.dashSpeed : -player.dashSpeed;
        if (!player.isDashing)
        {
            if (player.IsOnFloor())
                PlayerStateMachine.TransitionTo("Idle");
            else
                PlayerStateMachine.TransitionTo("Fall");

            return;
        }
        player.velocity = player.MoveAndSlide(player.velocity, Vector2.Up);

        // Handle Collision here

    }
}
