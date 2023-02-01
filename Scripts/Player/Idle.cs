using Godot;
using System;

/*
 * @auther  Nihar
 * @company	DeadW0Lf Games
 */
public class Idle : PlayerState
{
    public override void Enter()
    {
        player.velocity.x = 0;
        player.animationState.Travel("Idle");
        if (!player.HasDashes())
            player.ResetDashCounter();
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
        player.ApplyGravity(delta);
        //player.velocity = player.MoveAndSlide(player.velocity, Vector2.Up);
        player.velocity = player.MoveAndSlideWithSnap(
            player.velocity,
            player.snapVector,
            Vector2.Up,
            true,               //stops on slope
            4,
            player.floorMaxAngle,
            false
        );

        // Handle Collisions
        if (player.GetSlideCount() > 0)
        {
            for (int i = 0; i < player.GetSlideCount(); i++)
            {
                var collision = player.GetSlideCollision(i);
                var collider = collision.Collider;
                if ("SpikeClub".Equals(collider.GetType().Name) ||
                    "SpikePit".Equals(collider.GetType().Name) ||
                    "Enemy".Equals(collider.GetType().Name))
                {
                    PlayerStateMachine.TransitionTo("Death");
                }
            }
        }

        // Handle Other Transitions

        if (Input.IsActionJustPressed("Jump"))
        {
            PlayerStateMachine.TransitionTo("Jump");
        }
        else if (Input.IsActionPressed("ui_left") || Input.IsActionPressed("ui_right"))
        {
            PlayerStateMachine.TransitionTo("Walk");
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
