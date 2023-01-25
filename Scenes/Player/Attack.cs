using Godot;
using System;

public class Attack : PlayerState
{
    public override void Enter()
    {
        player.isAttacking = true;
        player.animationState.Travel("Attack");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void PhysicsUpdate(float delta)
    {
        if (!player.isAttacking)
            PlayerStateMachine.TransitionTo("Idle");
    }
}
