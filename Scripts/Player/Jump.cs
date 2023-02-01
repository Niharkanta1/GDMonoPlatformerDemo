using Godot;
using System;

/*
 * @auther  Nihar
 * @company	DeadW0Lf Games
 */
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
        if (player.IsOnCeiling())
        {
            player.velocity.y = 0;
        }

        // Handle Other Transitions
        if (Input.IsActionJustPressed("Dash") && player.HasDashes())
        {
            PlayerStateMachine.TransitionTo("Dash");
        }

    }

}
