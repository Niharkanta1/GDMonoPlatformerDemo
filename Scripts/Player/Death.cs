using Godot;
using System;

/*
 * @auther  Nihar
 * @company	DeadW0Lf Games
 */
public class Death : PlayerState
{
    public override void Enter()
    {
        player.animationState.Travel("Death");
        player.playerCollisionShape.Disabled = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void PhysicsUpdate(float delta)
    {
        base.PhysicsUpdate(delta);
    }
}
