using Godot;
using System;

/*
 * @auther  Nihar
 * @company	DeadW0Lf Games
 */
public class Attack : PlayerState
{
    public override void Enter()
    {
        player.isAttacking = true;
        player.animationState.Travel("Attack");
        ((SoundManager)GetNode("/root/SoundManager")).attackSound.Play();
    }

    public override void Exit()
    {

    }

    public override void PhysicsUpdate(float delta)
    {
        if (!player.isAttacking)
            PlayerStateMachine.TransitionTo("Idle");


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
    }
}
