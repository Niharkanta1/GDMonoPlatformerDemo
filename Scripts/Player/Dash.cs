using Godot;
using System;

/*
 * @auther  Nihar
 * @company	DeadW0Lf Games
 */
public class Dash : PlayerState
{
    public override void Enter()
    {
        player.numDash--;
        player.isDashing = true;
        player.animationState.Travel("Dash");
        ((SoundManager)GetNode("/root/SoundManager")).dashSound.Play();
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
        // No Transitions
    }
}
