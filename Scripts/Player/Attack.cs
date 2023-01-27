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
