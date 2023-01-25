using Godot;
using System;

public class Death : PlayerState
{
    public override void Enter()
    {
        player.animationState.Travel("Death");
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
