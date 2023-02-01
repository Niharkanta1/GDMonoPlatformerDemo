using Godot;
using System;

/*
 * @auther  Nihar
 * @company	DeadW0Lf Games
 */
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
        if (Input.IsActionJustPressed("Dash") && player.HasDashes())
        {
            PlayerStateMachine.TransitionTo("Dash");
        }
    }

}
