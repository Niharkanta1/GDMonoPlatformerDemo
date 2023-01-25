using Godot;
using System;

public class State : Node
{
    public StateMachine PlayerStateMachine { get; set; }

    public virtual void HandleInput(InputEvent @event) { }
    public virtual void Update(float delta) { }
    public virtual void PhysicsUpdate(float delta) { }
    public virtual void Enter() { }
    public virtual void Exit() { }
}
